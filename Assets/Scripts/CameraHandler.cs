using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour {
    [SerializeField] GameObject HUDCamera;

    void Awake() {
        if (GameObject.Find("Game Manager") != null) {
            GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            gameManager.Collision += OnCollision;
            gameManager.GenerationFinished += OnGenerationFinished;
        }
        HUDCamera.SetActive(false);
    }

    void OnHit() {
        HUDCamera.SetActive(false);
    }

    void OnCollision() {
        HUDCamera.SetActive(false);
    }

    void OnGenerationFinished() {
        HUDCamera.SetActive(true);
    }
}