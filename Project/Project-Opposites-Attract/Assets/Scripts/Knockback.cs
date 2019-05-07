using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    //public Vector2 baseKnockback;
    public Vector2 knockback;

    void Start()
    {
        //knockback = baseKnockback;
    }

    void Update()
    {
        
    }

    public void KnockbackObj(GameObject obj)
    {
        obj.GetComponent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag("redTile") && collision.gameObject.CompareTag("bluePlayer"))
        {
            KnockbackObj(collision.gameObject);
        }
        else if (gameObject.CompareTag("blueTile") && collision.gameObject.CompareTag("redPlayer"))
        {
            KnockbackObj(collision.gameObject);
        }
    }
}
