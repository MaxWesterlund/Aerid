using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BodyMaterialChanger : MonoBehaviour {
    [SerializeField] Material broken1;
    [SerializeField] Material broken2;

    bool hasCrashed = false;

    void Awake() {
        if (GameObject.Find("Game Manager") != null) {
            GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            gameManager.Hit += OnHit;
            gameManager.Collision += OnCollision;
        }
    }

    void OnHit() {
        if (hasCrashed) {
            return;
        }
        gameObject.GetComponent<MeshRenderer>().material = broken1;
    }

    void OnCollision() {
        gameObject.GetComponent<MeshRenderer>().material = broken2;
        hasCrashed = true;
    }
}