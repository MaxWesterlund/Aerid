using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spin : MonoBehaviour {
    RectTransform t;
	void Awake() {
        t = GetComponent<RectTransform>();
    }

    float lerp = 0;

    void Update() {
        lerp += Time.deltaTime;
        float zRot = Mathf.SmoothStep(t.rotation.z, 360, lerp);
        if (lerp >= 1) {
            lerp = 0;
        }
        t.rotation = Quaternion.Euler(0, 0, zRot);
    }
}
