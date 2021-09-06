using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProductionBuildingAsset : BuildingAssetTemplate
{
    [SerializeField] private List<ProductionBuilding> productionBuildingLevels;
    
    public BuildingAssetTemplate GetLevelDetails(int level)
    {
        return productionBuildingLevels[level-1];
    }
}
