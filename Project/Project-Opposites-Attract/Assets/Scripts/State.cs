using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    public KeyCode moveLeft;// = KeyCode.A;
    public KeyCode moveRight; // = KeyCode.D;
    public KeyCode grab; // = KeyCode.Q;
    public KeyCode throws; // = KeyCode.E;

    public bool grounded;

    void Start()
    {
        mechanics = GetComponent<Mechanics>();
        otherMechanics = otherPlayer.GetComponent<Mechanics>();

        otherState = otherPlayer.GetComponent<State>();
    }

    void Update()
    {
        MoveStatesHandler();
    }

    void MoveStatesHandler()
    {
        switch (currentMoveState)
        {
            case MoveStates.CAN_MOVE:
                CanMoveInputHandler();
                break;

            case MoveStates.CAN_NOT_MOVE:
                CanNoteMoveInputHandler();
                break;
        }
    }

    void CanMoveInputHandler()
    {
        switch (currentState)
        {
            case States.IDLE:
                State_IDLE();
                break;

            case States.MOVE_LEFT:
                State_MOVELEFT();
                break;

            case States.MOVE_RIGHT:
                State_MOVERIGHT();
                break;

            case States.GRAB:
                State_GRAB();
                break;

            case States.THROW:
                State_THROW();
                break;

            case States.IN_THROW:
                State_INTHROW();
                break;

            case States.IN_GRAB:
                State_INGRAB();
                break;

            default:
                currentState = States.IDLE;
                break;
        }
    }
    void CanNoteMoveInputHandler()
    {
      switch(currentState)
        {
            case States.GRAB:
                currentState = States.IDLE;
                mechanics.GrabDeattach(otherPlayer);
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
            if (mechanics.InRange(gameObject, otherPlayer))
            {
                currentState = States.GRAB;
                otherState.currentState = States.IN_GRAB;
            }
        }
        else if (Input.GetKeyDown(throws))
        {
            if (mechanics.InRange(gameObject, otherPlayer) && otherState.grounded)
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
        currentState = States.IDLE;
    }
    void State_INTHROW()
    {
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
    }
    void State_MOVELEFT()
    {
        mechanics.MoveLeft();
        if (!Input.GetKey(moveLeft))
        {
            currentState = States.IDLE;
        }
        else if (Input.GetKey(moveRight))
        {
            currentState = States.IDLE;
        }
    }
    void State_MOVERIGHT()
    {
        mechanics.MoveRight();
        if (!Input.GetKey(moveRight))
        {
            currentState = States.IDLE;
        }
        else if (Input.GetKey(moveLeft))
        {
            currentState = States.IDLE;
        }
    }
    void State_GRAB()
    {
        mechanics.GrabAttach(gameObject, otherPlayer);
        if (!Input.GetKey(grab))
        {
            mechanics.GrabDeattach(otherPlayer);
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
    }
    void State_INGRAB()
    {
        if (otherState.currentState != States.GRAB)
        {
            mechanics.GrabDeattach(otherPlayer);
            currentState = States.IDLE;
        }
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

        //Debug.Log(collision.gameObject.transform.position);

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        currentMoveState = MoveStates.CAN_MOVE;
    }
}
