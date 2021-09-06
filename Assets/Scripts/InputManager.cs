using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public List<BuildingAssetTemplate> buildingList = new List<BuildingAssetTemplate>();
    public UnityEvent CreateBuilding;
    public UnityEvent DestroyBuilding;
    public UnityEvent UpgradeBuilding;
    public UnityEvent RotateBuilding;
    public UnityEvent<BuildingAssetTemplate> ChangeBuilding;

    public Dictionary<string, BuildingAssetTemplate> buildings;

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
        buildings = new Dictionary<string, BuildingAssetTemplate>() 
        {
            ["Farm"] = buildingList[0],
            ["Fisheries"] = buildingList[1],
            ["Lumber"] = buildingList[2],
            ["Mines"] = buildingList[3],
            ["Production"] = buildingList[4]
            
        };
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateBuilding?.Invoke();
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
