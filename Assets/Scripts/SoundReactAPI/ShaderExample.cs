using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderExample : MonoBehaviour
{
    private SoundReact soundReact;

    [SerializeField]
    private GameObject[] reacts;

    // Start is called before the first frame update
    void Start()
    {
        soundReact = GetComponent<SoundReact>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeShaderProperty();
        bandScale();
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

    private void bandScale()
    {
        int band = 0;
        foreach (GameObject react in reacts)
        {
            soundReact.BandScale(react, band, Vector3.one, 0.2f, 1);
            band++;
        }
    }
}
