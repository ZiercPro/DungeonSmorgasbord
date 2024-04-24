using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using ZiercCode.Core.Extend;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.Old.Audio;
using ZiercCode.DungeonSmorgasbord.ScriptObject;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    public abstract class WeaponBase : MonoBehaviour, IWeaponBase
    {
        [SerializeField] protected Animator animator;
        [SerializeField] protected WeaponDataSo weaponDataSo;
        [SerializeField] protected List<AudioName> waveAudios;
        [SerializeField] protected WeaponAnimationEventHandler animationEventHandler;

        private IWeaponUserBase _weaponUserBase;


        /// <summary>
        /// 武器初始化，使用武器前必须初始化
        /// </summary>
        /// <param name="weaponUserBase">武器使用者</param>
        public void Init(IWeaponUserBase weaponUserBase)
        {
            _weaponUserBase = weaponUserBase;
        }

        /// <summary>
        /// 获取最终伤害信息
        /// </summary>
        /// <returns></returns>
        protected DamageInfo GetDamageInfo()
        {
            DamageInfo damageInfo;
            int damageAmount = weaponDataSo.Damage;
            if (MyMath.ChanceToBool(_weaponUserBase.GetCriticalChance()))
                damageAmount *= 2;

            damageInfo = new(damageAmount, weaponDataSo.DamageType,
                _weaponUserBase.GetWeaponUser());
            return damageInfo;
        }

        /// <summary>
        /// 获取动画控制器
        /// </summary>
        /// <returns></returns>
        protected Animator GetAnimator()
        {
            return animator;
        }

        protected IWeaponUserBase GetWeaponUser()
        {
            return _weaponUserBase;
        }

        public virtual void OnLeftButtonPressStarted() { }
        public virtual void OnLeftButtonPressing() { }
        public virtual void OnLeftButtonPressCanceled() { }
        public virtual void OnRightButtonPressStarted() { }
        public virtual void OnRightButtonPressing() { }
        public virtual void OnRightButtonPressCanceled() { }
    }
}