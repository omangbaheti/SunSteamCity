﻿using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class GridProperties : MonoBehaviour
{
    private static int width = 5;
    private static int height = 5;
    private static int cellSize = 25;
    private Cell[,] gridArray = new Cell[width, height];

    public static UnityEvent OnBuildingChanged;
    public static int Width { get => width; }
    public static int Height { get => height; }
    public static int CellSize { get => cellSize; }
    public Cell[,] GridArray { 
        get => gridArray;
        set => gridArray = value;
    }
    
    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x,0,z) * cellSize;
    }

    public void GetGridPosition(Vector3 worldPosition, out int x, out int z)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        z = Mathf.FloorToInt(worldPosition.z / cellSize);
    }
}


