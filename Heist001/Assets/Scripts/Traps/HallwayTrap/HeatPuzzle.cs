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
        }
    }

    private void ClickButton() {
        if (buttonsInOrder[currentButton].enabled) {
            audioSource.clip = failClip;
            audioSource.Play();
            foreach(CallButton button in buttonsInOrder) {
                button.enabled = true;
            }
            foreach (GameObject light in lightsInOrder) {
                light.SetActive(true);
            }
        } else {
            buttonsInOrder[currentButton].enabled = false;
            lightsInOrder[currentButton].SetActive(false);
            currentButton++;
        }

        if(currentButton == buttonsInOrder.Length) {
            FinishedPuzzle();
        }
    }

    public void StartPuzzle(HallwayTrap trap) {
        hallwayTrap = trap;
        player = FindObjectOfType<Interactor>();
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
                r.material.color = new Color(i/steps, r.material.color.g, r.material.color.b);
            }
            Image overlay = player.deathOverlay.GetComponent<Image>();
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, i/100);
            yield return new WaitForSeconds(steps / time);
        }
        GameManager.gameManager.Fail();
    }
}
