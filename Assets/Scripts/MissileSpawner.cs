using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour {
    [SerializeField] GameObject missile;
    [SerializeField] Transform target;
    [SerializeField] float delay;
    [SerializeField] float spawnDistance;

    Coroutine spawnTimer;

    void Awake() {
        if (GameObject.Find("Game Manager") != null) {
            GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        }
    }

    void Update() {
        if (spawnTimer == null) {
            spawnTimer = StartCoroutine(SpawnTimer());
        }
    }

    IEnumerator SpawnTimer() {
        Instantiate(missile, target.position - target.forward * spawnDistance, Quaternion.identity);
        yield return new WaitForSeconds(delay);
        spawnTimer = null;
    }
}