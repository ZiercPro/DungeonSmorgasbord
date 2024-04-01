using UnityEngine;
using UnityEngine.Events;

namespace ZRuntime
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