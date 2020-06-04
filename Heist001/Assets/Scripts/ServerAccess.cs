using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerAccess : MonoBehaviour
{
    public SecurityLevel securityLevelAcess;
    public ServerPuzzle serverPuzzle;
    public AudioSource audioSource;
    public AudioClip succesClip, failedClip;
    public GameObject serverMesh;

    public void Connect(HackerDevice device) {
        serverMesh.GetComponent<Renderer>().material.color = Color.yellow;
        serverPuzzle.StartPuzzle(device, this);
    }

    public void Access() {
        audioSource.clip = succesClip;
        audioSource.Play();
        serverMesh.GetComponent<Renderer>().material.color = Color.green;
        GameManager.gameManager.UgradeSecurityLevel(securityLevelAcess);
    }

    public void Failed() {
        audioSource.clip = failedClip;
        serverMesh.GetComponent<Renderer>().material.color = Color.red;
        GameManager.gameManager.UpgradeDangerLevel(1);
    }
}
