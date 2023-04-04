using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCollision : MonoBehaviour
{
    [SerializeField] Transform lWing;
    [SerializeField] Transform rWing;

    void OnCollisionEnter(Collision info) {
        GetComponent<PlaneMovement>().enabled = false;
        GetComponent<Rigidbody>().useGravity = true;
        TransformWing(lWing);
        TransformWing(rWing);
    }

    void TransformWing(Transform wing) {
        wing.transform.parent = null;
        if (wing.GetComponent<Rigidbody>() == null) {
            wing.gameObject.AddComponent<Rigidbody>();
        }
    }
}
