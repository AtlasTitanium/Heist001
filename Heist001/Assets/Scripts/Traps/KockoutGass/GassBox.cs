using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GassBox : MonoBehaviour
{
    public GameObject fanOpening;
    public GameObject gassBlock;
    public float gasSpeed = 0.1f;

    private KnockoutGass trapParent;
    private bool trapActive = false;
    private GameObject blockingArt;
    private Vector3 ogGasPos;

    private void Start() {
        ogGasPos = gassBlock.transform.position;
    }

    public void Update() {
        if (trapActive) {
            if(Physics.CheckSphere(fanOpening.transform.position, 2)) {
                Collider[] closeObjects = Physics.OverlapSphere(fanOpening.transform.position, 2);
                foreach(Collider c in closeObjects) {
                    if (c.GetComponent<ArtBehaviour>()) {
                        blockingArt = c.gameObject;
                        StartCoroutine(BreakBox());
                        trapActive = false;
                    }
                }
            }
            gassBlock.transform.position += Vector3.up * Time.deltaTime * gasSpeed;
            if(gassBlock.transform.position.y >= ogGasPos.y + 4) {
                GameManager.gameManager.Fail();
            }
        }
    }

    public void ActivateGass(KnockoutGass trap) {
        trapParent = trap;
        trapActive = true;
    }

    IEnumerator BreakBox() {
        Vector3 gassBlockPosition = gassBlock.transform.position;
        for (int i = 0; i < 10; i++) {
            gassBlock.transform.position = Vector3.Lerp(gassBlockPosition, ogGasPos, i / 20.0f);
            fanOpening.transform.localScale += Vector3.one * 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
        trapParent.TrapDeactivate();
    }
}
