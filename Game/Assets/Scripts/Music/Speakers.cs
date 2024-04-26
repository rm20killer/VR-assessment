using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speakers : MonoBehaviour
{
    public AudioSource MainSpeaker;
    private AudioSource audioSource;
    private MusicController MusicController;
    private void Start()
    {
        MusicController = FindObjectOfType<MusicController>();
        audioSource = GetComponent<AudioSource>();
        MusicController.OnMusicChange.AddListener(UpdateAudioSource);
        audioSource.clip = MainSpeaker.clip;
        audioSource.Play();
    }
    
    void Update()
    {
        // var volume = MusicController.volume;
        audioSource.volume = MainSpeaker.volume * 0.4f;
    }
    
    void UpdateAudioSource()
    {
        audioSource.clip = MainSpeaker.clip;
        audioSource.Play();
    }
    
    
    
}
