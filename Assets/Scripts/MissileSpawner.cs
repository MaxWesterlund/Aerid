using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour {
    [SerializeField] GameObject missile;
    [SerializeField] Transform target;
    [SerializeField] float delay;
    [SerializeField] float spawnDistance;

    Coroutine spawnTimer;

    bool canStart = false;

    void Awake() {
        if (GameObject.Find("Game Manager") != null) {
            GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            gameManager.GenerationFinished += OnGenerationFinished;
        }
    }

    void OnGenerationFinished() {
        canStart = true;
    }

    void Update() {
        if (!canStart) {
            return;
        }
        if (spawnTimer == null) {
            spawnTimer = StartCoroutine(SpawnTimer());
        }
    }

    IEnumerator SpawnTimer() {
        float x = Random.Range(-1f, 1f);
        float y = Mathf.Sin(Mathf.Acos(x)) * Random.Range(-1, 1);

        Vector3 positionVector = new(x, 0, y);
        
        GameObject obj = Instantiate(missile, target.position + positionVector * spawnDistance, Quaternion.identity);
        obj.GetComponent<MissileFollow>().PlayAudio();
        yield return new WaitForSeconds(delay);
        spawnTimer = null;
    }
}