using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    /// <summary>
    /// 用于提供动画触发方法
    /// </summary>
    public class WeaponAnimationHandler : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [SerializeField, AllowNesting] private List<WeaponAnimationConfig> weaponAnimationsFunc;

        /// <summary>
        /// 激活动画
        /// </summary>
        /// <param name="funcIndex">方法在字典中的顺序</param>
        public void ActiveAnimationFunc(int funcIndex)
        {
            switch (weaponAnimationsFunc[funcIndex].weaponAnimationParameterType)
            {
                case WeaponAnimationParameterType.Bool:
                    animator.SetBool(weaponAnimationsFunc[funcIndex].parameterName,
                        weaponAnimationsFunc[funcIndex].boolValue);
                    break;
                case WeaponAnimationParameterType.Float:
                    animator.SetFloat(weaponAnimationsFunc[funcIndex].parameterName,
                        weaponAnimationsFunc[funcIndex].floatValue);
                    break;
                case WeaponAnimationParameterType.Trigger:
                    animator.SetTrigger(weaponAnimationsFunc[funcIndex].parameterName);
                    break;
                case WeaponAnimationParameterType.Int:
                    animator.SetInteger(weaponAnimationsFunc[funcIndex].parameterName,
                        weaponAnimationsFunc[funcIndex].intValue);
                    break;
            }
        }


        /// <summary>
        /// 武器动画设置参数类型
        /// </summary>
        private enum WeaponAnimationParameterType
        {
            Trigger,
            Int,
            Float,
            Bool
        }

        /// <summary>
        /// 武器动画配置类
        /// </summary>
        [Serializable]
        private class WeaponAnimationConfig
        {
            /// <summary>
            /// 动画参数名
            /// </summary>
            public string parameterName;

            /// <summary>
            /// 动画参数数据类型
            /// </summary>
            public WeaponAnimationParameterType weaponAnimationParameterType;

            [ShowIf("weaponAnimationParameterType", WeaponAnimationParameterType.Int), AllowNesting]
            public int intValue;

            [ShowIf("weaponAnimationParameterType", WeaponAnimationParameterType.Bool), AllowNesting]
            public bool boolValue;

            [ShowIf("weaponAnimationParameterType", WeaponAnimationParameterType.Float), AllowNesting]
            public float floatValue;

            public WeaponAnimationConfig(WeaponAnimationParameterType weaponAnimationParameterType)
            {
                this.weaponAnimationParameterType = weaponAnimationParameterType;
            }
        }
    }
}