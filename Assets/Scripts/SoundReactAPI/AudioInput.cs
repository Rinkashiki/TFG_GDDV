#region Dependencies

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#endregion

[RequireComponent(typeof(AudioSource))]
public class AudioInput : MonoBehaviour
{
    #region Audio_Input_Variables

    private AudioSource audioSrc;
    public static float[] samples = new float[512];
    public float[] freqBand = new float[8];
    public static float[] bandBuffer = new float[8];
    private float[] bufferDecrease = new float[8];

    public float Amplitude, AmplitudeBuffer;
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
}
