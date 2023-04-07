using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CheckpointChecker : MonoBehaviour {
    public event Action Entered;

    void OnTriggerEnter(Collider info) {
        if (info.gameObject.tag != "Checkpoint") {
            return;
        }

        Entered?.Invoke();
    }
}
