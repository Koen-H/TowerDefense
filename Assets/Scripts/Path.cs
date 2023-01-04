using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Path : MonoBehaviour
{
    public bool isStart = false;
    public bool isEnd = false;
    public int pathNodeId;
    [SerializeField] PathGenerator pathGenerator;
    LayerMask _layer;

    private void Awake()
    {
        this.gameObject.name = UnityEngine.Random.Range(0, 3000).ToString();
    }


    void Start()
    {
        pathGenerator = GameManager.Instance.pathGenerator;
        pathGenerator.AddPathNode(this);
        _layer = LayerMask.GetMask("Path");
    }

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
