using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
        private string textPopupPoolName;

        [SerializeField]
        private int maxNum;

        [SerializeField]
        private int minNum;

        [SerializeField]
        private int maxActiveNum; //屏幕显示的最大text数量

        [SerializeField]
        private Canvas canvas;

        private LinkedList<GameObject> _unPooledTextPopupList = new(); //没有释放的文本

        private void OnEnable()
        {
            SceneComponent.Instance.onSceneStartLoad.AddListener(OnSceneLoaded);
        }

        private void OnDisable()
        {
            SceneComponent.Instance.onSceneStartLoad.RemoveListener(OnSceneLoaded);
        }

        public void Init()
        {
            PoolManager.Instance.Register(textPopupPoolName, popupPrefab, minNum, maxNum, true);
        }

        public void SpawnText(Vector3 startPosition, Color textColor, int amount)
        {
            SpawnText(startPosition, textColor, amount.ToString());
        }

        public void SpawnText(Vector3 startPosition, Color textColor, string text)
        {
            CheckTextMaxNum(); //优化
            GameObject newText = (GameObject)PoolManager.Instance.Get(textPopupPoolName);
            _unPooledTextPopupList.AddFirst(newText);
            Camera mainCamera = Camera.main;
            startPosition = mainCamera.WorldToScreenPoint(startPosition);
            newText.transform.position = startPosition;
            newText.transform.SetParent(canvas.transform);
            newText.transform.SetAsLastSibling(); //调整在父物体的子物体列表中的顺序以确保渲染顺序
            newText.GetComponent<TextMeshProUGUI>().color = textColor;
            newText.GetComponent<TextMeshProUGUI>().SetText(text);
            newText.GetComponent<TextPopupAnimation>()
                .Popup(() =>
                {
                    PoolManager.Instance.Release(textPopupPoolName, newText);
                    _unPooledTextPopupList.Remove(newText);
                });
        }

        private void OnSceneLoaded(string sceneName, AsyncOperation load)
        {
            CleanTextPool();
        }

        //清理对象池并释放所有未释放的文本
        private void CleanTextPool()
        {
            while (_unPooledTextPopupList.First != null)
            {
                PoolManager.Instance.Release(textPopupPoolName, _unPooledTextPopupList.First.Value);
                _unPooledTextPopupList.RemoveFirst();
            }

            PoolManager.Instance.Dispose(textPopupPoolName, false);
        }

        //检测文本存活数量是否超限
        private void CheckTextMaxNum()
        {
            while (_unPooledTextPopupList.Count > maxActiveNum)
            {
                _unPooledTextPopupList.Last.Value.GetComponent<TextPopupAnimation>().CompleteAnimation();
            }
        }
    }
}