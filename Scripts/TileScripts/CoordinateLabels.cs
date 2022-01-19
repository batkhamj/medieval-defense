using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabels : MonoBehaviour
{
    [SerializeField] Color availableCoordinateColor = Color.white;
    [SerializeField] Color takenCoordinateColor = Color.gray;
    [SerializeField] Color foundCoordinateColor = Color.yellow;
    [SerializeField] Color pathCoordinateColor = new Color(1f, 0.5f, 0f);
    TextMeshPro coordinate; 
    Vector2Int position = new Vector2Int();
    GridManager gridManage;
    void Awake() 
    {
        gridManage = FindObjectOfType<GridManager>();
        coordinate = GetComponent<TextMeshPro>();
        coordinate.enabled = false;
        ShowCoordinates();
    }
    
    void Update()
    {
        if(!Application.isPlaying)
        {
            ShowCoordinates();
            UpdateObjectName();
            coordinate.enabled = true;
        }

        ProcessCoordinate();
        ToggleCoordinates();
    }

    void ShowCoordinates()
    {
        if(gridManage == null){ return; }
        position.x = Mathf.RoundToInt(transform.parent.position.x / gridManage.UnitySize);
        position.y = Mathf.RoundToInt(transform.parent.position.z / gridManage.UnitySize);
        coordinate.text = position.x + "," + position.y;    
    }

    void UpdateObjectName()
    {
        transform.parent.name = position.ToString();
    }

    void ProcessCoordinate()
    {
        if(gridManage == null){ return; }
        Node node = gridManage.GetNode(position);    
         
        if(node == null){ return; }
        //change coordinate color depending on wether tile is available or not; 
        if(!node.nodeAvailable){
            coordinate.color = takenCoordinateColor;
        }
        else if(node.path){
            coordinate.color = pathCoordinateColor;
        }
        else if(node.nodeFound){
            coordinate.color = foundCoordinateColor;
        }
        else{
            coordinate.color = availableCoordinateColor;
        }
    }

    void ToggleCoordinates()
    {
        if(Input.GetKeyDown(KeyCode.N)){
            coordinate.enabled = !coordinate.IsActive();
        }
    }
    
}
