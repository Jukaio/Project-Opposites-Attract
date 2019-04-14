using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public KeyCode moveLeftRed;
    public KeyCode moveRightRed;

    public KeyCode grabLeftRed;
    public KeyCode grabRightRed;

    public KeyCode moveLeftBlue;
    public KeyCode moveRightBlue;

    public KeyCode grabLeftBlue;
    public KeyCode grabRightBlue;


    public SpawnPlayers spawn;

    public GameObject playerRed;
    public GameObject playerBlue;

    public Movement moveBlue;
    public Movement moveRed;

    void Start()
    {
        spawn = gameObject.GetComponent<SpawnPlayers>();

        playerRed = spawn.playerRed;
        moveRed = playerRed.AddComponent<Movement>();

        playerBlue = spawn.playerBlue;
        moveBlue = playerBlue.AddComponent<Movement>();


        moveLeftRed = KeyCode.A;
        moveRightRed = KeyCode.D;

        grabLeftRed = KeyCode.Q;
        grabRightRed = KeyCode.E;

        moveLeftBlue = KeyCode.J;
        moveRightBlue = KeyCode.L;

        grabLeftBlue = KeyCode.U;
        grabRightBlue = KeyCode.O;


        StartCoroutine(PlayerBlueUpdate());
        StartCoroutine(PlayerRedUpdate());


    }

    void Update()
    {

    }

    void InputHandlerRed()
    {
        if (Input.GetKey(moveLeftRed) &&
            Input.GetKey(moveRightRed))
        {
            Debug.Log(moveLeftRed + " " + moveRightRed + " " );
            StartCoroutine(moveBlue.MovePlayerRed(KeyCode.None));
        }
        else if (Input.GetKey(moveLeftRed))
        {
            StartCoroutine(moveRed.MovePlayerRed(moveLeftRed));
            Debug.Log(moveLeftRed + " " );
        }
        else if (Input.GetKey(moveRightRed))
        {
            StartCoroutine(moveRed.MovePlayerRed(moveRightRed));
            Debug.Log(moveRightRed + " " );
        }

        if (Input.GetKey(grabLeftRed))
        {
            Debug.Log(grabLeftRed + " " );
        }
        else if (Input.GetKey(grabRightRed))
        {
            Debug.Log(grabRightRed + " " );
        }

    }

    void InputHandlerBlue()
    {

        if (Input.GetKey(moveLeftBlue) &&
            Input.GetKey(moveRightBlue))
        {
            Debug.Log(moveLeftBlue + " " + moveRightBlue + " ");
            StartCoroutine(moveBlue.MovePlayerBlue(KeyCode.None));
        }
        else if (Input.GetKey(moveLeftBlue))
        {
            StartCoroutine(moveBlue.MovePlayerBlue(moveLeftBlue));
            Debug.Log(moveLeftBlue + " ");
        }
        else if (Input.GetKey(moveRightBlue))
        {
            StartCoroutine(moveBlue.MovePlayerBlue(moveRightBlue));
            Debug.Log(moveRightBlue + " ");
        }

        if (Input.GetKey(grabLeftBlue))
        {
            Debug.Log(grabLeftBlue + " ");
        }
        else if (Input.GetKey(grabRightBlue))
        {
            Debug.Log(grabRightBlue + " ");
        }
    }
    

    IEnumerator PlayerRedUpdate() // player red
    {
        while(true)
        {
            InputHandlerRed();


            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator PlayerBlueUpdate() // player blue
    {
        while (true)
        {
            InputHandlerBlue();

            yield return new WaitForEndOfFrame();
        }
    }

}
