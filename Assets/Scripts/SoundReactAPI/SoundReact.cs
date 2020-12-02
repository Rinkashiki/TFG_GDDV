using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReact : MonoBehaviour
{
    #region Sound_React_Variables

    public AudioInput audioInput;

    #endregion

    #region Audio_Functions

    public void AmplitudeScale(GameObject go, Vector3 axis, float scaleFactor, float startScale)
    {
        go.transform.localScale = new Vector3(((audioInput.GetAmplitudeBuffer() * scaleFactor) * axis.x) + startScale,
                                              ((audioInput.GetAmplitudeBuffer() * scaleFactor) * axis.y) + startScale,
                                              ((audioInput.GetAmplitudeBuffer() * scaleFactor) * axis.z) + startScale);
    }

    public void AmplitudeRotate(GameObject go, Vector3 axis, float rotFactor)
    {
        go.transform.Rotate(axis, audioInput.GetAmplitudeBuffer() * rotFactor);
    }

    public void AmplitudeBright(GameObject go, float brightFactor, float startBrightness)
    {
        float colorValue = startBrightness + audioInput.GetAmplitudeBuffer() * brightFactor;
        Color color = new Color(colorValue, colorValue, colorValue);
        go.GetComponent<MeshRenderer>().material.color = color;
    }

    public void BandScale(GameObject go, int band, Vector3 axis, float scaleFactor, float startScale)
    {
        go.transform.localScale = new Vector3(((audioInput.GetBandBuffer(band) * scaleFactor) * axis.x) + startScale,
                                               ((audioInput.GetBandBuffer(band) * scaleFactor) * axis.y) + startScale,
                                               ((audioInput.GetBandBuffer(band) * scaleFactor) * axis.z) + startScale);
    }

    public void BandRotate(GameObject go, int band, Vector3 axis, float rotFactor)
    {
        go.transform.Rotate(axis, audioInput.GetBandBuffer(band) * rotFactor);
    }

    public void BandBright(GameObject go, int band, float brightFactor, float startBrightness)
    {
        float colorValue = startBrightness + audioInput.GetBandBuffer(band) * brightFactor;
        Color color = new Color(colorValue, colorValue, colorValue);
        go.GetComponent<MeshRenderer>().material.color = color;
    }

    #endregion
}
