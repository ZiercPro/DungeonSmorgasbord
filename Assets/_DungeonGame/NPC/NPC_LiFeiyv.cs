using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Component;

namespace ZiercCode._DungeonGame.NPC
{
    public class NPC_LiFeiyv : MonoBehaviour
    {
        [SerializeField] private RandomMoveWithNavMesh randomMoveWithNavMesh;
        [SerializeField] private AutoFlipComponent autoFlipComponent;
        [SerializeField] private UnityEngine.AI.NavMeshAgent navMeshAgent;
        [SerializeField] private Animator animator;

        private void Update()
        {
            if (navMeshAgent.velocity != Vector3.zero)
            {
                //播放移动动画
                animator.SetBool("move", true);
            }
            else
            {
                animator.SetBool("move", false);
            }

            autoFlipComponent.FaceTo(navMeshAgent.velocity + transform.position);
        }
    }
}