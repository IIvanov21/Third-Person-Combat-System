using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private Attack attack;
    private float previousFrameTime;
    private bool alreadyAppliedForce = false;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        //Start the attack animation using a crossfade
        stateMachine.animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);

        //Set the damage value for the current attack
        stateMachine.weaponDamage.SetAttack(attack.Damage);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        FaceTarget();

        //Get the normalised time of the current animation
        float normalisedTime = GetNormalisedTime();

        if (normalisedTime < 1.0f)
        {
            //Apply force
            if (normalisedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }

            if (stateMachine.inputReader.IsAttacking)
            {
                TryComboAttack(normalisedTime);
            }
        }
        else 
        {
            if (stateMachine.targeter.currentTarget != null) stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            else stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }

        //Store the current normalised time for use in the next frame
        previousFrameTime = normalisedTime;
    }

    public override void Exit() 
    {

    }

    private void TryComboAttack(float normalisedTime)
    {
        //If there is no combo available, return early
        if (attack.ComboStateIndex == -1)
        {
            return;
        }

        //If the normalized Time hasnt reached the combo attack window, return early
        if (normalisedTime < attack.ComboAttackTime) return;

        //Transition to the next combo attack state
        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, attack.ComboStateIndex));
    }

    //Get the normalized time of the current or next attack animation
    private float GetNormalisedTime()
    {
        //Get current and next animator state info
        AnimatorStateInfo currentInfo = stateMachine.animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.animator.GetNextAnimatorStateInfo(0);

        //If the animator is transitioning into an attack state, return the normalised time of the next state
        if (stateMachine.animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        //If not transition but already in a attack state, return the normalised time of the current state
        else if (!stateMachine.animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0.0f;
        }
    }

    //Apply force to the player during the attack if it hasn't been applied already
    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;

        //Apply the force
        stateMachine.forceReceiver.AddForce(stateMachine.transform.forward * attack.Force);

        //Mark that the force has been applied
        alreadyAppliedForce = true;
    }

}
