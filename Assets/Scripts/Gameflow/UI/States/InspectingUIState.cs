using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// opens and closes the inspect card
/// </summary>
public class InspectingUIState :  BasicState
{
    InspectCardManager inspectCardManager;

    public InspectingUIState(UIManager uIManager)
    {
        inspectCardManager = uIManager.inspectCardUI.GetComponent<InspectCardManager>();
    }


    public void EnterState()
    {
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
    public void ExitState()
    {
        inspectCardManager.CloseCard();
        Debug.Log("exit inspecting state");
    }

    
}
