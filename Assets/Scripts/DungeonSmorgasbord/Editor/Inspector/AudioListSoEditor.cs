using UnityEditor;
using UnityEngine.UIElements;
using ZiercCode.Old.ScriptObject;

namespace ZiercCode.DungeonSmorgasbord.Editor.Inspector
{


    [CustomEditor(typeof(AudioListSo))]
    public class AudioListSoEditor : UnityEditor.Editor
    {
        public VisualTreeAsset _treeAsset;

        private AudioListSo _audioListSo;
        private Button _resetConfigButton;
        private Button _autoConfigButton;
        private Button _deleteButton;
        private Button _clearButton;

        private void OnEnable()
        {
            _audioListSo = (AudioListSo)target;
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new();
            _treeAsset.CloneTree(root);
            _clearButton = root.Q<Button>("clearButton");
            _autoConfigButton = root.Q<Button>("configButton");
            _resetConfigButton = root.Q<Button>("resetButton");
            _deleteButton = root.Q<Button>("deleteAudioButton");

            _clearButton.RegisterCallback<ClickEvent>(Clear);
            _deleteButton.RegisterCallback<ClickEvent>(DeleteAudio);
            _autoConfigButton.RegisterCallback<ClickEvent>(AutoConfig);
            _resetConfigButton.RegisterCallback<ClickEvent>(ResetConfig);
            return root;
        }

        private void Clear(ClickEvent e)
        {
            _audioListSo.Clear();
        }

        private void DeleteAudio(ClickEvent e)
        {
            _audioListSo.Delete();
        }

        private void AutoConfig(ClickEvent e)
        {
            _audioListSo.AutoConfig();
        }

        private void ResetConfig(ClickEvent e)
        {
            _audioListSo.ReSetConfig();
        }
    }
}