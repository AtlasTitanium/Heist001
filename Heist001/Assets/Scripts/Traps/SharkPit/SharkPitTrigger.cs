using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkPitTrigger : TriggerBehaviour
{
    public override void Trigger() {
        base.Trigger();
        GetComponent<SharkPit>().TrapDeactivate();
    }
}
