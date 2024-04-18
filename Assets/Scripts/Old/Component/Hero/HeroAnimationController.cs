using UnityEngine;

namespace ZiercCode.Runtime.Component.Hero
{
    /// <summary>
    /// 暂时使用
    /// </summary>
    public class HeroAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void MoveAnimation(Vector2 moveDir)
        {
            int runningID = Animator.StringToHash("running");
            if (moveDir.sqrMagnitude > 0)
            {
                animator.SetBool(runningID, true);
            }
            else
            {
                animator.SetBool(runningID, false);
            }
        }

        public void HitAnimation()
        {
            int getHitID = Animator.StringToHash("getHit");
            animator.SetTrigger(getHitID);
        }

        public void DeadAnimation()
        {
            int isDeadID = Animator.StringToHash("isDead");
            animator.SetTrigger(isDeadID);
        }
    }
}