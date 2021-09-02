using UnityEngine;

public class Cell
{
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
        return building == null;
    }

}
