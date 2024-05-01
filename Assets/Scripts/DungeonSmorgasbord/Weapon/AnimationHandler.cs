using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    /// <summary>
    /// 用于提供动画触发方法
    /// </summary>
    public class AnimationHandler : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [SerializeField, AllowNesting] private List<AnimationConfig> animationsFunc;

        /// <summary>
        /// 激活动画
        /// </summary>
        /// <param name="funcIndex">方法在链表中的顺序</param>
        public void ActiveAnimationFunc(int funcIndex)
        {
            switch (animationsFunc[funcIndex].animationParameterType)
            {
                case AnimationParameterType.Bool:
                    animator.SetBool(animationsFunc[funcIndex].parameterName,
                        animationsFunc[funcIndex].boolValue);
                    break;
                case AnimationParameterType.Float:
                    animator.SetFloat(animationsFunc[funcIndex].parameterName,
                        animationsFunc[funcIndex].floatValue);
                    break;
                case AnimationParameterType.Trigger:
                    animator.SetTrigger(animationsFunc[funcIndex].parameterName);
                    break;
                case AnimationParameterType.Int:
                    animator.SetInteger(animationsFunc[funcIndex].parameterName,
                        animationsFunc[funcIndex].intValue);
                    break;
            }
        }


        /// <summary>
        /// 动画设置参数类型
        /// </summary>
        private enum AnimationParameterType
        {
            Trigger,
            Int,
            Float,
            Bool
        }

        /// <summary>
        /// 动画配置类
        /// </summary>
        [Serializable]
        private class AnimationConfig
        {
            /// <summary>
            /// 动画参数名
            /// </summary>
            public string parameterName;

            /// <summary>
            /// 动画参数数据类型
            /// </summary>
            public AnimationParameterType animationParameterType;

            [ShowIf("animationParameterType", AnimationParameterType.Int), AllowNesting]
            public int intValue;

            [ShowIf("animationParameterType", AnimationParameterType.Bool), AllowNesting]
            public bool boolValue;

            [ShowIf("animationParameterType", AnimationParameterType.Float), AllowNesting]
            public float floatValue;
        }
    }
}