#region Dependencies

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#endregion

[RequireComponent(typeof(AudioSource))]
public class AudioInput : MonoBehaviour
{
    #region Audio_Input_Variables

    private AudioSource audioSrc;
    private float[] samples = new float[512];
    private float[] freqBand = new float[8];
    private float[] bandBuffer = new float[8];
    private float[] bufferDecrease = new float[8];

    private float Amplitude, AmplitudeBuffer;
    private float AmplitudHighest = 10;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        ComputeAmplitude();
    }

    #region Compute_Functions

    private void GetSpectrumAudioSource()
    {
        audioSrc.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    private void BandBuffer()
    {
        for (int i = 0; i < freqBand.Length; i++)
        {
            if (freqBand[i] > bandBuffer[i])
            {
                bandBuffer[i] = freqBand[i];
                bufferDecrease[i] = 0.005f;
            }
            if (freqBand[i] < bandBuffer[i])
            {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f;
            }
        }

    }

    private void ComputeAmplitude()
    {
        float currentAmplitude = 0;
        float currentAmplitudeBuffer = 0;
        for (int i = 0; i < freqBand.Length; i++)
        {
            currentAmplitude += freqBand[i];
            currentAmplitudeBuffer += bandBuffer[i];
        }
        if (currentAmplitude > AmplitudHighest)
        {
            AmplitudHighest = currentAmplitude;
        }
        Amplitude = currentAmplitude / AmplitudHighest;
        AmplitudeBuffer = currentAmplitudeBuffer / AmplitudHighest;

    }

    private void MakeFrequencyBands()
    {
        int count = 0;

        for (int i = 0; i < freqBand.Length; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if (i == 7)
            {
                sampleCount += 2;
            }
            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;
            freqBand[i] = average * 10;
        }

    }

    #endregion

    #region Audio_Getters

    public float[] GetSamples()
    {
        return samples;
    }

    public float[] GetFreqBands()
    {
        return freqBand;
    }

    public float GetFreqBand(int band)
    {
        if(band < 0 || band > 7)
        {
            throw new ArgumentOutOfRangeException("band", "There are 8 bands. Band parameter must be between 0 and 7");
        }
        return freqBand[band];
    }

    public float[] GetBandsBuffer()
    {
        return bandBuffer;
    }

    public float GetBandBuffer(int band)
    {
        if (band < 0 || band > 7)
        {
            throw new ArgumentOutOfRangeException("band", "There are 8 bands. Band parameter must be between 0 and 7");
        }
        return bandBuffer[band];
    }

    public float GetAmplitude()
    {
        return Amplitude;
    }

    public float GetAmplitudeBuffer()
    {
        return AmplitudeBuffer;
    }

    #endregion
}
