using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State currentState;

    public void SwitchState(State newState)
    {
        //If we have a current state, tidy up the current state and prepare for a transition to a new state.
        currentState?.Exit();

        //Set the current state to the new state
        currentState = newState;

        //Prepare the new state
        currentState?.Enter();
    }

    private void Update()
    {
        //Run the new state's tick function every frame.
        currentState?.Tick(Time.deltaTime);
    }
}
