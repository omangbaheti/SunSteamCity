using System.Collections.Generic;
using UnityEngine;

public class BuildingTemplate
{
    [SerializeField] private string levelName;
    [SerializeField] private int level;
    [SerializeField] private float cost;
    [SerializeField] private float productionRate;
    [SerializeField] private GameObject model;
    [TextArea (4,4)]
    [SerializeField] private string buildingDescription;
    
    public string LevelName => levelName;
    public int Level => level;
    public float Cost => cost;
    public float ProductionRate => productionRate;
    public GameObject Model => model;
    public string BuildingDescription => buildingDescription;
}
