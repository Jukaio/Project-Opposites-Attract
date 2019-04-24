using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    Buttons buttonScript;
    private void Start()
    {
        buttonScript = GetComponent<Buttons>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            buttonScript.ReloadScene();
        }

    }
}
