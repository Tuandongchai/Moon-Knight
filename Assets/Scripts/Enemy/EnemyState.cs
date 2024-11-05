using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Rigidbody2D rb;

    protected Enemy enemyBase;
    protected EnemyStateMachine stateMachine;

    protected string animBoolName;

    protected bool triggerCalled;
    protected float stateTimer;

    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }
    public virtual void Enter()
    {
        enemyBase.anim.SetBool(animBoolName, true);
        triggerCalled = false;
        rb=enemyBase.rb;
    }
    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);
        enemyBase.AssigneLastAnimName(animBoolName);
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }
    public virtual void AnimationFinishTrigger()
    {

        triggerCalled = true;
    }
}
