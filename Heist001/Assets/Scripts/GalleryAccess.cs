using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryAccess : MonoBehaviour
{
    public CallerBehaviour call;
    public Camera screenCam;
    public GameObject canvas;
    public GameObject[] correctLights;
    public Color rightLightColor;

    private GameObject mainCamera;
    private Interactor player;
    private Vector3 screenCamPos, screenCamRot;

    private void Start() {
        call.OnCall += ChangeCam;
        screenCamPos = screenCam.transform.position;
        screenCamRot = screenCam.transform.eulerAngles;
        mainCamera = Camera.main.gameObject;

        foreach (GameObject light in correctLights) {
            light.SetActive(false);
        }
    }

    private void ChangeCam() {
        screenCam.transform.position = mainCamera.transform.position;
        EnablePlayer(false);
        screenCam.gameObject.SetActive(true);
        StartCoroutine(MoveCam());
    }

    private void EnablePlayer(bool enable) {
        player = mainCamera.GetComponentInChildren<Interactor>();
        player.GetComponentInParent<CameraControl>().enabled = enable;
        player.GetComponentInParent<Movement>().enabled = enable;
        player.enabled = enable;
        mainCamera.GetComponent<Camera>().enabled = enable;
        

        Cursor.visible = !enable;
        if (!Cursor.visible) {
            Cursor.lockState = CursorLockMode.Locked;
        } else {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    IEnumerator MoveCam() {
        float steps = 50.0f;
        for (int i = 0; i < steps; i++) {
            screenCam.transform.position = Vector3.Lerp(mainCamera.transform.position, screenCamPos, i/ steps);
            screenCam.transform.eulerAngles = Vector3.Lerp(mainCamera.transform.eulerAngles, screenCamRot, i/ steps);
            yield return new WaitForSeconds(1/ steps);
            Debug.Log("screencam");
        }
        canvas.SetActive(true);
    }

    public void ExitSystem() {
        screenCam.gameObject.SetActive(false);
        canvas.SetActive(false);
        EnablePlayer(true);

        foreach(GameObject light in correctLights) {
            light.SetActive(true);
            light.GetComponent<Light>().color = rightLightColor;
        }
    }
}
