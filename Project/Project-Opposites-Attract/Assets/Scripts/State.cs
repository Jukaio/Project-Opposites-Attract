using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public enum States
    {
        IDLE,
        MOVE_LEFT,
        MOVE_RIGHT,
        GRAB,
        IN_GRAB,
        THROW,
        IN_THROW
    }
    public States currentState;

    public enum MoveStates
    {
        CAN_MOVE,
        CAN_NOT_MOVE
    }
    public MoveStates currentMoveState;

    

    public GameObject otherPlayer;
    public State otherState;

    Mechanics mechanics;
    Mechanics otherMechanics;

    float rangeDist = 3;

    public KeyCode moveLeft;// = KeyCode.A;
    public KeyCode moveRight; // = KeyCode.D;
    public KeyCode grab; // = KeyCode.Q;
    public KeyCode throws; // = KeyCode.E;

    public bool grounded;
    public bool canMove;

    void Start()
    {
        mechanics = GetComponent<Mechanics>();
        otherMechanics = otherPlayer.GetComponent<Mechanics>();

        otherState = otherPlayer.GetComponent<State>();
    }

    void Update()
    {
       InputHandler();
    }

    void InputHandler()
    {
        switch (currentMoveState)
        {
            case MoveStates.CAN_MOVE:
                switch (currentState)
                {
                    case States.IDLE:
                        State_IDLE();
                        break;

                    case States.MOVE_LEFT:
                        mechanics.MoveLeft();
                        if (!Input.GetKey(moveLeft))
                        {
                            currentState = States.IDLE;
                        }
                        else if (Input.GetKey(moveRight))
                        {
                            currentState = States.IDLE;
                        }
                        break;

                    case States.MOVE_RIGHT:
                        mechanics.MoveRight();
                        if (!Input.GetKey(moveRight))
                        {
                            currentState = States.IDLE;
                        }
                        else if (Input.GetKey(moveLeft))
                        {
                            currentState = States.IDLE;
                        }
                        break;

                    case States.GRAB:
                        mechanics.GrabAttach(gameObject, otherPlayer);
                        if (!Input.GetKey(grab))
                        {
                            mechanics.GrabDeattach(gameObject, otherPlayer);
                            currentState = States.IDLE;
                        }
                        else if (Input.GetKey(moveLeft) && !Input.GetKey(moveRight))
                        {
                            mechanics.MoveLeft();
                        }
                        else if (Input.GetKey(moveRight) && !Input.GetKey(moveLeft))
                        {
                            mechanics.MoveRight();
                        }
                        break;

                    case States.THROW:
                        mechanics.Throw(otherPlayer);
                        currentState = States.IDLE;
                        break;

                    case States.IN_THROW:
                        if (grounded)
                        {
                            currentState = States.IDLE;
                        }
                        if (Input.GetKey(moveLeft) && !Input.GetKey(moveRight))
                        {
                            mechanics.MoveLeft();
                        }
                        else if (Input.GetKey(moveRight) && !Input.GetKey(moveLeft))
                        {
                            mechanics.MoveRight();
                        }
                        break;

                    case States.IN_GRAB:
                        if (otherState.currentState != States.GRAB)
                            mechanics.GrabDeattach(gameObject, otherPlayer);
                        break;

                    default:
                        currentState = States.IDLE;
                        break;
                }
                break;
            case MoveStates.CAN_NOT_MOVE:
                break;
        }
    }

    void State_IDLE()
    {
        if (Input.GetKey(moveLeft) && !Input.GetKey(moveRight))
        {
            currentState = States.MOVE_LEFT;
        }
        else if (Input.GetKey(moveRight) && !Input.GetKey(moveLeft))
        {
            currentState = States.MOVE_RIGHT;
        }
        else if (Input.GetKey(grab))
        {
            if (mechanics.InRange(gameObject, otherPlayer, rangeDist))
            {
                currentState = States.GRAB;
                otherState.currentState = States.IN_GRAB;
            }
        }
        else if (Input.GetKeyDown(throws))
        {
            if (mechanics.InRange(gameObject, otherPlayer, rangeDist) && otherState.grounded)
            {
                otherState.grounded = false;
                otherState.currentState = States.IN_THROW;
                currentState = States.THROW;
            }
        }
    }
    void State_THROW()
    {
        mechanics.Throw(otherPlayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            print("grounds");
            grounded = true;
            currentMoveState = MoveStates.CAN_MOVE;
        }
        else if (collision.gameObject.CompareTag("blueTile"))
        {
            if (gameObject.tag == "redPlayer")
                currentMoveState = MoveStates.CAN_NOT_MOVE;
            else if (gameObject.tag == "bluePlayer")
                currentMoveState = MoveStates.CAN_MOVE;
            grounded = true;
        }
        else if (collision.gameObject.CompareTag("redTile"))
        {
            if (gameObject.tag == "bluePlayer")
                currentMoveState = MoveStates.CAN_NOT_MOVE;
            else if (gameObject.tag == "redPlayer")
                currentMoveState = MoveStates.CAN_MOVE;
            grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        currentMoveState = MoveStates.CAN_MOVE;
    }
}
