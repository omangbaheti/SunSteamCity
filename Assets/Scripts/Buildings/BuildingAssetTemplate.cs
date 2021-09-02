﻿using System.Collections.Generic;
using UnityEngine;

public class BuildingAssetTemplate : ScriptableObject
{
    [SerializeField] private List<CoreBuildingTypes> validInputs;
    [SerializeField] private List<CoreBuildingTypes> validOutputs;
    [SerializeField] private int width;
    [SerializeField] private int length;
    
    public List<CoreBuildingTypes> ValidInputs => validInputs;
    public List<CoreBuildingTypes> ValidOutputs => validOutputs;
    public int Length => length;
    public int Width => width;
    
    public List<Vector2Int> FillGridPositions(Vector2Int offset)
    {
        List<Vector2Int> occupiedGridPositions = new List<Vector2Int>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                occupiedGridPositions.Add(offset + new Vector2Int(x, y));
            }
        }

        return occupiedGridPositions;
    }
    
}
