﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using XInputDotNetPure;


public class State : MonoBehaviour
{
    bool playerIndexSet = false;
    public PlayerIndex playerMatIndex; //which player? 
    public PlayerIndex playerPadIndex;

    GamePadState state;
    GamePadState prevState;

    GamePadState playerMat;
    GamePadState playerPad;

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

    void CheckGamepadStates()
    {
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndexSet = true;
                }
                else
                    Debug.Log(string.Format("GamePad not found {0}", testPlayerIndex));

            }
        }
        prevState = state;
        state = GamePad.GetState(playerMatIndex);
    }

    void Update()
    {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it

        Debug.Log("State " + GamePad.GetState(playerPadIndex));

        CheckGamepadStates();

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
        if ((Input.GetKey(moveLeft) && !Input.GetKey(moveRight)) || 
            (GamePad.GetState(playerMatIndex).DPad.Left == ButtonState.Pressed && GamePad.GetState(playerMatIndex).DPad.Right == ButtonState.Released)) //GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed
        {
            currentState = States.MOVE_LEFT;
        }
        else if (Input.GetKey(moveRight) && !Input.GetKey(moveLeft) ||
            GamePad.GetState(playerMatIndex).DPad.Right == ButtonState.Pressed && GamePad.GetState(playerMatIndex).DPad.Left == ButtonState.Released)
        {
            currentState = States.MOVE_RIGHT;
        }
        else if (Input.GetKey(grab) ||
            GamePad.GetState(playerPadIndex).Triggers.Left != 0) //grab key
        {
            if (mechanics.InRange(gameObject, otherPlayer))
            {
                currentState = States.GRAB;
                otherState.currentState = States.IN_GRAB;
            }
        }
        else if (Input.GetKeyDown(throws) ||
            GamePad.GetState(playerPadIndex).Triggers.Right != 0) // throw key
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
        if (Input.GetKey(moveLeft) && !Input.GetKey(moveRight) ||
            GamePad.GetState(playerMatIndex).DPad.Left == ButtonState.Pressed && GamePad.GetState(playerMatIndex).DPad.Right == ButtonState.Released) // move key
        {
            mechanics.MoveLeft();
        }
        else if (Input.GetKey(moveRight) && !Input.GetKey(moveLeft) ||
            GamePad.GetState(playerMatIndex).DPad.Right == ButtonState.Pressed && GamePad.GetState(playerMatIndex).DPad.Left == ButtonState.Released) //move key
        {
            mechanics.MoveRight();
        }
    }
    void State_MOVELEFT()
    {
        mechanics.MoveLeft();
        if (!Input.GetKey(moveLeft) ||
            GamePad.GetState(playerMatIndex).DPad.Left == ButtonState.Released) //move key
        {
            currentState = States.IDLE;
        }
        else if (Input.GetKey(moveRight) ||
            GamePad.GetState(playerMatIndex).DPad.Right == ButtonState.Pressed) //move key
        {
            currentState = States.IDLE;
        }
    }
    void State_MOVERIGHT()
    {
        mechanics.MoveRight();
        if (!Input.GetKey(moveRight) ||
            GamePad.GetState(playerMatIndex).DPad.Right == ButtonState.Released) //move key
        {
            currentState = States.IDLE;
        }
        else if (Input.GetKey(moveLeft) ||
            GamePad.GetState(playerMatIndex).DPad.Left == ButtonState.Pressed) //move key
        {
            currentState = States.IDLE;
        }
    }
    void State_GRAB()
    {
        mechanics.GrabAttach(gameObject, otherPlayer);
        if (!Input.GetKey(grab) ||
            GamePad.GetState(playerPadIndex).Triggers.Left == 0) //grab key
        {
            mechanics.GrabDeattach(otherPlayer);
            currentState = States.IDLE;
        }
        else if (Input.GetKey(moveLeft) && !Input.GetKey(moveRight) ||
            GamePad.GetState(playerMatIndex).DPad.Left == ButtonState.Pressed && GamePad.GetState(playerMatIndex).DPad.Right == ButtonState.Released) //move key
        {
            mechanics.MoveLeft();
        }
        else if (Input.GetKey(moveRight) && !Input.GetKey(moveLeft) ||
            GamePad.GetState(playerMatIndex).DPad.Right == ButtonState.Pressed && GamePad.GetState(playerMatIndex).DPad.Left == ButtonState.Released) //move key
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