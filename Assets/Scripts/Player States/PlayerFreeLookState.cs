using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine){}

    /*
     * Animation variables
     */
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private const float AnimationDampTime = 0.1f;


    public override void Enter()
    {
        Debug.Log("We have entered the PlayerFreeLookState");
        stateMachine.animator.CrossFadeInFixedTime(FreeLookBlendTreeHash,AnimationDampTime);
        stateMachine.inputReader.TargetEvent += OnTarget;
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.Movement = CalculateMovement();

        
        Move(stateMachine.Movement * stateMachine.FreeLookMovementSpeed, deltaTime);

        FaceMovementDirection(deltaTime);
    }

    public override void Exit()
    {
        stateMachine.inputReader.TargetEvent -= OnTarget;
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
        //Animation handling
        //If we are not moving play idle animation
        if(stateMachine.inputReader.MovementValue==Vector2.zero)
        {
            stateMachine.animator.SetFloat(FreeLookSpeedHash, 0, AnimationDampTime, deltaTime);
            return;
        }
        //If we are moving play running/walking animation
        stateMachine.animator.SetFloat(FreeLookSpeedHash, 1, AnimationDampTime, deltaTime);
        

        //Camera rotation handling
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(stateMachine.Movement), deltaTime * stateMachine.RotationDampaning);
    }

    private void OnTarget()
    {
        //If there is no target, ignore the atempt to target an object
        if (!stateMachine.targeter.SelectTarget()) return;

        //If there is a target
        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));


    }
}
