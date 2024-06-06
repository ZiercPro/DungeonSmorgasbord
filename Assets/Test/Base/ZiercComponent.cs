using UnityEngine;

namespace ZiercCode.Test.Base
{
    public abstract class ZiercComponent : MonoBehaviour
    {
        private void Awake()
        {
            GameEntry.RegisterComponent(this);
        }
    }
}