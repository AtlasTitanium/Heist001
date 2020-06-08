using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public CameraControl cameraController;
    public Transform body;
    public HackerDevice hackingDevice;
    public InventoryManager inventoryManager;
    public GameObject menuCanvas;
    public GameObject deathOverlay;
    public int inventorySize = 4;
    public ArtBehaviour[] items;
    public int currentItemSlot = 0;
    public float reachDistance = 3;
    public bool hackerDeviceActive = false;

    private void Start() {
        items = new ArtBehaviour[inventorySize];
        inventoryManager.SetCurrentItemSlot(currentItemSlot);

        menuCanvas.GetComponent<MenuManager>().SetValues();
    }

    private void Update() {
        if (!hackerDeviceActive) {
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, reachDistance)) {
                    if (hit.transform.GetComponent<CallerBehaviour>()) {
                        hit.transform.GetComponent<CallerBehaviour>().Call();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.F)) {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, reachDistance)) {
                    if (hit.transform.GetComponent<ArtBehaviour>()) {
                        ArtBehaviour art = hit.transform.GetComponent<ArtBehaviour>();
                        if (items[currentItemSlot] != null) {
                            items[currentItemSlot].Drop(transform);
                        }
                        items[currentItemSlot] = art;
                        art.Pickup(body);
                        inventoryManager.SetInventoryImage(currentItemSlot, art.inventoryIcon);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Q)) {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + transform.forward, -transform.forward, out hit, 1)) {
                    Debug.Log("Wall");
                } else {
                    if (items[currentItemSlot] != null) {
                        items[currentItemSlot].Drop(transform);
                        inventoryManager.RemoveInventoryImage(currentItemSlot);
                        items[currentItemSlot] = null;
                    }
                }
                
            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0) {
                currentItemSlot += (int)(Input.GetAxis("Mouse ScrollWheel") * 10);
                if (currentItemSlot >= inventorySize) {
                    currentItemSlot = 0;
                }
                if (currentItemSlot <= -1) {
                    currentItemSlot = inventorySize - 1;
                }

                inventoryManager.SetCurrentItemSlot(currentItemSlot);
            }

            if (Input.GetKeyDown(KeyCode.Escape)) {
                PauseGame();
            }
        } else {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, reachDistance)) {
                if (hit.transform.GetComponent<ServerAccess>()) {
                    hackingDevice.ServerNearby();
                    if (Input.GetMouseButtonDown(0)) {
                        hit.transform.GetComponent<ServerAccess>().Connect(hackingDevice);
                    }
                } else {
                    hackingDevice.ResetScreen();
                }
                if (hit.transform.GetComponent<DoorBehaviour>()) {
                    if (GameManager.gameManager.securityLevelAcess < hit.transform.GetComponent<DoorBehaviour>().securityLevelAcess) {
                        hackingDevice.ShowInfo("This door requires Level: " + (int)hit.transform.GetComponent<DoorBehaviour>().securityLevelAcess + " access.\nFind a server and hack it for higher level access");
                    }
                }
            } else {
                hackingDevice.ResetScreen();
            }
            
        }

        if (Input.GetMouseButtonDown(1)) {
            hackingDevice.GetComponent<Animator>().SetBool("Holding", !hackingDevice.GetComponent<Animator>().GetBool("Holding"));
            hackerDeviceActive = !hackerDeviceActive;
        }
    }

    public void PauseGame() {
        inventoryManager.gameObject.SetActive(!inventoryManager.gameObject.activeSelf);
        menuCanvas.SetActive(!menuCanvas.activeSelf);
        cameraController.enabled = !menuCanvas.activeSelf;
        body.GetComponent<Movement>().enabled = !menuCanvas.activeSelf;
        if (menuCanvas.activeSelf) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
