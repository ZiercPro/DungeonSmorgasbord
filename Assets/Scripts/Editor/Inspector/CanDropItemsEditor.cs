using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Inspector
{
    using Runtime.Component.Enemy;
    using Runtime.DroppedItem;
    using UnityEditor;

    [CustomEditor(typeof(CanDropItems))]
    public class CanDropItemsEditor : Editor
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