using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Scene startingScene;
    
    public Button startGameBtn;
    public Button exitGameBtn;

    public Slider mouseSensitivity;
    public Slider audioVolume;

    void Start()
    {
        exitGameBtn.onClick.AddListener(ExitGame);
        startGameBtn.onClick.AddListener(StartGame);

        mouseSensitivity.value = GameManager.gameManager.GetMouseSensitivity();
        audioVolume.value = GameManager.gameManager.GetMasterVolume();
        audioVolume.onValueChanged.AddListener(delegate { GameManager.gameManager.SetMasterVolume(audioVolume.value); });
        mouseSensitivity.onValueChanged.AddListener(delegate { GameManager.gameManager.SetMouseSensitivity(mouseSensitivity.value); });
    }
    

    void StartGame() {
        GameManager.gameManager.NextScene(startingScene);
    }

    void ExitGame() {
        Application.Quit();
    }
}
