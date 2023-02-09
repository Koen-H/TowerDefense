using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectTowerManager : MonoBehaviour
{
    LayerMask towerLayerMask;
    float range = 15;
    [SerializeField] InspectCardManager inspectCard;
    private void Start()
    {
        towerLayerMask = LayerMask.GetMask("Tower");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), range, towerLayerMask);
            if (rayHit)
            {
                inspectCard.OnTowerSelected(rayHit.transform.gameObject.GetComponent<Tower>());

            }

        }
    }
}
