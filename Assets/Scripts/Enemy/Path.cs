using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A path can be seen as a node, it's on a path gameobject
/// </summary>
public class Path : MonoBehaviour
{
    public bool isStart = false;//The fist node of the path, is off screen
    public bool isFinish = false;//The finish is the gate that the ships enter through, once the ship hits
    public bool isEnd = false;//The last node of the path, is off screen
    public int pathNodeId;
    [SerializeField] PathGenerator pathGenerator;
    LayerMask _layer;

    void Start()
    {
        pathGenerator = GameManager.Instance.pathGenerator;
        pathGenerator.AddPathNode(this);
        _layer = LayerMask.GetMask("Path");
    }

    /// <summary>
    /// Gets the alignednodes next to each other
    /// </summary>
    /// <returns>Returns the two nodes next to it</returns>
    public List<Path> GetAlignedNodes()
    {
        List<Path> alignedNodes = new List<Path>();
        List<Vector2> directions = new List<Vector2>() { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        foreach (Vector2 direction in directions)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1.5f, _layer);
            Debug.DrawRay(transform.position, direction, Color.red, 1.5f);
            if (hit.collider != null)
            {
                alignedNodes.Add(hit.collider.GetComponent<Path>());
                Debug.DrawRay(transform.position, direction, Color.red, 100000);
               // Debug.Log($"{gameObject.name} hit {hit.collider.name} by direction {direction}");
            }
        }

        return alignedNodes;
    }
}
