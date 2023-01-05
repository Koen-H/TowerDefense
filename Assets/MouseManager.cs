using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    MouseStatus mouseStatus = MouseStatus.Idle;
    public PlaceTowerManager placeTowerManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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