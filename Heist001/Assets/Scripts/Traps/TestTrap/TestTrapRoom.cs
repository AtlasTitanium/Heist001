using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrapRoom : TriggerBehaviour
{
    public Transform[] dropDownDoors;
    public int lockdownSpeed = 10;
    private Collider col;

    private void Start() {
        foreach (Transform t in dropDownDoors) {
            t.gameObject.SetActive(false);
        }
        col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            TrapActivate();
        }
    }

    public override void Trigger() {
        base.Trigger();
        TrapDeactivate();
    }

    private void TrapActivate() {
        col.enabled = false;
        foreach(Transform t in dropDownDoors) {
            t.gameObject.SetActive(true);
        }
        StartCoroutine(Lockdown(true));
    }

    public void TrapDeactivate() {
        foreach (Transform t in dropDownDoors) {
            t.gameObject.SetActive(false);
        }
        StartCoroutine(Lockdown(false));
    }

    IEnumerator Lockdown(bool active) {
        if (active) {
            foreach (Transform t in dropDownDoors) {
                for (int i = 0; i < lockdownSpeed; i++) {
                    t.position -= Vector3.up * (1.0f / lockdownSpeed) * t.localScale.y;
                    yield return new WaitForSeconds(0.01f);
                }
            }
        }
        else {
            foreach (Transform t in dropDownDoors) {
                for (int i = 0; i < lockdownSpeed; i++) {
                    t.position += Vector3.up * (1.0f / lockdownSpeed) * t.localScale.y;
                    yield return new WaitForSeconds(0.01f);
                }
            }
        }
    }
}
