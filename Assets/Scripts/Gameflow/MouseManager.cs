using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The mouse manager is used for creating a good QOL within the ui, such as clicking next to the canvas will make it close.
/// NOTE: Mouse manager does not provide any additional value to the project right now besides a reference to the placeTowerManager, it only keeps track of the status for future implementation.
/// </summary>
public class MouseManager : MonoBehaviour
{
    MouseStatus mouseStatus = MouseStatus.Idle;
    [SerializeField] PlaceTowerManager placeTowerManager;

    // Start is called before the first frame update
    void Start()
    {
        BuyCardManager.OnTowerBuy += OnTowerBuy;

    }

    private void OnDestroy()
    {
        BuyCardManager.OnTowerBuy -= OnTowerBuy;
    }

    void OnTowerBuy(TowerSO tower)
    {
        SetMouseStatus(MouseStatus.Placing);
        placeTowerManager.OnTowerBuy(tower);
    }


    public void SetMouseStatus(MouseStatus newStatus)
    {
        mouseStatus = newStatus;
    }
}

public enum MouseStatus
{
    Idle,
    Placing,
    Shopping,
    Inspecting,
}