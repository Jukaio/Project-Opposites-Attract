﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Command : MonoBehaviour
{
    //Keyboard
    public KeyCode moveLeft;// = KeyCode.A;
    public KeyCode moveRight; // = KeyCode.D;
    public KeyCode grab; // = KeyCode.Q;
    public KeyCode throws; // = KeyCode.E;

    public KeyCode A;
    public KeyCode B;
    public KeyCode X;
    public KeyCode Y;

    //Gamepad
    public PlayerIndex playerPadIndex;

    //Dance mat
    public PlayerIndex playerMatIndex;

    public bool MoveLeft()
    {
        return ((Input.GetKey(moveLeft) && !Input.GetKey(moveRight)) ||
        (GamePad.GetState(playerMatIndex).DPad.Left == ButtonState.Pressed && GamePad.GetState(playerMatIndex).DPad.Right == ButtonState.Released));
    }

    public bool MoveRight()
    {
        return ((Input.GetKey(moveRight) && !Input.GetKey(moveLeft)) ||
        GamePad.GetState(playerMatIndex).DPad.Right == ButtonState.Pressed && GamePad.GetState(playerMatIndex).DPad.Left == ButtonState.Released);
    }

    public bool Grab()
    {
        return (Input.GetKey(grab) ||
        GamePad.GetState(playerPadIndex).Triggers.Left != 0);
    }

    public bool Throw()
    {
        return (Input.GetKey(throws) ||
        GamePad.GetState(playerPadIndex).Triggers.Right != 0);
    }

    public bool ButtonA()
    {
        return (Input.GetKeyDown(A) ||
        GamePad.GetState(playerMatIndex).Buttons.A == ButtonState.Pressed);
    }
    public bool ButtonB()
    {
        return (Input.GetKeyDown(B) ||
        GamePad.GetState(playerMatIndex).Buttons.B == ButtonState.Pressed);
    }
    public bool ButtonX()
    {
        return (Input.GetKeyDown(X) ||
        GamePad.GetState(playerMatIndex).Buttons.X == ButtonState.Pressed);
    }
    public bool ButtonY()
    {
        return (Input.GetKeyDown(Y) ||
        GamePad.GetState(playerMatIndex).Buttons.Y == ButtonState.Pressed);
    }

}
