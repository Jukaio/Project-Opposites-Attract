using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using XInputDotNetPure;

public enum States
{
    IDLE,
    MOVE_LEFT,
    MOVE_RIGHT,
    GRAB,
    IN_GRAB,
    THROW,
    IN_THROW,
    IN_CHARGE,
    IN_FALL,
}

public enum GroundType
{
    GREEN, //Later add more object tags here
    RED,
    BLUE,
    AIR
}

public class State : MonoBehaviour
{
    public string canNotMoveOn;
    public string canMoveOn;

    public GroundType groundType;
    public GroundType prevGroundType;
    public string GroundTypeString;
    public string prevGroundTypeString;

    public States currentState;


    public GameObject otherPlayer;
    public State otherState;

    Mechanics mechanics;
    Mechanics otherMechanics;

    public bool grounded;

    void Start()
    {
        mechanics = GetComponent<Mechanics>();
        otherMechanics = otherPlayer.GetComponent<Mechanics>();

        otherState = otherPlayer.GetComponent<State>();
    }

    void Update()
    {
        CheckGroundType();
    }

    void CheckGroundType()
    {
        if (gameObject.tag == "bluePlayer")
        {
            switch (groundType)
            {
                case GroundType.GREEN:
                    CheckState_Ground();
                    break;

                case GroundType.BLUE:
                    CheckState_Ground();
                    break;
                case GroundType.RED:
                    if (GetComponent<OnWrongGround>() == null)
                        gameObject.AddComponent<OnWrongGround>();
                    currentState = GetComponent<OnWrongGround>().Main_WrongGround(currentState);
                    break;

                case GroundType.AIR:
                    CheckState_Air();
                    break;
            }
        }
        else if (gameObject.tag == "redPlayer")
        {
            switch (groundType)
            {
                case GroundType.GREEN:
                    CheckState_Ground();
                    break;

                case GroundType.BLUE:
                    if (GetComponent<OnWrongGround>() == null)
                        gameObject.AddComponent<OnWrongGround>();
                    currentState = GetComponent<OnWrongGround>().Main_WrongGround(currentState);
                    break;
                case GroundType.RED:
                    CheckState_Ground();
                    break;

                case GroundType.AIR:
                    CheckState_Air();
                    break;
            }
        }
    }

    void CheckState_Ground()
    {
        switch (currentState)
        {
            case States.IDLE:
                if (GetComponent<Idle>() == null)
                    gameObject.AddComponent<Idle>();
                currentState = GetComponent<Idle>().Main_Idle(currentState);
                break;

            case States.MOVE_LEFT:
                if (GetComponent<Move>() == null)
                    gameObject.AddComponent<Move>();
                currentState = GetComponent<Move>().Main_Left(currentState, groundType);
                break;

            case States.MOVE_RIGHT:
                if (GetComponent<Move>() == null)
                    gameObject.AddComponent<Move>();
                currentState = GetComponent<Move>().Main_Right(currentState, groundType);
                break;

            case States.GRAB:
                if (GetComponent<Grab>() == null)
                    gameObject.AddComponent<Grab>();
                currentState = GetComponent<Grab>().Main_Grab(currentState);
                break;

            case States.THROW:
                if (GetComponent<Throw>() == null)
                    gameObject.AddComponent<Throw>();
                currentState = GetComponent<Throw>().Main_Charge(currentState);
                break;

            case States.IN_THROW:
                if (GetComponent<InThrow>() == null)
                    gameObject.AddComponent<InThrow>();
                currentState = GetComponent<InThrow>().Main_InThrow(groundType, currentState);
                break;

            case States.IN_GRAB:
                if (GetComponent<InGrab>() == null)
                    gameObject.AddComponent<InGrab>();
                currentState = GetComponent<InGrab>().Main_InGrab(currentState);
                break;

            case States.IN_CHARGE:
                break;

            default:
                currentState = States.IDLE;
                break;
        }
    }

    void CheckState_Air()
    {
        switch(currentState)
        {
            case States.MOVE_LEFT:
                if (GetComponent<Move>() == null)
                    gameObject.AddComponent<Move>();
                currentState = GetComponent<Move>().Air_Left(currentState, groundType);
                break;

            case States.MOVE_RIGHT:
                if (GetComponent<Move>() == null)
                    gameObject.AddComponent<Move>();
                currentState = GetComponent<Move>().Air_Right(currentState, groundType);
                break;

            case States.IN_FALL:
                if (GetComponent<Idle>() == null)
                    gameObject.AddComponent<Idle>();
                currentState = GetComponent<Idle>().Air_Idle(currentState);
                break;

            case States.IN_THROW:
                if (GetComponent<InThrow>() == null)
                    gameObject.AddComponent<InThrow>();
                currentState = GetComponent<InThrow>().Main_InThrow(groundType, currentState);
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        groundType = GroundType.AIR;
    }
} 