using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkPit : TriggerBehaviour
{
    public Transform[] dropDownDoors;
    public AudioSource buttonIndication;
    public OpenFloorTest dangerFloor;
    public int lockdownSpeed = 10;
    private Collider col;

    private void Start() {
        col = GetComponent<Collider>();
    }

    public override void Trigger() {
        TrapDeactivate();
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
        buttonIndication.Play();
    }

    public void TrapDeactivate() {
        StartCoroutine(Lockdown(false));
        dangerFloor.Deactivate();
        buttonIndication.Stop();
    }

    IEnumerator Lockdown(bool active) {
        if (active) {
            foreach (Transform t in dropDownDoors) {
                t.gameObject.SetActive(true);
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
                t.gameObject.SetActive(false);
            }
        }
    }
}
