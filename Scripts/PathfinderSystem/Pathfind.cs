using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfind : MonoBehaviour
{
    [SerializeField] Vector2Int start;
    public Vector2Int StartCoordinate{ get{ return start; }}
    [SerializeField] Vector2Int destination;
    public Vector2Int DestinationCoordinate{ get{ return destination; }}
    Node startNode;
    Node destinationNode;
    Node currentNode;

    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    GridManager gridManage;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    Dictionary<Vector2Int, Node> explored = new Dictionary<Vector2Int, Node>(); 
    Queue<Node> frontier = new Queue<Node>();
    void Awake()
    {
        gridManage = FindObjectOfType<GridManager>();
        if(gridManage != null){
            grid = gridManage.Grid;
            startNode = grid[start];
            destinationNode = grid[destination];
        }
    }
    void Start()
    {
        GetPath();
    }

    public List<Node> GetPath()
    {
        return GetPath(StartCoordinate);
    }

    public List<Node> GetPath(Vector2Int coordinate)
    {
        gridManage.ResetNode();
        BFS(coordinate); //breadth first search and create path tree
        return CreatePath();    
    }

    void ProcessNeighbors()
    {
        List<Node> neighborList = new List<Node>();
        
        foreach(Vector2Int direction in directions){
            Vector2Int neighbors = currentNode.coordinates + direction;
            
            if(grid.ContainsKey(neighbors)){
                neighborList.Add(grid[neighbors]);
            }
        }
        foreach(Node neighbor in neighborList){
            if(!explored.ContainsKey(neighbor.coordinates) && neighbor.nodeAvailable){
                neighbor.connection = currentNode;
                explored.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }    
    }

    void BFS(Vector2Int coordinate)
    {
        startNode.nodeAvailable = true;
        destinationNode.nodeAvailable = true;

        frontier.Clear();
        explored.Clear();

        bool running = true;
        frontier.Enqueue(grid[coordinate]);
        explored.Add(coordinate, grid[coordinate]);

        while(frontier.Count > 0 && running){
            currentNode = frontier.Dequeue();
            currentNode.nodeFound = true;
            ProcessNeighbors();
            if(currentNode.coordinates == destination){
                running = false;    
            }
        }
    }

    List<Node> CreatePath()
    {
        List<Node> newPath = new List<Node>();
        Node currentNode = destinationNode;
        newPath.Add(currentNode);
        currentNode.path = true;

        while(currentNode.connection != null){
            currentNode = currentNode.connection;
            newPath.Add(currentNode);
            currentNode.path = true;
        }
        newPath.Reverse();
        return newPath;
    }

    public bool BlockPath(Vector2Int coordinate)
    {
        if(grid.ContainsKey(coordinate)){
            bool check = grid[coordinate].nodeAvailable; //previous state of grid node 
            
            grid[coordinate].nodeAvailable = false;
            List<Node> newPath = GetPath();
            grid[coordinate].nodeAvailable = check;

            if(newPath.Count <= 1){
                GetPath();
                return true;
            }
        }
        return false;
    }

    public void Broadcast()
    {
        BroadcastMessage("RecalculateEnemyPath", false, SendMessageOptions.DontRequireReceiver);
    }
}
