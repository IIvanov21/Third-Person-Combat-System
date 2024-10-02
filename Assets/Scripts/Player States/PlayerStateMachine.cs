using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field:SerializeField]
    public InputReader inputReader { get; private set; }


    private void Start()
    {
        SwitchState(new PlayerTestState(this));
    }
}
