using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.AirState);

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.JumpState);
    }
}

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        if (xInput == 0 || player.IsWallDetected())
            stateMachine.ChangeState(player.IdleState);
    }
}

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = Vector2.zero;
    }

    public override void Update()
    {
        base.Update();

        if (xInput == player.facingDirection && player.IsWallDetected())
            return;

        if (xInput != 0)
            stateMachine.ChangeState(player.MoveState);
    }
}
