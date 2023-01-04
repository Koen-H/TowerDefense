using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    [SerializeField] protected bool moving;
    [SerializeField] protected float speed;
    [SerializeField] protected float rotSpeed;
    [SerializeField] protected int targetNode = 0;
    [SerializeField] protected Vector2 targetNodePos;
    protected List<Path> path;
    

    public int GetTargetNode()
    {
        return targetNode;
    }
}
