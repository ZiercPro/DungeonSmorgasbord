using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZiercCode.Core.Pool;


namespace ZiercCode
{
#if UNITY_EDITOR
    public class SpawnerTest : MonoBehaviour
    {
        [SerializeField] private PoolObjectSo poolObjectSo;

        [SerializeField] private PoolObjectSpawner spawner;

        private List<SpawnHandle> _spawnHandles;

        private void Awake()
        {
            _spawnHandles = new List<SpawnHandle>();
        }

        [Button("生成")]
        public void Spawn()
        {
            _spawnHandles.Add(spawner.SpawnPoolObject(poolObjectSo));
        }

        [Button("释放")]
        public void Release()
        {
            if (_spawnHandles.Count > 0)
            {
                _spawnHandles[^1].Release();
                _spawnHandles.RemoveAt(_spawnHandles.Count - 1);
            }
        }
    }
#endif
}