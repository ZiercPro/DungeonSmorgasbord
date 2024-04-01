using UnityEngine;
using UnityEngine.Events;

namespace ZiercCode.Runtime.Helper
{
    public class AnimationEventHelper : MonoBehaviour
    {
        public UnityEvent AnimationEventTrigger;

        public void EventStart()
        {
            AnimationEventTrigger?.Invoke();
        }
    }
}