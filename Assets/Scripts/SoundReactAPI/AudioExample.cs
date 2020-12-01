using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioExample : MonoBehaviour
{

    public float startScale, scaleMultiplier;
    public float rotMultiplier;
    public float startBrightness, brightnessMultiplier;
    public AudioInput audioInput;

    public GameObject[] cubes;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
        AmplitudeScale();
        AmplitudeRotate();
        AmplitudeBright();
        */
        for(int i = 0; i < cubes.Length; i++)
        {
            BandScale(cubes[i], i, Vector3.up, scaleMultiplier, startScale);
            BandBright(cubes[i], i, brightnessMultiplier);
        };
    }

    private void AmplitudeScale(GameObject go, Vector3 axis, float scaleFactor, float startScale)
    {
        go.transform.localScale = new Vector3(((audioInput.GetAmplitudeBuffer() * scaleFactor) * axis.x) + startScale, 
                                              ((audioInput.GetAmplitudeBuffer() * scaleFactor) * axis.y) + startScale,
                                              ((audioInput.GetAmplitudeBuffer() * scaleFactor) * axis.z) + startScale);
    }

    private void AmplitudeRotate(GameObject go, Vector3 axis, float rotFactor)
    {
        go.transform.Rotate(axis, audioInput.GetAmplitudeBuffer() * rotFactor);
    }

    private void AmplitudeBright(GameObject go, float brightFactor)
    {
        float colorValue = startBrightness + audioInput.GetAmplitudeBuffer() * brightFactor;
        Color color = new Color(colorValue, colorValue, colorValue);
        go.GetComponent<MeshRenderer>().material.color = color;
    }

    private void BandScale(GameObject go, int band, Vector3 axis, float scaleFactor, float startScale)
    {
        go.transform.localScale = new Vector3(((audioInput.GetBandBuffer(band) * scaleFactor) * axis.x) + startScale,
                                               ((audioInput.GetBandBuffer(band) * scaleFactor) * axis.y) + startScale,
                                               ((audioInput.GetBandBuffer(band) * scaleFactor) * axis.z) + startScale);
    }

    private void BandRotate(GameObject go, int band, Vector3 axis, float rotFactor)
    {
        go.transform.Rotate(axis, audioInput.GetBandBuffer(band) * rotFactor);
    }

    private void BandBright(GameObject go, int band, float brightFactor)
    {
        float colorValue = startBrightness + audioInput.GetBandBuffer(band) * brightFactor;
        Color color = new Color(colorValue, colorValue, colorValue);
        go.GetComponent<MeshRenderer>().material.color = color;
    }
}
