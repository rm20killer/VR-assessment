
using System;
using UnityEngine;

public class Lazer_controller : MonoBehaviour
{
    public FrequencyBandAnalyser FFT;
    public FrequencyBandAnalyser.Bands FreqBands = FrequencyBandAnalyser.Bands.Eight;
    public int FrequencyBandIndex = 3;

    public string ColourName = "_EmissionColor";
    public Color Colour1;
    public Color Colour2;
    public MusicController _MusicController;
    // public bool UseSecondColour = false;
    public bool UseSecondaryColor = false;
    public float StrengthScalar = 1;
    public float EmissionScalar = 1;
    
    MeshRenderer MeshRenderer;
    Material Material;

    public enum Side
    {
        Left,
        Right
    }
    
    public enum Position 
    {
        Floor,
        Ceiling,
        Wall
    }
    public Position position;
    public Side side;
    public bool rotateRight  = true;
    public float MIN_ROTATION_X = -110.0f;
    public float MAX_ROTATION_X = 110.0f;
    public float currentAngle = 0.0f;
    public float rotationMultiplier = 1.0f;
    public float middleRotationMultiplier = 1.0f;
    public GameObject middleLazer;
    public GameObject topLazer;
    public GameObject Lazer;
    public float StartMiddleRotation;
    public float StartTopRotation;
    public float ThresholdMultiplier = 1.0f;
    private void Start()
    {
        //look for the frequency band analyser in the scene
        FFT = FindObjectOfType<FrequencyBandAnalyser>();
        _MusicController = FindObjectOfType<MusicController>();
        _MusicController.OnMusicChange.AddListener(UpdateAudioSource);
        MusicChange();
        //get meshrenderer from the lazer child
        Lazer = topLazer.transform.GetChild(0).gameObject;
        MeshRenderer = Lazer.GetComponent<MeshRenderer>();
        //get the first material of the mesh renderer
        Material = MeshRenderer.materials[0];
        
        StartMiddleRotation = middleLazer.transform.rotation.z;
        StartTopRotation = topLazer.transform.rotation.x;
    }
    
    void MusicChange()
    { 
        topLazer.transform.Rotate( new Vector3(StartTopRotation, 0, 0));
        middleLazer.transform.Rotate( new Vector3(0, 0, StartMiddleRotation));
        
        if (UseSecondaryColor)
        {
            Colour1 = _MusicController.MusicScript.SecondaryColor1;
            Colour2 = _MusicController.MusicScript.SecondaryColor2;
        }
        else
        {
            Colour1 = _MusicController.MusicScript.BaseColor1;
            Colour2 = _MusicController.MusicScript.BaseColor2;
        }
    }
    void Update()
    {        
        float strength = FFT.GetBandValue(FrequencyBandIndex, FreqBands) * StrengthScalar;
        
        if(strength < _MusicController.MusicScript.Threshold * ThresholdMultiplier)
        {
            // strength = 0.1f;
            Lazer.SetActive(false);
        }
        else
        {
            Lazer.SetActive(true);
        }

        if(middleLazer)
        {
            //Rotate it on the z axis
            //if it is on the left side rotate it clockwise
            if(side == Side.Left)
            {
                middleLazer.transform.Rotate(Vector3.forward, strength * Time.deltaTime * 100 * middleRotationMultiplier);
            }
            else if(side == Side.Right)
            {
                middleLazer.transform.Rotate(Vector3.back, strength * Time.deltaTime * 100 * middleRotationMultiplier);
            }

        }
        
        if(topLazer)
        {
            //Rotate it on the x axis limited to 110 degrees if it is on the floor  
            if (position == Position.Floor)
            {
                float rotationSpeed = strength * Time.deltaTime * 100.0f * (rotateRight ? 1 : -1);
                currentAngle += rotationSpeed * rotationMultiplier;
                
                if (currentAngle >= MAX_ROTATION_X)
                {
                    rotateRight = false;
                    currentAngle = MAX_ROTATION_X; // Clamp to the limit
                }
                else if (currentAngle <= MIN_ROTATION_X)
                {
                    rotateRight = true;
                    currentAngle = MIN_ROTATION_X; // Clamp to the limit
                }
                
                topLazer.transform.localRotation = Quaternion.Euler(currentAngle, 0, 0);

            }
            else
            {
                topLazer.transform.Rotate(Vector3.right, strength * Time.deltaTime * 100 * rotationMultiplier, Space.Self); // Rotate locally
            }

            
            //change the colour of the lazer
            Material.SetColor(ColourName, Colour1 * strength);

            Color color3 = Color.Lerp(Colour1, Colour2, strength);
            
            Material.SetColor("_EmissionColor", color3 * strength * EmissionScalar);
            
        }

    }
    
    
    private float ClampAngle(float angle) 
    {
        angle = angle % 360;
        return angle < 0 ? angle + 360 : angle;
    }
    
    void OnDestroy()  // Unsubscribe!
    {
        if (_MusicController != null)
            _MusicController.OnMusicChange.RemoveListener(UpdateAudioSource);
    }

    void UpdateAudioSource()
    {
        MusicChange();
    }
    void OnDrawGizmos()
    {
        //show which way it is rotating
        if (topLazer)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(topLazer.transform.position, topLazer.transform.position + topLazer.transform.forward * 2);
        }
        
    }
    
    
}
