using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform body;
    float xRot;
    float sensitivity = 2f;

    private void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(GameManager.gameManager != null) {
            sensitivity = GameManager.gameManager.mouseSensitivity;
        }

        body.Rotate(body.up, Input.GetAxis("Mouse X") * sensitivity);

        xRot -= Input.GetAxis("Mouse Y") * sensitivity;
        xRot = Mathf.Clamp(xRot, -90, 85);
        transform.localEulerAngles = new Vector3(xRot, 0, 0);
    }
}
