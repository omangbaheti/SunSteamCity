using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : GridProperties
{
    private SourceBuildingAsset currentBuilding;
    [SerializeField] private GameObject housePrefab;
    [SerializeField] private List<SourceBuildingAsset> buildingList;

    public void Start()
    {
        currentBuilding = buildingList[0];
        for (int x = 0; x < GridArray.GetLength(0); x++)
        {
            for (int z = 0; z < GridArray.GetLength(1); z++)
            {
                GridArray[x, z] = new Cell();
            }
        }
    }

    private void Update()
    {   
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = MouseToScreen.GetWorldPosition();
            GetGridPosition(worldPosition, out int x, out int z);
            if (ValidateGridCells(new Vector2Int(x,z)))
            {
                GameObject house = Instantiate(currentBuilding.GetLevelDetails(1).Model, GetWorldPosition(x, z), Quaternion.identity);
                List<Vector2Int> occupiedSpaces = currentBuilding.FillGridPositions(new Vector2Int(x, z));
                foreach (Vector2Int cell in occupiedSpaces)
                {
                    GridArray[cell.x,cell.y].SetBuilding(house.transform);
                }
            }
        }
    }

    private void UpdateBuilding(int index)
    {
        currentBuilding = buildingList[index];
    }
    
    private bool ValidateGridCells(Vector2Int currentPosition)
    {
        bool canBuild = false;
        
        for (int x = 0; x < currentBuilding.Width; x++)
        {
            for (int z = 0; z < currentBuilding.Length; z++)
            {
                canBuild = GridArray[x, z].CanBuild();
            }
        }
        return canBuild;
    }


}
