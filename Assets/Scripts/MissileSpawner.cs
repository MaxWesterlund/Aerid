using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour {
    MissileStats missileStats;

    [SerializeField] GameObject missile;
    [SerializeField] Transform target;
    [SerializeField] float spawnDelay;
    [SerializeField] float spawnDistance;

    List<MissileBehaviour> missiles = new();

    Coroutine spawnTimer;

    void Awake() {
        if (GameObject.Find("Game Manager") != null) {
            GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            missileStats = GetComponent<MissileStats>();
            gameManager.Hit += OnHit;
            gameManager.Collision += OnCollision;
            gameManager.GenerationFinished += OnGenerationFinished;
        }
    }

    void OnHit() {
        StopCoroutine(SpawnTimer());
    }

    void OnCollision() {
        StopCoroutine(SpawnTimer());
    }

    void OnGenerationFinished() {
        StartCoroutine(SpawnTimer());
    }

    void Update() {
    }

    IEnumerator SpawnTimer() {
        float interval = missileStats.SpawnInterval;
        yield return new WaitForSeconds(interval);

        float x = Random.Range(-1f, 1f);
        float y = Mathf.Sin(Mathf.Acos(x)) * Random.Range(-1, 1);

        Vector3 spawnPos = target.position + new Vector3(x, 0, y) * spawnDistance;

        bool missileFound = false;

        foreach(MissileBehaviour missile in missiles) {
            if (missile.isDestroyed) {
                missileFound = true;
                missile.SetPosition(spawnPos);
                missile.isDestroyed = false;
                break;
            }
        }
        if (!missileFound) {
            GameObject obj = Instantiate(missile, spawnPos, Quaternion.identity);
            missiles.Add(obj.GetComponent<MissileBehaviour>());
        }
        
        StartCoroutine(SpawnTimer());
    }
}