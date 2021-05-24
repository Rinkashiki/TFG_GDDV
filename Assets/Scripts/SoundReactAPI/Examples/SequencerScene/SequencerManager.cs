﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SequencerManager : MonoBehaviour
{
    private AmplitudeReact ampReact;

    // Fade In
    [Header("Fade In")]
    [SerializeField] Image fadeIn;

    // Camera Rotation
    [Header("Camera Rotation")]
    [SerializeField] GameObject cameraObj;
    [SerializeField] float rotationFactor;
    private bool enableCameraRotation;

    // Atoms rotation
    [Header("Atoms Rotation")]
    [SerializeField] GameObject[] atoms;
    [SerializeField] float atomRotationFactor;
    private bool enableAtomsRotation;

    // Post-Processing
    [Header("Post-Processing")]
    [SerializeField] Volume vol;
    [SerializeField] float bloomFactor;
    [SerializeField] float vignetteFactor;
    [SerializeField] float chromAberrationFactor;
    private Bloom bloom;
    private Vignette vignette;
    private ChromaticAberration ca;
    private bool enableBloom, enableVignette, enableChromAberration;

    // Start is called before the first frame update
    void Start()
    {
        ampReact = GetComponent<AmplitudeReact>();

        // Post-Processing
        bloom = (Bloom)vol.profile.components[0];
        vignette = (Vignette)vol.profile.components[1];
        ca = (ChromaticAberration)vol.profile.components[2];

    }

    // Update is called once per frame
    void Update()
    {
        // Camera Rotation
        if (enableCameraRotation)
        {
            ampReact.AmplitudeRotation(cameraObj, Vector3.up, rotationFactor);
        }

        // Atoms Rotation
        if (enableAtomsRotation)
        {
            foreach (GameObject atom in atoms)
            {
                ampReact.AmplitudeRotation(atom, Vector3.right, atomRotationFactor);
            }
        }

        // Post-Processing
        if (enableBloom)
        {
            ampReact.AmplitudeBloom(bloom, bloomFactor);
        }

        if (enableVignette)
        {
            ampReact.AmplitudeVignette(vignette, vignetteFactor);
        }

        if (enableChromAberration)
        {
            ampReact.AmplitudeChromaticAberration(ca, chromAberrationFactor);
        }
    }

    #region FadeIn

    public void FadeIn()
    {
        fadeIn.CrossFadeAlpha(0, 10, false);
    }

    #endregion

    #region Camera_Rotation

    public void EnableCameraRotation()
    {
        enableCameraRotation = !enableCameraRotation;
    }

    #endregion

    #region Atoms_Rotation

    public void EnableAtomsRotation()
    {
        enableAtomsRotation = !enableAtomsRotation;
    }
    #endregion

    #region Post-Processing

    public void EnableBloom()
    {
        enableBloom = !enableBloom;
    }

    public void EnableVignette()
    {
        enableVignette = !enableVignette;
    }

    public void EnableChromAberration()
    {
        enableChromAberration = !enableChromAberration;
    }

    #endregion

    


}