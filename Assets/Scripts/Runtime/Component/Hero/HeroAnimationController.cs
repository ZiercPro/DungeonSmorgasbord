using UnityEngine;

namespace Runtime.Component.Hero
{
    using Base;
    using Player;

    /// <summary>
    /// 暂时使用
    /// </summary>
    public class HeroAnimationController : MonoBehaviour
    {
        [SerializeField] private Health Health;
        [SerializeField] private Animator Animator;
        [SerializeField] private InputManager InputManager;

        public void MoveAnimation(Vector2 moveDir)
        {
            int runningID = UnityEngine.Animator.StringToHash("running");
            if (moveDir.sqrMagnitude > 0)
            {
                Animator.SetBool(runningID, true);
            }
            else
            {
                Animator.SetBool(runningID, false);
            }
        }

        public void HitAnimation()
        {
            int getHitID = Animator.StringToHash("getHit");
            Animator.SetTrigger(getHitID);
        }

        public void DeadAnimation()
        {
            int isDeadID = Animator.StringToHash("isDead");
            Animator.SetTrigger(isDeadID);
        }
    }
}