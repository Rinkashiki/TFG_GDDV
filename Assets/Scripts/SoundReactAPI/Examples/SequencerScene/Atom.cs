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
    private bool enableReliefMap;
    private Mesh coreMesh;
    private Vector3[] initPos;

    // Amplitude Scale
    [Header("Amplitude Scale")]
    [SerializeField] float scaleFactor;
    private bool enableScale;

    // Amplitude Color
    [Header("Amplitude Color")]
    [SerializeField] float colorFactor;
    private bool enableColor;
    private Color coreColor;
    private Color electronColor;


    void Start()
    {
        ampReact = GetComponent<AmplitudeReact>();
        bandReact = GetComponent<FreqBandReact>();

        atomRenderers = GetComponentsInChildren<MeshRenderer>();
        trails = GetComponentsInChildren<TrailRenderer>();

        // Core Relief Map
        coreMesh = core.GetComponent<MeshFilter>().mesh;
        initPos = coreMesh.vertices;

        // Amplitude Color
        coreColor = atomRenderers[0].material.color;
        electronColor = atomRenderers[1].material.color;
    }

    void Update()
    {
        // Rotate Electrons
        ElectronsRotation();

        // Core Relief Map
        if (enableReliefMap)
        {
            ampReact.AmplitudeReliefMap(coreMesh, noiseFactor, reliefFactor, initPos, ampReact.audioInput.GetAmplitudeBuffer() * waveFactor);
        }

        // Amplitude Scale
        if (enableScale)
        {
            ampReact.AmplitudeScale(core, Vector3.one, scaleFactor, Vector3.one);
        }

        // Amplitude Color
        if (enableColor)
        {
            AtomColors();
        }
    }

    private void ElectronsRotation()
    {
        for (int i = 0; i < electrons.Length; i++)
        {
            int band = Random.Range(0, 8);
            electrons[i].Rotate(Vector3.right, electronSpeed + ampReact.audioInput.GetBandBuffer(i) * bandFactor);
        }
    }

    private void AtomColors()
    {
        Color newColor;

        for (int i = 0; i < atomRenderers.Length; i++)
        {
            if (i == 0)
            {
                newColor = new Color(ampReact.audioInput.GetAmplitudeBuffer() * colorFactor, coreColor.g, coreColor.b);
                atomRenderers[i].material.color = Color.Lerp(atomRenderers[i].material.color, newColor, 0.2f);
                atomRenderers[i].material.SetColor("_EmissionColor", Color.Lerp(atomRenderers[i].material.color, newColor, 0.2f));
            }
            else
            {
                newColor = new Color(electronColor.r, electronColor.g, ampReact.audioInput.GetAmplitudeBuffer() * colorFactor);
                atomRenderers[i].material.color = Color.Lerp(atomRenderers[i].material.color, newColor, 0.2f);
                atomRenderers[i].material.SetColor("_EmissionColor", Color.Lerp(atomRenderers[i].material.color, newColor, 0.2f));
            }
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

    #region Core_Relief_Map

    public void EnableReliefMap()
    {
        enableReliefMap = !enableReliefMap;
    }

    #endregion

    #region Amplitude_Color

    public void EnableColor()
    {
        enableColor = !enableColor;
    }

    #endregion
}
