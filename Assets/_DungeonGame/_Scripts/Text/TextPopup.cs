using TMPro;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Extend;
using ZiercCode.ObjectPool;
using ZiercCode.Utilities;

namespace ZiercCode._DungeonGame._Scripts.Text
{
    /// <summary>
    /// 生成跳动文本
    /// </summary>
    public class TextPopup : USingleton<TextPopup>
    {
        [SerializeField]
        private GameObject popupPrefab;

        [SerializeField]
        private Canvas canvas;

        private void OnEnable()
        {
            SceneComponent.Instance.onSceneStartLoad.AddListener(ResetPool);
            //_eventsGroup.AddListener<GameStateEvent.GamePause>(a => PoolManager.Instance.Dispose("textPopup", false));
        }

        private void OnDisable()
        {
            SceneComponent.Instance.onSceneStartLoad.RemoveListener(ResetPool);
            //_eventsGroup.RemoveAllListener();
        }

        public void Init()
        {
            PoolManager.Instance.Register("textPopup", popupPrefab, 20, 300,true);
        }

        public void InitPopupText(Vector3 startPosition, Color textColor, int amount)
        {
            InitPopupText(startPosition, textColor, amount.ToString());
        }

        public void InitPopupText(Vector3 startPosition, Color textColor, string text)
        {
            GameObject obj = (GameObject)PoolManager.Instance.Get("textPopup");
            Camera mainCamera = Camera.main;
            startPosition = mainCamera.WorldToScreenPoint(startPosition);
            obj.transform.position = startPosition;
            obj.transform.SetParent(canvas.transform);
            obj.transform.SetAsLastSibling(); //调整在父物体的子物体列表中的顺序以确保渲染顺序
            obj.GetComponent<TextMeshProUGUI>().color = textColor;
            obj.GetComponent<TextMeshProUGUI>().SetText(text);
            obj.GetComponent<TextPopupAnimation>()
                .Popup(() => PoolManager.Instance.Release("textPopup", obj));
        }

        private void ResetPool(string sceneName, AsyncOperation load)
        {
            PoolManager.Instance.Dispose("textPopup", false);
        }
    }
}