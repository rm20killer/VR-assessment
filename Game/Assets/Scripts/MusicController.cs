
using UnityEngine;
using UnityEngine.Events;

public class MusicController : MonoBehaviour
{
    public UnityEvent OnMusicChange;

    public BaseMusicScript MusicScript;
    public Material SkyBoxMaterial;
    public GameObject FFTObject;
    public Material LightColour;
    public Material StageLightColour;
 
    
    public FrequencyBandAnalyser FFT;
    public FrequencyBandAnalyser.Bands FreqBands = FrequencyBandAnalyser.Bands.Eight;
    public int LightFrequencyBandIndex = 3;
    public int StageLightFrequencyBandIndex = 4;
    public float StrengthScalar = 1;
    public float StrengthScalarStage = 1;
    private BaseMusicScript PreviousMusicScript;

    private AudioSource audioSource;
    
    public float volume;

    /// <summary>
    /// set the music script and start the music
    /// </summary>
    void Start()
    {
        PreviousMusicScript = MusicScript;
        FFT = FindObjectOfType<FrequencyBandAnalyser>();
        MusicChange();
        LightColour.SetColor("_EmissionColor", MusicScript.BaseColor1);
    }
    
    /// <summary>
    /// when the music changes, play the new music
    /// update the light and other objects
    /// call the on music change event so other objects can update
    /// </summary>
    void MusicChange()
    {
        PreviousMusicScript.MusicChanged = true;
        Debug.Log("Music Changed");
        MusicScript.execute();
        audioSource = FFTObject.GetComponent<AudioSource>();
        audioSource.clip = MusicScript.Music;
        audioSource.Play();
        MusicScript.MusicChanged = false;
        OnMusicChange.Invoke();
    }
    
    /// <summary>
    /// update the light based on the music and other objects
    /// check if the music has changed by checking a boolean in the music script
    /// </summary>
    void Update()
    {
        //set the volume of the music
        audioSource.volume = volume;
        UpdateLight();
        UpdateOther();
        //check if music changed
        if (MusicScript.MusicChanged)
        {
            MusicChange();
        }
        else
        {
            PreviousMusicScript = MusicScript;
        }

    }
    
    /// <summary>
    /// change the light colour based on the music
    /// </summary>
    void UpdateLight()
    {
        // Color Colour1 = UnityEngine.Random.ColorHSV();
        // Color Colour2 = UnityEngine.Random.ColorHSV();
        float strength = FFT.GetBandValue(LightFrequencyBandIndex, FreqBands) * MusicScript.StageLightMultiplier;
        LightColour.SetColor("_EmissionColor", Color.Lerp(MusicScript.BaseColor1 * strength * StrengthScalar, MusicScript.BaseColor2 *StrengthScalar *strength, strength));
        // LightColour.SetColor("_EmissionColor", Color.Lerp(Colour1 * strength * StrengthScalar, Colour2 *StrengthScalar *strength, strength));
    }

    /// <summary>
    /// change the stage light colour based on the music
    /// </summary>
    void UpdateOther()
    {
        // Color Colour1 = UnityEngine.Random.ColorHSV();
        // Color Colour2 = UnityEngine.Random.ColorHSV();
        float strength = FFT.GetBandValue(StageLightFrequencyBandIndex, FreqBands);
        StageLightColour.SetColor("_EmissionColor", Color.Lerp(MusicScript.BaseColor1 * strength * StrengthScalarStage, MusicScript.BaseColor2 *StrengthScalarStage *strength, strength));
        // StageLightColour.SetColor("_EmissionColor", Color.Lerp(Colour1 * strength * StrengthScalarStage, Colour2 *StrengthScalarStage *strength, strength));
    }
    
    
    /// <summary>
    /// reset the music changed boolean when the object is destroyed
    /// </summary>
    void OnDestroy()  
    {
        MusicScript.MusicChanged = true;
        PreviousMusicScript.MusicChanged = true;
    }
    
    
    /// <summary>
    /// set the volume of the music
    /// </summary>
    /// <param name="newVolume"></param>
    public void SetVolume(float newVolume)
    {
        volume = newVolume;
    }
}
