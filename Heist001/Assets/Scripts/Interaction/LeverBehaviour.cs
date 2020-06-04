using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverBehaviour : TriggerBehaviour {
    public GameObject leverStick;

    public override void Trigger() {
        base.Trigger();

        Debug.Log(base.caller + " activated this");
        MoveLever();
    }

    private void MoveLever() {
        leverStick.GetComponent<Animator>().SetBool("Trigger", !leverStick.GetComponent<Animator>().GetBool("Trigger"));
    }
}
