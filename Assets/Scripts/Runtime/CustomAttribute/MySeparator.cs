using UnityEngine;

namespace Runtime.CustomAttribute
{
    /// <summary>
    /// 自定义分割线属性
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true)]
    public class MySeparator : PropertyAttribute
    {
        public readonly float Thickness;
        public readonly float Spacing;
        public readonly Color Color;

        /// <summary>
        /// 分割线
        /// </summary>
        /// <param name="thickness">分割线厚度</param>
        /// <param name="spacing">分割线间距</param>
        /// <param name="color">分割线颜色</param>
        public MySeparator(float thickness = 2f, float spacing = 5f)
        {
            // if (color.r==0f&& color.g==0f&& color.b==0f)
            // {
            //     Color=Color.gray;
            // }
            // else
            // {
            //     Color = color;
            // }

            Thickness = thickness;
            Spacing = spacing;
        }
    }
}