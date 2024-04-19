using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeBasedUI : MonoBehaviour
{
    public Camera camera;
    public GameObject[] ObjectToHide;
    
    /// <summary>
    /// setting the camera to the main camera
    /// </summary>
    void Start()
    {
        camera = Camera.main;    
    }
    // Update is called once per frame
    
    /// <summary>
    /// using raycast to detect if the user is looking at the object or not
    /// if the user is looking at the object, the object will be visible
    /// else the object will be hidden
    /// </summary>
    void Update()
    {
        RaycastHit hit;
        Ray ray;
        ray = new Ray(camera.transform.position, camera.transform.rotation * Vector3.forward);
        // Debug.DrawRay(camera.transform.position, camera.transform.rotation * Vector3.forward * 500, Color.red);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                foreach (GameObject obj in ObjectToHide)
                {
                    obj.SetActive(true);
                }
            }
            else
            {
                foreach (GameObject obj in ObjectToHide)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
    
}
