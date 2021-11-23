using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManagerMonoBehaviour : GridPropertiesMonoBehaviour
{ 
    public GameObject BuildingToPlace = null;
    
    protected Cell[,] cells = new Cell[Width, Height];
    protected GameObject[,] buildingTiles = new GameObject[Width, Height];

    [SerializeField] private GameObject tile;
    [SerializeField] private List<GameObject> buildings;
    public GameObject[,] Tiles { get=> buildingTiles;}
    private EconomyManager economyManager;
    
    
    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        economyManager = FindObjectOfType<EconomyManager>();
    }

    public void Initialize()
    {
        buildingTiles = CreateBoard();
    }

    public void UpdateGrid(Vector2Int coordinates)
    {
       if(BuildingToPlace == null)
           return;
       SimpleBuilding buildingToPlace = BuildingToPlace.GetComponent<SimpleBuilding>();
       Vector2Int dimension = SimpleBuilding.buildingDimensions[buildingToPlace.SimpleBuildingType]; 
       
       if((economyManager.SteamCoin < (ulong)buildingToPlace.Price))
           return;
       ClearPreviousPositions(buildingToPlace.SimpleBuildingType);
       
       if (ValidateGridCells(coordinates, dimension))
       {
           for (int i = 0; i < dimension.x; i++)
           {
               for (int j = 0; j < dimension.y; j++)
               {
                   cells[coordinates.x + i , coordinates.y + j ].buildingOccupancy = buildingToPlace.SimpleBuildingType;
               }
           }
           
           cells[coordinates.x, coordinates.y].SetBuilding(BuildingToPlace);
           buildingToPlace.placedPosition = coordinates;
           economyManager.AddToIncrementValue(buildingToPlace.ProductionRate);
           economyManager.DeductBalance(buildingToPlace.Price);
       }
    }

    public void AssignBuildingToBePlaced(int BuildingIndex)
    {
        BuildingToPlace = buildings[BuildingIndex];
    }

    protected GameObject[,] CreateBoard()
    {
        int counter = 0;
        GameObject[,] tiles = new GameObject[Width,Height];
        Cell[] childrenCell = GetComponentsInChildren<Cell>();
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                cells[i, j] = childrenCell[counter++];
                cells[i,j].CellCoords = new Vector2Int(i, j);
                tiles[i, j] = cells[i, j].gameObject;
            }
        }
        return tiles;
    }
    
    protected bool ValidateGridCells(Vector2Int currentPosition, Vector2Int dimension)
    {
        for (int i = 0; i < dimension.x; i++)
        {
            for (int j = 0; j < dimension.y; j++)
            {
                if (cells[currentPosition.x + i, currentPosition.y + j].buildingOccupancy != SimpleBuildingType.NULL)
                    return false;
            }
        }
        return true;
    }

    protected void ClearPreviousPositions(SimpleBuildingType buildingType)
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if (cells[i, j].buildingOccupancy == buildingType)
                {
                    cells[i, j].buildingOccupancy = SimpleBuildingType.NULL;
                }
            }
        }
    }
    
    
}
