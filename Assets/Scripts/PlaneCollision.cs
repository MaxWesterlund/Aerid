using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlaneCollision : MonoBehaviour
{
    [SerializeField] LayerMask obstacleLayer;

    public event Action Collision;

    void OnCollisionEnter(Collision info) {
        if (info.gameObject.layer != 6) {
            return;
        }
        Collision?.Invoke();
    }
}
