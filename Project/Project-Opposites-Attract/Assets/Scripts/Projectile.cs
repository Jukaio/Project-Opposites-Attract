using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private void OnEnable()
    {
        
    }

    public void MoveProjectile(Vector2 direction)
    {
        gameObject.SetActive(true);
        GetComponent<Rigidbody2D>().AddForce(direction, ForceMode2D.Impulse);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("redPlayer") && 
            !collision.gameObject.CompareTag("bluePlayer") && 
            !collision.gameObject.CompareTag("breakable"))
        {
            gameObject.SetActive(false);
        }
    }
}
