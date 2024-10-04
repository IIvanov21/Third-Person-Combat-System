using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine )
    {
        this.stateMachine = stateMachine;
    }

    protected void FaceTarget()
    {
        if (stateMachine.targeter.currentTarget == null) return;

        Vector3 facingVector = stateMachine.targeter.currentTarget.transform.position - stateMachine.transform.position;

        facingVector.y = 0;

        stateMachine.transform.rotation = Quaternion.LookRotation(facingVector);
    }

    protected void Move(float deltaTime)
    {
        stateMachine.characterController.Move((Vector3.zero + stateMachine.forceReceiver.Movement) * deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.characterController.Move((motion + stateMachine.forceReceiver.Movement)*deltaTime);
    }
}
