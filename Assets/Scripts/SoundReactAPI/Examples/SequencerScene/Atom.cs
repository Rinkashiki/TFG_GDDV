using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atom : MonoBehaviour
{
    private AmplitudeReact ampReact;
    private FreqBandReact bandReact;

    private MeshRenderer[] atomRenderers;
    private TrailRenderer[] trails;

    // Rotate Electrons
    [Header("Rotate Electrons")]
    [SerializeField] Transform[] electrons;
    [SerializeField] float electronSpeed;
    [SerializeField] float bandFactor;
    [SerializeField] float waveFactor;

    // Core Relief Map
    [Header("Core Relief Map")]
    [SerializeField] GameObject core;
    [SerializeField] float noiseFactor;
    [SerializeField] float reliefFactor;
    private Mesh coreMesh;
    private Vector3[] initPos;

    // Amplitude Scale
    [Header("Amplitude Scale")]
    [SerializeField] float scaleFactor;
    private bool enableScale;


    void Start()
    {
        ampReact = GetComponent<AmplitudeReact>();
        bandReact = GetComponent<FreqBandReact>();

        atomRenderers = GetComponentsInChildren<MeshRenderer>();
        trails = GetComponentsInChildren<TrailRenderer>();

        // Core Relief Map
        coreMesh = core.GetComponent<MeshFilter>().mesh;
        initPos = coreMesh.vertices;
    }

    void Update()
    {
        // Rotate Electrons
        ElectronsRotation();

        // Core Relief
        //ampReact.AmplitudeReliefMap(coreMesh, noiseFactor, reliefFactor, initPos, ampReact.audioInput.GetAmplitudeBuffer() * waveFactor);

        // Amplitude Scale
        if (enableScale)
        {
            ampReact.AmplitudeScale(core, Vector3.one, scaleFactor);
        }
    }

    private void ElectronsRotation()
    {
        for (int i = 0; i < electrons.Length; i++)
        {
            electrons[i].Rotate(Vector3.right, electronSpeed + ampReact.audioInput.GetBandBuffer(i) * bandFactor);
        }
    }

    public void EnableAtom()
    {
        foreach(MeshRenderer rend in atomRenderers)
        {
            rend.enabled = true;
        }

        foreach(TrailRenderer trail in trails)
        {
            trail.enabled = true;
        }
    }

    public void EnableRenderers()
    {
        foreach (MeshRenderer rend in atomRenderers)
        {
            rend.enabled = true;
        }
    }

    #region Amplitude_Scale

    public void EnableScale()
    {
        enableScale = !enableScale;
    }

    #endregion
}
