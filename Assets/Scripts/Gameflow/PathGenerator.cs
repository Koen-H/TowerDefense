using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// The pathgenerator generates a path by creating a sorted list out of nodes (path.cs)
/// </summary>
public class PathGenerator : MonoBehaviour
{
    private List<Path> pathNodes = new List<Path>();
    private List<Path> path = new List<Path>();

    public Path startNode;
    public Path endNode;
    private static PathGenerator _instance;

    public static PathGenerator Instance
    {
        get
        {
            if (_instance == null) Debug.LogError("Pathgenerator is null");
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this;
    }



    public void AddPathNode(Path node)
    {
        pathNodes.Add(node);
        if (node.isStart) startNode = node;
        else if (node.isEnd) endNode = node;
    }

    /// <summary>
    /// Generates a path by getting the aligned nodes of each node starting from the first node.
    /// Can't be run in Start or Awake
    /// The tilemap is being generated in Awake and moved on Start (not my fault)
    /// </summary>
    public void GeneratePath()
    {
        //Let all nodes get their alligned nodes. ??? Doesn't work in start, needs to be one frame after start...?
        foreach (Path node in pathNodes)
        {
            node.GetAlignedNodes();
        }
        path.Add(startNode);
        while (path.Count < pathNodes.Count)
        {
            Path lastNode = path.Last();//The last node we checked, or the strtnode if we begin.
            lastNode.pathNodeId = path.Count;
            lastNode.gameObject.name = path.Count.ToString();
            List<Path> alignedNodes = lastNode.GetAlignedNodes();
            if (alignedNodes.Count > 2)
            {
                Debug.LogError("There is an incorrect path defined, make sure there's one tile inbetween paths.");
                break;
            }
            path.Add(path.Contains(alignedNodes[0]) ? alignedNodes[1] : alignedNodes[0]);
        }
        endNode.pathNodeId = path.Count;
        endNode.gameObject.name = path.Count.ToString();
    }

    public List<Path> GetPath() { return path; }
}
