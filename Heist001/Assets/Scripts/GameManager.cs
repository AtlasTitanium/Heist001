using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public enum SecurityLevel {
    Level0 = 0,
    Level1 = 1,
    Level2 = 2
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

    public SecurityLevel securityLevelAcess = SecurityLevel.Level0;
    public AudioMixer mixer;
    public float mouseSensitivity;
    public LoadScene loader;

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
        loader.LoadLoader(nextScene);
    }

    public void UgradeSecurityLevel(SecurityLevel level) {
        securityLevelAcess = level;
    }

    public void Fail() {
        Debug.Log("Game over = restart (DEBUG)");
        loader.LoadLoader(SceneManager.GetActiveScene());
    }

}

public static class ExtensionMethods {

    public static float Remap(this float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}
