using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AmplitudeReactExample : MonoBehaviour
{

    private AmplitudeReact ampReact;

    // Amplitude Scale
    [Header("Amplitude Scale")]
    [SerializeField] float scaleFactor;

    // Amplitude Bright
    /*
    [Header("Amplitude Bright")]
    [SerializeField] float brightFactor;
    private Color initialColor;
    */

    // Amplitude Relief Map
    /*
    [Header("Amplitude Relief Map")]
    [SerializeField] float noiseFactor;
    [SerializeField] float heightFactor;
    [SerializeField] float waveSpeed;
    private Mesh mesh;
    private Vector3[] initPos;
    */

    //Amplitude Rotation
    [Header("Amplitude Rotation")]
    [SerializeField] float rotationFactor;

    // Amplitude Shader Graph Property
    [Header("Amplitude Shader Graph Property")]
    [SerializeField] float propertyFactor;
    private Material mat;

    // Amplitude Post-Processing
    [Header("Amplitude Post-Processing")]
    [SerializeField] Volume vol;
    [SerializeField] float bloomFactor;
    [SerializeField] float vignetteFactor;
    private Bloom bloom;
    private Vignette vignette;
    

    // Start is called before the first frame update
    void Start()
    {
        ampReact = GetComponent<AmplitudeReact>();

        // Amplitude Bright
        /*
        initialColor = this.gameObject.GetComponent<MeshRenderer>().material.color;
        */

        // Amplitude Relief Map
        /*
        mesh = this.gameObject.GetComponent<MeshFilter>().mesh;
        initPos = mesh.vertices;
        */

        // Amplitude Shader Graph Property
        mat = this.gameObject.GetComponent<MeshRenderer>().material;

        // Amplitude Post-Processing
        bloom = (Bloom)vol.profile.components[0];
        vignette = (Vignette)vol.profile.components[1];
    }

    // Update is called once per frame
    void Update()
    {
        // Amplitude Scale
        ampReact.AmplitudeScale(this.gameObject, Vector3.one, scaleFactor);

        // Amplitude Bright
        /*
        ampReact.AmplitudeBright(this.gameObject, brightFactor, initialColor);
        */

        // Amplitude Relief Map
        /*
        ampReact.AmplitudeReliefMap(mesh, noiseFactor, heightFactor, initPos, waveSpeed);
        */

        // Amplitude Rotation
        ampReact.AmplitudeRotation(this.gameObject, new Vector3(1, 1, 0), rotationFactor);

        // Amplitude Shader Graph Property
        ampReact.AmplitudeShaderGraphMatProperty(mat, "DissolveFactor", GenericSoundReact.MatPropertyType.Float, propertyFactor);

        // Amplitude Post-Processing
        ampReact.AmplitudeBloom(bloom, bloomFactor);
        ampReact.AmplitudeVignette(vignette, vignetteFactor);
    }
}
