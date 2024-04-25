using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using ZiercCode.Core.Extend;
using ZiercCode.Core.System;
using ZiercCode.DungeonSmorgasbord.ScriptObject;
using ZiercCode.Old.Audio;
using ZiercCode.Old.Enemy;
using ZiercCode.Old.ScriptObject;

namespace ZiercCode.Old.Manager
{
    public class BattleManager : USingletonComponentDestroy<BattleManager>
    {
        /// <summary>
        /// 战斗模式状态
        /// </summary>
        private enum BattleState
        {
            BattleBefore, //战斗开始之前
            LevelBefore, //波次开始之前
            LevelIng, //波次进行
            LevelAfter, //波次结束
            Card, //选择奖励卡
            BattleEnd //战斗结束
        }

        public event Action<int> OnLevelChange;
        public UnityEvent onBattleStart;
        public UnityEvent onBattleEnd;

        [SerializeField] private GameObject enemySpawnerTemp;
        [SerializeField] private EnemyDataListSo enemyDataListSo;
        [SerializeField] private BattleDifficultyDataSo difficultyDataSo;

        /// <summary>
        /// 当前波次
        /// </summary>
        private int _currentLevel = 1;

        /// <summary>
        /// 单波生成敌怪间隔，由配置文件决定，游戏运行时实时读取
        /// </summary>
        private float _spawnInterval = 0f;

        /// <summary>
        /// 敌怪生成范围
        /// </summary>
        private Vector2 _range = new Vector2(8, 5);

        /// <summary>
        /// 单波每种敌怪对应生成数量的哈希表
        /// </summary>
        private int[] _enemyNumHash;

        /// <summary>
        /// 当前波次总体难度，用来决定不同敌怪生成的数量
        /// </summary>
        private int _currentDifficulty = 0;

        /// <summary>
        /// 当前战斗状态
        /// </summary>
        private BattleState _currentState = BattleState.BattleBefore;

        private void Start()
        {
            _enemyNumHash = new int[enemyDataListSo.enemyAttributeSoList.Count];
            OnLevelChange?.Invoke(_currentLevel);
        }

        /// <summary>
        /// 改变状态
        /// </summary>
        /// <param name="state"></param>
        private void ChangeState(BattleState state)
        {
            if (_currentState != state)
                _currentState = state;
            else
                Debug.LogWarning($"已经处于状态{state}");
        }

        /// <summary>
        /// 波次数++，设置敌人生成间隔
        /// </summary>
        private void LevelUp()
        {
            _currentLevel++;
            _spawnInterval = difficultyDataSo.intervalOfLevel[_currentLevel];
            OnLevelChange?.Invoke(_currentLevel);
        }

        /// <summary>
        /// 波次开始之前调用
        /// </summary>
        private void BeforeLevelStart()
        {
            _currentState = BattleState.BattleBefore;
            Debug.Log("当前状态为:" + _currentState);
        }

        /// <summary>
        /// 波次结束之后调用
        /// </summary>
        private void AfterLevelEnd()
        {
            _currentState = BattleState.LevelAfter;
        }

        /// <summary>
        /// 波次开始
        /// </summary>
        private void LevelStart()
        {
            if (_currentState != BattleState.LevelIng) return;
            onBattleStart?.Invoke();
            SpawnEnemy();
            DroppedItem.DroppedItem.ClearAllItem();
            AudioPlayer.Instance.PlayAudioAsync(AudioName.BattleBgmNormal);
        }

        /// <summary>
        /// 战斗结束
        /// </summary>
        private void LevelEnd()
        {
            _currentState = BattleState.LevelBefore;
            LevelUp();
            onBattleEnd?.Invoke();
            AfterLevelEnd();
        }

        /// <summary>
        /// 战斗结束
        /// </summary>
        private void BattleEnd()
        {
        }

        /// <summary>
        ///玩家死亡 
        /// </summary>
        private void PlayerDead()
        {
            //战斗结束之前要做的事情
            //...
            BattleEnd();
            //战斗结束之后要做的事情
            //...
        }

        #region Enemy

        /// <summary>
        /// 获取每种敌人对应的数量
        /// </summary>
        /// <param name="difficulty">难度值</param>
        private void GetNumOfEnemy(int difficulty)
        {
            int dif = difficulty;

            _enemyNumHash = new int[enemyDataListSo.enemyAttributeSoList.Count];

            while (dif > 0)
            {
                for (int i = 0; i < _enemyNumHash.Length; i++)
                {
                    int temp = enemyDataListSo.enemyAttributeSoList[i].difficulty;
                    dif -= temp;
                    _enemyNumHash[i]++;
                }
            }
        }

        //实例化怪物出生圈
        private void GetSpawner(int index, Vector2 pos)
        {
            GameObject newSpawner = Instantiate(enemySpawnerTemp, pos, Quaternion.identity);
            EnemySpawner_RedCircle es = newSpawner.GetComponent<EnemySpawner_RedCircle>();
            es.enemyTemp = enemyDataListSo.enemyAttributeSoList[index].instance;
        }

        private void SpawnEnemy()
        {
            StartCoroutine(EnemySpawnCoroutine());
        }

        IEnumerator EnemySpawnCoroutine()
        {
            yield return null;
            _currentDifficulty = difficultyDataSo.DifficultyPerLevel[_currentLevel];
            GetNumOfEnemy(_currentDifficulty);

            int wave = difficultyDataSo.waveNumPerLevel[_currentLevel];
            while (wave > 0)
            {
                wave--;
                for (int i = 0; i < _enemyNumHash.Length; i++)
                {
                    for (int j = 0; j < _enemyNumHash[i]; j++)
                    {
                        GetSpawner(i, MyMath.GetRandomPos(_range));
                    }
                }

                yield return new WaitForSeconds(_spawnInterval);
            }

            _currentState = BattleState.LevelIng;
        }

        #endregion


        private bool IsWaveDone()
        {
            if (EnemySpawner_RedCircle.GetSpawner() == null || EnemySpawner_RedCircle.GetSpawner().Count > 0)
                return false;
            if (Enemy.Enemy.GetEnemys() == null || Enemy.Enemy.GetEnemys().Count > 0) return false;
            return true;
        }


        /// <summary>
        /// 波次数重置
        /// </summary>
        public void LevelReset()
        {
            _currentLevel = 1;
            OnLevelChange?.Invoke(_currentLevel);
        }

        /// <summary>
        /// 战斗入口
        /// </summary>
        public void BattleEntry()
        {
            BeforeLevelStart();
            LevelStart();
        }
    }
}