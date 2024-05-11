using UnityEngine;
using UnityEngine.Events;
using ZiercCode.Core.Extend;
using ZiercCode.Core.Pool;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    public class SpawnerRedCircle : MonoBehaviour, IPoolObject
    {
        private SpawnHandle _spawnHandle;
        private UnityEvent _redCircleActive;
        private AnimationEventHelper _animationEventHelper;

        private void Awake()
        {
            _animationEventHelper = GetComponentInChildren<AnimationEventHelper>();
            _redCircleActive = new UnityEvent();
        }

        public void SetActiveEvent(UnityAction action)
        {
            _redCircleActive.AddListener(action);
        }


        public void OnGet()
        {
            if (_redCircleActive != null)
                _animationEventHelper.animationTriggeredPerform.AddListener(_redCircleActive.Invoke);
            _animationEventHelper.animationEnded.AddListener(_spawnHandle.Release);
        }

        public void OnRelease()
        {
            _animationEventHelper.animationTriggeredPerform.RemoveAllListeners();
            _animationEventHelper.animationEnded.RemoveAllListeners();
            _redCircleActive.RemoveAllListeners();
        }

        public void SetSpawnHandle(SpawnHandle handle)
        {
            _spawnHandle = handle;
        }
    }
}