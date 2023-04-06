using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTunnel : MonoBehaviour
{
    [SerializeField] GameObject part;
    [SerializeField] Transform target;

    const int amount = 8;

    GameObject[] parts = new GameObject[amount];

    float currentPosition = 0;
    int partIndex = 0;

    void Start() {
        GameObject spawnPoint = new("Spawn Point");
        spawnPoint.transform.position = Vector3.zero;
        target.position = spawnPoint.transform.position;
 
        for (int i = 0; i < amount; i++) {
            GameObject obj = Instantiate(part, new Vector3(0, 0, -10 * amount / 2 + 10 * i), Quaternion.identity);
            obj.name = "Tunnel " + i;
            obj.GetComponent<PartSelection>().ChooseRandom();
            parts[i] = obj;
        }
    }

    void Update() {
        if (target.transform.position.z + (10 * amount / 2) - currentPosition > 10) {
            currentPosition += 10;
            parts[partIndex].transform.position = new Vector3(0, 0, currentPosition);
            parts[partIndex].GetComponent<PartSelection>().ChooseRandom();
            partIndex += 1;
            if (partIndex >= amount) {
                partIndex = 0;
            }
        }
    }
}
