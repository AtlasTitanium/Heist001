using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Button : CallerBehaviour
{
    public override void Call() {
        base.Call();
        Debug.Log("Pressed the button");
    }
}
