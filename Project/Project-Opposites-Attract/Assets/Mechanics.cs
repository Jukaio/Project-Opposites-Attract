using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanics : MonoBehaviour
{
    public void MoveLeft()
    {
        transform.Translate(new Vector2(-0.1f, 0f));
    }

    public void MoveRight()
    {
        transform.Translate(new Vector2(0.1f, 0f));
    }

    public void GrabAttach(GameObject obj1, GameObject obj2)
    {
        obj2.transform.parent = obj1.transform;
    }

    public void GrabDeattach(GameObject obj1, GameObject obj2)
    {
        obj2.transform.parent = obj1.transform.parent;
    }

    public void Throw(GameObject obj2)
    {
        obj2.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10f), ForceMode2D.Impulse);
    }

    public bool InRange(GameObject obj1, GameObject obj2, float dist) //just range
    {
        return Vector2.Distance(obj1.transform.position, obj2.transform.position) <= dist;
    }
}
