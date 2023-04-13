using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {
    public float TimeSurvived;
    public int MissilesDestroyed;
    public float TotalScore;

    bool countTime = false;

    void Awake() {
        if (GameObject.Find("Game Manager") != null) {
            GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            gameManager.MissileDestroyed += OnMissileDestroyed;
            gameManager.Hit += OnHit;
            gameManager.Collision += OnCollision;
            gameManager.GenerationFinished += OnGenerationFinished;
        }
    }

    void OnMissileDestroyed() {
        MissilesDestroyed += 1;
    }
    
    void OnHit() {
        countTime = false;
    }

    void OnCollision() {
        countTime = false;
    }

    void OnGenerationFinished() {
        countTime = true;
    }

    void Update() {
        if (countTime) {
            TimeSurvived += Time.deltaTime;
        }
        TotalScore = TimeSurvived * MissilesDestroyed;
    }
}