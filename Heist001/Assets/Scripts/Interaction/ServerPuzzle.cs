using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerPuzzle : MonoBehaviour {
    private ServerAccess server;

    public GameObject routersParent;
    public CallerBehaviour[] routers;
    private int amountOfRouters;

    private void Start() {
        foreach(CallerBehaviour router in routers){
            router.OnCall += RouterActivated;
        }
        amountOfRouters = routers.Length;
    }

    public void StartPuzzle(HackerDevice device, ServerAccess _server) {
        routersParent.SetActive(true);
        server = _server;
        device.ShowInfo("Server acces needed: Please activate all 3 router switches to open access for security level codes");
    }

    public void RouterActivated() {
        amountOfRouters--;
        if(amountOfRouters <= 0) {
            server.Access();
        }
    }
}
