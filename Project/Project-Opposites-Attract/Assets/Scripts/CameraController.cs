using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector2 velocity;

    public float smoothTimeY;
    public float smoothTimeX;

    public GameObject mid;
    public GameObject obj1;
    public GameObject obj2;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    private void FixedUpdate()
    {
        mid.transform.position = new Vector3((obj1.transform.position.x + obj2.transform.position.x) / 2,
            (obj1.transform.position.y + obj2.transform.position.y) / 2, 0f);

        float posX = Mathf.SmoothDamp(transform.position.x, mid.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, mid.transform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, transform.position.z);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
    }        
}
