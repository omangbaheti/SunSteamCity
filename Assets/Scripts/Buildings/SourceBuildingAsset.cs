using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SourceBuildingAsset : BuildingAssetTemplate
{
    [SerializeField] private SourceBuildingTypes sourceBuildingType;
    [SerializeField] private List<SourceBuilding> sourceBuildingLevels;
    
    public SourceBuildingTypes SourceBuildingType => sourceBuildingType;
    
    public SourceBuilding GetLevelDetails(int level)
    {
        return sourceBuildingLevels[level-1];
    }
}
