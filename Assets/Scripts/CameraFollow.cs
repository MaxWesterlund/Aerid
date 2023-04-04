using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] Transform target;

    [SerializeField] float rotationSpeed;
    [SerializeField] float moveSpeed;

    void Start() {
        transform.position = target.position;
    }

    void FixedUpdate() {
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotationSpeed * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
}
