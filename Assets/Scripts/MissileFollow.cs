using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileFollow : MonoBehaviour {
    Transform target;

    Rigidbody rb;
    Rigidbody targetRb;

    [SerializeField] float additionalSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float delay;

    [SerializeField] GameObject model;
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] ParticleSystem fireParticles;

    [SerializeField] AudioSource flyingSound;
    [SerializeField] AudioSource explosionSound;

    Vector3 lastPosition;

    void Awake() {
        if (GameObject.Find("Plane") != null) {
            target = GameObject.Find("Plane").GetComponent<Transform>();
        }
        rb = GetComponent<Rigidbody>();
        targetRb = target.GetComponent<Rigidbody>();
        lastPosition = transform.position;
    }

    void FixedUpdate() {
        StartCoroutine(CheckDelay());
        Vector3 distance = lastPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(distance);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);

        rb.velocity = targetRb.velocity.magnitude * transform.forward + additionalSpeed * transform.forward;
    }

    IEnumerator CheckDelay() {
        yield return new WaitForSeconds(delay);
        lastPosition = target.position;
    }

    public void PlayAudio() {
        flyingSound.Play();
    }

    void OnCollisionEnter() {
        explosionParticles.Play();
        fireParticles.Stop();
        flyingSound.Stop();
        explosionSound.Play();
        model.SetActive(false);
    }
}