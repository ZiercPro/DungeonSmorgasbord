using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHelper : MonoBehaviour
{
    public UnityEvent AnimationEventTrigger;
    public void EventStart() {
        AnimationEventTrigger?.Invoke();
    }
}
