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

    // Atoms Fade
    [Header("Atoms Fade")]
    [SerializeField] GameObject atomsParent;
    private bool enableAtomsFade;

    // Trails Fade
    [Header("Trails Fade")]
    [SerializeField] TrailRenderer[] trails;
    private bool enableTrailsFade;

    // Enable Cells
    [Header("Enable Cell")]
    [SerializeField] GameObject[] cells;
    [SerializeField] Transform[] cellPositions;
    private bool enableCell;
    private bool enableMultipleCells;

    // DNA Trail
    [Header("DNA Trail")]
    [SerializeField] Vector3 tunnelSpeed;
    [SerializeField] float degree;
    [SerializeField] float speedFactor;
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;
    private GameObject tunnelObj;
    private TrailRenderer phylloTrail;
    private bool enableDNATrail;

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

        // Atoms Fade
        if (enableAtomsFade)
        {
            atomsParent.transform.localScale = Vector3.Lerp(atomsParent.transform.localScale, Vector3.zero, 0.1f);

            if (atomsParent.transform.localScale == Vector3.zero)
            {
                atomsParent.SetActive(false);
            }
        }

        if (enableTrailsFade)
        {
            foreach (TrailRenderer trail in trails)
            {
                trail.emitting = false;
            }
        }

        // Enable cells
        if (enableCell)
        {
            cells[0].transform.localScale = Vector3.Lerp(cells[0].transform.localScale, Vector3.one, 0.1f);
        }

        if (enableMultipleCells)
        {
            for(int i = 1; i < cells.Length; i++)
            {
                cells[i].transform.localPosition = Vector3.Lerp(cells[i].transform.localPosition, cellPositions[i - 1].localPosition, 0.1f);
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

    #region Atoms_Fade

    public void EnableAtomsFade()
    {
        enableAtomsFade = !enableAtomsFade;
    }

    #endregion

    #region Trails_Fade

    public void EnableTrailsFade()
    {
        enableTrailsFade = !enableTrailsFade;
    }

    #endregion

    # region Enable_Cells

    public void EnableCell()
    {
        cells[0].SetActive(true);
        enableCell = !enableCell;
    }

    public void EnableMultipleCells()
    {
        for(int i = 1; i < cells.Length; i++)
        {
            cells[i].SetActive(true);
        }

        enableMultipleCells = !enableMultipleCells;
    }

    #endregion

    #region DNA_Trail

    public void EnableDNATrail()
    {
        tunnelObj = ampReact.AmplitudePhyllotunnel(tunnelSpeed, degree, speedFactor, 0, 0, null, 2);
        tunnelObj.transform.position = new Vector3(0, -5, -5);
        tunnelObj.transform.Rotate(-90f, 0, 0);
        phylloTrail = tunnelObj.GetComponentInChildren<TrailRenderer>();
        phylloTrail.startWidth = 0.2f;
        phylloTrail.time = 20;
        phylloTrail.material = new Material(Shader.Find("Sprites/Default"));
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(startColor, 0.0f), new GradientColorKey(endColor, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        phylloTrail.colorGradient = gradient;
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
