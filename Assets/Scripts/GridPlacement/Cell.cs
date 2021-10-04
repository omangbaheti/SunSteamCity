using UnityEngine;
using UnityEngine.Events;

public class Cell
{
    public CoreBuildingTypes CellType = CoreBuildingTypes.Empty;
    private int productionValue = 0;

    public int ProductionValue => productionValue;

    private Transform building = null;

    public Transform Building { get => building; }
    public void SetBuilding(Transform transform, int prodValue)
    {
        building = transform;
        if(CellType == CoreBuildingTypes.Source) productionValue=prodValue;
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
