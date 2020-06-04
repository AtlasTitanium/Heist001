using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SteamBehaviour : TriggerBehaviour
{
    public ParticleSystem[] steamEffects;
    public float timeTillFail = 1.0f;
    public bool activeGass = false;
    private Collider col;
    private float deathCount = 0;
    private Interactor player;

    private void Start() {
        col = GetComponent<Collider>();
        if (activeGass){
            Activate();
        } else {
            StartCoroutine(GassLeak());
        }
    }

    public override void Trigger() {
        base.Trigger();

        activeGass = false;
        Debug.Log("GasLever");
        Deactivate();
        StartCoroutine(GassLeak());
    }

    public void Activate() {
        foreach(ParticleSystem system in steamEffects) {
            system.Play();
        }
        col.enabled = true;
    }

    public void Deactivate() {
        foreach (ParticleSystem system in steamEffects) {
            system.Stop();
        }
        col.enabled = false;
        if (player != null) {
            player.deathOverlay.GetComponent<Image>().color = new Color(1, 0, 0, 0);
            player = null;
        }
    }

    IEnumerator GassLeak() {
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        Activate();
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        Deactivate();
        StartCoroutine(GassLeak());
    }

    private void OnTriggerEnter(Collider other) {
        deathCount = Time.time;
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Player") {
            player = other.gameObject.GetComponentInChildren<Interactor>();
            player.deathOverlay.GetComponent<Image>().color = new Color(1, 0, 0, (Time.time - deathCount) / timeTillFail);
            if (Time.time >= (deathCount + timeTillFail)) {
                GameManager.gameManager.Fail();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(player != null) {
            player.deathOverlay.GetComponent<Image>().color = new Color(1, 0, 0, 0);
            player = null;
        }
    }
}
