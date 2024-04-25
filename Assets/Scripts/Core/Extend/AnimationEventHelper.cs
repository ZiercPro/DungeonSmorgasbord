using UnityEngine;
using UnityEngine.Events;

namespace ZiercCode.Old.Helper
{
    /// <summary>
    /// 用于帮助实现动画帧事件
    /// </summary>
    public class AnimationEventHelper : MonoBehaviour
    {
        public UnityEvent animationStarted;
        public UnityEvent animationTriggeredStart;
        public UnityEvent animationTriggeredPerform;
        public UnityEvent animationTriggeredEnd;
        public UnityEvent animationEnded;

        private void OnAnimationStarted()
        {
            animationStarted?.Invoke();
        }

        private void OnAnimationTriggeredStart()
        {
            animationTriggeredStart?.Invoke();
        }

        private void OnAnimationTriggeredPerform()
        {
            animationTriggeredPerform?.Invoke();
        }

        private void OnAnimationTriggeredEnd()
        {
            animationTriggeredEnd?.Invoke();
        }

        private void OnAnimationEnded()
        {
            animationEnded?.Invoke();
        }
    }
}