using UnityEditor;
using UnityEngine.UIElements;
using ZiercCode.Old.Manager;

namespace ZiercCode.DungeonSmorgasbord.Editor.Inspector
{
    [CustomEditor(typeof(ParallaxMoveManager))]
    public class ParallaxMoveManagerEditor : UnityEditor.Editor
    {
        public VisualTreeAsset treeAsset;

        private Button _addButton;
        private Button _deleteButton;

        private ParallaxMoveManager _parallaxMoveManager;

        private void OnEnable()
        {
            _parallaxMoveManager = (ParallaxMoveManager)target;
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();

            treeAsset.CloneTree(root); //将自定义的内容绘制出来

            _addButton = root.Q<Button>("addObjectButton");
            _deleteButton = root.Q<Button>("deleteObjectButton");

            _addButton.RegisterCallback<ClickEvent>(Add);
            _deleteButton.RegisterCallback<ClickEvent>(Delete);
            return root;
        }

        private void Add(ClickEvent @event)
        {
            _parallaxMoveManager.Add();
        }

        private void Delete(ClickEvent @event)
        {
            _parallaxMoveManager.Delete();
        }
    }
}