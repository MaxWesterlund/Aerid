using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] Transform[] checkPoints;

    public event Action Hit;
    public event Action Collision;
    public event Action GenerationFinished;

    void Awake() {
        GameObject plane = GameObject.Find("Plane");
        
        PlaneCollision planeCollision = plane.GetComponent<PlaneCollision>();
        planeCollision.Hit += OnHit;
        planeCollision.Collision += OnCollision;

        TerrainGen terrainGen = GetComponent<TerrainGen>();
        terrainGen.GenerationFinished += OnGenerationFinished;
    }
    
    void OnHit() {
        print("Gamemanager: Hit");
        Hit?.Invoke();
    }

    void OnCollision() {
        print("Gamemanager: Collided");
        Collision?.Invoke();
    }

    void OnGenerationFinished() {
        print("Gamemanager: Generation Finished");
        GenerationFinished?.Invoke();
    }
}
