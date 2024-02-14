using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeOut : MonoBehaviour
{
    public Slider timeSlider;
    public RectTransform defeatUI;
    public float maxTime = 20f;
    private float currentTime;

    void Start() {
        currentTime = maxTime;
    }

    void Update() {
        currentTime -= Time.deltaTime; // 현재 시간 감소

        timeSlider.value = currentTime / maxTime;

        if(currentTime <= 0f) {
            currentTime = 0f;

            // 시간 종료되면 패배UI
            // defeatUI.gameObject.SetActive(true);
            // Time.timeScale = 0f;
            SceneManager.LoadScene("MainScene");
        }
    }
}
