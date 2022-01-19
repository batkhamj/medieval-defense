using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int coordinates;
    public bool nodeAvailable; //check if node can be added to tree 
    public bool nodeFound;
    public bool path;
    public Node connection; //node that current node is connected to

    public Node(Vector2Int coordinates, bool nodeAvailable)
    {
        this.nodeAvailable = nodeAvailable;
        this.coordinates = coordinates;
    }
}
