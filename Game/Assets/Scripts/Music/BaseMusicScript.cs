using UnityEngine;

[CreateAssetMenu(fileName = "Music", menuName = "ScriptableObjects/Music")]
public class BaseMusicScript : ScriptableObject
{
    public string Name;
    public AudioClip Music;
    public float Threshold = 0.1f;
    public float StageLightMultiplier = 1;
    public Color BaseColor1;
    public Color BaseColor2;
    
    public Color SecondaryColor1;
    public Color SecondaryColor2;
    
    public Color accentsColor1;
    public Color accentsColor2;
    
    public Color backgroundColor1;
    public Color backgroundColor2;

    public bool MusicChanged;
    virtual public void execute()
    {
        Debug.Log("BaseMusicScript execute");
    }
    
    
}
