using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public enum States
    {
        IDLE,
        MOVE,
        GRAB,
        IN_GRAB,
        THROW,
        IN_AIR
    }
    public States currentState;

    public GameObject redPlayer;
    public GameObject bluePlayer;

    Movement movementScript;

    KeyCode move = KeyCode.Alpha1;
    KeyCode grab = KeyCode.Alpha2;
    KeyCode throws = KeyCode.Alpha3;

    public bool grounded;

    void Start()
    {
        movementScript = GetComponent<Movement>();
    }

    void Update()
    {
        InputHandler();
    }

    void InputHandler()
    {
        switch (currentState)
        {
            case States.IDLE:
                if (Input.GetKey(move))
                {
                    print("idle-move");
                    currentState = States.MOVE;
                }
                else if (Input.GetKey(grab))
                {
                    print("idle-grab");
                    currentState = States.GRAB;
                }
                else if (Input.GetKey(throws))
                {
                    print("idle-throw");
                    currentState = States.THROW;
                }
                else if (!grounded)
                {
                    print("in air");
                }
                break;
            case States.MOVE:
                if (!Input.GetKey(move))
                {
                    print("not move");
                    currentState = States.IDLE;
                }
                break;
            case States.GRAB:
                if (!Input.GetKey(grab))
                {
                    print("not grab");
                    currentState = States.IDLE;
                }
                else if (Input.GetKey(move))
                {
                    print("grab-move");
                }
                else if (Input.GetKey(throws))
                {
                    print("grab-throw");
                }
                break;
            case States.THROW:
                if (!Input.GetKey(throws))
                {
                    print("not throw");
                    currentState = States.IDLE;
                }
                else if (Input.GetKey(move))
                {
                    print("throwe-move");
                }
                else if (grounded)
                {
                    print("grounded");
                }
                break;
            case States.IN_AIR:
       
            default:
                currentState = States.IDLE;
                break;
        }
    }
}
