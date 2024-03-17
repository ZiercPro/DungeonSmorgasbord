using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Separator))]
public class SeparatorDrawer : DecoratorDrawer
{
    public override void OnGUI(Rect position)
    {
        Separator separator = attribute as Separator;
        Rect newRect = new Rect(position.xMin,
            position.yMin + separator.Spacing,
            position.width,
            separator.Thickness);
        EditorGUI.DrawRect(newRect, Color.gray);
    }

    public override float GetHeight()
    {
        Separator separator = attribute as Separator;
        float newHeight = separator.Thickness + separator.Spacing * 2f;
        return newHeight;
    }
}