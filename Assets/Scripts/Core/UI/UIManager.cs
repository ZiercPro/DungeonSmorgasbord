using System.Collections.Generic;
using UnityEngine;

namespace ZiercCode.Core.UI
{
    /// <summary>
    /// 储存所有UI信息 并可以创建或销毁UI
    /// </summary>
    public class UIManager
    {
        /// <summary>
        /// 储存所有UI信息的字典，每一个UI信息都会对应一个GameObject
        /// </summary>
        private Dictionary<UIType, GameObject> _uiDictionary;

        public UIManager()
        {
            if (_uiDictionary == null)
                _uiDictionary = new Dictionary<UIType, GameObject>();
        }

        /// <summary>
        /// 获取一个UI对象
        /// </summary>
        /// <param name="type"> UI信息 </param>
        /// <returns></returns>
        public GameObject GetSingleUI(UIType type)
        {
            GameObject parent = GameObject.Find("Canvas");
            if (!parent)
            {
                Debug.LogError("Canvas不存在!");
                return null;
            }

            if (_uiDictionary.ContainsKey(type))
                return _uiDictionary[type];

            GameObject ui = GameObject.Instantiate(Resources.Load<GameObject>(type.Path), parent.transform);
            ui.name = type.Name;

            _uiDictionary.Add(type, ui);

            return ui;
        }

        /// <summary>
        /// 销毁一个UI对象
        /// </summary>
        public void DestroyUI(UIType type)
        {
            if (_uiDictionary.ContainsKey(type))
            {
                GameObject.Destroy(_uiDictionary[type]);
                _uiDictionary.Remove(type);
            }
        }
    }
}