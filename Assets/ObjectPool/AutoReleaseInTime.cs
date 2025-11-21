using UnityEngine;

namespace ZiercCode.ObjectPool
{
    public class AutoReleaseInTime : MonoBehaviour //定时自动释放 用于对象池物品
    {
        [SerializeField]
        protected string poolName; //对象池名

        [SerializeField]
        protected float stayTime = 2f; //存活时间

        protected float stayTimer;

        protected virtual void OnEnable()
        {
            stayTimer = stayTime;
        }

        protected virtual void Update()
        {
            AutoRelease();
        }

        private void AutoRelease()
        {
            if (stayTimer > 0)
            {
                stayTimer -= Time.deltaTime;
            }
            else
            {
                PoolManager.Instance.Release(poolName, gameObject);
            }
        }
    }
}