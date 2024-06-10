using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using ZiercCode.Test.Procedure;
using ZiercCode.Test.Utility;

namespace ZiercCode.Test.Editor
{
    [CustomEditor(typeof(ProcedureComponent))]
    public class ProcedureComponentInspector : UnityEditor.Editor
    {
        [SerializeField] private VisualTreeAsset treeAsset;

        private DropdownField _launchProcedure;
        private List<string> _availableProcedureNames; //显示名称缩写

        private int _launchProcedureFullNameIndex;
        private SerializedProperty _availableProcedureFullNames; //组件字段
        private SerializedProperty _launchProcedureFullName; //组件字段

        private void OnEnable()
        {
            _availableProcedureFullNames = serializedObject.FindProperty("availableTypeFullNames");
            _launchProcedureFullName = serializedObject.FindProperty("launchProcedureFullName");
        }

        public override VisualElement CreateInspectorGUI()
        {
            serializedObject.Update();

            RefreshAvailableProcedureNameToggles();
            RefreshAvailableProcedureNames();

            VisualElement root = new VisualElement();

            treeAsset.CloneTree(root);

            _launchProcedure = root.Q<DropdownField>("LaunchProcedure");
            _launchProcedure.choices = _availableProcedureNames;

            _launchProcedure.RegisterCallback<ChangeEvent<string>>(OnLaunchProcedureSelected);

            return root;
        }

        private void OnLaunchProcedureSelected(ChangeEvent<string> @event)
        {
            _launchProcedureFullNameIndex = _availableProcedureNames.IndexOf(@event.newValue);

            if (_launchProcedureFullNameIndex < 0)
            {
                Debug.LogError($"无法找到该流程：{@event.newValue}");
                return;
            }

            _launchProcedureFullName.stringValue = _availableProcedureFullNames
                .GetArrayElementAtIndex(_launchProcedureFullNameIndex).stringValue;

            serializedObject.ApplyModifiedProperties();
        }

        private void RefreshAvailableProcedureNameToggles()
        {
            if (_availableProcedureNames == null) _availableProcedureNames = new List<string>();
            else _availableProcedureNames.Clear();

            Assembly main = Assembly.LoadFrom("Library/ScriptAssemblies/Assembly-Csharp.dll");
            Type[] types = main.GetTypes();


            foreach (var type in types)
            {
                if (ZiercType.GetAbstractTypes(type).Any(i => i == typeof(ProcedureBase)))
                {
                    _availableProcedureNames.Add(type.Name);
                }
            }
        }

        private void RefreshAvailableProcedureNames()
        {
            _availableProcedureFullNames.ClearArray();

            Assembly main = Assembly.LoadFrom("Library/ScriptAssemblies/Assembly-Csharp.dll");
            Type[] types = main.GetTypes();

            int index = 0;

            foreach (var type in types)
            {
                if (ZiercType.GetAbstractTypes(type).Any(i => i == typeof(ProcedureBase)))
                {
                    _availableProcedureFullNames.InsertArrayElementAtIndex(index);
                    _availableProcedureFullNames.GetArrayElementAtIndex(index).stringValue = type.FullName;
                    index++;
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}