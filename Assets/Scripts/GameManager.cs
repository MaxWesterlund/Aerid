using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public event Action Restart;
    public event Action Collision;

    void Awake() {
        PlaneCollision planeCollision = GameObject.Find("Plane").GetComponent<PlaneCollision>();
        planeCollision.Collision += OnCollision;
    }

    void OnRestart() {
        Restart?.Invoke();
    }

    void OnCollision() {
        Collision?.Invoke();
    }
}
