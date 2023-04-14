using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MissileBehaviour : MonoBehaviour {
    GameManager gameManager;
    MissileStats missileStats;
    Stats stats;

    Transform target;

    Rigidbody rb;
    Rigidbody targetRb;

    [SerializeField] GameObject model;
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] ParticleSystem fireParticles;

    [SerializeField] AudioSource flyingSound;
    [SerializeField] AudioSource explosionSound;

    public bool isDestroyed = false;

    void Awake() {
        if (GameObject.Find("Game Manager") != null) {
            stats = GameObject.Find("Game Manager").GetComponent<Stats>();
            gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            gameManager.Collision += OnCollision;
        }
        if (GameObject.Find("Missile Manager") != null) {
            missileStats = GameObject.Find("Missile Manager").GetComponent<MissileStats>();
        }
        if (GameObject.Find("Plane") != null) {
            target = GameObject.Find("Plane").GetComponent<Transform>();
        }
        rb = GetComponent<Rigidbody>();
        targetRb = target.GetComponent<Rigidbody>();
    }

    void OnCollision() {
        isDestroyed = true;
    }

    void Update() {
        if (isDestroyed) {
            model.SetActive(false);
            if (flyingSound.isPlaying) {
                flyingSound.Stop();
            }
            if (fireParticles.isEmitting) {
                fireParticles.Stop();
            }
        }
        else {
            model.SetActive(true);
            if (!flyingSound.isPlaying) {
                flyingSound.Play();
            }
            if (!fireParticles.isEmitting) {
                fireParticles.Play();
            }
        }
    }

    void FixedUpdate() {
        Follow();
    }

    void Follow() {
        float speed = missileStats.Speed;
        float rotationSpeed = missileStats.RotationSpeed;

        if (isDestroyed) {
            return;
        }
        Vector3 distance = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(distance);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);

        rb.velocity = targetRb.velocity.magnitude * transform.forward + speed * transform.forward;
    }

    void OnCollisionEnter(Collision info) {
        isDestroyed = true;

        explosionParticles.Play();
        explosionSound.Play();

        if (info.gameObject.layer != 3) {
            gameManager.OnMissileDestroyed(transform.position);
        }
    }

    public void SetPosition(Vector3 pos) {
        transform.position = pos;
    }
}