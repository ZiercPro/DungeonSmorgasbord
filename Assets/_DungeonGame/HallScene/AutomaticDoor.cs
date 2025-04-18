using UnityEngine;

namespace ZiercCode._DungeonGame.HallScene
{
    //自动门
    //玩家在一定范围内会自动开关
    //接触则触发
    public class AutomaticDoor : BaseDoor
    {
        [SerializeField]
        private float openDetectRadius; //开门检测范围

        private bool _isPlayerInRange;

        protected override void FixedUpdate()
        {
            _isPlayerInRange = RangeDetector.DetectInCircleByTag("Player", transform.position, openDetectRadius);
            if (_isPlayerInRange && IOpenClose.IsClosed)
            {
                IOpenClose.Open();
            }
            else if (!_isPlayerInRange && !IOpenClose.IsClosed)
            {
                IOpenClose.Close();
            }

            base.FixedUpdate();
        }

#if UNITY_EDITOR
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, openDetectRadius);
        }
#endif
    }
}