using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointController controller;

    private void Start()
    {
        controller = GetComponentInParent<CheckpointController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        controller.currentPoint = new Vector3(collision.transform.position.x, collision.transform.position.y + 2, 0f);
    }
}
