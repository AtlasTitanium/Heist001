using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Text progressPercent;
    public Slider progressBar;
    public GameObject loadBar;
    public Image blackoutImage;

    private Scene nextScene;
    private AsyncOperation operation;

    public void LoadLoader(Scene scene) {
        nextScene = scene;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn() {
        UpdateProgressUI(0);
        blackoutImage.gameObject.SetActive(true);
        loadBar.SetActive(true);
        for (int i = 0; i < 20; i++) {
            blackoutImage.color = new Color(blackoutImage.color.r, blackoutImage.color.g, blackoutImage.color.b, i / 20.0f);
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine(BeginLoad());
    }

    IEnumerator FadeOut() {
        UpdateProgressUI(1);
        for (int i = 20; i > 0; i--) {
            blackoutImage.color = new Color(blackoutImage.color.r, blackoutImage.color.g, blackoutImage.color.b, i / 20.0f);
            yield return new WaitForSeconds(0.01f);
        }
        loadBar.SetActive(false);
        blackoutImage.gameObject.SetActive(false);
    }

    IEnumerator BeginLoad() {
        operation = SceneManager.LoadSceneAsync(nextScene.handle);

        while (!operation.isDone) {
            UpdateProgressUI(operation.progress);
            yield return null;
        }
        
        operation = null;
        StartCoroutine(FadeOut());
    }

    private void UpdateProgressUI(float percent) {
        progressBar.value = percent;
        progressPercent.text = (int)(percent * 100f) + "%";
    }
}
