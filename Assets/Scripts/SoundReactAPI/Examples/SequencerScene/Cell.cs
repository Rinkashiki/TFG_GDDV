using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private AmplitudeReact ampReact;
    private FreqBandReact bandReact;

    // Core Scale
    [Header("Core Scale")]
    [SerializeField] GameObject cellCore;
    [SerializeField] float cellScaleFactor;

    // Appends Scale
    [Header("Appends Scale")]
    [SerializeField] GameObject[] appends;
    [SerializeField] float appendsScaleFactor;

    // Cell Rotation
    [Header("Cell Rotation")]
    [SerializeField] float rotFactor;
    private Vector3 axis;

    // Cell Color
    [Header("Cell Color")]
    [SerializeField] float colorFactor;
    private MeshRenderer cellRenderer;
    private Color newColor;
    private Color cellColor;

    // Appends Materials
    [Header("Appends Materials")]
    [SerializeField] Material appendsMaterial;
    [SerializeField] float dissolveFactor;
    
    // Start is called before the first frame update
    void Start()
    {
        ampReact = GetComponent<AmplitudeReact>();
        bandReact = GetComponent<FreqBandReact>();

        axis = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        // Cell Color
        cellRenderer = cellCore.GetComponent<MeshRenderer>();
        cellColor = cellRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        // Core Scale
        ampReact.AmplitudeScale(cellCore, Vector3.one, cellScaleFactor, Vector3.one * 2);

        // Appends Scale
        ScaleAppends();

        // Cell Rotation
        ampReact.AmplitudeRotation(gameObject, axis, rotFactor);

        // Cell Color
        newColor = new Color(cellColor.r, ampReact.audioInput.GetAmplitudeBuffer() * colorFactor, cellColor.b);
        cellRenderer.material.color = Color.Lerp(cellRenderer.material.color, newColor, 0.2f);
        cellRenderer.material.SetColor("_EmissionColor", Color.Lerp(cellRenderer.material.color, newColor, 0.2f));

        // Appends Material
        ampReact.AmplitudeShaderGraphMatProperty(appendsMaterial, "DissolveFactor", GenericSoundReact.MatPropertyType.Float, dissolveFactor);
    }

    private void ScaleAppends()
    {
        for (int i = 0; i < appends.Length; i++)
        {
            bandReact.BandScale(appends[i], i % 8, Vector3.up, appendsScaleFactor, Vector3.one * 0.5f);
        }
    }
}
