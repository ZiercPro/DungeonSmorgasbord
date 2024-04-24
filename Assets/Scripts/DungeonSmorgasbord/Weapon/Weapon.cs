using System.Collections.Generic;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.DungeonSmorgasbord.ScriptObject;
using ZiercCode.Old.Audio;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    public abstract class Weapon : MonoBehaviour, IWeaponBase
    {
        [SerializeField] protected WeaponDataSo weaponDataSo;
        [SerializeField] protected List<AudioName> waveAudios;
        [SerializeField] protected WeaponAnimationEventHandler animationEventHandler;

        protected Animator Animator;

        private IWeaponUserBase _weaponUserBase;

        protected virtual void Awake()
        {
            Animator = GetComponentInChildren<Animator>();
            animationEventHandler = GetComponent<WeaponAnimationEventHandler>();
        }

        protected DamageInfo GetDamageInfo()
        {
            return _weaponUserBase.DamageInfo;
        }

        /// <summary>
        /// 装备武器
        /// </summary>
        /// <param name="weaponUserBase">武器使用类</param>
        public void Equip(IWeaponUserBase weaponUserBase)
        {
            _weaponUserBase = weaponUserBase;
        }

        /// <summary>
        /// 舍弃武器
        /// </summary>
        public void Drop()
        {
            _weaponUserBase = null;
        }

        public virtual void OnLeftButtonPressStarted() { }
        public virtual void OnLeftButtonPressing() { }
        public virtual void OnLeftButtonPressCanceled() { }
        public virtual void OnRightButtonPressStarted() { }
        public virtual void OnRightButtonPressing() { }
        public virtual void OnRightButtonPressCanceled() { }
    }
}