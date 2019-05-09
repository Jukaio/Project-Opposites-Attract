using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Vector3 pos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pos = collision.transform.position;
    }
}
