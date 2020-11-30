using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioExample : MonoBehaviour
{

    public float startScale, scaleMultiplier;
    public float rotMultiplier;
    public float startBrightness, brightnessMultiplier;
    public AudioInput audioInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AudioScale();
        AudioRotate();
        AudioBright();
    }

    private void AudioScale()
    {
        transform.localScale = new Vector3((audioInput.GetAmplitudeBuffer() * scaleMultiplier) + startScale, (audioInput.GetAmplitudeBuffer() * scaleMultiplier)
                                          + startScale, (audioInput.GetAmplitudeBuffer() * scaleMultiplier) + startScale);
    }

    private void AudioRotate()
    {
        transform.Rotate(Vector3.up, audioInput.GetAmplitudeBuffer() * rotMultiplier);
    }

    private void AudioBright()
    {
        float colorValue = startBrightness + audioInput.GetAmplitudeBuffer() * brightnessMultiplier;
        Color color = new Color(colorValue, colorValue, colorValue);
        GetComponent<MeshRenderer>().material.color = color;
    }
}
