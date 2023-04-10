using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DeactivateOnCollision : MonoBehaviour {
	public bool onHit;
    public bool onCollision;

    void Awake() {
        if (GameObject.Find("Game Manager") != null) {
            GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            if (onHit) {
                gameManager.Hit += Deactivate;
            }
            if (onCollision) {
                gameManager.Collision += Deactivate;
            }
        }
    }

    void Deactivate() {
        this.gameObject.SetActive(false);
    }
}