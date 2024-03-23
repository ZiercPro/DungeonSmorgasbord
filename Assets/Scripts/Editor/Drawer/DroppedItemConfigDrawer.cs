using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DroppedItemConfig))]
public class DroppedItemConfigDrawer : PropertyDrawer
{
    private SerializedProperty _haveItemNumRange;
    private SerializedProperty _droppedItemTemp;
    private SerializedProperty _dropChance;
    private SerializedProperty _itemNum;
    private SerializedProperty _minNum;
    private SerializedProperty _maxNum;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        _haveItemNumRange = property.FindPropertyRelative("haveItemNumRange");
        _droppedItemTemp = property.FindPropertyRelative("droppedItemTemp");
        _dropChance = property.FindPropertyRelative("dropChance");
        _itemNum = property.FindPropertyRelative("itemNum");
        _minNum = property.FindPropertyRelative("minNum");
        _maxNum = property.FindPropertyRelative("maxNum");

        Rect foldOutBox = new Rect(position.xMin, position.yMin, position.size.x, EditorGUIUtility.singleLineHeight);
        property.isExpanded = EditorGUI.Foldout(foldOutBox, property.isExpanded, label);
        if (property.isExpanded)
        {
            //draw property
            DrawItemTemp(position);
            DrawHaveItemRange(position);
            bool haveItemRange = _haveItemNumRange.boolValue;
            if (haveItemRange)
            {
                //draw min and max
                DrawMinAndMax(position);
            }
            else
            {
                DrawItemNum(position);
            }

            DrawDropChance(position, haveItemRange);
        }


        EditorGUI.EndProperty();
    }

    private void DrawItemTemp(Rect position)
    {
        Rect tempR = new Rect(position.xMin, position.yMin + EditorGUIUtility.singleLineHeight, position.size.x,
            EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(tempR, _droppedItemTemp, new GUIContent(_droppedItemTemp.name));
    }

    private void DrawHaveItemRange(Rect position)
    {
        Rect tempR = new Rect(position.xMin, position.yMin + 10f + EditorGUIUtility.singleLineHeight * 2,
            position.size.x,
            EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(tempR, _haveItemNumRange, new GUIContent(_haveItemNumRange.name));
    }

    private void DrawMinAndMax(Rect position)
    {
        Rect tempR_1 = new Rect(position.xMin, position.yMin + 20f + EditorGUIUtility.singleLineHeight * 3,
            position.size.x,
            EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(tempR_1, _minNum, new GUIContent(_minNum.name));
        Rect tempR_2 = new Rect(position.xMin, position.yMin + 30f + EditorGUIUtility.singleLineHeight * 4,
            position.size.x,
            EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(tempR_2, _maxNum, new GUIContent(_maxNum.name));

        // Rect temp_3 = new Rect(position.xMin, position.yMin + 30f + EditorGUIUtility.singleLineHeight * 5,
        //     position.size.x,
        //     EditorGUIUtility.singleLineHeight);
        // if (_maxNum.intValue < _minNum.intValue)
        // {
        //     EditorGUI.HelpBox(temp_3, "最大值不可以小于最小值!", MessageType.Warning);
        // }
    }

    private void DrawItemNum(Rect position)
    {
        Rect tempR = new Rect(position.xMin, position.yMin + 20f + EditorGUIUtility.singleLineHeight * 3,
            position.size.x,
            EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(tempR, _itemNum, new GUIContent(_itemNum.name));
    }

    private void DrawDropChance(Rect position, bool haveItemNumRange)
    {
        if (haveItemNumRange)
        {
            Rect tempR = new Rect(position.xMin, position.yMin + 40f + EditorGUIUtility.singleLineHeight * 5,
                position.size.x,
                EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(tempR, _dropChance, new GUIContent(_dropChance.name));
        }
        else
        {
            Rect tempR = new Rect(position.xMin, position.yMin + 30f + EditorGUIUtility.singleLineHeight * 4,
                position.size.x,
                EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(tempR, _dropChance, new GUIContent(_dropChance.name));
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height;
        if (property.isExpanded)
        {
            height = 6 * EditorGUIUtility.singleLineHeight + 50f;
        }
        else
        {
            height = EditorGUIUtility.singleLineHeight;
        }

        return height;
    }
}