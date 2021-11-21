using UnityEngine;
using UnityEngine.Events;

public class Cell
{
    public SimpleBuildingType CellType = SimpleBuildingType.Empty;
    private int productionValue = 0;

    public int ProductionValue => productionValue;

    private Transform building = null;

    public Transform Building { get => building; }
    public void SetBuilding(Transform transform, int prodValue)
    {
        building = transform;
        productionValue=prodValue;
    }

    public void DestroyBuilding()
    {
        building = null;
    }

    public bool CanBuild()
    {
        return CellType == SimpleBuildingType.Empty;
    }
    
}
