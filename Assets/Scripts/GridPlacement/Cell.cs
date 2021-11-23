using System;
using System.Collections;
using UnityEngine;

public class Cell: MonoBehaviour
{
    public Vector2Int cellCoordinates;
    public Vector2Int CellCoords { get => cellCoordinates;
        set { cellCoordinates = value; }
    }
    public SimpleBuildingType buildingOccupancy;
    
    private GridManagerMonoBehaviour gridManager;
    private bool placed;


    public void SetBuilding(GameObject building)
    {
        Instantiate(building, transform.position, Quaternion.identity, gridManager.transform);
        buildingOccupancy = building.GetComponent<SimpleBuilding>().SimpleBuildingType;
    }
    private void Awake()
    {
        gridManager = GetComponentInParent<GridManagerMonoBehaviour>();
        buildingOccupancy = SimpleBuildingType.NULL;
        
    }

    private void OnMouseDown()
    {
        gridManager.UpdateGrid(CellCoords);
    }


}
