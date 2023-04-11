using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField] GameObject plane;

    public event Action GenerateTerrain;
    public event Action Hit;
    public event Action Collision;
    public event Action GenerationFinished;

    void Awake() {
        if (GetComponent<TerrainGen>() != null) {
            TerrainGen terrainGen = GetComponent<TerrainGen>();
            terrainGen.GenerationFinished += OnGenerationFinished;
        }
        plane.SetActive(false);
    }

    void OnGenerationFinished() {
        print("Gamemanager: Generation Finished");
        plane.SetActive(true);
        GenerationFinished?.Invoke();

        if (GameObject.Find("Spawn") != null) {
            GameObject spawn = GameObject.Find("Spawn");
            plane.transform.position = spawn.transform.position;
        }

        PlaneCollision planeCollision = plane.GetComponent<PlaneCollision>();
        
        planeCollision.Hit += OnHit;
        planeCollision.Collision += OnCollision;
    }
    
    void OnHit() {
        print("Gamemanager: Hit");
        Hit?.Invoke();
    }

    void OnCollision() {
        print("Gamemanager: Collided");
        Collision?.Invoke();
    }

}
