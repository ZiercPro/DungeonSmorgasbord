using System;
using ZiercCode.Old.Helper;

namespace ZiercCode.Old.Weapon
{
    public class WeaponAnimationEventHandler : AnimationEventHelper
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
}