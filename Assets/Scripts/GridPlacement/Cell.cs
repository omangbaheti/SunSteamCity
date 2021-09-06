using UnityEngine;

public class Cell
{
    
    private Transform building = null;
    public CoreBuildingTypes CellType { get; set; } = CoreBuildingTypes.Empty;
    public void SetBuilding(Transform transform)
    {
        building = transform;
    }
    
    public void DestroyBuilding()
    {
        building = null;
    }

    public bool CanBuild()
    {
        return CellType == CoreBuildingTypes.Empty;
    }

}
