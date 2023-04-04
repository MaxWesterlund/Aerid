using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTunnel : MonoBehaviour
{
    [SerializeField] GameObject[] parts;
    [SerializeField] Transform target;

    const int amount = 8;

    GameObject[] currentParts = new GameObject[amount];

    float lastPosition = 0;
    int lastIndex = 0;

    void Start() {
        GameObject spawnPoint = new("Spawn Point");
        spawnPoint.transform.position = Vector3.zero;
        target.position = spawnPoint.transform.position;
 
        for (int i = 0; i < amount; i++) {
            currentParts[i] = Instantiate(parts[Random.Range(0, parts.Length)], new Vector3(0, 0, -10 * amount / 2 + 10 * i), Quaternion.identity);
        }
    }

    void Update() {
        if (target.transform.position.z + (10 * amount / 2) - lastPosition > 10) {
            lastIndex += 1;
            if (lastIndex >= amount) {
                lastIndex = 0;
            }
            lastPosition += 10;
            currentParts[lastIndex].transform.position = new Vector3(0, 0, lastPosition);
        }
    }
}
