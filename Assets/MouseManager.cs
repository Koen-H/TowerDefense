using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    MouseStatus mouseStatus = MouseStatus.Idle;
    [SerializeField] PlaceTowerManager placeTowerManager;

    // Start is called before the first frame update
    void Start()
    {
        BuyCardManager.OnTowerBuy += OnTowerBuy;

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