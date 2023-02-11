using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InspectingUIState :  BasicState
{
    InspectCardManager inspectCardManager;
    public void EnterState(StateMachine _UIManager)
    {
        if (inspectCardManager == null) inspectCardManager = ((UIManager)_UIManager).inspectCardUI.GetComponent<InspectCardManager>();
        Debug.Log("entering inspecting state");
    }

    public void MouseBehaviour()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), 15, 1 << 6);
            if (rayHit)
            {
                inspectCardManager.OnTowerSelected(rayHit.transform.gameObject.GetComponent<Tower>());
            }
            else
            {
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, results);

                if (results.Count <= 0)//If it doesn't hit UI
                {
                    UIManager.Instance.ChangeState(UIManager.Instance.idleState);
                }
               
            }

        }
    }
    public void ExitState(StateMachine _UIManager)
    {
        inspectCardManager.CloseCard();
        Debug.Log("exit inspecting state");
    }

    
}
