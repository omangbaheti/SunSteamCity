using System.Collections.Generic;
using UnityEngine;

public class SimpleBuilding : MonoBehaviour
{
    [SerializeField] private SimpleBuildingType simpleBuildingType;
    [SerializeField] private int price;
    [SerializeField] private int productionRate;
    [SerializeField] private int width;
    [SerializeField] private int length;
    [SerializeField] private GameObject model;


    public SimpleBuildingType SimpleBuildingType => simpleBuildingType;
    public int Price => price;
    public int ProductionRate => productionRate;
    public int Width => width;
    public int Length => length;
    public GameObject Model => model;
    
    public List<Vector2Int> FillGridPositions(Vector2Int offset, BuildingAssetTemplate building=null, SimpleBuilding simpleBuilding=null)
    {
        List<Vector2Int> occupiedGridPositions = new List<Vector2Int>();

        for (int x = 0; x < simpleBuilding.Width; x++)
        {
            for (int y = 0; y < simpleBuilding.Length; y++)
            {
                // if (x < 0 || x >= GridProperties.Width || y < 0 || y >= GridProperties.Height) continue;
                occupiedGridPositions.Add(offset + new Vector2Int(x, y));
                Debug.Log($"Filling:{offset + new Vector2Int(x, y)}");
            }
        }

        return occupiedGridPositions;
    }
}

public enum SimpleBuildingType
{
    NULL=-1,
    Farm=0,
    Mine=1,
    Lumber=2,
    Empty=3
}
