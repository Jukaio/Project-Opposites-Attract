﻿using System.Collections;
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
        IN_AIR
    }
    public States currentState;

    public GameObject otherPlayer;

    Mechanics mechanics;
    Mechanics otherMechanics;

    float rangeDist = 3;

    public KeyCode moveLeft;// = KeyCode.A;
    public KeyCode moveRight; // = KeyCode.D;
    public KeyCode grab; // = KeyCode.Q;
    public KeyCode throws; // = KeyCode.E;

    public bool grounded;

    void Start()
    {
        mechanics = GetComponent<Mechanics>();
        otherMechanics = otherPlayer.GetComponent<Mechanics>();
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
                if (Input.GetKey(moveLeft) && Input.GetKey(moveRight))
                    break;
                else if (Input.GetKey(moveLeft))
                {
                    mechanics.MoveLeft();
                    currentState = States.MOVE_LEFT;
                }
                else if (Input.GetKey(moveRight))
                {
                    mechanics.MoveRight();
                    currentState = States.MOVE_RIGHT;
                }
                else if (Input.GetKey(grab))
                {
                    if (mechanics.InRange(gameObject, otherPlayer, rangeDist))
                    {
                        mechanics.GrabAttach(gameObject, otherPlayer);
                        currentState = States.GRAB;
                    }
                }
                else if (Input.GetKey(throws))
                {
                    if (mechanics.InRange(gameObject, otherPlayer, rangeDist) && otherMechanics.grounded)
                    {
                        currentState = States.THROW;
                    } 
                }
                else if (!grounded)
                {
                    print("in air");
                }
                break;

            case States.MOVE_LEFT:
                mechanics.MoveLeft();
                if (!Input.GetKey(moveLeft))
                {
                    print("not move");
                    currentState = States.IDLE;
                }
                else if(Input.GetKey(moveRight))
                {
                    currentState = States.IDLE;
                }
                break;

            case States.MOVE_RIGHT:
                mechanics.MoveRight();
                if (!Input.GetKey(moveRight))
                {
                    print("not move");
                    currentState = States.IDLE;
                }
                else if (Input.GetKey(moveLeft))
                {
                    currentState = States.IDLE;
                }
                break;

            case States.GRAB:
                mechanics.GrabAttach(gameObject, otherPlayer);
                if(!Input.GetKey(grab))
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
            case States.IN_AIR:
                break;
            default:
                currentState = States.IDLE;
                break;
        }
    }
}