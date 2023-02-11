using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// In the idle states it tries to go in to the inspecting state.
/// </summary>
public class IdleUIState :  BasicState
{
    UIManager uiManager;

    public IdleUIState(UIManager _uiManager)
    {
        uiManager = _uiManager;
    }
    public  void EnterState()
    {
        Debug.Log("entering idle state");
    }

    /// <summary>
    /// Tries to hit a tower to inspect
    /// </summary>
    public void MouseBehaviour()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), 15, 1 << 6);
            if (rayHit)
            {
                uiManager.inspectCardUI.GetComponent<InspectCardManager>().OnTowerSelected(rayHit.transform.gameObject.GetComponent<Tower>());
                uiManager.ChangeState(uiManager.inspectingState);
            }

        }
    }
    public  void ExitState()
    {
        Debug.Log("exit idle state");
    }

    
}
