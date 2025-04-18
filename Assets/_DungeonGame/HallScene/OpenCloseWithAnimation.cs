using UnityEngine;

namespace ZiercCode._DungeonGame.HallScene
{
    //包含开关两种动画的基础开关组件
    public class OpenCloseWithAnimation : MonoBehaviour, IOpenClose
    {
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private bool defaultClosed = true; //默认是否为关闭状态

        private bool _isClosed; //当前是否关闭

        private void Awake()
        {
            _isClosed = defaultClosed;
        }

        public bool IsClosed => _isClosed;

        public void Open()
        {
            if (_isClosed)
            {
                animator.SetTrigger("trigger");
                animator.SetBool("isClosed", _isClosed);
                _isClosed = false;
            }
        }

        public void Close()
        {
            if (!_isClosed)
            {
                animator.SetTrigger("trigger");
                animator.SetBool("isClosed", _isClosed);
                _isClosed = true;
            }
        }
    }
}