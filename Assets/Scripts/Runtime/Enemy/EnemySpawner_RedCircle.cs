using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZiercCode.Runtime.Enemy
{
    public class EnemySpawner_RedCircle : MonoBehaviour
    {
        public GameObject enemyTemp;
        public Action<GameObject> SpawnComplete;

        private static List<EnemySpawner_RedCircle> _spawns;

        private void OnEnable()
        {
            if (_spawns == null) _spawns = new List<EnemySpawner_RedCircle>();
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