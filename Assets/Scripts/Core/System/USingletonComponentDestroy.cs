﻿using UnityEngine;

namespace ZiercCode.Core.System
{
    /// <summary>
    /// unity组件单例 场景转换时会被销毁
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    public class USingletonComponentDestroy<T> : MonoBehaviour where T : UnityEngine.Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindAnyObjectByType<T>();
                if (_instance == null)
                {
                    GameObject newG = new GameObject(typeof(T).Name);
                    _instance = newG.AddComponent<T>();
                }

                return _instance;
            }
        }
        protected virtual void Awake()
        {
            if (_instance != null && _instance.gameObject != gameObject)
            {
                Destroy(gameObject);
            }
        }
    }
}