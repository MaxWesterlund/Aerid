using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaneMovement : MonoBehaviour
{   
    Rigidbody rb;

    [Header("Stuff")]
    [SerializeField] Transform lWing;
    [SerializeField] Transform rWing;

    [Header("Pitch")]
    [SerializeField] float pitchAcceleration;
    [SerializeField] float pitchDeacceleration;
    [SerializeField] float maxPitchSpeed;
    [Header("Roll")]
    [SerializeField] float rollAcceleration;
    [SerializeField] float rollDeacceleration;
    [SerializeField] float maxRollSpeed;
    [Header("Yaw")]
    [SerializeField] float yawAcceleration;
    [SerializeField] float yawDeacceleration;
    [SerializeField] float maxYawSpeed;
    [Header("Uncategorized")]
    [SerializeField] float forwardAcceleration;
    [SerializeField] float forwardDeacceleration;
    [SerializeField] float minForwardSpeed;
    [SerializeField] float maxForwardSpeed;
    [SerializeField] float gravityAcceleration;

    Vector2 tiltVector;
    Vector2 steerVector;

    float roll;
    float yaw;
    float pitch;

    float forwardSpeed;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        TiltPlane();
        SteerPlane();
        AddForces();
    }

    void OnTilt(InputValue info) {
        tiltVector = info.Get<Vector2>();
    }

    void OnSteer(InputValue info) {
        steerVector = info.Get<Vector2>();
    }

    void TiltPlane() {
        float pitchAcc = tiltVector.y * pitchAcceleration * Time.fixedDeltaTime;
        float yawAcc = steerVector.x * yawAcceleration * Time.fixedDeltaTime;
        float rollAcc = tiltVector.x * rollAcceleration * Time.fixedDeltaTime * -1;
        
        pitch += pitchAcc;
        if (tiltVector.y == 0) {
            pitch = Mathf.Lerp(pitch, 0, pitchDeacceleration * Time.fixedDeltaTime);
        }
        pitch = Mathf.Clamp(pitch, -maxPitchSpeed, maxPitchSpeed);
        
        yaw += yawAcc;
        if (tiltVector.y == 0) {
            yaw = Mathf.Lerp(yaw, 0, yawDeacceleration * Time.fixedDeltaTime);
        }
        yaw = Mathf.Clamp(yaw, -maxYawSpeed, maxYawSpeed);

        roll += rollAcc;
        if (tiltVector.x == 0) {
            roll = Mathf.Lerp(roll, 0, rollDeacceleration * Time.fixedDeltaTime);
        }
        roll = Mathf.Clamp(roll, -maxRollSpeed, maxRollSpeed);

        transform.Rotate(new Vector3(pitch, yaw, roll), Space.Self);
    }

    void SteerPlane() {
        if (steerVector.y > 0) {
            forwardSpeed += forwardAcceleration * Time.fixedDeltaTime;
        }
        else if (steerVector.y < 0) {
            forwardSpeed -= forwardAcceleration * Time.fixedDeltaTime;
        }
        else {
            forwardSpeed -= forwardDeacceleration * Time.fixedDeltaTime;
        }
        forwardSpeed = Mathf.Clamp(forwardSpeed, minForwardSpeed, maxForwardSpeed);
    }
    
    void AddForces() {
        Vector3 totalForce = ForwardForce() + LiftForce() + GravityForce();
        totalForce *= Time.fixedDeltaTime;
        rb.velocity = totalForce;
    }

    Vector3 ForwardForce() {
        return transform.forward * forwardSpeed;
    }

    Vector3 LiftForce() {
        Vector3 force = transform.up * gravityAcceleration;

        return force;
    }
    
    Vector3 GravityForce() {
        return Vector3.down * LiftForce().z;
    }
}