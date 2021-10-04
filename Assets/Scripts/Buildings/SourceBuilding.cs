using UnityEngine;

[System.Serializable]
public class SourceBuilding : BuildingTemplate
{
    [SerializeField] private int produceValue;
    public int ProduceValue => produceValue;
}


