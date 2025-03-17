using UnityEngine;

namespace ZiercCode.Core.UI
{
    /// <summary>
    /// UI管理工具，获取一些UI的子对象
    /// </summary>
    public class UITool
    {
        /// <summary>
        /// 当前活动面板
        /// </summary>
        GameObject thisPanel;

        public UITool(GameObject panel)
        {
            thisPanel = panel;
        }

        /// <summary>
        /// 获取当前活动的面板组件，没有则添加
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <returns></returns>
        public T GetOrAddComponent<T>() where T : Component
        {
            T result = thisPanel.GetComponent<T>();

            if (!result)
            {
                result = thisPanel.AddComponent<T>();
                Debug.LogWarning($"{thisPanel.name}无法找到{typeof(T).Name}组件");
            }

            return result;
        }

        /// <summary>
        /// 根据名字查找子对象
        /// </summary>
        /// <param name="name">子物体名称</param>
        /// <param name="findDisabled">是否查找已被禁用的物体，默认为true</param>
        /// <returns></returns>
        public GameObject FindChildGameObject(string name, bool findDisabled = true)
        {
            Transform[] trans = thisPanel.GetComponentsInChildren<Transform>(findDisabled);

            foreach (Transform transform in trans)
            {
                if (transform.name == name) return transform.gameObject;
            }

            Debug.LogWarning($"{thisPanel.name}中找不到名为{name}的子对象");
            return null;
        }

        /// <summary>
        /// 根据名称获取一个子对象的组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="name">子对象名称</param>
        /// <param name="findDisabled">是否查找已被禁用的组件，默认为true</param>
        /// <returns></returns>
        public T GetComponentInChildrenUI<T>(string name, bool findDisabled = true) where T : Component
        {
            GameObject child = FindChildGameObject(name);

            if (child)
            {
                if (!child.GetComponentInChildren<T>(findDisabled))
                {
                    Debug.LogWarning($"{name}物体及其子物体无组件{typeof(T).Name}!");
                    return null;
                }

                return child.GetComponentInChildren<T>();
            }

            Debug.LogWarning($"无名为{name}的子物体!");
            return null;
        }
    }
}