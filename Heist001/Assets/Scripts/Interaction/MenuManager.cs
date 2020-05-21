using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public int mainMenuSceneInteger;

    public void ExitLevel() {
        SceneManager.LoadScene(mainMenuSceneInteger);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
