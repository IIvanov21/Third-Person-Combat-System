using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{
    /*
     * The enter function is called when a new state is entered. Used to prepare the current state.
     */
    public abstract void Enter();

    /*
     * The tick function will execute code every frame while State entered.
     */
    public abstract void Tick(float deltaTime);

    /*
     * Called when the state is exited. Used to tidy up and transition next state.
     */
    public abstract void Exit();
}
