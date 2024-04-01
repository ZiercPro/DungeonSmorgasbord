using UnityEngine;
using System.Collections.Generic;

namespace ZRuntime
{

    /// <summary>
    /// 储存所有UI信息 并可以创建或销毁UI
    /// </summary>
    public class UIManager
    {
        /// <summary>
        /// 储存所有UI信息的字典，每一个UI信息都会对应一个GameObject
        /// </summary>
        private Dictionary<UIType, GameObject> uiDic;

        public UIManager()
        {
            if (uiDic == null)
                uiDic = new Dictionary<UIType, GameObject>();
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

            if (uiDic.ContainsKey(type))
                return uiDic[type];

            GameObject ui = GameObject.Instantiate(Resources.Load<GameObject>(type.Path), parent.transform);
            ui.name = type.Name;

            uiDic.Add(type, ui);

            return ui;
        }

        /// <summary>
        /// 销毁一个UI对象
        /// </summary>
        public void DestroyUI(UIType type)
        {
            if (uiDic.ContainsKey(type))
            {
                GameObject.Destroy(uiDic[type]);
                uiDic.Remove(type);
            }
        }
    }
}