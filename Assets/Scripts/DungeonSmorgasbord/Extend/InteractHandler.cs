using System;
using UnityEngine;

namespace ZiercCode.DungeonSmorgasbord.Extend
{
    public class InteractHandler : MonoBehaviour
    {
        private event Action InteractiveEvent;

        public void AddToEvent(Action interAction)
        {
            InteractiveEvent = null;
            InteractiveEvent += interAction;
        }

        public void RemoveFromEvent(Action interAction)
        {
            if (InteractiveEvent != null)
                InteractiveEvent -= interAction;
        }

        public void OnInteractive()
        {
            InteractiveEvent?.Invoke();
            InteractiveEvent = null;
        }
    }
}