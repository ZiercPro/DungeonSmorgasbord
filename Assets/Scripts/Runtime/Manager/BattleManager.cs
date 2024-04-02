using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using ZiercCode.Runtime.Audio;
using ZiercCode.Runtime.Basic;
using ZiercCode.Runtime.Enemy;
using ZiercCode.Runtime.Helper;
using ZiercCode.Runtime.ScriptObject;

namespace ZiercCode.Runtime.Manager
{
    public class BattleManager : USingletonComponentDestroy<BattleManager>
    {
        public event Action<int> OnLevelChange;
        public UnityEvent onBattleStart;
        public UnityEvent onBattleEnd;

        [SerializeField] private GameObject enemySpawnerTemp;
        [SerializeField] private List<GameObject> enemyTemps;
        [SerializeField] private BattleDifficultyDataSo difficultyDataSo;
        [SerializeField] private EnemyDifficultyDataSo enemyDifficultyDataS0;

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
            _spawnInterval = difficultyDataSo.intervalOfLevel[_currentLevel];
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
            DroppedItem.DroppedItem.ClearAllItem();
            AudioPlayer.Instance.PlayAudioAsync(AudioName.BattleBgmNormal);
        }

        //结束战斗(玩家死亡
        public void BattleEixt()
        {
            currentState = BattleState.Before;
        }

        //获取当前波的总难度值
        private void GetDifficultyOfCurrentLevel(int curLevel)
        {
            _currentDifficulty = difficultyDataSo.DifficultyPerLevel[curLevel];
        }

        #region Enemy

        //获取每种敌人对应的生成数量
        private void GetNumOfEnemy(int currentDif)
        {
            int dif = currentDif;

            _enemyNumHash = new int[enemyTemps.Count];

            while (dif > 0)
            {
                for (int i = 0; i < _enemyNumHash.Length; i++)
                {
                    int temp = enemyDifficultyDataS0.EnemyDifficultyDictionary[enemyTemps[i].name];
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
            StartCoroutine(EnemySpawnCoroutine());
        }

        IEnumerator EnemySpawnCoroutine()
        {
            GetDifficultyOfCurrentLevel(_currentLevel);
            GetNumOfEnemy(_currentDifficulty);

            int wave = difficultyDataSo.wavesOfLevel[_currentLevel];
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
            onBattleStart?.Invoke();
            SpawnEnemy();
        }

        private void BattleEnd()
        {
            LevelUp();
            onBattleEnd?.Invoke();
        }

        private bool IsWaveDone()
        {
            if (EnemySpawner_RedCircle.GetSpawner() == null || EnemySpawner_RedCircle.GetSpawner().Count > 0)
                return false;
            if (Enemy.Enemy.GetEnemys() == null || Enemy.Enemy.GetEnemys().Count > 0) return false;
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