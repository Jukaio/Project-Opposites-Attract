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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("portal"))
        {
            print("portal");
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                buttonScript.LoadLevel(1);
            }
        }
    }
}
