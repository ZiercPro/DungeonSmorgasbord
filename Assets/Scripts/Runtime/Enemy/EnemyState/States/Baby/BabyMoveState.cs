using UnityEngine;

public class BabyMoveState : BabyNormalState
{
    private Vector2 _moveDir;
    public BabyMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, Enemy_Baby enemy) : base(enemyBase, stateMachine, enemy)
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
        _enemy.movement.StopMovePerform();
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
        _enemy.movement.MovePerform(_moveDir);
    }
}
