using UnityEngine;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    /// <summary>
    /// 攻击状态
    /// </summary>
    public class EnemyAttackStateBase : EnemyState
    {
        private readonly int _attack = Animator.StringToHash("attack");
        
        public EnemyAttackStateBase(Enemy enemyBase, EnemyStateMachine stateMachine, Animator animator) : base(enemyBase, stateMachine, animator)
        {
        }
    }
}