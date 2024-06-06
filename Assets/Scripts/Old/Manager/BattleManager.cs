using NaughtyAttributes.Scripts.Core.DrawerAttributes;
using NaughtyAttributes.Scripts.Core.DrawerAttributes_SpecialCase;
using NaughtyAttributes.Scripts.Core.MetaAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using ZiercCode.Core.Pool;
using ZiercCode.Core.Utilities;
using ZiercCode.Old.ScriptObject;

namespace ZiercCode.Old.Manager
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private PoolObjectSo redCircle;
        [SerializeField, Expandable] private BattleDataSo battleData;
        [SerializeField] private PoolObjectSpawner poolObjectSpawner;

        [Header("范围")] [SerializeField] private Vector2 spawnSize = new Vector2(8, 5);
        [SerializeField] private Vector2 spawnCenter = Vector2.zero;
        [SerializeField] private Color GizmosColor;

        private List<GameObject> _enemyS;

        /// <summary>
        /// 战斗模式状态
        /// </summary>
        public enum BattleState
        {
            BattleBefore, //战斗开始之前
            BattleIng, //波次进行
            BattleEnd //战斗结束
        }

        /// <summary>
        /// 当前战斗状态
        /// </summary>
        private BattleState _currentState;

        /// <summary>
        /// 当前层数
        /// </summary>
        private int _currentLevel;

        /// <summary>
        /// 战斗状态改变
        /// </summary>
        public event Action<BattleState> OnBattleStateChange;

        /// <summary>
        /// 波次改变
        /// </summary>
        public event Action<int> OnBattleLevelChange;

        private void Awake()
        {
            _enemyS = new List<GameObject>();
        }

        private void Start()
        {
            //刚进入游戏 战斗状态更新 同时也广播一下当前的战斗状态
            ChangeState(BattleState.BattleBefore);
            //波次更新  同时也广播一下当前的波次
            ChangeLevel(1);
        }

        private void Update()
        {
            //如果在战斗状态 那检测战斗是否结束
            if (_currentState == BattleState.BattleIng)
            {
                if (CheckBattleFinish())
                {
                    //如果战斗结束 则进入奖励卡状态
                    ChangeState(BattleState.BattleEnd);
                }
            }
        }

        [SerializeField] private bool debugMode;
#if UNITY_EDITOR
        /// <summary>
        /// 战斗入口
        /// </summary>
        [Button("开始战斗"), ShowIf("debugMode")]
#endif
        public void StartBattle()
        {
            SpawnEnemy();
            ChangeState(BattleState.BattleIng);
        }


#if UNITY_EDITOR
        /// <summary>
        /// 进入下一层
        /// </summary>
        [Button("下一层"), ShowIf("debugMode")]
#endif
        public void MoveToNextLevel()
        {
            ChangeLevel(++_currentLevel);
            if (_currentLevel <= battleData.battleDataList.Length)
            {
                StartBattle();
            }
        }

        /// <summary>
        /// 生成敌人
        /// </summary>
        private void SpawnEnemy()
        {
            foreach (var enemyAndNum in battleData.battleDataList[_currentLevel - 1].enemyToSpawn)
            {
                for (int i = 0; i < enemyAndNum.num; i++)
                {
                    SpawnHandle handle = poolObjectSpawner.SpawnPoolObject(enemyAndNum.enemyAttributeSo.poolObjectSo,
                        MyMath.GetRandomPos(spawnCenter, spawnSize));
                    _enemyS.Add(handle.GetObject());
                }
            }
        }

        /// <summary>
        /// 检测战斗是否结束
        /// </summary>
        /// <returns></returns>
        private bool CheckBattleFinish()
        {
            bool battleFinish = true;

            if (_enemyS is { Count: > 0 })
            {
                foreach (var enemy in _enemyS)
                {
                    if (enemy.activeInHierarchy)
                    {
                        battleFinish = false;
                        break;
                    }
                }
            }

            return battleFinish;
        }


        /// <summary>
        /// 改变状态
        /// </summary>
        /// <param name="state">要进入的状态</param>
        private void ChangeState(BattleState state)
        {
            if (_currentState != state)
                _currentState = state;
            else
                Debug.LogWarning($"已经处于状态{state}");

            Debug.Log($"当前状态：{_currentState}");

            OnBattleStateChange?.Invoke(state);
        }

        /// <summary>
        /// 改变波次
        /// </summary>
        /// <param name="level">目标波次</param>
        private void ChangeLevel(int level)
        {
            _currentLevel = level;
            OnBattleLevelChange?.Invoke(_currentLevel);
        }


#if UNITY_EDITOR
        public void OnDrawGizmos()
        {
            Gizmos.color = GizmosColor;
            Gizmos.DrawCube(spawnCenter, spawnSize);
        }
#endif
    }
}