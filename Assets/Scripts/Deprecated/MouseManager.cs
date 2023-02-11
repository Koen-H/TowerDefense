using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The mouse manager is used for creating a good QOL interaction with the ui, such as clicking next to the canvas will make it close.
/// It's made with the SFM pattern.
/// </summary>
public class MouseManager : StateMachine
{
    private static MouseManager _instance;
    //MouseStatus mouseStatus = MouseStatus.Idle;
    //[SerializeField] PlaceTowerManager placeTowerManager;
    [SerializeField] public GameObject shopUI;
    [SerializeField] public GameObject inspectCardUI;


    //[HideInInspector] public ShoppingMouseState shoppingMouseState;
   // [HideInInspector] public IdleMouseState idleMouseState;


    /// <summary>
    /// Gets the singleton GameManager
    /// </summary>
    public static MouseManager Instance
    {
        get
        {
            if (_instance == null) Debug.LogError("MouseManager is null");
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        //idleMouseState = new IdleMouseState();
       // shoppingMouseState = new ShoppingMouseState();
    }

    private void Start()
    {
        currentState = GetInitialState();
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
           // ChangeMouseState(idleMouseState);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
          //  ChangeMouseState(shoppingMouseState);
        }
    }


    protected override BasicState GetInitialState()
    {
        return null;
        //return idleMouseState;
    }

    public void ChangeMouseState(BasicState newMouseState)
    {
        currentState.ExitState(this);
        currentState = newMouseState;
        currentState.EnterState(this);
    }

    //public void SetMouseStatus(MouseStatus newStatus)
    //{
    //    mouseStatus = newStatus;
    //}
}

public enum MouseStatus
{
    Idle,
    Placing,
    Shopping,
    Inspecting,
}