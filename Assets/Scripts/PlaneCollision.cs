using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlaneCollision : MonoBehaviour
{
    public event Action Collision;

    void OnCollisionEnter(Collision info) {
        Collision?.Invoke();
    }
}
