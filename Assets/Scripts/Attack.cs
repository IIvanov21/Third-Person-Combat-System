using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//Ensure we can view it in the editor
public class Attack 
{
    //All the references needed to calculate our Attack's Damage and Force
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float Force { get; private set; }
    [field: SerializeField] public float ForceTime { get; private set; }
    [field: SerializeField] public float ComboAttackTime { get; private set; }

    //Animation Trackers
    [field: SerializeField] public int ComboStateIndex { get; private set; } = -1;
    [field: SerializeField] public string AnimationName { get; private set; }
    [field: SerializeField] public float TransitionDuration { get; private set; }
}
