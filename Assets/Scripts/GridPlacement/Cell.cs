using UnityEngine;

public class Cell
{
    private int x = 0;
    private int z = 0;
    public Vector2Int Position { get; }

    public Cell(int x, int z)
    {
        this.x = x;
        this.z = z;
        Position = new Vector2Int(this.x, this.z);
    }
    
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