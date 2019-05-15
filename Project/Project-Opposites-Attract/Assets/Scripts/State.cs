﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using XInputDotNetPure;


public class State : MonoBehaviour
{
    Command command;
    public string canNotMoveOn;
    public string canMoveOn;

    public float chargeTime = 0;
    bool chargePressed = false;

    public enum GroundType
    {
        GREEN, //Later add more object tags here
        RED,
        BLUE,
        AIR
    }
    public GroundType groundType;
    public GroundType prevGroundType;
    public string GroundTypeString;
    public string prevGroundTypeString;

    public enum States
    {
        IDLE,
        MOVE_LEFT,
        MOVE_RIGHT,
        GRAB,
        IN_GRAB,
        THROW,
        IN_THROW,
        IN_CHARGE
    }
    public States currentState;


    public GameObject otherPlayer;
    public State otherState;

    Mechanics mechanics;
    Mechanics otherMechanics;

    public bool grounded;

    void Start()
    {
        command = GetComponent<Command>();
        mechanics = GetComponent<Mechanics>();
        otherMechanics = otherPlayer.GetComponent<Mechanics>();

        otherState = otherPlayer.GetComponent<State>();

    }

    //void CheckGamepadStates()
    //{
    //    if (!playerIndexSet || !prevState.IsConnected)
    //    {
    //        for (int i = 0; i < 4; ++i)
    //        {
    //            PlayerIndex testPlayerIndex = (PlayerIndex)i;
    //            GamePadState testState = GamePad.GetState(testPlayerIndex);
    //            if (testState.IsConnected)
    //            {
    //                Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
    //                playerIndexSet = true;
    //            }
    //            else
    //                Debug.Log(string.Format("GamePad not found {0}", testPlayerIndex));

    //        }
    //    }
    //    prevState = state;
    //    state = GamePad.GetState(playerMatIndex);
    //}

    void Update()
    {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it

        //Debug.Log("State " + GamePad.GetState(playerPadIndex));

        //CheckGamepadStates();

        MoveStatesHandler();
    }

