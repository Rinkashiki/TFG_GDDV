using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ShaderExample : MonoBehaviour
{
    private AmplitudeReact ampReact;
    private FreqBandReact bandReact;

    [SerializeField]
    private GameObject[] reacts;

    [SerializeField]
    private Volume vol;

    private ChromaticAberration ca;
    private Bloom bloom;
    private Animator[] anims;

    [SerializeField]
    private GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        ampReact = GetComponent<AmplitudeReact>();
        bandReact = GetComponent<FreqBandReact>();

        ca = (ChromaticAberration)vol.profile.components[0];
        bloom = (Bloom)vol.profile.components[1];
        anims = new Animator[reacts.Length];

        for (int i = 0; i < anims.Length; i++)
            anims[i] = reacts[i].GetComponent<Animator>();
            
    }

    // Update is called once per frame
    void Update()
    {
        
        ChangeShaderProperty();
        bandScale();
        ChangeChromaticAberration();
        ChangeBloom();
        ChangeAnimSpeed();

        //AudioTranslate();
    }

    private void ChangeShaderProperty()
    {
        int band = 0;
        foreach(GameObject react in reacts)
        {
            bandReact.BandShaderGraphMatProperty(react.GetComponent<MeshRenderer>().material, band, "DissolveFactor", GenericSoundReact.MatPropertyType.Float, 0.25f);
            band++;
        }
    }

    private void ChangeChromaticAberration()
    {
        ampReact.AmplitudeChromaticAberration(ca, 1f);
    }

    private void ChangeBloom()
    {
        ampReact.AmplitudeBloom(bloom, 10f);
    }

    private void ChangeAnimSpeed()
    {
        for (int i = 0; i < anims.Length; i++)
            bandReact.BandAnimationSpeed(anims[i], i, 1f);
    }

    private void bandScale()
    {
        int band = 0;
        foreach (GameObject react in reacts)
        {
            bandReact.BandScale(react, band, Vector3.one, 0.2f, Vector3.one);
            band++;
        }
    }

    private void AudioTranslate()
    {
        ampReact.AmplitudeTranslation(go, Vector3.right, 0.05f);
    }
}
