using System.Collections;
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

    public States Main_Left(States currentState)
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
        return currentState;
    }

    public States Main_Right(States currentState)
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
        return currentState;
    }

}
