﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Mechanics mechanics;
    Command command;

    private void Awake()
    {
        mechanics = GetComponent<Mechanics>();
        command = GetComponent<Command>();
    }

    public States Main_Left(States currentState, GroundType groundType)
    {
        mechanics.MoveLeft();
        if (!command.MoveLeft()) //move key
        {
            return States.IDLE;
        }
        else if (command.MoveRight()) //move key
        {
            return States.IDLE;
        }
        else if (groundType == GroundType.AIR)
        {
            return States.IN_FALL;
        }
        return currentState;
    }

    public States Main_Right(States currentState, GroundType groundType)
    {
        mechanics.MoveRight();
        if (!command.MoveRight()) //move key
        {
            return States.IDLE;
        }
        else if (command.MoveLeft()) //move key
        {
            return States.IDLE;
        }
        else if (groundType == GroundType.AIR)
        {
            return States.IN_FALL;
        }
        return currentState;
    }

}
