using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapDoorBehaviour : TriggerBehaviour
{
    public GameObject trapDoor;
    public Scene leadingScene;

    public override void Trigger() {
        StartCoroutine(OpenTrapdoor());
    }

    IEnumerator OpenTrapdoor() {
        for (int i = 0; i < 46; i++) {
            trapDoor.transform.Rotate(transform.forward, 2f);
            yield return new WaitForSeconds(0.01f);
        }
        GameManager.gameManager.NextScene(leadingScene);
    }
}
