using UnityEngine;
using UnityEngine.Events;

public class Cell
{
    public CoreBuildingTypes CellType = CoreBuildingTypes.Empty;
    private Transform building = null;
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
