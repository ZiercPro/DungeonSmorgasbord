using UnityEngine;
using UnityEngine.Events;
using ZiercCode.Utilities;

namespace ZiercCode._DungeonGame.TestRoom
{
    /// <summary>
    /// 一个玩家进入或离开就会触发事件的区域
    /// </summary>
    public class TriggerArea : MonoBehaviour
    {
        [SerializeField]
        private Vector2 collisionBox;

        private bool _isPlayerEnter; //玩家是否进入过
        private RangeDetector _rangeDetector = new(1);

        [HideInInspector]
        public UnityEvent onPlayerEnter = new();

        [HideInInspector]
        public UnityEvent onPlayerExit = new();

        private void FixedUpdate()
        {
            bool detected = _rangeDetector.DetectInBoxByTag("Player", transform.position, collisionBox);
            if (detected && !_isPlayerEnter)
            {
                _isPlayerEnter = true;
                onPlayerEnter?.Invoke();
            }
            else if (!detected && _isPlayerEnter)
            {
                _isPlayerEnter = false;
                onPlayerExit?.Invoke();
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, collisionBox);
        }
#endif
    }
}