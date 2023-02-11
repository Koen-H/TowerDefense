using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BasicState 
{
    public void EnterState();
    public void MouseBehaviour();
    public void ExitState();
    //public virtual void EnterState(StateMachine mouseManager)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public virtual void ExitState(StateMachine mouseManager)
    //{
    //    throw new System.NotImplementedException();
    //}
}
