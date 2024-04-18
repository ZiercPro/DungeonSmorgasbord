using UnityEngine;

namespace ZiercCode.Runtime.Basic
{
    public class USingletonNewDestroy<T> : MonoBehaviour where T : new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
            }
        }
    }
}