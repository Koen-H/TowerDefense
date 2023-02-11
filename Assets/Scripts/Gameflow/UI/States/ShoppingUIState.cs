using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShoppingUIState :  BasicState
{
    UIManager UIManager;
    public void EnterState(StateMachine _UIManager)
    {
        if (UIManager == null) UIManager = (UIManager)_UIManager;
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
    public void ExitState(StateMachine _UIManager)
    {
        if (UIManager == null) UIManager = (UIManager)_UIManager;
        UIManager.shopUI.SetActive(false);
        Debug.Log("exit shopping state");
    }

    
}
