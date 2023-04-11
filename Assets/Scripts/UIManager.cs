using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    RectTransform backgroundTransform;
    RectTransform spinnerTransform;

    [SerializeField] GameObject background;
    [SerializeField] GameObject spinner;

    [SerializeField] float closeSpeed;
    [SerializeField] float spinSpeed;

    bool hasGenerated = false;

    float spinLerp = 0;
    float closeLerp = 0;

    void Awake() {
        if (GameObject.Find("Game Manager") != null) {
            GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            gameManager.GenerationFinished += OnGenerationFinished;
        }
        backgroundTransform = background.GetComponent<RectTransform>();
        spinnerTransform = spinner.GetComponent<RectTransform>();
    }
    
    void OnGenerationFinished() {
        hasGenerated = true;
    }

    void Update() {
        if (!hasGenerated) {
            Spin();
        }
        else {
            spinner.SetActive(false);
            Close();
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

    void Close() {
        closeLerp += Time.deltaTime;
        float closeAmount = Mathf.SmoothStep(backgroundTransform.offsetMin.y, 600, closeSpeed * closeLerp);
        backgroundTransform.offsetMin = new Vector2(backgroundTransform.offsetMin.x, closeAmount);
        backgroundTransform.offsetMax = new Vector2(backgroundTransform.offsetMax.x, closeAmount);
    }
}