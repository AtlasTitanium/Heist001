using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CallButton : CallerBehaviour
{
    public override void Call() {
        if(this.enabled) base.Call();
    }
}
