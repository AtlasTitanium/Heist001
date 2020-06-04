using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathGround : MonoBehaviour
{
    public float timeTillFail = 1.0f;
    private float deathCount = 0;
    private Interactor player;

    private void OnTriggerEnter(Collider other) {
        deathCount = Time.time;
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player") {
            player = other.gameObject.GetComponentInChildren<Interactor>();
            player.deathOverlay.GetComponent<Image>().color = new Color(1, 0, 0, (Time.time - deathCount) / timeTillFail);
            if (Time.time >= (deathCount + timeTillFail)) {
                GameManager.gameManager.Fail();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (player != null) {
            player.deathOverlay.GetComponent<Image>().color = new Color(1, 0, 0, 0);
            player = null;
        }
    }
}
