using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Scene mainMenuScene;

    public Slider mouseSensitivity;
    public Slider audioVolume;

    public void SetValues() {
        mouseSensitivity.value = GameManager.gameManager.GetMouseSensitivity();
        audioVolume.value = GameManager.gameManager.GetMasterVolume();
    }

    private void Start() {
        audioVolume.onValueChanged.AddListener(delegate { GameManager.gameManager.SetMasterVolume(audioVolume.value); });
        mouseSensitivity.onValueChanged.AddListener(delegate { GameManager.gameManager.SetMouseSensitivity(mouseSensitivity.value); });
    }

    public void ExitLevel() {
        GameManager.gameManager.NextScene(mainMenuScene);
    }
}
