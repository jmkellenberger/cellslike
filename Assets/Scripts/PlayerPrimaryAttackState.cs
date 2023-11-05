using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{

    private int comboCounter;
    private float lastAttackTime;
    private float comboWindow = .2f;

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (comboCounter > 2 || Time.time >= lastAttackTime + comboWindow)
            comboCounter = 0;

        player.Anim.SetInteger("ComboCounter", comboCounter);

        Vector2 _attackMovement = player.attackMovement[comboCounter];

        player.SetVelocity(_attackMovement.x * player.facingDirection, _attackMovement.y);

        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;
        lastAttackTime = Time.time;
        player.StartCoroutine(player.BusyFor(.15f));
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            player.ZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(player.IdleState);
    }
}
