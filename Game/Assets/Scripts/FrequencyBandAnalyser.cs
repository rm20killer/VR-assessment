using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.Serialization;

public class FrequencyBandAnalyser : MonoBehaviour
{
    public enum Bands
    {
        Eight = 8,
        SixtyFour = 64,
    }


    AudioSource AudioSource;
    int FrequencyBins = 512;

    float[] Samples;
    float[] SampleBuffer;

    public float SmoothDownRate = 0;
    
    public float[] FreqBands8;
    public float[] FreqBands64;


    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();

        FreqBands8 = new float[8];
        FreqBands64 = new float[64];
        Samples = new float[FrequencyBins];
        SampleBuffer = new float[FrequencyBins];
    }


    void UpdateFreqBands8()
    {
        
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if(i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += Samples[count] * (count + 1);
                count++;
            }

            average /= count;
            FreqBands8[i] = average;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void UpdateFreqBands64()
    {
        int count = 0;
        int sampleCount = 1;
        int power = 0;

        for (int i = 0; i < 64; i++)
        {
            float average = 0;

            if (i == 16 || i == 32 || i == 40 || i == 48 || i == 56)
            {
                power++;
                sampleCount = (int)Mathf.Pow(2, power);
                if (power == 3)
                    sampleCount -= 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += Samples[count] * (count + 1);
                count++;
            }

            average /= count;
            FreqBands64[i] = average;
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        //get the spectrum data from the audio source and store it in the sample buffer
        AudioSource.GetSpectrumData(SampleBuffer, 0, FFTWindow.BlackmanHarris);

        //loop through the samples and lerp the samples to the sample buffer based on the smooth down rate
        for (int i = 0; i < Samples.Length; i++)
        {
            if (SampleBuffer[i] > Samples[i])
                Samples[i] = SampleBuffer[i];
            else
                Samples[i] = Mathf.Lerp(Samples[i], SampleBuffer[i], Time.deltaTime * SmoothDownRate);
        }

        //update the frequency bands
        UpdateFreqBands8();
        UpdateFreqBands64();
    }

    /// <summary>
    /// Gets the value of a frequency band
    /// </summary>
    /// <param name="index">The index of the frequency band</param>
    /// <param name="bands">If it either a eight or 64 band</param>
    /// <returns>The value of the specified frequency band</returns>
    public float GetBandValue(int index, Bands bands)
    {
        if(bands == Bands.Eight)
        {
            return FreqBands8[index];
        }
        return FreqBands64[index];
    }
    
}