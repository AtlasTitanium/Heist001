using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerAccess : MonoBehaviour
{
    public SecurityLevel securityLevelAcess;
    public ServerPuzzle serverPuzzle;

    public void Connect(HackerDevice device) {
        serverPuzzle.StartPuzzle(device, this);
    }

    public void Access() {
        Debug.Log("Finished server");
        GameManager.gameManager.UgradeSecurityLevel(securityLevelAcess);
    }
}
