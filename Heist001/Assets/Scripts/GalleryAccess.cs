using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryAccess : MonoBehaviour
{
    public CallerBehaviour call;
    public Camera screenCam;

    private Interactor player;
    private Vector3 screenCamPos, screenCamRot;

    private void Start() {
        call.OnCall += ChangeCam;
        screenCamPos = screenCam.transform.position;
        screenCamRot = screenCam.transform.eulerAngles;
    }

    private void ChangeCam() {
        EnablePlayer(false);
        screenCam.transform.position = Camera.main.transform.position;
        StartCoroutine(MoveCam());
    }

    private void EnablePlayer(bool enable) {
        player = Camera.main.GetComponentInChildren<Interactor>();
        player.GetComponentInParent<CameraControl>().enabled = enable;
        player.GetComponentInParent<Movement>().enabled = enable;
        player.enabled = enable;
        Camera.main.enabled = enable;
        screenCam.gameObject.SetActive(!enable);

        Cursor.visible = !enable;
        if (!Cursor.visible) {
            Cursor.lockState = CursorLockMode.Locked;
        } else {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    IEnumerator MoveCam() {
        for (int i = 0; i < 20; i++) {
            screenCam.transform.position = Vector3.Lerp(Camera.main.transform.position, screenCamPos, i/20);
            screenCam.transform.eulerAngles = Vector3.Lerp(Camera.main.transform.eulerAngles, screenCamPos, i/20);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void ExitSystem() {
        EnablePlayer(true);
    }
}
