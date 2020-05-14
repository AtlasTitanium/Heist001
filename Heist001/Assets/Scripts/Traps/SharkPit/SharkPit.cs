using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkPit : MonoBehaviour
{
    public Transform[] dropDownDoors;
    public OpenFloorTest dangerFloor;
    public int lockdownSpeed = 10;
    private Collider col;

    private void Start() {
        col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            TrapActivate();
        }
    }

    private void TrapActivate() {
        col.enabled = false;
        StartCoroutine(Lockdown(true));
        dangerFloor.Activate();
    }

    public void TrapDeactivate() {
        StartCoroutine(Lockdown(false));
        dangerFloor.Deactivate();
    }

    IEnumerator Lockdown(bool active) {
        if (active) {
            foreach (Transform t in dropDownDoors) {
                for (int i = 0; i < lockdownSpeed; i++) {
                    t.position -= Vector3.up * (1.0f / lockdownSpeed) * t.localScale.y;
                    yield return new WaitForSeconds(0.01f);
                }
            }
        } else {
            foreach (Transform t in dropDownDoors) {
                for (int i = 0; i < lockdownSpeed; i++) {
                    t.position += Vector3.up * (1.0f / lockdownSpeed) * t.localScale.y;
                    yield return new WaitForSeconds(0.01f);
                }
            }
        }
    }
}
