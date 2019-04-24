using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour
{
    public Vector2 velocity;
    public Vector2 startVelocity;

    public Vector2 gravity;

    private float previousTime;
    private float currentTime;

    private float dt;

    public bool isGrounded;

    public bool jump;

    void Awake()
    {
        dt = currentTime - previousTime;
        startVelocity = velocity;
    }

    void Update()
    {

    }

    public void UpdatePosition()
    {
        transform.position = new Vector2(transform.position.x + velocity.x * dt, transform.position.y + velocity.y * dt);
        velocity += gravity * dt;
        Updatedt();

        //if (isGrounded)
        //{
        //    jump = false;
        //}
    }

    void Updatedt()
    {
        previousTime = currentTime;
        currentTime = Time.time;

        dt = currentTime - previousTime;

        if (dt > 0.15f)
        {
            dt = 0.15f;
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "ground")
    //    {
    //        print("ground");
    //        isGrounded = true;
    //        velocity = startVelocity;
    //    }
    //}

    public IEnumerator RunJump(KeyCode key)
    {
        while (true)
        {
            UpdatePosition();
            yield return new WaitForEndOfFrame();
            if (velocity.y <= -startVelocity.y)
            {
                velocity = startVelocity;
                break;
            }
        }
        
    }
}
