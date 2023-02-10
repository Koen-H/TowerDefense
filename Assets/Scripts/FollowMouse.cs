using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the gameobject follow the mouse
/// </summary>
public class FollowMouse : MonoBehaviour
{

    
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x,mousePosition.y,0);
            
    }
}
