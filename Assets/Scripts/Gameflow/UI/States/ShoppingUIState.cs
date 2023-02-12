using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Opens and closes the shop
/// </summary>
public class ShoppingUIState :  BasicState
{
    UIManager UIManager;

    public ShoppingUIState(UIManager _UIManager)
    {
        UIManager = _UIManager;
    }


    public void EnterState()
    {
        UIManager.shopUI.SetActive(true);
        Debug.Log("entering shopping state");
    }

    /// <summary>
    /// Closes the shop, if the click isn't in the shop
    /// </summary>
    public void MouseBehaviour()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            if(results.Count <= 0)//If it doesn't hit UI
            {
                UIManager.Instance.ChangeState(UIManager.Instance.idleState);
            }
        }

    }
    public void ExitState()
    {
        UIManager.shopUI.SetActive(false);
        Debug.Log("exit shopping state");
    }

    
}
