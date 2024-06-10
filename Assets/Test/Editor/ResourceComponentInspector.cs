using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.UIElements;
using ZiercCode.Test.Resources;
using Toggle = UnityEngine.UIElements.Toggle;

namespace ZiercCode
{
    [CustomEditor(typeof(ResourceComponent))]
    public class ResourceComponentInspector : Editor
    {
        [SerializeField] private VisualTreeAsset treeAsset;

        private Dictionary<string, Toggle> _assetToggles;

        private SerializedProperty _loadAssetLabels;

        private void OnEnable()
        {
            _loadAssetLabels = serializedObject.FindProperty("loadAssetLabels");
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();

            treeAsset.CloneTree(root);

            RefreshToggles(root);

            return root;
        }

        private void OnLabelToggleValueChange(ChangeEvent<bool> eventArgs)
        {
            RefreshAssetLabels();
        }

        private void RefreshAssetLabels()
        {
            _loadAssetLabels.ClearArray();

            int index = 0;
            foreach (var kv in _assetToggles)
            {
                if (kv.Value.value)
                {
                    _loadAssetLabels.InsertArrayElementAtIndex(index);
                    _loadAssetLabels.GetArrayElementAtIndex(index).stringValue = kv.Key;
                    index++;
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void RefreshToggles(VisualElement root)
        {
            if (_assetToggles == null) _assetToggles = new Dictionary<string, Toggle>();
            else _assetToggles.Clear();

            //获取所有资源tag
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
            List<string> referenceLabels = settings.GetLabels();

            //获取容器
            VisualElement assetLabel = root.Q<VisualElement>("AssetLabelsContainer");

            foreach (var label in referenceLabels)
            {
                Toggle newT = new Toggle();
                newT.text = "  " + label;
                newT.viewDataKey = label;
                newT.RegisterCallback<ChangeEvent<bool>>(OnLabelToggleValueChange);
                _assetToggles.Add(label, newT);
                assetLabel.Add(newT);
            }

            RefreshAssetLabels();
        }
    }
}