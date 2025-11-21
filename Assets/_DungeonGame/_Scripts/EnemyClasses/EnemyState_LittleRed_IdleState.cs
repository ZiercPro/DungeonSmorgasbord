using UnityEngine;
using ZiercCode._DungeonGame._Scripts.AttackSystem;

namespace ZiercCode._DungeonGame._Scripts.EnemyClasses
{
    /// <summary>
    /// 小红初始状态
    /// </summary>
    public class EnemyState_LittleRed_IdleState : EnemyState_Base
    {
        public EnemyState_LittleRed_IdleState(Animator myAnimator) : base(myAnimator)
        {
        }

        public override void OnEnter()
        {
            Animator.Play("littleRed_idle"); //播放idle动画

            //重置寻路状态
            MyEnemy.navMeshAgent.velocity = Vector3.zero;
            MyEnemy.navMeshAgent.isStopped = true;
        }

        public override void OnUpdate()
        {
            //检测是否有目标进入追击范围
            if (RangeDetector.DetectInCircleByLayer(MyEnemy.targetFaction, MyEnemy.transform.position,
                    ((Enemy_LittleRed)MyEnemy).chaseRange.x))
            {
                Collider2D[] t = RangeDetector.GetColliders();
                for (int i = 0; i < t.Length; i++)
                {
                    if (t[i] && t[i].TryGetComponent(out IAttackAble attackAble))
                    {
                        //设置目标，切换到追击状态
                        MyEnemy.attackTarget = attackAble;
                        stateMachine.ChangeState<EnemyState_LittleRed_ChaseState>();
                    }
                }
            }
        }

        public override void OnExit()
        {
        }
    }
}