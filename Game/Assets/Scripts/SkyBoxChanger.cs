using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxChanger : MonoBehaviour
{
    public FrequencyBandAnalyser _FFT;
    public FrequencyBandAnalyser.Bands _FreqBands = FrequencyBandAnalyser.Bands.Eight;
    public int _FrequencyBandIndex = 0;
    
    public float _StrengthScalar = 1;

    public Material _SkyBoxMaterial;

    private Color startColour;
    public Color Colour1;
    public Color Colour2;
    public Color Colour3;
    public MusicController _MusicController;
    void Start()
    {
        startColour = _SkyBoxMaterial.GetColor("_GroundColor");
        _MusicController = FindObjectOfType<MusicController>();
        //listen for music changes event from the music controller
        _MusicController.OnMusicChange.AddListener(UpdateAudioSource);
        MusicChange();
    }

    /// <summary>
    /// get the colours from the music controller
    /// </summary>
    private void MusicChange()
    {
        Colour1 = _MusicController.MusicScript.backgroundColor1;
        Colour2 = _MusicController.MusicScript.backgroundColor2;
        Colour3 = _MusicController.MusicScript.accentsColor1;
        
    }

    /// <summary>
    /// change the skybox colour based on the music
    /// </summary>
    void Update()
    {        
        float strength = _FFT.GetBandValue(_FrequencyBandIndex, _FreqBands) * _StrengthScalar;
        // _SkyBoxMaterial.SetColor(_ColourName, _Col * strength);
        //change the hue of the skybox based on the strength
        //change hue of the colour 
        Color color2 = Color.Lerp(Colour1, Colour2, strength);
        _SkyBoxMaterial.SetColor("_SkyTint", color2);
        
        _SkyBoxMaterial.SetColor("_GroundColor", Color.Lerp(startColour, Colour3, strength));
    }
    
    /// <summary>
    /// remove the listener when the object is destroyed
    /// </summary>
    void OnDestroy()  
    {
        if (_MusicController != null)
            _MusicController.OnMusicChange.RemoveListener(UpdateAudioSource);
    }

    void UpdateAudioSource()
    {
        MusicChange();
    }
}
