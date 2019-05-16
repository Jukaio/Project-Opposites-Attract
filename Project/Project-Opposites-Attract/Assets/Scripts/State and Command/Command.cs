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
    GamePadState state;
    GamePadState prevState;

    //Dance mat
    public PlayerIndex playerMatIndex;

    //charge
    private float chargeTime = 0;
    private bool chargePressed = false;

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
        GamePad.GetState(playerPadIndex).Buttons.B == ButtonState.Pressed && GamePad.GetState(playerPadIndex).Buttons.A == ButtonState.Pressed ||
        GamePad.GetState(playerPadIndex).Buttons.X == ButtonState.Pressed && GamePad.GetState(playerPadIndex).Buttons.Y == ButtonState.Pressed);
    }

    public bool Throw()
    {
        return (Input.GetKeyDown(throws) ||
        GamePad.GetState(playerPadIndex).Buttons.X == ButtonState.Pressed && GamePad.GetState(playerPadIndex).Buttons.A == ButtonState.Pressed 
        && prevState.Triggers.Right == 0);
    }

    public bool ChargeThrow()
    {
        return (Input.GetKey(throws) ||
       GamePad.GetState(playerPadIndex).Buttons.X == ButtonState.Pressed && GamePad.GetState(playerPadIndex).Buttons.A == ButtonState.Pressed);
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
    
    private void Update()
    {
        //ChargingThrow();
        prevState = state;
        state = GamePad.GetState(playerPadIndex);
    }
}
