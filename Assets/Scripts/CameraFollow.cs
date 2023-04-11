using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] float rotationSpeed;
    [SerializeField] float moveSpeed;

    void Awake() {
        if (GameObject.Find("Game Manager") != null) {
            GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            gameManager.Hit += OnHit;
            gameManager.Collision += OnCollision;
            gameManager.GenerationFinished += OnGenerationFinished;
        }
    }

    void OnHit() {
        
    }

    void OnCollision() {

    }

    void OnGenerationFinished() {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    void FixedUpdate() {
        Quaternion rotation = target.rotation;

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        
        transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
}
