using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SourceBuildingAsset:ScriptableObject
{
    [SerializeField] private SourceBuildingTypes sourceBuildingType;
    [SerializeField] private List<CoreBuildingTypes> validInputs;
    [SerializeField] private List<CoreBuildingTypes> validOutputs;
    [SerializeField] private List<SourceBuilding> sourceBuildingLevels;

    public List<CoreBuildingTypes> ValidInputs => validInputs;
    public List<CoreBuildingTypes> ValidOutputs => validOutputs;
    public SourceBuildingTypes SourceBuildingType => sourceBuildingType;
    
    public SourceBuilding GetLevelDetails(int level)
    {
        return sourceBuildingLevels[level-1];
    }
}