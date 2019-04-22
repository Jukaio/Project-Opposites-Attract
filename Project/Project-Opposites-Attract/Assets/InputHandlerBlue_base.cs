using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandlerBlue_base : MonoBehaviour
{
    public Camera mainCamera;

    public KeyCode moveLeftBlue;
    public KeyCode moveRightBlue;

    public KeyCode grabBlue;
    public KeyCode throwBlue;

    public GameObject playerRed;
    public GameObject playerBlue;

    public Movement moveBlue;
    public Movement moveRed;

    public PlayerController controller;

    void Start()
    { 
        moveRed = playerRed.GetComponent<Movement>();
        moveBlue = playerBlue.GetComponent<Movement>();

        SetButtons();

        StartCoroutine(PlayerBlueUpdate());
    }

    private void Update()
    {
       
    }

    void InputHandler()
    {

        if (Input.GetKey(moveLeftBlue) &&
            Input.GetKey(moveRightBlue))
        {
            StartCoroutine(moveBlue.MovePlayerBlue(KeyCode.None));
            //Debug.Log(moveLeftBlue + " " + moveRightBlue);
        }
        else if (Input.GetKey(moveLeftBlue))
        {
            StartCoroutine(moveBlue.MovePlayerBlue(moveLeftBlue));
            //Debug.Log(moveLeftBlue);
        }
        else if (Input.GetKey(moveRightBlue))
        {
            StartCoroutine(moveBlue.MovePlayerBlue(moveRightBlue));
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

    IEnumerator PlayerBlueUpdate()
    {
        while (true)
        {
            InputHandler();
            yield return new WaitForEndOfFrame();
        }
    }

    void SetButtons()
    {
        moveLeftBlue = KeyCode.J;
        moveRightBlue = KeyCode.L;

        grabBlue = KeyCode.U;
        throwBlue = KeyCode.O;
    }
}
