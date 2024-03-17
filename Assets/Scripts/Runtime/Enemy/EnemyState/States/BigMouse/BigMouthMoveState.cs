using UnityEngine;
public class BigMouthMoveState : BigMouthNormalState
{
    private Vector2 _moveDir;
    public BigMouthMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, Enemy_BigMouse enemy) : base(enemyBase, stateMachine, enemy)
    {
    }
    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }

    public override void EntryState()
    {
        base.EntryState();
        _enemy.animator.SetBool("running", true);
    }

    public override void ExitState()
    {
        base.ExitState();
        _enemy.animator.SetBool("running", false);
        enemyBase.movement.StopMovePerform();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        _moveDir = ((Vector2)(GameManager.playerTans.position - _enemy.transform.position)).normalized;
        if (GameManager.playerTans == null)
        {
            stateMachine.ChangeState(_enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemyBase.movement.MovePerform(_moveDir);
    }
}
