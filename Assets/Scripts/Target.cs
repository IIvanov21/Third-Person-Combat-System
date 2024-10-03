using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //An event which is accesible in the inspector
    public event Action<Target> OnDestroyed;

    //Simply on destroy invoke the OnDestroy method
    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }
}
