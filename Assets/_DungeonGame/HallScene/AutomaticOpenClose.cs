using UnityEngine;
using UnityEngine.Events;

namespace ZiercCode._DungeonGame.HallScene
{
    public class AutomaticOpenClose : MonoBehaviour //玩家在附近就自动开关
    {
        private IOpenClose _iOpenClose;
        [SerializeField] private float detectRadius;
        [SerializeField] private Vector2 enterRange; //进入检测范围

        private RangeDetect _rangeDetect = new RangeDetect(5);
        private bool _isPlayerInRange;
        private bool _isPlayerEnter;

        public UnityEvent onPlayerEnter;

        private void Awake()
        {
            _iOpenClose = GetComponentInChildren<IOpenClose>();
        }

        private void FixedUpdate()
        {
            _isPlayerInRange = _rangeDetect.DetectByTagInCircle("Player", transform.position, detectRadius);
            if (_isPlayerInRange && _iOpenClose.IsClosed)
            {
                _iOpenClose.Open();
            }
            else if (!_isPlayerInRange && !_iOpenClose.IsClosed)
            {
                _iOpenClose.Close();
            }

            DetectEnter();
        }


        //检测玩家是否进入
        private void DetectEnter()
        {
            if (!_iOpenClose.IsClosed && !_isPlayerEnter)
            {
                if (_rangeDetect.DetectByTagInBox("Player", transform.position, enterRange))
                {
                    onPlayerEnter.Invoke();
                    _isPlayerEnter = true;
                }
            }
            else if (_isPlayerEnter && !_rangeDetect.DetectByTagInBox("Player", transform.position, enterRange))
            {
                _isPlayerEnter = false;
            }
        }


        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.yellow;
        //     Gizmos.DrawWireSphere(transform.position, detectRadius);
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawWireCube(transform.position, enterRange);
        // }
    }
}