using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterUnion : MonoBehaviour
{
    private FreqBandReact bandReact;
    private AmplitudeReact ampReact;

    [SerializeField] int band;
    [SerializeField] float scaleFactor;

    // Scale
    private Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        bandReact = GetComponent<FreqBandReact>();
        ampReact = GetComponent<AmplitudeReact>();

        // Scale
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Scale
        bandReact.BandScale(transform, band, Vector3.up, scaleFactor, initialScale);

        // Material Control
        //ampReact.AmplitudeShaderGraphMatProperty(gameObject.GetComponent<MeshRenderer>().material, "FresnelPower", GenericSoundReact.MatPropertyType.Float, 5f);
    }
}