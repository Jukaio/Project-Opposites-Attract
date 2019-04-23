using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanics : MonoBehaviour
{
    //grab shit 
    public bool inRange;
    public bool isGrabed;
    public bool isGrounded;
    public float checkDist;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveLeft()
    {
        transform.Translate(new Vector2(-0.5f, 0));
    }

    public void MoveRight()
    {
        transform.Translate(new Vector2(0.5f, 0));
    }


    public void Grab(KeyCode obj1Code, GameObject obj1, GameObject obj2)
    {
        if (!isGrabed && inRange)
        {
            isGrabed = true;
            if (Input.GetKey(obj1Code))
            {
                obj2.transform.parent = obj1.transform;
            }
            isGrabed = false;
            obj1.transform.DetachChildren();
            obj2.transform.parent = transform;
        }
    }

    public void Throw(KeyCode obj1Code, GameObject obj2)
    {
        if (inRange)
        {
            if (Input.GetKey(obj1Code) && isGrounded)
            {
                obj2.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 7f);
                isGrounded = false;
            }
        }
    }

    bool InRange(GameObject obj1, GameObject obj2, float dist) //just range
    {
        return Vector2.Distance(obj1.transform.position, obj2.transform.position) <= dist;
    }
}
