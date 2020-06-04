using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerPuzzle : ServerPuzzle {

    public GameObject[] lazers;
    public CallerBehaviour[] buttons;
    private int amountOfButtons;
    private int currentButton = 0;
    private HackerDevice hackerDevice;

    private void Start() {
        foreach (CallerBehaviour button in buttons) {
            button.OnCall += ButtonClicked;
        }
        amountOfButtons = buttons.Length;
    }

    public override void StartPuzzle(HackerDevice device, ServerAccess _server) {
        base.StartPuzzle(device, _server);
        hackerDevice = device;

        lazers[currentButton].SetActive(true);
        StartCoroutine(CountDown(5));
    }

    public void ButtonClicked() {
        lazers[currentButton].SetActive(false);
        StopAllCoroutines();
        currentButton++;
        amountOfButtons--;
        if (amountOfButtons <= 0) {
            hackerDevice = null;
            base.PuzzleFinished();
        } else {
            lazers[currentButton].SetActive(true);
            StartCoroutine(CountDown(5));
        }
    }

    IEnumerator CountDown(int count) {
        for (int i = 0; i < count; i++) {
            hackerDevice.ShowInfo("Server acces needed: You have " + (count - i) + " seconds to flip the next switch!");
            yield return new WaitForSeconds(1);
        }
        PuzzleFailed();
    }
}
