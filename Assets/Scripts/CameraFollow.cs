using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] float rotationSpeed;
    [SerializeField] float moveSpeed;

    [SerializeField] float strenghtMultiplier;
    [SerializeField] float shakeInterval;
    [SerializeField] int shakeAmount;

    [SerializeField] float shakeSpeed;
    Vector3 shake;

    bool isShaking;
    bool canMove = false;

    void Awake() {
        if (GameObject.Find("Game Manager") != null) {
            GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            gameManager.MissileDestroyedWithPos += OnMissileDestroyed;
            gameManager.Hit += OnHit;
            gameManager.Collision += OnCollision;
            gameManager.GenerationFinished += OnGenerationFinished;
        }
    }

    void OnMissileDestroyed(object sender, GameManager.MissileDestroyedWithPosArgs args) {
        StartCoroutine(Shake(args.Pos, strenghtMultiplier, shakeInterval, shakeAmount));
    }

    void OnHit() {
        StartCoroutine(Shake(transform.position, strenghtMultiplier, shakeInterval, shakeAmount));
    }

    void OnCollision() {
        StartCoroutine(Shake(transform.position, strenghtMultiplier, shakeInterval, shakeAmount));
    }

    void OnGenerationFinished() {
        transform.position = target.position;
        transform.rotation = target.rotation;

        canMove = true;
    }

    IEnumerator Shake(Vector3 position, float multiplier, float interval, int amount, int i = 0) {
        if (i < amount) {
            print(i);
            isShaking = true;
            Vector2 seed = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
            float distance = Vector3.Distance(position, transform.position);
            distance = Mathf.Clamp(distance, 1, 100);
            float strength = 1f / distance;

            strength *= multiplier;
            
            shake = transform.right * seed.x * strength + transform.up * seed.y * strength;
            yield return new WaitForSeconds(interval);
            int j = i + 1;
            StartCoroutine(Shake(position, multiplier, interval, amount, j));
        }
        else {
            isShaking = false;
        }
    }

    void FixedUpdate() {
        if (!canMove) {
            return;
        }

        Quaternion rotation = target.rotation;

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed * Time.deltaTime);
        if (isShaking) {
            transform.position = Vector3.Lerp(transform.position, target.position + shake, shakeSpeed * Time.deltaTime);
        }
    }
}
