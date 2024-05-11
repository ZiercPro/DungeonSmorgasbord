using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using ZiercCode.DungeonSmorgasbord.Item;
using ZiercCode.Old.Component.Enemy;
using ZiercCode.Old.DroppedItem;

namespace ZiercCode.DungeonSmorgasbord.Editor.Inspector
{
    [CustomEditor(typeof(CanDropItems))]
    public class CanDropItemsEditor : UnityEditor.Editor
    {
        public VisualTreeAsset treeAsset;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            if (GUILayout.Button("New Drop"))
            {
                CreateDropElement();
            }

            if (GUILayout.Button("Delete Drop"))
            {
                DeleteDropElement();
            }

            base.OnInspectorGUI();

            serializedObject.ApplyModifiedProperties();
        }

        private void CreateDropElement()
        {
            CanDropItems canDropItems = (CanDropItems)target;
            DroppedItemConfig newConfig = new DroppedItemConfig();
            canDropItems.DroppedItems.Add(newConfig);
        }

        private void DeleteDropElement()
        {
            CanDropItems canDropItems = (CanDropItems)target;
            if (canDropItems.DroppedItems.Count > 0)
            {
                int lastIndex = canDropItems.DroppedItems.Count - 1;
                canDropItems.DroppedItems.RemoveAt(lastIndex);
            }
        }
    }
}