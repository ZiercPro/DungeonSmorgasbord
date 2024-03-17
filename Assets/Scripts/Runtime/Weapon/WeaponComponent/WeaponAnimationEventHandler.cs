using System;
using UnityEngine;

public class WeaponAnimationEventHandler : MonoBehaviour
{
    public event Action AnimationStarted;
    public event Action AnimationTriggeredStart;
    public event Action AnimationTriggeredPerform;
    public event Action AnimationTriggeredEnd;
    public event Action AnimationEnded;

    private void OnAnimationStarted()
    {
        AnimationStarted?.Invoke();
    }

    private void OnAnimationTriggeredStart()
    {
        AnimationTriggeredStart?.Invoke();
    }

    private void OnAnimationTriggeredPerform()
    {
        AnimationTriggeredPerform?.Invoke();
    }

    private void OnAnimationTriggeredEnd()
    {
        AnimationTriggeredEnd?.Invoke();
    }

    private void OnAnimationEnded()
    {
        AnimationEnded?.Invoke();
    }
}