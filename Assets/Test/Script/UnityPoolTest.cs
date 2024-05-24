using UnityEngine;
using UnityEngine.Pool;

namespace ZiercCode.Test.Script
{
    public class UnityPoolTest : MonoBehaviour
    {
        [SerializeField] private GameObject poolObject;
        private ObjectPool<GameObject> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<GameObject>(CreateFunc, GetFunc, ReleaseFunc, DestroyFunc, false, 10, 100);
        }

        private void Start()
        {
            GameObject newG = _pool.Get();
            Debug.Log(newG.name);
        }

        private GameObject CreateFunc()
        {
            GameObject newG = Instantiate(poolObject, transform);
            return newG;
        }

        private void GetFunc(GameObject g)
        {
            g.SetActive(true);
        }

        private void ReleaseFunc(GameObject g)
        {
            g.SetActive(false);
        }

        private void DestroyFunc(GameObject g)
        {
            Destroy(g);
        }
    }
}