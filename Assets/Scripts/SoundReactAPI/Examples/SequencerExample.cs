using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SequencerExample : MonoBehaviour
{
    private AmplitudeReact ampReact;

    // Sample Cubes
    [Header("Samples Circle")]
    [SerializeField] GameObject sampleCube;
    private GameObject[] sampleCubes;
    private bool enableSampleCubes;


    void Start()
    {
        ampReact = GetComponent<AmplitudeReact>();

        // Samples Cubes
        sampleCubes = new GameObject[8];
    }

    void Update()
    {
        // Sample Cubes
        if (enableSampleCubes)
        {
            ScaleSampleCubes();
        }
    }

    #region Sample_Cubes

    public void InstantiateSampleCubes()
    {
        GameObject cubeInstance;

        for (int i = 0; i < 8; i++)
        {
            cubeInstance = Instantiate(sampleCube, transform);
            cubeInstance.name = "SampleCube" + i;
            transform.eulerAngles = new Vector3(0, (360.0f / 8) * i, 0);
            cubeInstance.transform.position = Vector3.forward * 20;
            sampleCubes[i] = cubeInstance;
        }

        EnableSampleCubes();
    }

    private void  EnableSampleCubes()
    {
        enableSampleCubes = !enableSampleCubes;
    }

    private void ScaleSampleCubes()
    {
        float[] bands = ampReact.audioInput.GetBandsBuffer();

        for(int i = 0; i < sampleCubes.Length; i++)
        {
            Vector3 newScale = new Vector3(sampleCubes[i].transform.localScale.x, bands[i] * 10, sampleCubes[i].transform.localScale.z);
            sampleCubes[i].transform.localScale = Vector3.Lerp(sampleCubes[i].transform.localScale, newScale, 0.1f);
        }
    }

    #endregion
}
