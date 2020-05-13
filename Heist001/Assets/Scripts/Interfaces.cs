using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CallerBehaviour : MonoBehaviour {
    public event Action OnCall;

    public virtual void Call() {
        OnCall.Invoke();
    }
}

public class TriggerBehaviour : MonoBehaviour {
    public CallerBehaviour caller;

    private void Start() {
        if(caller != null) {
            caller.OnCall += Trigger;
        }
    }

    public virtual void Trigger() {
        return;
    }
}
