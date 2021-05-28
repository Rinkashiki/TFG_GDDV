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
