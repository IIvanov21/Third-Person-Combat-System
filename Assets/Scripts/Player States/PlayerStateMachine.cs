using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field:SerializeField]
    public InputReader inputReader { get; private set; }
    [field:SerializeField]
    public CharacterController characterController { get; private set; }

    public Vector3 Movement;

    /*
     * Camera controls
     */
    [field:SerializeField]
    public float RotationDampaning { get; private set; }
    [field:SerializeField]
    public float FreeLookMovementSpeed { get; private set; }
    public Transform MainCameraTransform;

    /*
     * Animation controls
     */
    [field:SerializeField]
    public Animator animator { get; private set; }

    /*
     * Target controls
     */
    [field: SerializeField]
    public Targeter targeter { get; private set; }
    [field: SerializeField]
    public float TargetingMovementSpeed { get; private set; }

    private void Start()
    {
        SwitchState(new PlayerFreeLookState(this));
    }
}
