using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProductionBuildingAsset:ScriptableObject
{
    [SerializeField] private List<CoreBuildingTypes> validInputs;
    [SerializeField] private List<CoreBuildingTypes> validOutputs;
    [SerializeField] private List<ProductionBuilding> productionBuildingLevels;

    public List<CoreBuildingTypes> ValidInputs => validInputs;
    public List<CoreBuildingTypes> ValidOutputs => validOutputs;
    
    public ProductionBuilding GetLevelDetails(int level)
    {
        return productionBuildingLevels[level-1];
    }
}