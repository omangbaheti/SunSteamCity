﻿using UnityEngine;

[System.Serializable]
public class ProductionBuilding
{
    [SerializeField] private string levelName;
    [SerializeField] private int level;
    [SerializeField] private float cost;
    [SerializeField] private float productionRate;
    [SerializeField] private float improvementPercentage;
    [SerializeField] private GameObject model;
    [TextArea (2,4)]
    [SerializeField] private string buildingDescription;

    public string LevelName => levelName;
    public int Level => level;
    public float Cost => cost;
    public float ProductionRate => productionRate;
    public float ImprovementPercentage => improvementPercentage;
    public GameObject Model => model;
    public string BuildingDescription => buildingDescription;
}


