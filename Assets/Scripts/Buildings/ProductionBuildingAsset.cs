using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProductionBuildingAsset : BuildingAssetTemplate
{
    [SerializeField] private List<ProductionBuilding> productionBuildingLevels;
    
    public ProductionBuilding GetLevelDetails(int level)
    {
        return productionBuildingLevels[level-1];
    }
}
