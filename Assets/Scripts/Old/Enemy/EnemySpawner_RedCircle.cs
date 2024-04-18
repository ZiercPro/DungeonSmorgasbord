using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZiercCode.Old.Enemy
{
    public class EnemySpawner_RedCircle : MonoBehaviour
    {
        public GameObject enemyTemp;
        public Action<GameObject> SpawnComplete;

        private static readonly List<EnemySpawner_RedCircle> _spawns = new List<EnemySpawner_RedCircle>();

        private void OnEnable()
        {
            _spawns.Add(this);
        }

        private void OnDisable()
        {
            _spawns.Remove(this);
        }

        /// <summary>
        /// 由动画帧事件调用
        /// </summary>
        public void Spawn()
        {
            GameObject newEnemy = Instantiate(enemyTemp, transform.position, Quaternion.identity);
            SpawnComplete?.Invoke(newEnemy);
            Destroy(gameObject);
        }

        public static List<EnemySpawner_RedCircle> GetSpawner()
        {
            return _spawns;
        }
    }
}