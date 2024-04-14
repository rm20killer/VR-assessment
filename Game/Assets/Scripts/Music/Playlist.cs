using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Playlist : MonoBehaviour
{
    public MusicController musicController;
    public BaseMusicScript[] songs;
    public int currentSongIndex = 0;
    public bool shuffle = false;
    AudioSource audioSource;
    
    //increase the piroirty of the music script
    
    
    private void Start()
    {
        //loop through all the songs in the game
        foreach (var song in songs)
        {
            //if the song is not null
            if (song != null)
            {
                song.MusicChanged= true;
            }
        }

        currentSongIndex = RandomSong();
        musicController.MusicScript = songs[currentSongIndex];
        audioSource = musicController.FFTObject.GetComponent<AudioSource>();
        musicController.OnMusicChange.AddListener(UpdateAudioSource);

    }

    private void Update()
    {
        //if the music stopped playing
        if(!audioSource.isPlaying)
        {
            //play the next song
            PlayNextSong();
        }
    }
    
    public void PlayNextSong()
    {
        //if shuffle is enabled
        if (shuffle)
        {
            //get a random song
            currentSongIndex = RandomSong(currentSongIndex);
        }
        else
        {
            //play the next song
            currentSongIndex++;
            if (currentSongIndex >= songs.Length)
            {
                currentSongIndex = 0;
            }
        }
        
        //play the song
        musicController.MusicScript = songs[currentSongIndex];
    }
    
    int RandomSong()
    {
        return UnityEngine.Random.Range(0, songs.Length);
    }
    
    int RandomSong(int index)
    {
        //get a different random song
        int random = RandomSong();
        while (random == index)
        {
            random = RandomSong();
        }
        
        return random;
    }
    
    void PlaySong(int index)
    {
        //play the song
        //check if the index is valid
        if (index < 0 || index >= songs.Length)
        {
            //if not valid play the first song
            index = 0;
        }
        musicController.MusicScript = songs[index];
    }
    
    void UpdateAudioSource()
    {
        //get the current index of the song
        currentSongIndex = Array.IndexOf(songs, musicController.MusicScript);
    }
}
