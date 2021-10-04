using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : GridProperties
{
    private SourceBuildingAsset currentBuilding;
    [SerializeField] private GameObject housePrefab;
    [SerializeField] private EconomyManager ecoManager;

    public void Start()
    {
        ecoManager.SetIncrementValue(0);
        for (int x = 0; x < GridArray.GetLength(0); x++)
        {
            for (int z  = 0; z < GridArray.GetLength(1); z++)
            {
                GridArray[x, z] = new Cell();
            }
        }
        InputManager.Instance.CreateBuilding.AddListener(OnGridUpdate);
        InputManager.Instance.ChangeBuilding.AddListener(UpdateBuilding);
        InputManager.Instance.DestroyBuilding.AddListener(DestroyBuilding);
    }
    
    private void OnGridUpdate()
    {
        if(currentBuilding == null) return;
        
        Vector3 worldPosition = MouseToScreen.GetWorldPosition();
        Vector2Int gridPosition = GetGridPosition(worldPosition);
        if(!ValidateGridCells(gridPosition)) return;
        
        GameObject placedBuilding = Instantiate(currentBuilding.GetLevelDetails(1).Model, GetWorldPosition(gridPosition), Quaternion.identity);
        ecoManager.AddToIncrementValue(currentBuilding.GetLevelDetails(1).ProduceValue);
        List<Vector2Int> occupiedSpaces = currentBuilding.FillGridPositions(gridPosition, currentBuilding);
        foreach (Vector2Int cell in occupiedSpaces)
        {
            if (cell.x < 0 || cell.x >= Width || cell.y < 0 || cell.y >= Height) continue;
            GridArray[cell.x, cell.y].CellType = CoreBuildingTypes.Source;
            GridArray[cell.x,cell.y].SetBuilding(placedBuilding.transform, currentBuilding.GetLevelDetails(1).ProduceValue);
        } 
    }

    public void UpdateBuilding(SourceBuildingAsset building)
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
                if (!canBuild || x < 0|| x >= Width || z < 0 || z >= Height) return false;
                canBuild = GridArray[x, z].CanBuild();
                Vector2Int currentPos = new Vector2Int(x, z);
            }
        }
        return canBuild;
    }

    private void DestroyBuilding()
    {
        Vector3 worldPosition = MouseToScreen.GetWorldPosition();
        Vector2Int gridPosition = GetGridPosition(worldPosition);
        if(ValidateGridCells(gridPosition)) return;
        
        List<Vector2Int> occupiedSpaces = currentBuilding.FillGridPositions(gridPosition, currentBuilding);
        Debug.Log($"Destroying:{GridArray[gridPosition.x, gridPosition.y].Building.gameObject}");
        ecoManager.SubtractFromIncrementValue(GridArray[gridPosition.x, gridPosition.y].ProductionValue);
        Destroy(GridArray[gridPosition.x, gridPosition.y].Building.gameObject);
        foreach (Vector2Int cell in occupiedSpaces)
        {
            GridArray[cell.x, cell.y].CellType = CoreBuildingTypes.Empty;
            GridArray[cell.x,cell.y].DestroyBuilding();
        } 
    }
    private void OnDisable()
    {
        InputManager.Instance.CreateBuilding.RemoveListener(OnGridUpdate);
        InputManager.Instance.ChangeBuilding.RemoveListener(UpdateBuilding);
        InputManager.Instance.DestroyBuilding.RemoveListener(DestroyBuilding);
    }
}
