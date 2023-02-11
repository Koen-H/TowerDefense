using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected BasicState currentState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected virtual BasicState GetInitialState()
    {
        return null;
    }
}
