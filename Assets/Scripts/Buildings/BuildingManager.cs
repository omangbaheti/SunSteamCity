using UnityEngine;

public class BuildingManager:MonoBehaviour
{
    [Range(1,5)]
    [SerializeField] private int levelCheck;
    [SerializeField] private SourceBuildingAsset farmBoye;
    
    private void Awake()
    {
        SourceBuilding farmBoyeLevel1=farmBoye.GetLevelDetails(levelCheck);
        float costOfFarmLevel1 = farmBoyeLevel1.Cost;
        Debug.Log(costOfFarmLevel1);
    }
}
