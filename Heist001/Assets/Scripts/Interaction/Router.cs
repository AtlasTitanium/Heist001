using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Router : CallerBehaviour
{
    public override void Call() {
        base.Call();
        GetComponent<Renderer>().material.color = Color.green;
        this.enabled = false;
    }
}
