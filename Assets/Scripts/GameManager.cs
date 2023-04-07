using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour {
    [SerializeField] Transform[] checkPoints;

    public event Action Collision;

    void Awake() {
        GameObject plane = GameObject.Find("Plane");

        PlaneCollision planeCollision = plane.GetComponent<PlaneCollision>();
        planeCollision.Collision += OnCollision;

        CheckpointChecker checkpointChecker = plane.GetComponent<CheckpointChecker>();
        checkpointChecker.Entered += OnCheckpoint;
    }
    
    void OnCollision() {
        print("Gamemanager: Collided");
        Collision?.Invoke();
    }

    void OnCheckpoint() {
        print("Gamemanager: Checkpoint Reached");
    }
}
