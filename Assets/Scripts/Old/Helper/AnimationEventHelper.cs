using UnityEngine;
using UnityEngine.Events;

namespace ZiercCode.Old.Helper
{
    /// <summary>
    /// 用于帮助实现动画帧事件
    /// </summary>
    public class AnimationEventHelper : MonoBehaviour
    {

        public UnityEvent AnimationEventTrigger;

        protected virtual void EventStart()
        {
            AnimationEventTrigger?.Invoke();
        }
    }
}