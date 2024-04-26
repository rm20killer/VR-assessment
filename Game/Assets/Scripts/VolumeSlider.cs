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
    /// <summary>
    /// set the x location of the slider to the max or min x location so the volume is set to at 100%
    /// </summary>
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
    
    /// <summary>
    /// Update the volume of the music controller based on the x location of the slider
    /// clamp the x location of the slider to the min and max x location and lock the y and z location
    /// </summary>
    private void Update()
    {
        float localX = transform.localPosition.x;
        //get the volume of the slider based on the x location
        volume = Mathf.InverseLerp(MinXlocation, MaxXlocation, localX);
        if (MaxIsLowest)
        {
            volume = 1 - volume;
            volume = Mathf.Round(volume * 100) / 100;
            //allow for some leeway in the slider
            if (volume is < 0.05f or > 0.95f)
            {
                volume = Mathf.Round(volume);
            }
            
        }
        //set the volume of the music controller to the volume
        musicController.volume = volume;
        //if localX is less than MinXlocation, set localX to MinXlocation
        if (localX < MinXlocation)
        {
            transform.localPosition = new Vector3(MinXlocation, yLocation, zLocation);

        }
        else if (localX > MaxXlocation)
        {
            transform.localPosition = new Vector3(MaxXlocation, yLocation, zLocation);
        }
        else
        {
            transform.localPosition = new Vector3(localX, yLocation, zLocation);
        }
    }
    
    public void volumeChange()
    {
        
        volume = slider.value;
        //set the x location of the slider to the volume
        float xLocation = Mathf.Lerp(MaxXlocation, MinXlocation, volume);
        transform.localPosition = new Vector3(xLocation, yLocation, zLocation);
    }
}
