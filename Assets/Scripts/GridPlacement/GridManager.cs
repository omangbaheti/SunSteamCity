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
            Vector2Int gridPosition = GetGridPosition(worldPosition);
            if (ValidateGridCells(gridPosition))
            {
                GameObject house = Instantiate(currentBuilding.GetLevelDetails(1).Model, GetWorldPosition(gridPosition), Quaternion.identity);
                List<Vector2Int> occupiedSpaces = currentBuilding.FillGridPositions(gridPosition, currentBuilding);
                foreach (Vector2Int cell in occupiedSpaces)
                {
                    GridArray[cell.x, cell.y].CellType = CoreBuildingTypes.Source;
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
        bool canBuild = true;
        
        for (int x = currentPosition.x; x < currentPosition.x + currentBuilding.Width; x++)
        {
            for (int z = currentPosition.y; z < currentPosition.y + currentBuilding.Length; z++)
            {
                canBuild = GridArray[x, z].CanBuild();
                Vector2Int currentPos = new Vector2Int(x, z);
                Debug.Log($"{canBuild}:{currentPos}");
                Debug.Log($"Target: {currentPosition.x + currentBuilding.Width}, {currentPosition.y + currentBuilding.Length}");
            }
        }
        return canBuild;
    }


}
