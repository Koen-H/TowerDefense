using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BasicState 
{
    public void EnterState(StateMachine _stateMachine);
    public void MouseBehaviour();
    public void ExitState(StateMachine _stateMachine);
    //public virtual void EnterState(StateMachine mouseManager)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public virtual void ExitState(StateMachine mouseManager)
    //{
    //    throw new System.NotImplementedException();
    //}
}
