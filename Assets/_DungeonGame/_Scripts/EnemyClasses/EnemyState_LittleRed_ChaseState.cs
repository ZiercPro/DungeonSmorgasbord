using UnityEngine;
using ZiercCode.Utilities;

namespace ZiercCode._DungeonGame._Scripts.EnemyClasses
{
    /// <summary>
    /// 小红追击状态
    /// </summary>
    public class EnemyState_LittleRed_ChaseState : EnemyState_Base
    {
        public EnemyState_LittleRed_ChaseState(Animator myAnimator) : base(myAnimator)
        {
        }

        public override void OnEnter()
        {
            Animator.Play("littleRed_move");

            //设置寻路状态
            MyEnemy.navMeshAgent.isStopped = false;
        }

        public override void OnUpdate()
        {
            //目标存活，阵营敌对并在追击的最大范围内
            if (MyEnemy.attackTarget != null &&
                MyMath.CompareDistanceWithRange(
                    MyEnemy.attackTarget.Transform.position,
                    MyEnemy.Transform.position, ((Enemy_LittleRed)MyEnemy).chaseRange.y))
            {
                MyEnemy.navMeshAgent.destination = MyEnemy.attackTarget.Transform.position;
            }
            else
            {
                MyEnemy.attackTarget = null; //重置目标
                stateMachine.ChangeState<EnemyState_LittleRed_IdleState>();
            }
        }

        public override void OnExit()
        {
        }
    }
}