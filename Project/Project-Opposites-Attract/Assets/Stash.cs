using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stash : MonoBehaviour
{
    GameObject PlayerRed;
    GameObject PlayerBlue;
    GameObject PlayerParent;


    void Start()
    {
        PlayerParent = new GameObject("Parent Player", );
        PlayerParent.transform.parent = transform;

        PlayerRed = new GameObject("Red Player");
        PlayerRed.transform.parent = PlayerParent.transform;

        PlayerBlue = new GameObject("Blue Player");
        PlayerBlue.transform.parent = PlayerParent.transform;

    }

    void Update()
    {
        
    }
}
