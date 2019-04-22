using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandlerBlue_base : MonoBehaviour
{
    public Camera mainCamera;

    public KeyCode moveLeft;
    public KeyCode moveRight;

    public KeyCode grabBlue;
    public KeyCode throwBlue;

    public GameObject playerRed;
    public GameObject playerBlue;

    public Movement movementState;

    public PlayerController controller;

    void Awake()
    { 
        movementState = GetComponent<Movement>();

        SetButtons();
    }

    private void Update()
    {
        if (Input.GetKey(moveLeft) &&
            Input.GetKey(moveRight))
        {
            StartCoroutine(movementState.MovePlayer(KeyCode.None));
            //Debug.Log(moveLeftBlue + " " + moveRightBlue);
        }
        else if (Input.GetKey(moveLeft))
        {
            StartCoroutine(movementState.MovePlayer(moveLeft));
            //Debug.Log(moveLeftBlue);
        }
        else if (Input.GetKey(moveRight))
        {
            StartCoroutine(movementState.MovePlayer(moveRight));
            //Debug.Log(moveRightBlue);
        }

        if (Input.GetKey(grabBlue))
        {
            StartCoroutine(controller.Grab(grabBlue, playerBlue, playerRed));
            //Debug.Log(grabBlue);
        }
        else if (Input.GetKey(throwBlue))
        {
            StartCoroutine(controller.Throw(throwBlue, playerRed));
        }
    }

    void SetButtons()
    {
        moveLeft = KeyCode.J;
        moveRight = KeyCode.L;

        grabBlue = KeyCode.U;
        throwBlue = KeyCode.O;
    }
}
