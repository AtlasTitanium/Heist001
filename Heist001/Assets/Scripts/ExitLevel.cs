using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    public CallerBehaviour caller;
    public Canvas confirmationCanvas;

    private Interactor player;

    private void Start() {
        confirmationCanvas.enabled = false;
        caller.OnCall += OpenCanvas;
    }

    private void OpenCanvas() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        player = Camera.main.GetComponentInChildren<Interactor>();
        player.GetComponentInParent<Movement>().enabled = false;
        player.GetComponentInParent<CameraControl>().enabled = false;
        player.enabled = false;

        confirmationCanvas.enabled = true;
    }

    public void ExitGame() {
        GameManager.gameManager.Finish(player.GetArtValue());
    }

    public void Continue() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player.GetComponentInParent<Movement>().enabled = true;
        player.GetComponentInParent<CameraControl>().enabled = true;
        player.enabled = true;
        player = null;

        confirmationCanvas.enabled = false;
    }
}
