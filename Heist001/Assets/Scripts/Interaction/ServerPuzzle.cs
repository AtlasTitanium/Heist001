using UnityEngine;

public class ServerPuzzle : MonoBehaviour {
    private ServerAccess server;
    public virtual void StartPuzzle(HackerDevice device, ServerAccess _server) {
        server = _server;
    }

    public virtual void PuzzleFinished() {
        server.Access();
    }

    public virtual void PuzzleFailed() {
        server.Failed();
    }
}
