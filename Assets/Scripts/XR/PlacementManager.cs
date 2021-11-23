using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycast))]
public class PlacementManager : MonoBehaviour
{
    public TextMeshProUGUI InputTouches;
    public TextMeshProUGUI PlacementPoseValid;
    public TextMeshProUGUI SpawnedObject;
    public TextMeshProUGUI CurrentTime;
    
    [SerializeField] private GameObject arGameObject;
    [SerializeField] private GameObject placementIndicator;

    private ARRaycastManager arRaycastManager;
    private Pose placementPose;
    private bool placementPoseValid = false;
    private GameObject spawnedObject = null;

    public GameObject ARGamerObject
    {
        get => arGameObject;
        set => arGameObject = value;
    }

    private void Awake()
    {
        InputTouches.SetText($"InputTouches:LMAO");
        PlacementPoseValid.SetText($"PlacementPoseValid:LMAO");
        SpawnedObject.SetText($"Spawned Object:LMAO");
        arRaycastManager = GetComponent<ARRaycastManager>();
    }
    private void Update()
    {
        CurrentTime.SetText($"{Time.time}");
        InputTouches.SetText($"InputTouches:{Input.touchCount > 0}");
        // PlacementPoseValid.SetText($"PlacementPoseValid:{placementPoseValid}");
        SpawnedObject.SetText($"Spawned Object:{spawnedObject == null}");  
        if (spawnedObject == null && placementPoseValid && Input.touchCount > 0)
        {
            ARPlacedObject();
        }
        UpdatePlacementIndicator();
        UpdatePlacementPose();
    }
    

    private void UpdatePlacementPose()
    {
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);
        PlacementPoseValid.SetText($"PlacementPoseValid:{hits.Count}");
        placementPoseValid = hits.Count > 0;
        if (placementPoseValid)
        {
            placementPose = hits[0].pose;
        }
    }
    
    private void UpdatePlacementIndicator()
    {
        if (spawnedObject == null && placementPoseValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void ARPlacedObject()
    {
        spawnedObject = Instantiate(arGameObject, placementPose.position - new Vector3(0, -0.1f, 0), placementPose.rotation);
    }
}
