using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSelection : MonoBehaviour {
    [SerializeField] GameObject[] parts;

    public void ChooseRandom() {
        GameObject selectedPart = parts[Random.Range(0, parts.Length)];
        foreach (GameObject part in parts) {
            if (part != selectedPart) {
                part.SetActive(false);
            }
            else {
                part.SetActive(true);
            }
        }
    }
}
