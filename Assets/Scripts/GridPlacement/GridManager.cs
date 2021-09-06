using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : GridProperties
{
    private BuildingAssetTemplate currentBuilding;
    [SerializeField] private GameObject housePrefab;

    public void Start()
    {
        for (int x = 0; x < GridArray.GetLength(0); x++)
        {
            for (int z = 0; z < GridArray.GetLength(1); z++)
            {
                GridArray[x, z] = new Cell();
            }
        }
        InputManager.Instance.CreateBuilding.AddListener(OnGridUpdoot);
    }
    
    private void OnGridUpdoot()
    {
        Vector3 worldPosition = MouseToScreen.GetWorldPosition();
        Vector2Int gridPosition = GetGridPosition(worldPosition);
        if(!ValidateGridCells(gridPosition)) return;
        GameObject house = Instantiate(currentBuilding.GetLevelDetails(1).Model, GetWorldPosition(gridPosition), Quaternion.identity);
        List<Vector2Int> occupiedSpaces = currentBuilding.FillGridPositions(gridPosition, currentBuilding);
        foreach (Vector2Int cell in occupiedSpaces)
        {
            GridArray[cell.x, cell.y].CellType = CoreBuildingTypes.Source;
            GridArray[cell.x,cell.y].SetBuilding(house.transform);
        } 
    }

    public void UpdateBuilding(BuildingAssetTemplate building)
    {
        currentBuilding = building;
    }
    
    private bool ValidateGridCells(Vector2Int currentPosition)
    {
        bool canBuild = true;
        
        for (int x = currentPosition.x; x < currentPosition.x + currentBuilding.Width; x++)
        {
            for (int z = currentPosition.y; z < currentPosition.y + currentBuilding.Length; z++)
            {
                if(!canBuild) break;
                canBuild = GridArray[x, z].CanBuild();
                Vector2Int currentPos = new Vector2Int(x, z);
            }
        }
        return canBuild;
    }

    private void OnDisable()
    {
        InputManager.Instance.CreateBuilding.RemoveListener(OnGridUpdoot);
    }
}
