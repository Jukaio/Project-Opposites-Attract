using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    State state;
    State otherState;

    public GameObject otherPlayer;
    Mechanics mechanics;
    Command command;

    public float chargeTime = 0;
    bool charging = false;

    private void Awake()
    {
        state = GetComponent<State>();
        mechanics = GetComponent<Mechanics>();
        command = GetComponent<Command>();

        otherState = state.otherState;
        otherPlayer = state.otherPlayer;
    }

    public States Main_Throw(States currentState)
    {
        mechanics.Throw(otherPlayer);
        return States.IDLE;
    }

    public States Main_Charge(States currentState)
    {
        if (!charging)
        {
            StartCoroutine(State_Charge());
        }
        if (charging)
            return currentState;
        return States.IDLE;
    }

    IEnumerator State_Charge()
    {
        charging = true;
        chargeTime = 0;
        otherState.currentState = States.IN_CHARGE;
        while (command.ChargeThrow())
        {
            chargeTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        GetComponent<Mechanics>().ChargeThrow(GetComponent<State>().otherPlayer, chargeTime);
        otherState.currentState = States.IN_THROW;
        charging = false;
    }

}
