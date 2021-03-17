using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ShaderExample : MonoBehaviour
{
    private SoundReact soundReact;

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
        soundReact = GetComponent<SoundReact>();
        
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
            soundReact.BandShaderGraphMatProperty(react.GetComponent<MeshRenderer>().material, band, "DissolveFactor", GenericSoundReact.MatPropertyType.Float, 0.25f);
            band++;
        }
    }

    private void ChangeChromaticAberration()
    {
        soundReact.AmplitudeChangeChromaticAberration(ca, 1f);
    }

    private void ChangeBloom()
    {
        soundReact.AmplitudeChangeBloom(bloom, 10f);
    }

    private void ChangeAnimSpeed()
    {
        for (int i = 0; i < anims.Length; i++)
            soundReact.BandAnimationSpeed(anims[i], i, 1f);
    }

    private void bandScale()
    {
        int band = 0;
        foreach (GameObject react in reacts)
        {
            soundReact.BandScale(react, band, Vector3.one, 0.2f, 1);
            band++;
        }
    }

    private void AudioTranslate()
    {
        soundReact.AmplitudeTranslation(go, Vector3.right, 0.05f);
    }
}
