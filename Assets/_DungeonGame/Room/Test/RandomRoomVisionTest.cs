using NaughtyAttributes.Scripts.Core.DrawerAttributes_SpecialCase;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ZiercCode._DungeonGame.Room.Test
{
    public class RandomRoomVisionTest : MonoBehaviour //通过text显示得到的随机房间列表
    {
        [SerializeField] private GameObject textMeshProPrefab;
        [SerializeField] private RandomRoom randomRoomBuilder; //随机房间生成器
        [SerializeField] private float roomInterval; //房间之间的间隔
        [SerializeField] private string roomSeed; //房间种子
        [SerializeField] private Vector2Int seedLengthRange; //房间种子

        private List<GameObject> _roomTextList = new List<GameObject>();

        [Button("生成随机房间")]
        private void Test()
        {
            for (int i = 0; i < _roomTextList.Count; i++)
            {
                DestroyImmediate(_roomTextList[i]);
            }

            _roomTextList.Clear();

            randomRoomBuilder.Initialize(roomSeed);
            RandomRoom.RoomInfo[] roomList = randomRoomBuilder.SpawnRoomList();

            for (int i = 0; i < roomList.Length; i++)
            {
                GameObject roomObject = Instantiate(textMeshProPrefab, transform.position, Quaternion.identity);
                TextMeshPro textMeshPro = roomObject.GetComponent<TextMeshPro>();
                textMeshPro.text = roomList[i].myType.ToString();
                roomObject.transform.position += i * roomInterval * transform.right;
                _roomTextList.Add(roomObject);
            }
        }

        [Button("生成随机种子")]
        private void GetRandomSeed()
        {
            RandomSeedGenerator randomSeedGenerator = new RandomSeedGenerator();
            roomSeed = randomSeedGenerator.GetRandomSeed(seedLengthRange.x, seedLengthRange.y);
        }

        [Button("清理房间")]
        private void ClearRoom()
        {
            for (int i = 0; i < _roomTextList.Count; i++)
            {
                DestroyImmediate(_roomTextList[i]);
            }

            _roomTextList.Clear();
        }
    }
}