using UnityEngine;
using ZiercCode.Test.StateSystem;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    public class BigMouseEnemyState
    {
        public class Idle : EnemyState
        {
            public Idle(Enemy enemyBase, StateMachine stateMachine, Animator animator) : base(enemyBase, stateMachine, animator)
            {
            }
        }

        public class Chase : EnemyState
        {
            public Chase(Enemy enemyBase, StateMachine stateMachine, Animator animator) : base(enemyBase, stateMachine, animator)
            {
            }
        } 
    }
}