using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    Camera mainCamera;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();

        mainCamera.orthographic = true;
    }
}
