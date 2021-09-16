using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public List<SourceBuildingAsset> buildingList = new List<SourceBuildingAsset>();
    public UnityEvent CreateBuilding;
    public UnityEvent DestroyBuilding;
    public UnityEvent UpgradeBuilding;
    public UnityEvent RotateBuilding;
    public UnityEvent<SourceBuildingAsset> ChangeBuilding;

    public Dictionary<string, SourceBuildingAsset> buildings;

    public static InputManager Instance
    {
        get
        { 
            return _instance; 
        }
    }
    
    private void Awake()
    {
        if (_instance != null && _instance != this) 
        {
            Destroy(this.gameObject);
        }
 
        _instance = this;
        DontDestroyOnLoad( this.gameObject );
    }

    private void Start()
    {
        buildings = new Dictionary<string, SourceBuildingAsset>() 
        {
            ["Farm"] = buildingList[0],
            ["Fishery"] = buildingList[1],
            ["Lumber"] = buildingList[2],
            ["Mine"] = buildingList[3],
        };

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateBuilding?.Invoke();
        }
        if (Input.GetMouseButtonDown(1))
        {
            DestroyBuilding?.Invoke();
        }
    }
    
    public void OnCreateBuilding()
    {
        CreateBuilding?.Invoke();
    }
    
    public void OnDestroyBuilding()
    {
        DestroyBuilding?.Invoke();
    }

    public void OnUpgradeBuilding()
    {
        UpgradeBuilding?.Invoke();
    }

    public void OnRotateBuilding()
    {
        RotateBuilding?.Invoke();
    }

    public void OnChangeSelectedBuilding(string type)
    {
        ChangeBuilding.Invoke(buildings[type]);
    }
}
