using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Mechanics mechanics;

    void Start()
    {
        mechanics = GetComponent<Mechanics>();
    }

    void Update()
    {
        mechanics.MoveLeft();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("redPlayer") && !collision.gameObject.CompareTag("redPlayer") && !collision.gameObject.CompareTag("breakable"))
        {
            gameObject.SetActive(false);
        }
    }
}
