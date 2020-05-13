using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform body;
    public GameObject hackingDevice;
    public InventoryManager inventoryManager;
    public GameObject menuCanvas;
    public int inventorySize = 4;
    public ArtBehaviour[] items;
    public int currentItemSlot = 0;

    private void Start() {
        items = new ArtBehaviour[inventorySize];
        inventoryManager.SetCurrentItemSlot(currentItemSlot);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, 2)) {
                if (hit.transform.GetComponent<CallerBehaviour>()) {
                    hit.transform.GetComponent<CallerBehaviour>().Call();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2)) {
                if (hit.transform.GetComponent<ArtBehaviour>()) {
                    ArtBehaviour art = hit.transform.GetComponent<ArtBehaviour>();
                    if(items[currentItemSlot] != null) {
                        items[currentItemSlot].Drop(transform);
                    }
                    items[currentItemSlot] = art;
                    art.Pickup(body);
                    inventoryManager.SetInventoryImage(currentItemSlot, art.inventoryIcon);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            if (items[currentItemSlot] != null) {
                items[currentItemSlot].Drop(transform);
                inventoryManager.RemoveInventoryImage(currentItemSlot);
                items[currentItemSlot] = null;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0) {
            currentItemSlot +=  (int)(Input.GetAxis("Mouse ScrollWheel") * 10);
            if(currentItemSlot >= inventorySize) {
                currentItemSlot = 0;
            }
            if(currentItemSlot <= -1) {
                currentItemSlot = inventorySize - 1;
            }

            inventoryManager.SetCurrentItemSlot(currentItemSlot);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame();
        }

        if (Input.GetMouseButtonDown(1)) {
            hackingDevice.GetComponent<Animator>().SetBool("Holding", !hackingDevice.GetComponent<Animator>().GetBool("Holding"));
        }
    }

    public void PauseGame() {
        inventoryManager.gameObject.SetActive(!inventoryManager.gameObject.activeSelf);
        menuCanvas.SetActive(!menuCanvas.activeSelf);
        if (menuCanvas.activeSelf) {
            Time.timeScale = 0.0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else {
            Time.timeScale = 1.0f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
