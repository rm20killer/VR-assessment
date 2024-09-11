using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RandomFirework : MonoBehaviour
{ 
    public bool isFirework = true;
    
    //spawn range
    public float xRange = 10;
    public float yRange = 10;
    public float zRange = 10;
    public GameObject[] fireworkPrefabs;
    
    public FrequencyBandAnalyser _FFT;
    public FrequencyBandAnalyser.Bands _FreqBands = FrequencyBandAnalyser.Bands.Eight;
    public int _FrequencyBandIndex = 0;
    public float ThresholdMultiplier = 1.0f;
    public bool CanSpawn = true;
    /// <summary>
    /// get the frequency band analyser from the scene
    /// </summary>
    private void Start()
    {
        //look for the frequency band analyser in the scene
        _FFT = FindObjectOfType<FrequencyBandAnalyser>();
    }
    
    /// <summary>
    /// spawn a firework if the strength of the frequency band is greater than the threshold
    /// </summary>
    private void Update()
    {
        if(!isFirework)
        {
            return;
        }
        var strength = _FFT.FreqBands8[_FrequencyBandIndex];
        if (strength > ThresholdMultiplier)
        {
            spawnFirework();
        }
    }

    /// <summary>
    /// get a random firework prefab and spawn it at a random location within the range
    /// </summary>
    void spawnFirework()
    {
        //if we can't spawn a firework, return helps to prevent multiple fireworks from spawning at once and causing fps drops
        if (!CanSpawn)
        {
            return;
        }
        Debug.Log("firework spawned");
        
        //spawn a random firework at a random location within the range of the box
        Vector3 spawnLocation = new Vector3(UnityEngine.Random.Range(-xRange, xRange), UnityEngine.Random.Range(-yRange, yRange), UnityEngine.Random.Range(-zRange, zRange));
        //spawn location should be local to the parent object
        GameObject firework = Instantiate(fireworkPrefabs[UnityEngine.Random.Range(0, fireworkPrefabs.Length)], transform.position + spawnLocation, quaternion.identity);
        //destroy the firework after 5 seconds
        Destroy(firework, 5);
        
        CanSpawn = false;
        
        StartCoroutine(ResetSpawn());
    }

    /// <summary>
    /// wait for 3 seconds before allowing the firework to spawn again
    /// optimisation to prevent multiple fireworks from spawning at once
    /// </summary>
    /// <returns></returns>
    IEnumerator ResetSpawn()
    {
        yield return new WaitForSeconds(0.2f);
        CanSpawn = true;
    }

    /// <summary>
    /// create a red wire cube to show the range of the spawn area in the editor
    /// Testing reason
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(xRange, yRange, zRange));
    }
}

