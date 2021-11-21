using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycast))]
public class PlacementController : MonoBehaviour
{
    [SerializeField] private GameObject arGameObject;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private ARRaycastManager arRaycastManager;


    public GameObject ARGamerObject
    {
        get => arGameObject;
        set => arGameObject = value;
    }

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        
        if (!(Input.touchCount > 0))
        {
            touchPosition = default;
            return false;
        }
        touchPosition = Input.GetTouch(0).position;
        return true;
    }

    private void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            Instantiate(arGameObject, hitPose.position, hitPose.rotation);
        }
    }


}
