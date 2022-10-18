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
    [SerializeField] private GameObject arGameObject;
    [SerializeField] private GameObject placementIndicator;

    private ARRaycastManager arRaycastManager;
    private Pose placementPose;
    private bool placementPoseValid = false;
    private GameObject spawnedObject = null;
    private bool placed = false;
    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }
    private void Update()
    {
        if (spawnedObject == null && placementPoseValid && Input.touchCount > 0)
        {
            ARPlaceObject();
        }

        if (placed) return;
        UpdatePlacementIndicator();
        UpdatePlacementPose();
    }
    

    private void UpdatePlacementPose()
    {
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);
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

    private void ARPlaceObject()
    {
        spawnedObject = Instantiate(arGameObject, placementPose.position - new Vector3(0, -0.1f, 0), placementPose.rotation);
        placed = true;
        placementIndicator.SetActive(false);
    }
}
