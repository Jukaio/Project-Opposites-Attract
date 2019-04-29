using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject projectile;
    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        
    }

    public void CreateProjectile(float speed)
    {
        Instantiate(projectile, new Vector2 (speed, 0f), transform.rotation);
    }
}
