using UnityEngine;

namespace ZiercCode.Core.Extend
{
    public class DontDestroyItem : MonoBehaviour
    {
        public bool dontDestroyOnLoad = true;

        private void Awake()
        {
            if (dontDestroyOnLoad) DontDestroyOnLoad(this);
        }
    }
}