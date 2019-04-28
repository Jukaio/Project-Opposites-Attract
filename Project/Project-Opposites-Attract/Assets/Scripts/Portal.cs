using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    Buttons buttonScript;
    public bool onPortal;

    void Start()
    {
        buttonScript = GetComponent<Buttons>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            buttonScript.LoadRandom();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("portal"))
        {
            onPortal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("portal"))
        {
            onPortal = false;
        }
    }
}
