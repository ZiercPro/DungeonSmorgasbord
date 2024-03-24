using Runtime.ScriptObject;
using System;
using UnityEngine.UIElements;

namespace Editor.Drawer
{
    using UnityEditor;

    [CustomEditor(typeof(AudioListSo))]
    public class AudioListSoEditor : Editor
    {
        public VisualTreeAsset _treeAsset;

        private AudioListSo _audioListSo;
        private Button _deleteButton;
        private Button _addButton;

        private void OnEnable()
        {
            _audioListSo = (AudioListSo)target;
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new();
            _treeAsset.CloneTree(root);
            _addButton = root.Q<Button>("addAudioButton");
            _deleteButton = root.Q<Button>("deleteAudioButton");

            _addButton.RegisterCallback<ClickEvent>(AddAudio);
            _deleteButton.RegisterCallback<ClickEvent>(DeleteAudio);
            return root;
        }

        private void AddAudio(ClickEvent e)
        {
            _audioListSo.Add();
            ;
        }

        private void DeleteAudio(ClickEvent e)
        {
            _audioListSo.Delete();
        }
    }
}