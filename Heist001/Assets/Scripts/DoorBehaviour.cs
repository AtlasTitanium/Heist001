using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : TriggerBehaviour {
    public override void Trigger() {
        base.Trigger();
        
        GetComponent<Animator>().SetBool("DoorOpen", !GetComponent<Animator>().GetBool("DoorOpen"));
    }
}
