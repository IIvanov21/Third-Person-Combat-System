using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        
    }
    public override void Enter()
    {
        Debug.Log("We have entered the PlayerFreeLookState");
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.Movement = CalculateMovement();

        stateMachine.characterController.Move(stateMachine.Movement * deltaTime * stateMachine.FreeLookMovementSpeed);

        FaceMovementDirection(deltaTime);
    }

    public override void Exit()
    {

    }

    Vector3 CalculateMovement()
    {
        //Calculation for Forwards/ Backwards movement
        Vector3 cameraForward = stateMachine.MainCameraTransform.transform.forward;

        cameraForward.y = 0f;

        cameraForward.Normalize();

        //Get the right direction of the camera
        Vector3 cameraRight = stateMachine.MainCameraTransform.transform.right;

        cameraRight.y = 0f;

        cameraRight.Normalize();

        return cameraForward * stateMachine.inputReader.MovementValue.y + cameraRight*stateMachine.inputReader.MovementValue.x;
    }

    private void FaceMovementDirection(float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(stateMachine.Movement), deltaTime * stateMachine.RotationDampaning);
    }
}
