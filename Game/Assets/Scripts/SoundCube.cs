using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCube : MonoBehaviour
{
    public FrequencyBandAnalyser _FFT;
    public FrequencyBandAnalyser.Bands _FreqBands = FrequencyBandAnalyser.Bands.Eight;
    public int _FrequencyBandIndex = 0;
    private Vector3 _StartScale;
    public string _ColourName = "_EmissionColor";

    public Color Colour1;
    public Color Colour2;
    public MusicController _MusicController;
    public bool UseSecondColour = false;
    public bool UseSecondaryColor = false;
    public bool accentsColor1 = false;
    public float _StrengthScalar = 1;
    public float _ScaleMultiplier = 1;
    public float _RotationMultiplier = 1;

    MeshRenderer _MeshRenderer;

    public bool rotate = false;
    public bool scale = false;
    public bool colour = false;


    public bool XRotate = false;
    public bool YRotate = false;
    public bool ZRotate = false;

    public delegate void MyEventHandler(); // Delegate type

    private void Start()
    {
        _MeshRenderer = GetComponent<MeshRenderer>();
        //look for the frequency band analyser in the scene
        _FFT = FindObjectOfType<FrequencyBandAnalyser>();
        _MusicController = FindObjectOfType<MusicController>();
        _MusicController.OnMusicChange.AddListener(UpdateAudioSource);
        MusicChange();
        _StartScale = transform.localScale;
    }

    /// <summary>
    /// Changes the colors used by the SoundCube based on the current music settings.
    /// </summary>
    void MusicChange()
    {
        if (UseSecondaryColor)
        {
            Colour1 = _MusicController.MusicScript.SecondaryColor1;
            Colour2 = _MusicController.MusicScript.SecondaryColor2;
        }
        else if (accentsColor1)
        {
            Colour1 = _MusicController.MusicScript.accentsColor1;
            Colour2 = _MusicController.MusicScript.accentsColor2;
        }
        else
        {
            Colour1 = _MusicController.MusicScript.BaseColor1;
            Colour2 = _MusicController.MusicScript.BaseColor2;
        }
    }


    /// <summary>
    /// Updates the SoundCube's properties based on the current music settings
    /// </summary>
    void Update()
    {
        // Colour1 = UnityEngine.Random.ColorHSV();
        // Colour2 = UnityEngine.Random.ColorHSV();
        
        //get the strength based on the frequency band index from the frequency band analyser
        float strength = _FFT.GetBandValue(_FrequencyBandIndex, _FreqBands);
        
        //change the color of the object based on the strength of the music
        if (colour)
        {
            if (UseSecondColour)
            {
                _MeshRenderer.material.SetColor(_ColourName, Colour2 * strength * _StrengthScalar);
            }
            else
            {
                _MeshRenderer.material.SetColor(_ColourName, Colour1 * strength * _StrengthScalar);
            }   
        }
        //rotate the object based on the strength of the music
        if (rotate)
        {
            if (XRotate)
                transform.Rotate(Vector3.right, strength * _RotationMultiplier * Time.deltaTime * 100);
            if (YRotate)
                transform.Rotate(Vector3.up, strength * _RotationMultiplier * Time.deltaTime * 100);
            if (ZRotate)
                transform.Rotate(Vector3.forward, strength * _RotationMultiplier * Time.deltaTime * 100);
        }

        //scale the object based on the strength of the music
        if (scale)
        {
            transform.localScale = _StartScale + new Vector3(strength * _ScaleMultiplier, strength * _ScaleMultiplier,
                strength * _ScaleMultiplier);
        }
    }

    /// <summary>
    /// Removes the listener from the MusicController when the object is destroyed.
    /// </summary>
    void OnDestroy()
    {
        if (_MusicController != null)
        {
            _MusicController.OnMusicChange.RemoveListener(UpdateAudioSource);
        }
    }

    void UpdateAudioSource()
    {
        MusicChange();
    }
}