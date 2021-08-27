using UnityEngine;

[System.Serializable]
public class ProductionBuilding : BuildingTemplate
{
    [SerializeField] private float improvementPercentage;
    public float ImprovementPercentage => improvementPercentage;
}
