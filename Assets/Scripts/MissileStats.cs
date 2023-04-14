using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileStats : MonoBehaviour {
    [SerializeField] int levels;
    [SerializeField] float interval;

    [SerializeField] float startSpawnInterval;
    [SerializeField] float maxSpawnInterval;

    [SerializeField] float startSpeed;
    [SerializeField] float maxSpeed;

    [SerializeField] float startRotationSpeed;
    [SerializeField] float maxRotationSpeed;

    public float SpawnInterval;
    public float Speed;
    public float RotationSpeed;

    void Awake() {
        if (GameObject.Find("Game Manager") != null) {
            GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            gameManager.GenerationFinished += OnGenerationFinished;
        }
    }

    void Start() {
        SpawnInterval = startSpawnInterval;
        Speed = startSpeed;
        RotationSpeed = startRotationSpeed;
    }

    void OnGenerationFinished() {
        StartCoroutine(UpdateStats());
    }

    IEnumerator UpdateStats(int i = 1) {
        if (i > levels) {
            yield return null;
        }
        
        float lerp = (float)i / (float)levels;

        SpawnInterval = Mathf.Lerp(startSpawnInterval, maxSpawnInterval, lerp);
        Speed = Mathf.Lerp(startSpeed, maxSpeed, lerp);
        RotationSpeed = Mathf.Lerp(startSpeed, maxSpeed, lerp);

        yield return new WaitForSeconds(interval);

        int j = i + 1;
        StartCoroutine(UpdateStats(j));
    }
}