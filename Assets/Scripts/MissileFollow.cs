using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileFollow : MonoBehaviour {
    Transform target;

    Rigidbody rb;
    Rigidbody targetRb;

    [SerializeField] float additionalSpeed;
    [SerializeField] float rotationSpeed;

    [SerializeField] GameObject model;
    [SerializeField] ParticleSystem explosionParticles;

    void Awake() {
        if (GameObject.Find("Plane") != null) {
            target = GameObject.Find("Plane").GetComponent<Transform>();
        }
        rb = GetComponent<Rigidbody>();
        targetRb = target.GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        Vector3 distance = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(distance);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);

        rb.velocity = targetRb.velocity.magnitude * transform.forward + additionalSpeed * transform.forward;
    }

    void OnCollisionEnter() {
        explosionParticles.Play();
        model.SetActive(false);
    }
}