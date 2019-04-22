using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandlerRed_base : MonoBehaviour
{
    public Camera mainCamera;

    public KeyCode moveLeft;
    public KeyCode moveRight;

    public KeyCode grabRed;
    public KeyCode throwRed;

    public GameObject playerRed;
    public GameObject playerBlue;

    public Movement movementState;

    public PlayerController controller;

    void Awake()
    {
        movementState = GetComponent<Movement>();

        SetButtons();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(moveLeft) &&
          Input.GetKey(moveRight))
        {
            //Debug.Log(moveLeftRed + " " + moveRightRed);
            StartCoroutine(movementState.MovePlayer(KeyCode.None));
        }
        else if (Input.GetKey(moveLeft))
        {
            StartCoroutine(movementState.MovePlayer(moveLeft));
            // Debug.Log(moveLeftRed);
        }
        else if (Input.GetKey(moveRight))
        {
            StartCoroutine(movementState.MovePlayer(moveRight));
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
        moveLeft = KeyCode.A;
        moveRight = KeyCode.D;

        grabRed = KeyCode.Q;
        throwRed = KeyCode.E;
    }
}
