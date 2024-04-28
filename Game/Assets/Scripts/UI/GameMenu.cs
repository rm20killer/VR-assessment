using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameMenu : MonoBehaviour
{
    public Toggle fireworks;
    public Toggle bloom;
    public Button exit;
    public Button Return;
    
    
    public GameObject canvas;
    public RandomFirework randomFireworks;
    public Volume postProcessingVolume;
    
    
    public InputActionProperty ShowButton;
    public Transform headPosition;
    public float SpawnDistance = 2;
    
    /// <summary>
    /// set the canvas to be inactive
    /// add listeners to the toggle and buttons to do an action when the button is clicked
    /// </summary>
    void Start()
    {
        canvas.SetActive(false);
        fireworks.onValueChanged.AddListener((value) =>
        {
            //enable fireworks
            if (value)
            {
                randomFireworks.isFirework = true;
            }
            //disable fireworks
            else
            {
                randomFireworks.isFirework = false;
            }
        });
        
        bloom.onValueChanged.AddListener((value) =>
        {
            //enable bloom
            if (value)
            {
                postProcessingVolume.profile.TryGet(out Bloom bloomLayer);
                bloomLayer.active = true;
            }
            //disable bloom
            else
            {
                postProcessingVolume.profile.TryGet(out Bloom bloomLayer);
                bloomLayer.active = false;
            }
        });
        
        exit.onClick.AddListener(() =>
        {
            //exit the application
            Application.Quit();
        });
        
        Return.onClick.AddListener(() =>
        {
            //hide the canvas
            canvas.SetActive(false);
        });
    }
    
    /// <summary>
    /// check if the menu button is pressed and toggle the canvas
    /// make it so the canvas is in front of the player when it is toggled
    /// </summary>
    void Update()
    {
        if (ShowButton.action.WasPressedThisFrame())
        {
            canvas.SetActive(!canvas.activeSelf);
            var forward = headPosition.forward;
            canvas.transform.position = headPosition.position + new Vector3(forward.x, 0, forward.z) .normalized * SpawnDistance;
            
        }
        
        //return if the canvas is not active optimisation
        if (!canvas.activeSelf)
        {
            return;
        }
        //rotate the canvas to face the player
        var position = headPosition.position;
        canvas.transform.LookAt(new Vector3(position.x, canvas.transform.position.y, position.z));
        Quaternion flipRotation = Quaternion.Euler(0, 180, 0);
        canvas.transform.rotation *= flipRotation;
    }
}
