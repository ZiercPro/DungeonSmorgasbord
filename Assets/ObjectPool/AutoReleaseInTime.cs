using UnityEngine;
using ZiercCode.ObjectPool;

namespace ZiercCode._DungeonGame.Weapon.Weapon_LuGerGun
{
    public class AutoReleaseInTime : MonoBehaviour //定时自动释放 用于对象池物品
    {
        [SerializeField] private string poolName; //对象池名
        [SerializeField] private float stayTime = 2f; //存活时间

        private float _stayTimer;

        private void OnEnable()
        {
            _stayTimer = stayTime;
        }

        private void Update()
        {
            AutoRelease();
        }

        private void AutoRelease()
        {
            if (_stayTimer > 0)
            {
                _stayTimer -= Time.deltaTime;
            }
            else
            {
                PoolManager.Instance.Release(poolName, gameObject);
            }
        }
    }
}