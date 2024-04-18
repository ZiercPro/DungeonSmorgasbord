using UnityEngine;

namespace ZiercCode.Old.Basic
{
    /// <summary>
    /// unity组件单例 场景转换时不会被销毁
    /// 无单例则直接实例化
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    public class USingletonComponentDontDestroy<T> : MonoBehaviour where T : UnityEngine.Component
    {
        private static T _instance;

        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (_instance != null && _instance.gameObject != gameObject)
            {
                Destroy(gameObject);
            }
        }

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
    }



}