using UnityEngine;

namespace ZiercCode._DungeonGame.HallScene
{
    //手动门
    //需要互动开关
    public class ManuallyDoor : BaseDoor
    {
        [SerializeField]
        private float interactRadius; //能够开关门的范围

        private bool _isPlayerInRange;

        private void Update()
        {
            //临时逻辑
            if (Input.GetKeyDown(KeyCode.E))
            {
                _isPlayerInRange = RangeDetector.DetectInCircleByTag("Player", transform.position, interactRadius);
                if (_isPlayerInRange)
                {
                    Interact();
                }
            }
        }

        private void Interact()
        {
            if (IOpenClose.IsClosed)
            {
                IOpenClose.Open();
            }
            else
            {
                IOpenClose.Close();
            }
        }

#if UNITY_EDITOR
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, interactRadius);
        }
#endif
    }
}