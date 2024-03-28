using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Helper
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