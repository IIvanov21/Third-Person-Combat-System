using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    /*
     * Animation controls
     */
    private readonly int TargetingBlendTree = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForward = Animator.StringToHash("TargetingForward");//Controls the forward/backward animations
    private readonly int TargetingRight = Animator.StringToHash("TargetingRight");//Controls the right/left animations
    private const float AnimatorDampTime = 0.1f;

    public override void Enter()
    {
        //Transition to Targeting blend tree in the animator
        stateMachine.animator.CrossFadeInFixedTime(TargetingBlendTree, AnimatorDampTime);
        //Subscribe to the Cancel event
        stateMachine.inputReader.CancelTargetEvent += OnCancel;
    }

    public override void Tick(float deltaTime)
    {
        //If there is no target, we want to switch back to our Player Free Look State
        if (stateMachine.targeter.currentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }

        //Calculate normal movement
        stateMachine.Movement = CalculateMovement();
       
        Move(stateMachine.Movement*stateMachine.TargetingMovementSpeed,deltaTime);

        //Face the target
        FaceTarget();

        //Update Animations
        UpdateAnimator(deltaTime);

    }

    public override void Exit()
    {
        stateMachine.inputReader.CancelTargetEvent -= OnCancel;
    }

    private void OnCancel()
    {
        stateMachine.targeter.Cancel();

        //Switch back to the Player Free Look State
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 movement = new Vector3();//Capture movement result

        movement += stateMachine.transform.right * stateMachine.inputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.inputReader.MovementValue.y;

        return movement;
    }

    private void UpdateAnimator(float deltaTime)
    {
        //If the player is currently Idle
        if(stateMachine.inputReader.MovementValue==Vector2.zero)
        {

            stateMachine.animator.SetFloat(TargetingForward, 0, AnimatorDampTime, deltaTime);
            stateMachine.animator.SetFloat(TargetingRight, 0, AnimatorDampTime, deltaTime);

            return;
        }

        //If the player is currently moving
        stateMachine.animator.SetFloat(TargetingForward,stateMachine.inputReader.MovementValue.y,AnimatorDampTime, deltaTime);
        stateMachine.animator.SetFloat(TargetingRight, stateMachine.inputReader.MovementValue.x, AnimatorDampTime, deltaTime);

    }

    
}
