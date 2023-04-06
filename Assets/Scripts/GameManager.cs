using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour {
    [SerializeField] InputAction Reset;
    public event Action Restart;
    public event Action Collision;

    InputActionMap actions;
    float f;

    void Awake() {
        PlaneCollision planeCollision = GameObject.Find("Plane").GetComponent<PlaneCollision>();
        planeCollision.Collision += OnCollision;

        actions.AddAction("Reset");
        actions.Enable();
        actions.actions[0].Enable();
    }

    void OnEnable() {
        Reset.performed += OnReset;
    }

    void OnReset(InputAction.CallbackContext info) {
        print("Gamemanager: Restarted");
        // Restart?.Invoke();
        print(info.ReadValue<float>());
    }

    void OnCollision() {
        print("Gamemanager: Collided");
        Collision?.Invoke();
    }
}
