using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentState;

    public virtual void Initialize(EnemyState _stateState)
    {
        currentState = _stateState;
        currentState.Enter();
    }
    public virtual void ChangeState(EnemyState _nextState)
    {
        currentState.Exit();
        currentState = _nextState;
        currentState.Enter();
    }
}
