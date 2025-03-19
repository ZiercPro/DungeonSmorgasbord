using UnityEngine;
using UnityEngine.AI;
using ZiercCode.DungeonSmorgasbord.Component;
using Random = UnityEngine.Random;

namespace ZiercCode._DungeonGame.NPC
{
    public class NPC_MoCaiHuan : MonoBehaviour
    {
        [SerializeField] private RandomMoveWithNavMesh randomMoveWithNavMesh;
        [SerializeField] private AutoFlipComponent autoFlipComponent;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Animator animator;
        // [SerializeField] private TextBubble.TextBubble textBubble;

        private void Awake()
        {
            _randomTextBubbleWaitTime = Random.Range(5f, 20f);
            _randomTextBubbleShowTime = Random.Range(5f, 10f);
        }

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

            //ShowTextBubble();
        }

        //文本气泡
        private float _randomTextBubbleShowTime;
        private float _randomTextBubbleWaitTime;
        private bool _isShowingTextBubble;
        private string _showText;

        // private void ShowTextBubble()
        // {
        //     if (_randomTextBubbleWaitTime > 0f)
        //     {
        //         _randomTextBubbleWaitTime -= Time.deltaTime;
        //     }
        //     else
        //     {
        //         if (!_isShowingTextBubble)
        //         {
        //             _showText = textBubble.ShowRandomTextBubble();
        //             _isShowingTextBubble = true;
        //         }
        //
        //         if (_randomTextBubbleShowTime > 0f)
        //         {
        //             _randomTextBubbleShowTime -= Time.deltaTime;
        //         }
        //         else
        //         {
        //             _randomTextBubbleShowTime = Random.Range(5f, 10f);
        //             _randomTextBubbleWaitTime = Random.Range(5f, 20f);
        //             textBubble.HideTextBubble(_showText);
        //             _isShowingTextBubble = false;
        //         }
        //     }
        // }
    }
}