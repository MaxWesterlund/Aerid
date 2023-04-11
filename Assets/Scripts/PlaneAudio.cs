using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlaneAudio : MonoBehaviour {
    Rigidbody rb;
    [SerializeField] AudioSource planeSound;
    [SerializeField] AudioSource windSound;
 
    void Awake() {
        rb = GetComponent<Rigidbody>();
        if (GameObject.Find("Game Manager") != null) {
            GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            gameManager.Hit += OnHit;
            gameManager.Collision += OnCollision;
            gameManager.GenerationFinished += OnGenerationFinished;
        }
    }

    void OnHit() {
        planeSound.Stop();
    }

    void OnCollision() {
        planeSound.Stop();
        windSound.Stop();
    }

    void OnGenerationFinished() {
        planeSound.Play();
        windSound.Play();
    }

    void Update() {
        windSound.volume = .3f + Mathf.Abs(transform.forward.y);
    }
}