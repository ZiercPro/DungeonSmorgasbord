using UnityEditor;
using UnityEngine;

namespace Editor.Drawer
{
    using Runtime.CustomAttribute;

    [CustomPropertyDrawer(typeof(MySeparator))]
    public class MySeparatorDrawer : DecoratorDrawer
    {
        public override void OnGUI(Rect position)
        {
            MySeparator mySeparator = attribute as MySeparator;
            Rect newRect = new Rect(position.xMin,
                position.yMin + mySeparator.Spacing,
                position.width,
                mySeparator.Thickness);
            EditorGUI.DrawRect(newRect, Color.gray);
        }

        public override float GetHeight()
        {
            MySeparator mySeparator = attribute as MySeparator;
            float newHeight = mySeparator.Thickness + mySeparator.Spacing * 2f;
            return newHeight;
        }
    }
}