using UnityEngine;

[CreateAssetMenu(fileName = "Music", menuName = "ScriptableObjects/Music")]
public class BaseMusicScript : ScriptableObject
{
    //name of the music
    public string Name;
    //music clip to play
    public AudioClip Music;
    //base threshold for the lazer
    public float Threshold = 0.1f;
    //multiplier for the stage glow
    public float StageLightMultiplier = 1;
    //Color variables
    public Color BaseColor1;
    public Color BaseColor2;
    
    public Color SecondaryColor1;
    public Color SecondaryColor2;
    
    public Color accentsColor1;
    public Color accentsColor2;
    
    public Color backgroundColor1;
    public Color backgroundColor2;

    //boolean to check if the music has changed gets reset after the music has been played
    public bool MusicChanged;
    
    
    /// <summary>
    /// this is the method that will be called when the music changes used for testing
    /// </summary>
    virtual public void execute()
    {
        Debug.Log("BaseMusicScript execute");
    }
    
    
}
