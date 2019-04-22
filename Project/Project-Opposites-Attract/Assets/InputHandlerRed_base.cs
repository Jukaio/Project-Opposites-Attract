using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandlerRed_base : MonoBehaviour
{
    public Camera mainCamera;

    public KeyCode moveLeftRed;
    public KeyCode moveRightRed;

    public KeyCode grabRed;
    public KeyCode throwRed;

    public GameObject playerRed;
    public GameObject playerBlue;

    public Movement moveBlue;
    public Movement moveRed;

    public PlayerController controller;

    void Awake()
    {
        moveRed = playerRed.GetComponent<Movement>();
        moveBlue = playerBlue.GetComponent<Movement>();

        SetButtons();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(moveLeftRed) &&
          Input.GetKey(moveRightRed))
        {
            //Debug.Log(moveLeftRed + " " + moveRightRed);
            StartCoroutine(moveBlue.MovePlayerRed(KeyCode.None));
        }
        else if (Input.GetKey(moveLeftRed))
        {
            StartCoroutine(moveRed.MovePlayerRed(moveLeftRed));
            // Debug.Log(moveLeftRed);
        }
        else if (Input.GetKey(moveRightRed))
        {
            StartCoroutine(moveRed.MovePlayerRed(moveRightRed));
            //Debug.Log(moveRightRed);
        }

        if (Input.GetKey(grabRed))
        {
            StartCoroutine(controller.Grab(grabRed, playerRed, playerBlue));
            //Debug.Log(grabRed);
        }
        else if (Input.GetKey(throwRed))
        {
            StartCoroutine(controller.Throw(throwRed, playerBlue));
        }
    }

    void SetButtons()
    {
        moveLeftRed = KeyCode.A;
        moveRightRed = KeyCode.D;

        grabRed = KeyCode.Q;
        throwRed = KeyCode.E;
    }
}
