using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlaneCollision : MonoBehaviour
{
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] ParticleSystem smokeParticles;
 
    public event Action Hit;
    public event Action Collision;

    bool hasCrashed = false;

    void OnCollisionEnter(Collision info) {
        if (info.gameObject.layer == 7) {
            OnHit();
            Hit?.Invoke();
        }
        else if (info.gameObject.layer == 6) {
            OnCollision();
            Collision?.Invoke();
        }
    }

    void OnHit() {
        if (hasCrashed) {
            return;
        }
        hitParticles.Stop();
        hitParticles.Play();

        smokeParticles.Play();
    }

    void OnCollision() {
        hitParticles.Stop();
        hitParticles.Play();

        smokeParticles.Stop();

        hasCrashed = true;
    }
}
