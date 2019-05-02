using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 direction;

    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().AddForce(direction, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("redPlayer") && !collision.gameObject.CompareTag("bluePlayer") && !collision.gameObject.CompareTag("breakable"))
        {
            gameObject.SetActive(false);
        }
    }
}
