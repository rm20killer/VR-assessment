using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public MusicController musicController;
    public float volume;
    
    public float MinXlocation = -1f;
    public float MaxXlocation = 0.95f;
    
    public bool MaxIsLowest = false;

    private float yLocation;
    private float zLocation;
    
    public Slider slider;
    private void Start()
    {
        yLocation = transform.localPosition.y;
        zLocation = transform.localPosition.z;
        musicController = FindObjectOfType<MusicController>();
        if (MaxIsLowest)
        {
            transform.localPosition = new Vector3(MinXlocation, transform.localPosition.y, transform.localPosition.z);
        }
        else
        {
            transform.localPosition = new Vector3(MaxXlocation, transform.localPosition.y, transform.localPosition.z);
        }
    }
    private void Update()
    {
        float localX = transform.localPosition.x;
        volume = Mathf.InverseLerp(MinXlocation, MaxXlocation, localX);
        if (MaxIsLowest)
        {
            volume = 1 - volume;
        }
        
        musicController.volume = volume;
        
        
        //if localX is less than MinXlocation, set localX to MinXlocation
        if (localX < MinXlocation)
        {
            transform.localPosition = new Vector3(MinXlocation, yLocation, zLocation);

        }
        //if localX is greater than MaxXlocation, set localX to MaxXlocation
        else if (localX > MaxXlocation)
        {
            transform.localPosition = new Vector3(MaxXlocation, yLocation, zLocation);
        }
        else
        {
            transform.localPosition = new Vector3(localX, yLocation, zLocation);
        }
        // slider.value = volume;
    }
    
    public void volumeChange()
    {
        
        volume = slider.value;
        //set the x location of the slider to the volume
        float xLocation = Mathf.Lerp(MaxXlocation, MinXlocation, volume);
        transform.localPosition = new Vector3(xLocation, yLocation, zLocation);
    }
}
