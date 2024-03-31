using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace Runtime.Manager
{
    using Basic;
    using Audio;
    using Enemy;
    using Helper;
    using DroppedItem;
    using ScriptObject;

    public class BattleManager : SingletonIns<BattleManager>
    {
        public event Action<int> OnLevelChange;
        public UnityEvent OnBattleStart;
        public UnityEvent OnBattleEnd;

        [SerializeField] private GameObject enemySpawnerTemp;
        [SerializeField] private List<GameObject> enemyTemps;
        [SerializeField] private Transform battlePlatform;
        [SerializeField] private BattleDifficultyDataSO difficultyDataSO;
        [SerializeField] private EnemyDiffiultyDataSO enemyDiffiultyDataS0;

        private float _spawnInterval;
        private Vector2 _range;
        private int[] _enemyNumHash;
        private int _currentDifficulty;
        private int _currentLevel;

        private BattleState currentState;

        private void Start()
        {
            _range = new Vector2(8, 5);
            _currentDifficulty = 0;
            _currentLevel = 1;
            _spawnInterval = 0f;
            _enemyNumHash = new int[enemyTemps.Count];
            currentState = BattleState.Before;
            OnLevelChange?.Invoke(_currentLevel);
        }

        private void Update()
        {
            switch (currentState)
            {
                default:
                    break;
                case BattleState.Before:
                    break;
                case BattleState.Ing:
                    if (IsWaveDone())
                    {
                        Debug.Log("wavedone!");
                        currentState = BattleState.After;
                    }

                    break;
                case BattleState.After:
                    BattleEnd();
                    currentState = BattleState.Card;
                    break;
                case BattleState.Card:
                    break;
            }
        }

        //波数增加
        private void LevelUp()
        {
            _currentLevel++;
            _spawnInterval = difficultyDataSO.intervalOfLevel[_currentLevel];
            OnLevelChange?.Invoke(_currentLevel);
        }

        //波数清空
        public void LevelClear()
        {
            _currentLevel = 1;
            OnLevelChange?.Invoke(_currentLevel);
        }

        //开始战斗
        public void BattleEntry()
        {
            BattleStart();
            DroppedItem.ClearAllItem();
            AudioPlayerManager.Instance.PlayAudioAsync(AudioName.BattleBgmNormal);
        }

        //结束战斗(玩家死亡
        public void BattleEixt()
        {
            currentState = BattleState.Before;
        }

        //获取当前波的总难度值
        private void GetCurrentDifficulty(int curLevel)
        {
            _currentDifficulty = difficultyDataSO.difOfLevel[curLevel];
        }

        #region Enemy

        //获取每种敌人对应的生成数量
        private void GetHash(int currentDif)
        {
            int dif = currentDif;

            _enemyNumHash = new int[enemyTemps.Count];

            while (dif > 0)
            {
                for (int i = 0; i < _enemyNumHash.Length; i++)
                {
                    int temp = enemyDiffiultyDataS0.enemyDiffiultyDic[enemyTemps[i].name];
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
            es.enemyTemp = enemyTemps[index];
        }

        private void SpawnEnemy()
        {
            StartCoroutine(EnemySpawn());
        }

        IEnumerator EnemySpawn()
        {
            int wave = difficultyDataSO.wavesOfLevel[_currentLevel];
            while (wave > 0)
            {
                wave--;
                for (int i = 0; i < _enemyNumHash.Length; i++)
                {
                    for (int j = 0; j < _enemyNumHash[i]; j++)
                    {
                        GetSpawner(i, GetRandomPos(_range));
                    }
                }

                yield return new WaitForSeconds(_spawnInterval);
            }

            currentState = BattleState.Ing;
        }

        private Vector2 GetRandomPos(Vector2 range)
        {
            return new Vector2(MyMath.GetRandom(-range.x, range.x), MyMath.GetRandom(-range.y, range.y));
        }

        #endregion

        private void BattleStart()
        {
            OnBattleStart?.Invoke();
            GetCurrentDifficulty(_currentLevel);
            GetHash(_currentDifficulty);
            SpawnEnemy();
        }

        private void BattleEnd()
        {
            LevelUp();
            OnBattleEnd?.Invoke();
        }

        private bool IsWaveDone()
        {
            if (EnemySpawner_RedCircle.GetSpawner() == null || EnemySpawner_RedCircle.GetSpawner().Count > 0)
                return false;
            if (Enemy.GetEnemys() == null || Enemy.GetEnemys().Count > 0) return false;
            return true;
        }
    }

    public enum BattleState
    {
        Before,
        Ing,
        After,
        Card
    }
}