using UnityEngine;

namespace ZiercCode.Runtime.Basic
{
    /// <summary>
    /// 单例 场景转换时不会被销毁
    /// 无单例则直接实例化
    /// </summary>
    /// <typeparam name="T">单例对象类型</typeparam>
    public class UnitySingleton<T> : MonoBehaviour where T : UnityEngine.Component
    {
        private static T instance;

        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (instance != null && instance.gameObject != gameObject)
            {
                Destroy(gameObject);
            }
        }

        public static T Instance
        {
            get
            {
                if (instance == null)
                    instance = FindAnyObjectByType<T>();
                if (instance == null)
                {
                    GameObject newG = new GameObject(typeof(T).Name);
                    instance = newG.AddComponent<T>();
                }

                return instance;
            }
        }
    }

// public class Singleton<T> where T : class, new()
// {
//     private static T instance;
//
//     public static T Instance
//     {
//         get
//         {
//             if (instance == null)
//             {
//                 instance = new T();
//             }
//
//             return instance;
//         }
//     }
// }


    /// <summary>
    /// 简易单例 场景转换时会被销毁
    /// </summary>
    /// <typeparam name="T">单例对象类型</typeparam>
    public class SingletonIns<T> : MonoBehaviour where T : UnityEngine.Component
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                    instance = FindAnyObjectByType<T>();
                if (instance == null)
                {
                    GameObject newG = new GameObject(typeof(T).Name);
                    instance = newG.AddComponent<T>();
                }

                return instance;
            }
        }
    }

    public class SingletonNew<T> : MonoBehaviour where T : new()
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }

                return instance;
            }
        }
    }
}