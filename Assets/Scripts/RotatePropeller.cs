using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePropeller : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float speed;

    void Awake() {
        rb = GetComponentInParent<Rigidbody>();
    }

    void Update() {
        transform.Rotate(new Vector3(0, 1, 0) * speed * rb.velocity.magnitude, Space.Self);
    }
}
