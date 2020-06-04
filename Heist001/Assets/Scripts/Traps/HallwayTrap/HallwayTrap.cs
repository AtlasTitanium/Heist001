using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayTrap : MonoBehaviour
{
    public CallerBehaviour fakeDoor;
    public HeatPuzzle puzzle;

    public Transform dropDownDoor;
    public int lockdownSpeed = 10;

    private void Start() {
        if (fakeDoor != null) {
            fakeDoor.OnCall += TrapActivate;
        }
    }

    private void TrapActivate() {
        puzzle.StartPuzzle(this);
        StartCoroutine(Lockdown(true));
    }

    public void TrapDeactivate() {
        StartCoroutine(Lockdown(false));
    }

    IEnumerator Lockdown(bool active) {
        if (active) {
            dropDownDoor.gameObject.SetActive(true);
            for (int i = 0; i < lockdownSpeed; i++) {
                dropDownDoor.position -= Vector3.up * (1.0f / lockdownSpeed) * dropDownDoor.localScale.y;
                yield return new WaitForSeconds(0.01f);
            }
        } else {
            for (int i = 0; i < lockdownSpeed; i++) {
                dropDownDoor.position += Vector3.up * (1.0f / lockdownSpeed) * dropDownDoor.localScale.y;
                yield return new WaitForSeconds(0.01f);
            }
            dropDownDoor.gameObject.SetActive(false);
        }
    }
}
