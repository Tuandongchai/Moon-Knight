using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        /*stateTimer = 1f;*/
        if(xInput*player.facingDir>0)
            player.SetVelocity(2 * -player.facingDir, player.wallJumpForce);
        else
            player.SetVelocity(6*-player.facingDir, player.wallJumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        /*if(stateTimer < 0)
        {
            stateMachine.ChangeState(player.airState);
        }*/
        if (rb.velocity.y<0)
        {
            stateMachine.ChangeState(player.airState);
        }
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);

    }
}
