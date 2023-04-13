using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour {
    [SerializeField] GameObject missile;
    [SerializeField] Transform target;
    [SerializeField] float delay;
    [SerializeField] float spawnDistance;

    List<MissileBehaviour> missiles = new();

    Coroutine spawnTimer;

    bool canSpawn = false;

    void Awake() {
        if (GameObject.Find("Game Manager") != null) {
            GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            gameManager.Hit += OnHit;
            gameManager.Collision += OnCollision;
            gameManager.GenerationFinished += OnGenerationFinished;
        }
    }

    void OnHit() {
        canSpawn = false;
    }

    void OnCollision() {
        canSpawn = false;
    }

    void OnGenerationFinished() {
        canSpawn = true;
    }

    void Update() {
        if (!canSpawn) {
            return;
        }

        if (spawnTimer == null) {
            spawnTimer = StartCoroutine(SpawnTimer());
        }
    }

    IEnumerator SpawnTimer() {
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

        print("missile spawned");

        yield return new WaitForSeconds(delay);
        
        spawnTimer = null;
    }
}