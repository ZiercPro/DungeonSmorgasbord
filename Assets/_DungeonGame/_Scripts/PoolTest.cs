using UnityEngine;
using UnityEngine.Pool;

namespace ZiercCode._DungeonGame._Scripts
{
    public class PoolTest : MonoBehaviour
    {
        [SerializeField]
        private GameObject temp;

        private ObjectPool<GameObject> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<GameObject>(CreateFunc, GetFunc, ReleaseFunc, DestroyFunc, true, 20, 100);
        }


        private GameObject CreateFunc()
        {
            GameObject newO = Instantiate(temp, transform);
            return newO;
        }

        private void GetFunc(Object obj)
        {
            if (obj is GameObject go)
            {
                go.SetActive(true);
            }
        }

        private void ReleaseFunc(Object obj)
        {
            if (obj is GameObject go)
            {
                go.SetActive(false);
            }
        }

        private void DestroyFunc(Object obj)
        {
            Object.Destroy(obj);
        }
    }
}