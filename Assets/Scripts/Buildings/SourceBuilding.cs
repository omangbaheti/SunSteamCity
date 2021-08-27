using UnityEngine;

[System.Serializable]
public class SourceBuilding : BuildingTemplate
{
    [SerializeField] private float produceValue;
    public float ProduceValue => produceValue;
}


