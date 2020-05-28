using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackerDevice : MonoBehaviour
{
    public Renderer screen;
    public GameObject textBlock;
    public Text textBlockText;
    public Text levelAccessText;
    public Color redScreen;
    private Color ogColor;

    private void Start() {
        ogColor = screen.material.GetColor("_EmissionColor");
    }

    private void Update() {
        if(GameManager.gameManager != null) {
            levelAccessText.text = "Current Access Level: " + (int)GameManager.gameManager.securityLevelAcess;
        }
    }

    public void ServerNearby() {
        screen.material.SetColor("_EmissionColor", redScreen);
    }

    public void ResetScreen() {
        screen.material.SetColor("_EmissionColor", ogColor);
    }

    public void ShowInfo(string info) {
        StopAllCoroutines();
        StartCoroutine(ShowInfoFor(10,info));
    }

    IEnumerator ShowInfoFor(int time, string info) {
        textBlock.SetActive(true);
        textBlockText.text = info;
        yield return new WaitForSeconds(time);
        textBlock.SetActive(false);
    }
}
