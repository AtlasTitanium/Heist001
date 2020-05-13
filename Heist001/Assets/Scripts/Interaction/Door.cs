using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : TriggerBehaviour
{
    public float doorHeight = 2;
    public int moveSpeed = 100;
    private bool doorState = false;

    public override void Trigger() {
        base.Trigger();
        Debug.Log("Triggered door");

        doorState = !doorState;
        if (doorState) {
            StartCoroutine(OpenDoor());
        } else {
            StartCoroutine(CloseDoor());
        }
    }

    private IEnumerator OpenDoor() {
        for (int i = 0; i < moveSpeed; i++) {
            transform.position += Vector3.up * (1.0f/moveSpeed) * doorHeight;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator CloseDoor() {
        for (int i = 0; i < moveSpeed; i++) {
            transform.position -= Vector3.up * (1.0f / moveSpeed) * doorHeight;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
