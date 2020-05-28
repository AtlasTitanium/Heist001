using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerAccess : MonoBehaviour
{
    public SecurityLevel securityLevelAcess;
    public ServerPuzzle serverPuzzle;
    public AudioSource audioSource;
    public GameObject serverMesh;

    public void Connect(HackerDevice device) {
        serverMesh.GetComponent<Renderer>().material.color = Color.red;
        serverPuzzle.StartPuzzle(device, this);
    }

    public void Access() {
        audioSource.Play();
        serverMesh.GetComponent<Renderer>().material.color = Color.green;
        GameManager.gameManager.UgradeSecurityLevel(securityLevelAcess);
    }
}
