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
    
    //create a list to store the int values of the songs that been played
    private List<int> playedSongs = new List<int>();
    
    
    
    /// <summary>
    /// make sure music changed is set to true for all the songs
    /// play a random song from the list and attach to the music controller
    /// </summary>
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

    
    /// <summary>
    /// check if the music stopped playing and play the next song
    /// </summary>
    private void Update()
    {
        //if the music stopped playing
        if(!audioSource.isPlaying)
        {
            //play the next song
            PlayNextSong();
        }
    }
    
    /// <summary>
    /// play the next song in the list of songs unless shuffle is enabled then play a random song
    /// add song to the list of played songs to prevent it from being played again and allow the previous song to be played
    /// </summary>
    public void PlayNextSong()
    {
        playedSongs.Add(currentSongIndex);
        //if shuffle is enabled
        if (shuffle)
        {
            //get a random song
            currentSongIndex = RandomSong(playedSongs);
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
    
    /// <summary>
    /// play a random song from the list of songs
    /// </summary>
    /// <returns>random int between 0 and the amount of songs in the list </returns>
    int RandomSong()
    {
        return UnityEngine.Random.Range(0, songs.Length);
    }
    
    /// <summary>
    /// play a random song from the list of songs that is not the current song
    /// </summary>
    /// <param name="index">current song index</param>
    /// <returns>random int that does not match the input</returns>
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
    

    /// <summary>
    /// play a random song from the list of songs that is not in the list of played songs
    /// </summary>
    /// <param name="songs_">list of int of songs played</param>
    /// <returns>random int which is not in the list</returns>
    int RandomSong(List<int> songs_)
    {
        int random = RandomSong();
        while (songs_.Contains(random))
        {
            random = RandomSong();
        }
        return random;
    }
    
    /// <summary>
    /// play a specific song from the list of songs
    /// </summary>
    /// <param name="index">song index</param>
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
    
    /// <summary>
    /// play the previous song that was played unless there are no previous songs then play a random song
    /// </summary>
    public void PreviousSong()
    {
        //check if there are any previous songs
        if (playedSongs.Count > 0)
        {
            //get the last song that was played
            int lastSong = playedSongs[playedSongs.Count - 1];
            //remove the last song from the list
            playedSongs.RemoveAt(playedSongs.Count - 1);
            //play the last song
            PlaySong(lastSong);
        }
        else
        {
            //play the first song
            PlaySong(RandomSong());
        }
        
    }
    
    /// <summary>
    /// get the current song index to make sure the current song index is correct
    /// </summary>
    void UpdateAudioSource()
    {
        //get the current index of the song
        currentSongIndex = Array.IndexOf(songs, musicController.MusicScript);
    }
}
