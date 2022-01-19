using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] bool tileAvailable;
    public bool TileAvailable { get { return tileAvailable; } }
    [SerializeField] Tower tower; 
    Pathfind pathfind;
    GridManager gridManager;
    Vector2Int coordinate = new Vector2Int();

    void Awake() 
    {
        pathfind = FindObjectOfType<Pathfind>();
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager != null){
            coordinate = gridManager.CoordinateFromPosition(transform.position);

            if(!tileAvailable){
                gridManager.BlockNode(coordinate);    
            }
        }   
    }

    void OnMouseDown() 
    {
        if(!pathfind.BlockPath(coordinate) && gridManager.GetNode(coordinate).nodeAvailable)
        {
            bool checkIfActive = tower.BuildTower(tower,transform.position);
            if(checkIfActive == true){
                gridManager.BlockNode(coordinate);
                pathfind.Broadcast();
            }
        }
    }
}
