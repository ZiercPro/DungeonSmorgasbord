using NaughtyAttributes.Scripts.Core.DrawerAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZiercCode._DungeonGame.Room
{
    public class RandomRoom : MonoBehaviour //随机房间列表生成
    {
        [SerializeField, Expandable] private RoomGenerateConfig roomGenerateConfig; //所有可能生成的房间信息 so数据

        private List<RoomInfo> _allRoomInfo; //所有可能生成的房间
        private RoomInfo[] _roomList; //生成的房间列表
        private Vector2Int _roomListLengthRange; //房间长度范围

        private RandomGenerator _randomGenerator; //随机数生成器

        public void Initialize(string seed)//每次生成之前调用
        {
            _roomListLengthRange = roomGenerateConfig.roomLengthRange;
            _allRoomInfo = roomGenerateConfig.roomInfos;
            _randomGenerator = new(seed);
        }

        //房间的信息
        [Serializable]
        public struct RoomInfo
        {
            public bool isMustSpawn; //是否必须生成 是则至少生成一个
            [Min(0)] public int maxNum; //最大生成数量 如果是0 则说明没有限制 只要是在配置中的房间都有可能生成 所以不存在0
            public bool canBeReplacedAtLast; //在最后进行检查时是否可以被优化掉
            [Range(0, 100)] public int spawnWeight; //生成权重 为了适配种子 使用整型
            public RoomType myType; //自己的类型 唯一
            public RoomType previousType; //前一个房间的类型
            public RoomType nextType; //后一个房间的类型
        }

        [Flags]
        public enum RoomType //房间类型
        {
            None = 0, //无类型 说明有位置但是没有填充房间 
            Enter = 1 << 0, //入口房
            Little = 1 << 1, //小怪房
            Big = 1 << 2, //精英怪房
            Shop = 1 << 3, //商店
            Boss = 1 << 4, //boss房
            Special = 1 << 5, //特殊房
            Empty = 1 << 6 //空房间 说明没有位置 也就是房间列表的两端
        }

        //生成新的房间列表
        public RoomInfo[] SpawnRoomList()
        {
            int roomListLength = _randomGenerator.Next(_roomListLengthRange.x, _roomListLengthRange.y); //房间长度

            _roomList = new RoomInfo[roomListLength]; //房间列表
            
            Dictionary<RoomType, int> numRecord = new Dictionary<RoomType, int>(); //记录目前每种类型生成的数量

            //填充列表的头和尾部
            for (int i = 0; i < _allRoomInfo.Count; i++)
            {
                if (_allRoomInfo[i].previousType.HasFlag(RoomType.Empty)) //头部
                {
                    _roomList[0] = _allRoomInfo[i];
                    continue;
                }

                if (_allRoomInfo[i].nextType == RoomType.Empty) //尾部
                {
                    _roomList[roomListLength - 1] = _allRoomInfo[i];
                    continue;
                }

                if (_roomList[0].myType != RoomType.None &&
                    _roomList[roomListLength - 1].myType != RoomType.None) break;
            }

            List<RoomInfo> candidates = new List<RoomInfo>(); //存储可能生成的房间
            
            //中部 从前往后生成
            for (int i = 1; i < roomListLength - 1; i++)
            {
                candidates.Clear();

                //通过前面的房间节点筛选一次 填充候选房间
                if (_roomList[i - 1].myType == RoomType.None)
                {
                    Debug.LogWarning($"下标{i - 1}房间未填充");
                    return _roomList;
                }

                for (int j = 0; j < _allRoomInfo.Count; j++)
                {
                    if (_roomList[i - 1].nextType.HasFlag(_allRoomInfo[j].myType)) //如果前一个房间的后一个节点中包含了该房间类型 则添加到候选中
                    {
                        candidates.Add(_allRoomInfo[j]);
                    }
                }

                //将头尾两个类型的房间删除
                for (int j = 0; j < candidates.Count; j++)
                {
                    if (candidates[j].previousType == RoomType.Empty || candidates[j].nextType == RoomType.Empty)
                        candidates.RemoveAt(j);
                }

                //通过后面的房间筛选一次
                if (_roomList[i + 1].myType != RoomType.None)
                {
                    for (int j = candidates.Count - 1; j >= 0; j--)
                    {
                        if (!_roomList[i + 1].previousType.HasFlag(candidates[j].myType))
                            candidates.RemoveAt(j); //如果后面的房间要求的前一个节点中不包含候选类型或者本身前后不能有房间 则移除该候选 
                    }
                }

                //通过候选的房间本身再筛选一次
                for (int j = candidates.Count - 1; j >= 0; j--)
                {
                    if (!candidates[j].previousType.HasFlag(_roomList[i - 1].myType) ||
                        !candidates[j].nextType.HasFlag(_roomList[i + 1].myType))
                    {
                        candidates.RemoveAt(j);
                    }
                }

                //通过最大数量筛选一次
                for (int j = candidates.Count - 1; j >= 0; j--)
                {
                    if (candidates[j].maxNum != 0) //该房间有最大生成限制
                    {
                        if (numRecord.ContainsKey(candidates[j].myType)) //该房间已经生成过
                        {
                            if (numRecord[candidates[j].myType] == candidates[j].maxNum) //该房间数量已经达到最大限制
                            {
                                candidates.RemoveAt(j);
                            }
                        }
                    }
                }

                //从候选房间中选择一个类型生成
                AddToRoomList(ref _roomList, ref candidates, i);

                if (!numRecord.TryAdd(_roomList[i].myType, 1))
                {
                    numRecord[_roomList[i].myType]++;
                }
            }

            //检查是否有必须生成的房间没有生成
            CheckMustSpawnRoomList(ref _roomList);

            return _roomList;
        }

        //确保生成的房间列表中包含必须生成的房间
        private void CheckMustSpawnRoomList(ref RoomInfo[] roomList)
        {
            List<RoomInfo> mustSpawnRooms = new List<RoomInfo>(); //存储必须添加的房间

            for (int i = 0; i < _allRoomInfo.Count; i++)
                if (_allRoomInfo[i].isMustSpawn)
                    mustSpawnRooms.Add(_allRoomInfo[i]);

            //如果当前房间列表中已经包含了需要生成的房间 则从mustSpawnRooms移除
            for (int i = 0; i < roomList.Length; i++)
            {
                for (int j = mustSpawnRooms.Count - 1; j >= 0; j--)
                {
                    if (mustSpawnRooms[j].myType == roomList[i].myType) mustSpawnRooms.RemoveAt(j);
                }
            }

            //将没有生成的必须生成房间强行插入到房间列表中
            int check = _randomGenerator.Next(0, 1); //0则正序遍历 1则倒序遍历
            if (check == 0)
            {
                for (int i = 0; i < mustSpawnRooms.Count; i++)
                {
                    for (int j = 1; j < roomList.Length - 1; j++)
                    {
                        if (roomList[j - 1].nextType.HasFlag(mustSpawnRooms[i].myType) &&
                            roomList[j + 1].previousType.HasFlag(mustSpawnRooms[i].myType) &&
                            mustSpawnRooms[i].previousType.HasFlag(roomList[j - 1].myType) &&
                            mustSpawnRooms[i].nextType.HasFlag(roomList[j + 1].myType) &&
                            roomList[j].canBeReplacedAtLast)
                        {
                            roomList[j] = mustSpawnRooms[i];
                            break; //只要生成了 则不再生成同一类型
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < mustSpawnRooms.Count; i++)
                {
                    for (int j = roomList.Length - 2; j >= 1; j--)
                    {
                        if (roomList[j - 1].nextType.HasFlag(mustSpawnRooms[i].myType) &&
                            roomList[j + 1].previousType.HasFlag(mustSpawnRooms[i].myType) &&
                            mustSpawnRooms[i].previousType.HasFlag(roomList[j - 1].myType) &&
                            mustSpawnRooms[i].nextType.HasFlag(roomList[j + 1].myType) &&
                            roomList[j].canBeReplacedAtLast)
                        {
                            roomList[j] = mustSpawnRooms[i];
                            break; //只要生成了 则不再生成同一类型
                        }
                    }
                }
            }
        }

        //从候选者房间列表中随机选出一个房间类型填充到当前的房间列表中
        private void AddToRoomList(ref RoomInfo[] roomList, ref List<RoomInfo> candidates, in int currentIndex)
        {
            int totalWeight = 0;
            foreach (var room in candidates)
            {
                totalWeight += room.spawnWeight; //计算总权值
            }

            int randomValue = _randomGenerator.Next(0, totalWeight); //随机生成一个这个区间的数字 看它的位置落在哪个区间

            for (int i = 0; i < candidates.Count; i++)
            {
                randomValue -= candidates[i].spawnWeight;
                if (randomValue <= 0)
                {
                    roomList[currentIndex] = candidates[i];
                    break;
                }
            }
        }
    }
}