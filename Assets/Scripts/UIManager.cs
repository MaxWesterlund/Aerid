using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {
    Stats stats;

    RectTransform loadingScreenTransform;
    RectTransform spinnerTransform;

    RectTransform scoreScreenTransform;

    [Header("Loading Screen")]
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject spinner;
    
    [Header("Score Screen")]
    [SerializeField] GameObject scoreScreen;
    [SerializeField] TMP_Text timeSurvivedScore;
    [SerializeField] TMP_Text missilesDestroyedScore;
    [SerializeField] TMP_Text totalScoreScore;

    [SerializeField] Button restartButton;
    [SerializeField] Button exitButton;

    [SerializeField] float delay;
    [SerializeField] float closeSpeed;
    [SerializeField] float spinSpeed;

    [SerializeField] float scoreCountTime;
    [SerializeField] float scoreDecimals;

    bool showStatsScreen = false;
    bool showLoadingScreen = true;

    float spinLerp = 0;
    float lerp = 0;

    void Awake() {
        if (GameObject.Find("Game Manager") != null) {
            GameObject obj = GameObject.Find("Game Manager");
            GameManager gameManager = obj.GetComponent<GameManager>();
            gameManager.Collision += OnCollision;
            gameManager.GenerationFinished += OnGenerationFinished;
            stats = obj.GetComponent<Stats>();
        }
        loadingScreenTransform = loadingScreen.GetComponent<RectTransform>();
        spinnerTransform = spinner.GetComponent<RectTransform>();

        scoreScreenTransform = scoreScreen.GetComponent<RectTransform>();
    }

    void OnCollision() {
        StartCoroutine(ShowStats());
    }

    IEnumerator ShowStats() {
        yield return new WaitForSeconds(delay);
        showStatsScreen = true;
        lerp = 0;

        timeSurvivedScore.text = System.Math.Round(stats.TimeSurvived, 2).ToString() + "s";
        missilesDestroyedScore.text = stats.MissilesDestroyed.ToString();
        totalScoreScore.text = System.Math.Round(stats.TotalScore, 2).ToString();
    }
    
    void OnGenerationFinished() {
        showLoadingScreen = false;
    }

    void Update() {
        if (showLoadingScreen) {
            Spin();
        }
        else {
            spinner.SetActive(false);
            ShowScreen(false, loadingScreenTransform);
        }

        if (showStatsScreen) {
            ShowScreen(true, scoreScreenTransform);
        }
    }

    void Spin() {
        spinLerp += Time.deltaTime;
        float zRot = Mathf.SmoothStep(spinnerTransform.rotation.z, 360, spinSpeed * spinLerp);
        if (spinLerp >= 1) {
            spinLerp = 0;
        }
        spinnerTransform.rotation = Quaternion.Euler(0, 0, zRot);
    }

    void ShowScreen(bool show, RectTransform transform) {
        lerp += Time.deltaTime;
        float closeAmount = Mathf.SmoothStep(transform.offsetMin.y, 600 * (show? 0 : 1), closeSpeed * lerp);
        transform.offsetMin = new Vector2(transform.offsetMin.x, closeAmount);
        transform.offsetMax = new Vector2(transform.offsetMax.x, closeAmount);
    }
}