using UnityEngine;

public class Cell
{
    public bool canBuild;
    
    private Transform building = null;
    
    public void SetBuilding(Transform transform)
    {
        building = transform;
    }

    public bool CanBuild()
    {
        return building == null;
    }

    public void ClearCell()
    {
        building = null;
    }
}