using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : TriggerBehaviour {
    
    public SecurityLevel securityLevelAcess;

    public override void Trigger() {
        base.Trigger();
        
        if(GameManager.gameManager.securityLevelAcess >= securityLevelAcess) {
            MoveDoor();
        }
    }

    private void MoveDoor() {
        GetComponent<Animator>().SetBool("DoorOpen", !GetComponent<Animator>().GetBool("DoorOpen"));
    }
}
