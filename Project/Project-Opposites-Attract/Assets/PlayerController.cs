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



    void InputHandler(string playerID)
    {
        if (playerID == playerRed.name) //red player
        {
            if (Input.GetKey(moveLeftRed) &&
                Input.GetKey(moveRightRed))
            {
                Debug.Log(moveLeftRed + " " + moveRightRed + " " + playerID);
                StartCoroutine(moveBlue.MovePlayerRed(KeyCode.None));
            }
            else if (Input.GetKey(moveLeftRed))
            {
                StartCoroutine(moveRed.MovePlayerRed(moveLeftRed));
                Debug.Log(moveLeftRed + " " + playerID);

            }
            else if (Input.GetKey(moveRightRed))
            {
                StartCoroutine(moveRed.MovePlayerRed(moveRightRed));
                Debug.Log(moveRightRed + " " + playerID);

            }

            if (Input.GetKey(grabLeftRed))
            {
                Debug.Log(grabLeftRed + " " + playerID);

            }
            else if (Input.GetKey(grabRightRed))
            {
                Debug.Log(grabRightRed + " " + playerID);

            }
        }

        if (playerID == playerBlue.name) //blue player
        {
            if (Input.GetKey(moveLeftBlue) &&
                Input.GetKey(moveRightBlue))
            {
                Debug.Log(moveLeftBlue + " " + moveRightBlue + " " + playerID);
                StartCoroutine(moveBlue.MovePlayerBlue(KeyCode.None));
            }
            else if (Input.GetKey(moveLeftBlue))
            {
                StartCoroutine(moveBlue.MovePlayerBlue(moveLeftBlue));
                Debug.Log(moveLeftBlue + " " + playerID);

            }
            else if (Input.GetKey(moveRightBlue))
            {
                StartCoroutine(moveBlue.MovePlayerBlue(moveRightBlue));
                Debug.Log(moveRightBlue + " " + playerID);

            }

            if (Input.GetKey(grabLeftBlue))
            {
                Debug.Log(grabLeftBlue + " " + playerID);

            }
            else if (Input.GetKey(grabRightBlue))
            {
                Debug.Log(grabRightBlue + " " + playerID);

            }
        }
    }

    IEnumerator PlayerRedUpdate()
    {
        while(true)
        {
            InputHandler(playerRed.name);

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator PlayerBlueUpdate()
    {
        while (true)
        {
            InputHandler(playerBlue.name);

            yield return new WaitForEndOfFrame();
        }
    }

}
