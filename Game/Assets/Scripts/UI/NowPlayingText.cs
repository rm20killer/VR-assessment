using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NowPlayingText : MonoBehaviour
{
    public MusicController MusicController;
    public TextMeshProUGUI TextMeshPro;
    
    private void Start()
    {
        // MusicController = FindObjectOfType<MusicController>();
        MusicController.OnMusicChange.AddListener(UpdateAudioSource);
        UpdateAudioSource();
    }

    void UpdateAudioSource()
    {
        var songName = "Now Playing: " + MusicController.MusicScript.Name;
        TextMeshPro.text = songName;
    }
}
