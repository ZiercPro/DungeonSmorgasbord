using DG.Tweening;
using UnityEngine;
using ZiercCode._DungeonGame._Scripts.EventClasses;
using ZiercCode.EventBusSystem;

namespace ZiercCode._DungeonGame._Scripts.WeaponComponent
{
    /// <summary>
    /// 枪械后座效果 基于dotween实现
    /// </summary>
    public class GunRecoilComponent : BaseWeaponComponent
    {
        [SerializeField]
        private float duration = .1f;

        [SerializeField]
        private float strength = .25f;

        private Tween _recoilTween; //后座动画

        protected void OnEnable()
        {
            EventsGroup.AddListener<WeaponEvent.WeaponFired>(OnFire);
        }

        protected void OnDisable()
        {
            EventsGroup.RemoveAllListener();
        }

        private void OnFire(IEventArgs args)
        {
            if (args is WeaponEvent.WeaponFired weaponFire)
            {
                if (weaponFire.Weapon == MyWeapon)
                {
                    Recoil();
                }
            }
        }

        //后座效果
        public void Recoil()
        {
            if (_recoilTween == null)
            {
                _recoilTween = transform.DOLocalMoveX(-strength, duration)
                    .OnComplete(() => transform.DOLocalMoveX(0f, duration)).SetAutoKill(false);
            }
            else
            {
                _recoilTween.Restart();
            }
        }
    }
}