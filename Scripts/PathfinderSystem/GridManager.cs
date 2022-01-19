using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int size; //size of grid
    [SerializeField] int unitySize = 10; //unity snap settings size
    public int UnitySize{ get{ return unitySize; }} 
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get{ return grid; }}
    
    void Awake() 
    {
        //populate grid dictionary
        PopulateGrid();       
    }

    public Node GetNode(Vector2Int coordinate)
    {
        if(grid.ContainsKey(coordinate)){
            return grid[coordinate];
        }
        return null;
    }
    
    public void BlockNode(Vector2Int coordinate)
    {
        if(grid.ContainsKey(coordinate)){
            grid[coordinate].nodeAvailable = false;
        }
    }
    
    public void ResetNode()
    {
        foreach(KeyValuePair<Vector2Int, Node> entry in grid){
            entry.Value.connection = null;
            entry.Value.nodeFound = false;
            entry.Value.path = false;
        }
    }

    public Vector2Int CoordinateFromPosition(Vector3 position)
    {
        Vector2Int coordinate = new Vector2Int();
        coordinate.x = Mathf.RoundToInt(position.x / unitySize);
        coordinate.y = Mathf.RoundToInt(position.z / unitySize);
        return coordinate;
    }
    
    public Vector3 PositionFromCoordinate(Vector2Int coordinate)
    {
        Vector3 position = new Vector3();
        position.x = coordinate.x * unitySize;
        position.z = coordinate.y * unitySize;
        return position;
    }

    void PopulateGrid()
    {
        for(int x = 0; x < size.x; x++){
            for(int y = 0; y < size.y; y++){
                Vector2Int coordinate = new Vector2Int(x,y);
                grid.Add(coordinate, new Node(coordinate, true));
            }
        }
    }
}
