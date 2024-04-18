
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


    void Start()
    {
        PreviousMusicScript = MusicScript;
        FFT = FindObjectOfType<FrequencyBandAnalyser>();
        MusicChange();
        LightColour.SetColor("_EmissionColor", MusicScript.BaseColor1);
    }
    void MusicChange()
    {
        PreviousMusicScript.MusicChanged = true;
        Debug.Log("Music Changed");
        MusicScript.execute();
        AudioSource audioSource = FFTObject.GetComponent<AudioSource>();
        audioSource.clip = MusicScript.Music;
        audioSource.Play();
        MusicScript.MusicChanged = false;
        OnMusicChange.Invoke();
    }
    void Update()
    {
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
    
    void UpdateLight()
    {
        // Color Colour1 = UnityEngine.Random.ColorHSV();
        // Color Colour2 = UnityEngine.Random.ColorHSV();
        float strength = FFT.GetBandValue(LightFrequencyBandIndex, FreqBands) * MusicScript.StageLightMultiplier;
        LightColour.SetColor("_EmissionColor", Color.Lerp(MusicScript.BaseColor1 * strength * StrengthScalar, MusicScript.BaseColor2 *StrengthScalar *strength, strength));
        // LightColour.SetColor("_EmissionColor", Color.Lerp(Colour1 * strength * StrengthScalar, Colour2 *StrengthScalar *strength, strength));
    }

    void UpdateOther()
    {
        // Color Colour1 = UnityEngine.Random.ColorHSV();
        // Color Colour2 = UnityEngine.Random.ColorHSV();
        float strength = FFT.GetBandValue(StageLightFrequencyBandIndex, FreqBands);
        StageLightColour.SetColor("_EmissionColor", Color.Lerp(MusicScript.BaseColor1 * strength * StrengthScalarStage, MusicScript.BaseColor2 *StrengthScalarStage *strength, strength));
        // StageLightColour.SetColor("_EmissionColor", Color.Lerp(Colour1 * strength * StrengthScalarStage, Colour2 *StrengthScalarStage *strength, strength));
    }
    
    void OnDestroy()  
    {
        MusicScript.MusicChanged = true;
        PreviousMusicScript.MusicChanged = true;
    }
}
