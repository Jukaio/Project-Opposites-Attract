using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour
{
    public Vector2 position;
    public Vector2 velocity;
    public Vector2 gravity;

    private float previousTime;
    private float currentTime;

    private float dt;

    void Awake()
    {
        dt = currentTime - previousTime;
        position = transform.position;
    }

    void Update()
    {
        UpdatePosition(dt);
    }

    void UpdatePosition(float dt)
    {
        previousTime = currentTime;
        currentTime = Time.time;

        dt = currentTime - previousTime;

        if (dt > 0.15f)
        {
            dt = 0.15f;
        }

        transform.position = new Vector2(transform.position.x + velocity.x * dt, transform.position.y + velocity.y * dt);
        velocity = velocity + gravity * dt;
    }
}
