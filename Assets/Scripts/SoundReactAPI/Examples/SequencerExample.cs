using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SequencerExample : MonoBehaviour
{
    private AmplitudeReact ampReact;

    // Amplitude Scale
    [Header("Amplitude Scale")]
    [SerializeField] float scaleFactor;
    private float initialScale;
    private bool increaseScale;

    // Amplitude Bloom
    [Header("Amplitude Bloom")]
    [SerializeField] Volume vol;
    [SerializeField] float bloomFactor;
    private Bloom bloom;
    private bool enableBloom;

    // Amplitude Relief Map
    [Header("Amplitude Relief Map")]
    [SerializeField] float reliefFactor;
    [SerializeField] float noisefactor;
    private Vector3[] initPos;
    private bool enableRelief;


    void Start()
    {
        ampReact = GetComponent<AmplitudeReact>();

        // Amplitude Scale
        initialScale = 0.3f;

        // Amplitude Bloom
        bloom = (Bloom)vol.profile.components[0];

        // Amplitude Relief Map
        initPos = gameObject.GetComponent<MeshFilter>().mesh.vertices;
    }

    void Update()
    {
        // Amplitude Scale
        ampReact.AmplitudeScale(gameObject, Vector3.one, scaleFactor, initialScale);

        // Amplitude Bloom
        if (enableBloom)
        {
            ampReact.AmplitudeBloom(bloom, bloomFactor);
        }

        // Amplitude Relief Map
        if (enableRelief)
        {
            ampReact.AmplitudeReliefMap(gameObject.GetComponent<MeshFilter>().mesh, noisefactor, reliefFactor, initPos, 0.5f);
        }

    }

    public void IncreaseInitialScale()
    {
        initialScale = 1.0f;
    }

    public void EnableBloom()
    {
        enableBloom = !enableBloom;
    }

    public void EnableReliefMap()
    {
        enableRelief = !enableRelief;
    }
}
