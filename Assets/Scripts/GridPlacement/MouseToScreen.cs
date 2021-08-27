using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseToScreen : MonoBehaviour
{
    
    public static Vector3 GetWorldPosition()
    {
        Plane plane= new Plane(Vector3.up, 0);
        Ray screenToWorldRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(screenToWorldRay, out float distance))
        {
            Vector3 worldPosition = screenToWorldRay.GetPoint(distance);
            Debug.Log(worldPosition);
            return worldPosition;
        }
        else
        {
            Debug.Log("no Plane found");
            return Vector3.zero;
        }
    }
}