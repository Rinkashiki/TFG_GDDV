using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SequencerExample : MonoBehaviour
{
    private AmplitudeReact ampReact;
    private FreqBandReact bandReact;

    // Rotate Electrons
    [Header("Rotate Electrons")]
    [SerializeField] Transform[] electrons;
    private bool enableElectronsRotation;

    void Start()
    {
        ampReact = GetComponent<AmplitudeReact>();
        bandReact = GetComponent<FreqBandReact>();
    }

    void Update()
    {
        // Sample Cubes
        if (enableElectronsRotation)
        {
            ElectronsRotation();
        }
    }

    private void ElectronsRotation()
    {
        for (int i = 0; i < electrons.Length; i++)
        {
            electrons[i].Rotate(Vector3.right, 0.2f + ampReact.audioInput.GetBandBuffer(i) * 0.5f);
        }
    }

    public void EnableElectrons()
    {
        enableElectronsRotation = !enableElectronsRotation;
    }
}
