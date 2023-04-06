using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HandleWings : MonoBehaviour {
    [SerializeField] GameObject leftWing;
    [SerializeField] GameObject rightWing;

    void Awake() {
        GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gameManager.Collision += OnCollision;
        gameManager.Restart += OnRestart;
    }

    void OnCollision() {
        EnableWing(leftWing, false);
        EnableWing(rightWing, false);
    }

    void OnRestart() {
        EnableWing(leftWing, true);
        EnableWing(rightWing, true);
    }

    void EnableWing(GameObject wing, bool b) {
        if (b) {
            wing.transform.parent = this.transform;
            Destroy(wing.GetComponent<Rigidbody>());
        }
        else {
            wing.transform.parent = null;
            if (wing.GetComponent<Rigidbody>() != null) {
                return;
            }
            wing.AddComponent<Rigidbody>();
        }
    }
}
