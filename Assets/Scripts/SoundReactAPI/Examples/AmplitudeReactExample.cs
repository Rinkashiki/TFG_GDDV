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

    // Amplitude Phyllotunnel
    [Header("Amplitude Phyllotunnel")]
    [SerializeField] Vector3 tunnelSpeed;
    [SerializeField] Transform cameraTransform;
    [SerializeField] float cameraDistance;
    [SerializeField] float phyllotaxisDegree;
    [SerializeField] float speedFactor;
    [SerializeField] float tunnelScaleFactor;
    [SerializeField] float tunnelAperture;
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;
    private GameObject[] tunnelObjs;
    private TrailRenderer phylloTrail;

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

        tunnelObjs = new GameObject[2];

        // Amplitude Phyllotunnel
        for (int i = 0; i < tunnelObjs.Length; i++)
        {
            tunnelObjs[i] = ampReact.AmplitudePhyllotunnel(tunnelSpeed, phyllotaxisDegree, speedFactor, tunnelScaleFactor, cameraDistance, cameraTransform, tunnelAperture + i);
            tunnelObjs[i].transform.position = this.transform.position;

            // Modify Phyllotunnel Trail Renderer
            phylloTrail = tunnelObjs[i].GetComponentInChildren<TrailRenderer>();
            phylloTrail.startWidth = 0.1f;
            phylloTrail.time = 20;
            phylloTrail.material = new Material(Shader.Find("Sprites/Default"));
            float alpha = 1.0f;
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(startColor, 0.0f), new GradientColorKey(endColor, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
            phylloTrail.colorGradient = gradient;

            this.transform.SetParent(tunnelObjs[i].transform);

            phyllotaxisDegree = 90;
            speedFactor = 5;
            cameraTransform = null;
        }
        
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
        ampReact.AmplitudeScale(gameObject.transform, Vector3.one, scaleFactor, Vector3.one);

        // Amplitude Rotation
        ampReact.AmplitudeRotation(gameObject.transform, new Vector3(1, 1, 0), rotationFactor);

        // Amplitude Shader Graph Property
        ampReact.AmplitudeShaderGraphMatProperty(mat, "DissolveFactor", GenericSoundReact.MatPropertyType.Float, propertyFactor);

        // Amplitude Post-Processing
        ampReact.AmplitudeBloom(bloom, bloomFactor);
        ampReact.AmplitudeVignette(vignette, vignetteFactor);
    }
}