    void MoveStatesHandler()
    {
        if (gameObject.tag == "bluePlayer")
        {
            switch (groundType)
            {
                case GroundType.GREEN:
                    CanMoveInputHandler();
                    break;

                case GroundType.BLUE:
                    CanMoveInputHandler();
                    break;
                case GroundType.RED:
                    mechanics.RespawnOnPosition();

                    break;

                case GroundType.AIR:
                    break;
            }
        }
        else if(gameObject.tag == "redPlayer")
        {
            switch (groundType)
            {
                case GroundType.GREEN:
                    CanMoveInputHandler();
                    break;

                case GroundType.BLUE:
                    mechanics.RespawnOnPosition();
                    break;
                case GroundType.RED:
                    CanMoveInputHandler();
                    break;

                case GroundType.AIR:
                    break;
            }
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

            case States.IN_CHARGE:
                State_INCHARGE();
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
        if (command.MoveLeft()) //GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed
        {
            currentState = States.MOVE_LEFT;
        }
        else if (command.MoveRight())
        {
            currentState = States.MOVE_RIGHT;
        }
        else if (command.Grab()) //grab key
        {
            if (mechanics.InRange(gameObject, otherPlayer))
            {
                currentState = States.GRAB;
                otherState.currentState = States.IN_GRAB;
            }
        }
        else if (command.Throw()) // throw key
        {
            if (mechanics.InRange(gameObject, otherPlayer) && otherState.groundType != GroundType.AIR)
            {
                otherState.currentState = States.IN_THROW;
                currentState = States.THROW;
            }
        }
        else if (command.ButtonA())
        {
            StartCoroutine(mechanics.shootProjectile(new Vector2(2f, 7)));
        }
        else if (command.ButtonB())
        {
            StartCoroutine(mechanics.shootProjectile(new Vector2(-2f, 7)));
        }
        else if (command.ButtonX())
        {
            StartCoroutine(mechanics.shootProjectile(new Vector2(7f, 2)));
        }
        else if (command.ButtonY())
        {
            StartCoroutine(mechanics.shootProjectile(new Vector2(-7, 2)));
        }
        else if (command.testButton()) //test
        {
            otherState.currentState = States.IN_THROW;
            State_INCHARGE();
        }
    }

    void State_THROW()
    {
        mechanics.Throw(otherPlayer);
        currentState = States.IDLE;
    }
    void State_INCHARGE()
    {
        if(!chargePressed)
            StartCoroutine(State_Charge());
    }

    IEnumerator State_Charge()
    {
        chargePressed = true;
        chargeTime = 0;
        while (command.testButton())
        {
            chargeTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Debug.LogError(chargeTime);
        GetComponent<Mechanics>().ChargeThrow(GetComponent<State>().otherPlayer, chargeTime);
        chargePressed = false;
    }
    void State_INTHROW()
    {
        if (groundType != GroundType.AIR)
        {
            currentState = States.IDLE;
        }
        if (command.MoveLeft()) // move key
        {
            mechanics.MoveLeft();
        }
        else if (command.MoveRight()) //move key
        {
            mechanics.MoveRight();
        }
    }
    void State_MOVELEFT()
    {
        mechanics.MoveLeft();
        if (!command.MoveLeft()) //move key
        {
            currentState = States.IDLE;
        }
        else if (command.MoveRight()) //move key
        {
            currentState = States.IDLE;
        }
    }
    void State_MOVERIGHT()
    {
        mechanics.MoveRight();
        if (!command.MoveRight()) //move key
        {
            currentState = States.IDLE;
        }
        else if (command.MoveLeft()) //move key
        {
            currentState = States.IDLE;
        }
    }
    void State_GRAB()
    {
        mechanics.GrabAttach(gameObject, otherPlayer);
        if (!command.Grab()) //grab key
        {
            mechanics.GrabDeattach(otherPlayer); 
            currentState = States.IDLE;
        }
        else if (command.MoveLeft()) //move key
        {
            mechanics.MoveLeft();
        }
        else if (command.MoveRight()) //move key
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
        //if (collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("breakable"))
        //{
        //    print("grounds");

        //    grounded = true;
        //    currentMoveState = MoveStates.CAN_MOVE;
        //}
        //else if (collision.gameObject.CompareTag("blueTile"))
        //{
        //    if (gameObject.tag == "redPlayer")
        //    {
        //        currentMoveState = MoveStates.CAN_NOT_MOVE;
        //    }
        //    else if (gameObject.tag == "bluePlayer")
        //    {
        //        currentMoveState = MoveStates.CAN_MOVE;
        //    }
        //    grounded = true;
        //}
        //else if (collision.gameObject.CompareTag("redTile"))
        //{
        //    if (gameObject.tag == "bluePlayer")
        //    {
        //        currentMoveState = MoveStates.CAN_NOT_MOVE;
        //    }
        //    else if (gameObject.tag == "redPlayer")
        //    {
        //        currentMoveState = MoveStates.CAN_MOVE;
        //    }
        //    grounded = true;
        //}
        //if (collision.gameObject.tag != prevGroundTypeString)
        //{
        //    if (collision.gameObject.tag == "ground")
        //    {
        //        groundType = GroundType.GREEN;
        //    }
        //    else if (collision.gameObject.tag == "redTile")
        //    {
        //        groundType = GroundType.RED;
        //    }
        //    else if (collision.gameObject.tag == "blueTile")
        //    {
        //        groundType = GroundType.BLUE;
        //    }
        //}
        //prevGroundTypeString = collision.gameObject.tag;
        //prevGroundType = groundType;

        //Debug.Log(collision.gameObject.transform.position);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "ground")
        {
            groundType = GroundType.GREEN;
        }
        else if (collision.gameObject.tag == "redTile")
        {
            groundType = GroundType.RED;
        }
        else if (collision.gameObject.tag == "blueTile")
        {
            groundType = GroundType.BLUE;
        }

        prevGroundTypeString = collision.gameObject.tag;
        prevGroundType = groundType;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        groundType = GroundType.AIR;
    }

    

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    groundType = GroundType.AIR;
    //}
}
