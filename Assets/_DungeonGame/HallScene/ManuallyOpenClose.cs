using UnityEngine;

namespace ZiercCode._DungeonGame.HallScene
{
    public class ManuallyOpenClose : MonoBehaviour //手动开关
    {
        [SerializeField] private float interactRadius; //能够开关门的范围
        private IOpenClose _iOpenClose;
        private RangeDetect _rangeDetect;
        private bool _isPlayerInRange;

        private void Awake()
        {
            _iOpenClose = GetComponentInChildren<IOpenClose>();
            _rangeDetect = new RangeDetect(5);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _isPlayerInRange = _rangeDetect.DetectByTagInCircle("Player", transform.position, interactRadius);
                if (_isPlayerInRange)
                {
                    Interact();
                }
            }
        }

        private void Interact()
        {
            if (_iOpenClose.IsClosed)
                _iOpenClose.Open();
            else _iOpenClose.Close();
        }

        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.yellow;
        //     Gizmos.DrawWireSphere(transform.position, interactRadius);
        // }
    }
}