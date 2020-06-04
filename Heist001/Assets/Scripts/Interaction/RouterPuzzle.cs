using UnityEngine;

public class RouterPuzzle : ServerPuzzle {

    public GameObject routersParent;
    public CallerBehaviour[] routers;
    private int amountOfRouters;

    private void Start() {
        foreach (CallerBehaviour router in routers) {
            router.OnCall += RouterActivated;
        }
        amountOfRouters = routers.Length;
    }

    public override void StartPuzzle(HackerDevice device, ServerAccess _server) {
        base.StartPuzzle(device, _server);

        routersParent.SetActive(true);
        device.ShowInfo("Server acces needed: Please activate all 3 router switches to open access for security level codes");
    }

    public void RouterActivated() {
        amountOfRouters--;
        if (amountOfRouters <= 0) {
            base.PuzzleFinished();
        }
    }
}
