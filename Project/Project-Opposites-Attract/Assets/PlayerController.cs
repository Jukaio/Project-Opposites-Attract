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

    void Start()
    {
        spawn = gameObject.GetComponent<SpawnPlayers>();

        playerRed = spawn.playerRed;
        playerRed.AddComponent<Movement>();

        playerBlue = spawn.playerBlue;
        playerBlue.AddComponent<Movement>();


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



    KeyCode InputHandler(string playerID)
    {
        if (playerID == playerRed.name)
        {
            if (Input.GetKey(moveLeftRed) &&
                Input.GetKey(moveRightRed))
            {
                Debug.Log(moveLeftRed + " " + moveRightRed + " " + playerID);
                return 0;
            }
            else if (Input.GetKey(moveLeftRed))
            {
                Debug.Log(moveLeftRed + " " + playerID);
                return KeyCode.A;
            }
            else if (Input.GetKey(moveRightRed))
            {
                Debug.Log(moveRightRed + " " + playerID);
                return KeyCode.D;
            }

            if (Input.GetKey(grabLeftRed))
            {
                Debug.Log(grabLeftRed + " " + playerID);
                return KeyCode.Q;
            }
            else if (Input.GetKey(grabRightRed))
            {
                Debug.Log(grabRightRed + " " + playerID);
                return KeyCode.E;
            }
        }

        if (playerID == playerBlue.name)
        {
            if (Input.GetKey(moveLeftBlue) &&
                Input.GetKey(moveRightBlue))
            {
                Debug.Log(moveLeftBlue + " " + moveRightBlue + " " + playerID);
                return 0;
            }
            else if (Input.GetKey(moveLeftBlue))
            {
                Debug.Log(moveLeftBlue + " " + playerID);
                return KeyCode.J;
            }
            else if (Input.GetKey(moveRightBlue))
            {
                Debug.Log(moveRightBlue + " " + playerID);
                return KeyCode.L;
            }

            if (Input.GetKey(grabLeftBlue))
            {
                Debug.Log(grabLeftBlue + " " + playerID);
                return KeyCode.U;
            }
            else if (Input.GetKey(grabRightBlue))
            {
                Debug.Log(grabRightBlue + " " + playerID);
                return KeyCode.O;
            }
        }
        return 0;
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
