using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForTwo : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 offset;

    private void LateUpdate()
    { 
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPos = centerPoint + offset;
        transform.position = newPos;
    }

    Vector3 GetCenterPoint()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }
}
