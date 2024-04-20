using UnityEditor;
using UnityEngine.UIElements;
using ZiercCode.Old.Environment;

namespace ZiercCode.DungeonSmorgasbord.Editor.Drawer
{

    [CustomPropertyDrawer(typeof(ParallaxMoveFeedBack))]
    public class ParallaxMoveFeedBackDrawer : PropertyDrawer
    {
        private VisualTreeAsset _treeAsset;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            _treeAsset =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                    "Assets/Resources/UiToolKit/ParallaxMoveFeedBackDrawer.uxml");
            VisualElement root = new VisualElement();
            _treeAsset.CloneTree(root);
            return root;
        }
        // public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        // {
        //     EditorGUI.BeginProperty(position, label, property);
        //     SerializedProperty offSet = property.FindPropertyRelative("moveOffSet");
        //
        //     Rect foldOutBox = new Rect(position.xMin, position.yMin, position.size.x,
        //         EditorGUIUtility.singleLineHeight);
        //     property.isExpanded = EditorGUI.Foldout(foldOutBox, property.isExpanded, label);
        //     if (property.isExpanded)
        //     {
        //         Rect newR = new Rect(position.xMin, position.yMin + EditorGUIUtility.singleLineHeight, position.size.x,
        //             EditorGUIUtility.singleLineHeight);
        //         EditorGUI.PropertyField(newR, offSet, new GUIContent("OffSet"));
        //     }
        //
        //     EditorGUI.EndProperty();
        // }
        //
        // public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        // {
        //     int finalLines = 1;
        //     if (property.isExpanded)
        //     {
        //         finalLines = 2;
        //     }
        //     else
        //     {
        //         finalLines = 1;
        //     }
        //
        //     return EditorGUIUtility.singleLineHeight * finalLines;
        // }
    }
}