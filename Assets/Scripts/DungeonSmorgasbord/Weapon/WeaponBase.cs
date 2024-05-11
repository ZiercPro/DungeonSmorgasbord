using NaughtyAttributes;
using NaughtyAttributes.Scripts.Core.MetaAttributes;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.ScriptObject;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    public class WeaponBase : MonoBehaviour, IWeaponBase
    {
        [SerializeField] private WeaponDataSo weaponDataSo;
        [SerializeField] private bool haveInput;
        [SerializeField, ShowIf("haveInput")] private WeaponInputHandler weaponInputHandler;

        private IWeaponUserBase _weaponUserBase;

        /// <summary>
        /// 武器初始化，使用武器前必须初始化
        /// </summary>
        /// <param name="weaponUserBase">武器使用者</param>
        public virtual void Init(IWeaponUserBase weaponUserBase)
        {
            _weaponUserBase = weaponUserBase;
        }

        /// <summary>
        /// 获取武器使用者
        /// </summary>
        /// <returns></returns>
        public IWeaponUserBase GetWeaponUserBase()
        {
            return _weaponUserBase;
        }

        /// <summary>
        /// 获取武器数据
        /// </summary>
        /// <returns></returns>
        public WeaponDataSo GetWeaponDataSo()
        {
            return weaponDataSo;
        }


        public virtual void OnLeftButtonPressStarted()
        {
            weaponInputHandler.leftButtonPressPerformed?.Invoke();
        }

        public virtual void OnLeftButtonPressed() { }

        public virtual void OnLeftButtonPressCanceled()
        {
            weaponInputHandler.leftButtonPressCanceled?.Invoke();
        }

        public virtual void OnRightButtonPressStarted()
        {
            weaponInputHandler.rightButtonPressPerformed?.Invoke();
        }

        public virtual void OnRightButtonPressed() { }

        public virtual void OnRightButtonPressCanceled()
        {
            weaponInputHandler.rightButtonPressCanceled?.Invoke();
        }

        public virtual void OnViewPositionChange(Vector2 viewPosition)
        {
            weaponInputHandler.viewPositionChange?.Invoke(viewPosition);
        }
    }
}