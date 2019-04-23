using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanics : MonoBehaviour
{
    public bool grounded;

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
        grounded = false;
        obj2.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10f), ForceMode2D.Impulse);
        //obj2.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 7f);
    }

    public bool InRange(GameObject obj1, GameObject obj2, float dist) //just range
    {
        return Vector2.Distance(obj1.transform.position, obj2.transform.position) <= dist;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "ground")
        {
            print("grounds");
            grounded = true;
        }
    }

}
