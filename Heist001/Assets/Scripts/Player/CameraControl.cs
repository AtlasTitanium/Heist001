using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float sensitivity;
    public Transform body;
    float xRot;

    private void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float lookSensitivity = sensitivity * 100;
        body.Rotate(body.up, Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime);

        xRot -= Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime;
        xRot = Mathf.Clamp(xRot, -90, 85);
        transform.localEulerAngles = new Vector3(xRot, 0, 0);
    }
}
