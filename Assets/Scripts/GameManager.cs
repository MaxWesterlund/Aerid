using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour {
    public event Action Collision;

    void Awake() {
        PlaneCollision planeCollision = GameObject.Find("Plane").GetComponent<PlaneCollision>();
        planeCollision.Collision += OnCollision;
    }
    
    void OnCollision() {
        print("Gamemanager: Collided");
        Collision?.Invoke();
    }
}
