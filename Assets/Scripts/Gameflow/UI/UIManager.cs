using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The UI manager is used for creating a good QOL HUD. It makes sure all the other ui is closed to keep the screen as clean as possible.
/// Different states are being used, in the idle state it looks for tower clicks.
/// In the shopping state it looks for non-ui clicks to close the shop.
/// In the inspecting state it does both.
/// It's made with the SFM pattern.
/// The states are made with interfaces using the strategy pattern.
/// The UI manager is a singleton as UI can also be disabled by buying towers.
/// </summary>
public class UIManager : StateMachine
{
    private static UIManager _instance;
    [Header("UI elements to be toggled")]
    [SerializeField] public GameObject shopUI;
    [SerializeField] public GameObject inspectCardUI;

    [Header("UI buttons that should have listeners")]
    [SerializeField] GameObject openShopButton;
    [SerializeField] GameObject closeShopButton;
    [SerializeField] GameObject closeInspectCardButton;


    [HideInInspector] public IdleUIState idleState;
    [HideInInspector] public ShoppingUIState shoppingState;
    [HideInInspector] public InspectingUIState inspectingState;


    /// <summary>
    /// Gets the singleton GameManager
    /// </summary>
    public static UIManager Instance
    {
        get
        {
            if (_instance == null) Debug.LogError("UIManager is null");
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

        if (shopUI == null) Debug.LogError("shopUI is null");
        if (inspectCardUI == null) Debug.LogError("inspectCardUI is null");

        if (openShopButton == null) Debug.LogError("openShopButton is null");
        else openShopButton.GetComponent<Button>().onClick.AddListener(() =>ChangeState(shoppingState));
        if (closeShopButton == null) Debug.LogError("openShopButton is null");
        else closeShopButton.GetComponent<Button>().onClick.AddListener(() => ChangeState(idleState));
        if (closeInspectCardButton == null) Debug.LogError("closeInspectCardButton is null");
        else closeInspectCardButton.GetComponent<Button>().onClick.AddListener(() => ChangeState(idleState));

        idleState = new IdleUIState(this);
        shoppingState = new ShoppingUIState(this);
        inspectingState = new InspectingUIState(this);
        //In theory, there couldb e a building state, but this one is the same as the idle state so it has no real purpose.
    }

    private void Start()
    {
        currentState = GetInitialState();

    }

    private void Update()
    {
        currentState.MouseBehaviour();
    }

    protected override BasicState GetInitialState()
    {
        return idleState;
    }

    public void ChangeState(BasicState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
}
