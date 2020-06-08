using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatPuzzle : MonoBehaviour
{
    public GameObject heater;
    public int time = 10;
    public float steps = 100.0f;

    public CallButton[] buttonsInOrder;
    public GameObject[] lightsInOrder;
    public AudioSource audioSource;
    public AudioClip succesClip, failClip;

    private HallwayTrap hallwayTrap;
    private Interactor player;
    private Renderer[] heaterElements;
    private int currentButton = 0;

    private void Start() {
        heaterElements = new Renderer[heater.transform.childCount];
        for (int i = 0; i < heater.transform.childCount; i++) {
            heaterElements[i] = heater.transform.GetChild(i).GetComponent<Renderer>();
        }

        foreach (CallButton button in buttonsInOrder) {
            button.OnCall += ClickButton;
            button.enabled = false;
        }

        buttonsInOrder[currentButton].enabled = true;
    }

    private void ClickButton() {
        buttonsInOrder[currentButton].gameObject.SetActive(false);
        lightsInOrder[currentButton].SetActive(false);
        currentButton++;

        if (currentButton == buttonsInOrder.Length) {
            FinishedPuzzle();
        } else {
            buttonsInOrder[currentButton].gameObject.SetActive(true);
            buttonsInOrder[currentButton].enabled = true;
        }
    }

    public void StartPuzzle(HallwayTrap trap) {
        hallwayTrap = trap;
        Collider[] cols = Physics.OverlapBox(transform.position - transform.forward * 2, new Vector3(4, 4, 4));
        foreach(Collider col in cols) {
            if (col.GetComponentInChildren<Interactor>()) {
                Debug.Log("got player");
                player = col.GetComponentInChildren<Interactor>();
            }
        }
        
        StartCoroutine(HeatHeater());

        foreach(CallButton button in buttonsInOrder) {
            button.gameObject.SetActive(true);
        }
    }

    private void FinishedPuzzle() {
        StopAllCoroutines();
        audioSource.clip = succesClip;
        audioSource.Play();
        hallwayTrap.TrapDeactivate();
        player = null;

        foreach (CallButton button in buttonsInOrder) {
            button.gameObject.SetActive(false);
        }
    }

    IEnumerator HeatHeater() {
        for (int i = 0; i < steps; i++) {
            foreach (Renderer r in heaterElements) {
                r.material.color = new Color((i/steps)*2, r.material.color.g, r.material.color.b);
            }
            Debug.Log("heating up");
            Image overlay = player.deathOverlay.GetComponent<Image>();
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, i/100);
            yield return new WaitForSeconds(time / steps);
        }
        GameManager.gameManager.Fail();
    }
}
