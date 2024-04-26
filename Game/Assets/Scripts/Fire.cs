using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{        
    public GameObject ProjectilePrefab = null;
    public Transform GunPoint = null;
    public float LaunchSpeed = 100.0f;
    
    /// <summary>
    /// creating a new object apply force to it if there is a rigidbody
    /// </summary>
    public void shoot()
    {
        GameObject newObject = Instantiate(ProjectilePrefab, GunPoint.position, GunPoint.rotation, null);
        // if the new object has a rigidbody, apply force to it
        Rigidbody rigidBody = newObject.GetComponent<Rigidbody>();
        if (rigidBody != null)
        {
            Vector3 force = GunPoint.forward * LaunchSpeed;
            rigidBody.AddForce(force);
        }
    }
}
