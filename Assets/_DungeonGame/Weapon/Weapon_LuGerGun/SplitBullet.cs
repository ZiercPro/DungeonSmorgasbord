using UnityEngine;
using ZiercCode.ObjectPool;

namespace ZiercCode._DungeonGame.Weapon.Weapon_LuGerGun
{
    public class SplitBullet : MonoBehaviour //子弹碎片
    {
        [SerializeField] private float stayTime = 2f;

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
                PoolManager.Instance.Release("splitBullet", gameObject);
            }
        }
    }
}