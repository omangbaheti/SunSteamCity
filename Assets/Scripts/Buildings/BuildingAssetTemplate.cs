﻿using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingAssetTemplate: ScriptableObject
{
    [SerializeField] private List<CoreBuildingTypes> validInputs;
    [SerializeField] private List<CoreBuildingTypes> validOutputs;
    [SerializeField] private int width;
    [SerializeField] private int length;
    
    public List<CoreBuildingTypes> ValidInputs => validInputs;
    public List<CoreBuildingTypes> ValidOutputs => validOutputs;
    public int Length => length;
    public int Width => width;
    
    public List<Vector2Int> FillGridPositions(Vector2Int offset, BuildingAssetTemplate building=null, SimpleBuilding simpleBuilding=null)
    {
        List<Vector2Int> occupiedGridPositions = new List<Vector2Int>();

        for (int x = 0; x < simpleBuilding.Width; x++)
        {
            for (int y = 0; y < simpleBuilding.Length; y++)
            {
                // if (x < 0 || x >= GridProperties.Width || y < 0 || y >= GridProperties.Height) continue;
                occupiedGridPositions.Add(offset + new Vector2Int(x, y));
                Debug.Log($"Filling:{offset + new Vector2Int(x, y)}");
            }
        }

        return occupiedGridPositions;
    }
    
}
