using UnityEngine;

namespace ZiercCode.DungeonSmorgasbord.Component
{
    /// <summary>
    /// 需要使用到协程的组件需要继承
    /// </summary>
    public class CoroutineComponent : MonoBehaviour
    {
        protected virtual void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}