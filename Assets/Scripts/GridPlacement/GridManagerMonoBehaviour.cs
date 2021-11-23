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

    // public void RotateBuilding()
    // {
    //    SimpleBuilding buildingToPlace = BuildingToPlace.GetComponent<SimpleBuilding>();
    //    Quaternion buildingRotation = BuildingToPlace.transform.rotation;
    //    Vector3 finalRotation = new Vector3(buildingRotation.x, Convert.ToInt32(buildingToPlace.isVertical) * 90, buildingRotation.z);
    //    buildingToPlace.isVertical = !buildingToPlace.isVertical;
    //    ClearPreviousPositions(buildingToPlace.SimpleBuildingType);
    //    
    //    if (ValidateGridCells(buildingToPlace.placedPosition, buildingToPlace.dimension))
    //    {
    //        buildingRotation.eulerAngles = finalRotation;
    //        BuildingToPlace.transform.rotation = buildingRotation;
    //        UpdateGrid(buildingToPlace.placedPosition);
    //    }
    //    else
    //    {
    //        buildingToPlace.isVertical = !buildingToPlace.isVertical;
    //        buildingToPlace.FlashColor(Color.red);
    //        UpdateGrid(buildingToPlace.placedPosition);
    //    }
    //    
    // }

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
        GameObject[,] tiles = new GameObject[Width,Height];
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                Vector3 instantiatingPosition = new Vector3(Width * CellSize * i, 0f, Height * CellSize * j);
                tiles[i,j] = Instantiate(tile,instantiatingPosition, Quaternion.identity, transform);
                cells[i,j] = tiles[i, j].GetComponent<Cell>();
                cells[i,j].CellCoords = new Vector2Int(i, j);
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
