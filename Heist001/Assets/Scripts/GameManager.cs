using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public enum SecurityLevel {
    Level0 = 0,
    Level1 = 1,
    Level2 = 2,
    Level3 = 3
}

public class GameManager : MonoBehaviour {
    #region Singleton
    public static GameManager gameManager { get; private set; } = null;

    private void Awake() {
        if (gameManager != null && gameManager != this) {
            Destroy(this.gameObject);
            return;
        }

        gameManager = this;
        DontDestroyOnLoad(this);
    }
    #endregion

    [HideInInspector]
    public float mouseSensitivity;
    [HideInInspector]
    public bool failed = false;
    [HideInInspector]
    public int dangerLevel = 0;

    public SecurityLevel securityLevelAcess = SecurityLevel.Level0;
    public AudioMixer mixer;
    public LoadScene loader;
    public int maxDangerLevel;
    public GameObject finishedCanvas;
    public Text scoreInfo;

    private float timeSinceStart;

    public void SetMasterVolume(float volume) {
        mixer.SetFloat("MasterVolume", volume.Remap(0f, 1f, -80f, 20f));
    }
    public float GetMasterVolume() {
        float volume = 0;
        mixer.GetFloat("MasterVolume", out volume);
        return volume.Remap(-80f, 20f, 0f, 1f);
    }
    public void SetMouseSensitivity(float sensitivity) {
        mouseSensitivity = sensitivity.Remap(0f, 1f, 0.1f, 10f);
    }
    public float GetMouseSensitivity() {
        return mouseSensitivity.Remap(0.1f, 10f, 0f, 1f);
    }

    public void NextScene(Scene nextScene) {
        loader.LoadLoader(nextScene.handle);
    }

    public void UgradeSecurityLevel(SecurityLevel level) {
        securityLevelAcess = level;
    }

    public void UpgradeDangerLevel(int upWithAmount) {
        dangerLevel += upWithAmount;
        if(dangerLevel >= maxDangerLevel) {
            ActivateAlarm();
        }
    }

    public void Finish(int artsValue) {
        loader.LoadLoader(0);

        int seconds = (int)(Time.time - timeSinceStart);
        int minutes = 0;
        while(seconds >= 60) {
            minutes++;
            seconds -= 60;
        }

        int score = artsValue - (int)((dangerLevel / maxDangerLevel) * 50) - (int)(minutes / 2);

        scoreInfo.text = "Art value: " + artsValue + ".000$\nAlarm Level: " + dangerLevel + "\nTime: " + minutes +":" + seconds +"\n\nFull score: " + score;
        finishedCanvas.SetActive(true);
    }

    public void StartGame() {
        timeSinceStart = Time.time;
    }

    public void ActivateAlarm() {
        Debug.Log("alarm activated");
    }

    public void Fail() {
        if (!failed) {
            failed = true;
            Debug.Log("Game over = restart (DEBUG)" + SceneManager.GetActiveScene().buildIndex);
            loader.LoadLoader(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void FinishAndReset() {
        finishedCanvas.SetActive(false);
    }

}

public static class ExtensionMethods {

    public static float Remap(this float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}
