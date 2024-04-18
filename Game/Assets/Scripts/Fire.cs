using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{        
    public GameObject ProjectilePrefab = null;

    [SerializeField]
    private Transform GunPoint = null;

    [SerializeField]
    private float LaunchSpeed = 1.0f;

    public void shoot()
    {
        GameObject newObject = Instantiate(ProjectilePrefab, GunPoint.position, GunPoint.rotation, null);

        if (newObject.TryGetComponent(out Rigidbody rigidBody))
            ApplyForce(rigidBody);
    }

    void ApplyForce(Rigidbody rigidBody)
    {
        Vector3 force = GunPoint.forward * LaunchSpeed;
        rigidBody.AddForce(force);
    }
}
