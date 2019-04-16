using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;

    public KeyCode moveLeftRed;
    public KeyCode moveRightRed;

    public KeyCode grabRed;
    public KeyCode throwRed;

    public KeyCode moveLeftBlue;
    public KeyCode moveRightBlue;

    public KeyCode grabBlue;
    public KeyCode throwBlue;

    public GameObject playerRed;
    public GameObject playerBlue;

    public Movement moveBlue;
    public Movement moveRed;

    //grab shit 
    public bool inRange;
    public bool isGrabed;
    public bool isThrown;
    public bool isGrounded;
    public float checkDist;

    void Start()
    {
        playerRed.transform.position = new Vector2((mainCamera.orthographicSize * mainCamera.aspect) * (-9f / 10f), ((mainCamera.orthographicSize) * (-4f / 5f)));
        moveRed = playerRed.GetComponent<Movement>();

        playerBlue.transform.position = new Vector2((mainCamera.orthographicSize * mainCamera.aspect) * (-8f / 10f), ((mainCamera.orthographicSize) * (-4f / 5f)));
        moveBlue = playerBlue.GetComponent<Movement>();

        SetButtons();

        StartCoroutine(PlayerBlueUpdate());
        StartCoroutine(PlayerRedUpdate());
    }

    private void Update()
    {
        inRange = InRange(playerRed, playerBlue, checkDist);
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

            if (Input.GetKey(grabRed))
            {
                StartCoroutine(Grab(grabRed, playerRed, playerBlue));
                Debug.Log(grabRed + " " + playerID);
            }
            else if (Input.GetKey(throwRed))
            {
                StartCoroutine(Throw(throwRed, playerBlue));
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

            if (Input.GetKey(grabBlue))
            {
                StartCoroutine(Grab(grabBlue, playerBlue, playerRed));
                Debug.Log(grabBlue + " " + playerID);
            }
            else if (Input.GetKey(throwBlue))
            {
                StartCoroutine(Throw(throwBlue, playerRed));
            }
        }
    }

    IEnumerator PlayerRedUpdate()
    {
        while (true)
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

    IEnumerator Grab(KeyCode obj1Code, GameObject obj1, GameObject obj2)
    { 
        if (!isGrabed && inRange)
        {
            isGrabed = true;
            DisableMovememnt(obj2, true);
            while (Input.GetKey(obj1Code))
            {
                obj2.transform.parent = obj1.transform;
                yield return new WaitForEndOfFrame();
            }
            isGrabed = false;
            DisableMovememnt(obj2, false);
            obj1.transform.DetachChildren();
            obj2.transform.parent = transform;
        }
    }

    IEnumerator Throw(KeyCode obj1Code, GameObject obj2)
    {
        //limit jump times
        //just do grounded
        if (inRange && !isThrown)
        {
            isThrown = true;
            while (Input.GetKey(obj1Code))
            {
                //obj2.transform.Translate(new Vector2(0f, 1.5f));
                obj2.GetComponent<Physics>().jump = true;
                yield return new WaitForEndOfFrame();
            }
            isThrown = false;
        }
    }

    bool InRange(GameObject obj1, GameObject obj2, float dist) //just range
    {
        return Vector2.Distance(obj1.transform.position, obj2.transform.position) <= dist;
    }

    void DisableMovememnt(GameObject obj, bool setToZero)
    {
        if (setToZero)
            obj.GetComponent<Movement>().speed = 0;
        else
            obj.GetComponent<Movement>().speed = obj.GetComponent<Movement>().normalSpeed;
    }

    void SetButtons()
    {
        moveLeftRed = KeyCode.A;
        moveRightRed = KeyCode.D;

        grabRed = KeyCode.Q;
        throwRed = KeyCode.E;

        moveLeftBlue = KeyCode.J;
        moveRightBlue = KeyCode.L;

        grabBlue = KeyCode.U;
        throwBlue = KeyCode.O;
    }

}
