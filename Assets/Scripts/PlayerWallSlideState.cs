using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.WallJumpState);
            return;
        }


        if ((xInput != 0 && player.facingDirection != xInput) || player.IsGroundDetected())
            stateMachine.ChangeState(player.IdleState);

        if (yInput < 0)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y * .7f);
    }
}

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = .4f;
        player.SetVelocity(5 * -player.facingDirection, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(player.AirState);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.IdleState);
    }
}

