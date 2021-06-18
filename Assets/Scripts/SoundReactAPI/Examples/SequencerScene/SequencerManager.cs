using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SequencerManager : MonoBehaviour
{
    private AmplitudeReact ampReact;
    private FreqBandReact bandReact;

    // Fade In
    [Header("Fade In")]
    [SerializeField] Image fadeIn;


    // Camera Rotation
    [Header("Camera Rotation")]
    [SerializeField] Transform cameraObj;
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
    [SerializeField] GameObject cellsParent;
    private bool enableCell;
    private bool enableMultipleCells;
    private bool enableCellsFade;

    // DNA Trail
    [Header("DNA Trail")]
    [SerializeField] Vector3 tunnelSpeed;
    [SerializeField] float degree;
    [SerializeField] float speedFactor;
    [SerializeField] Color startColorRed;
    [SerializeField] Color endColorRed;
    [SerializeField] Color startColorBlue;
    [SerializeField] Color endColorBlue;
    private GameObject[] tunnelObjs;
    private TrailRenderer phylloTrail;
    private bool enableDNATrail;

    // Cell Clusters
    [Header("Cell Clusters")]
    [SerializeField] GameObject clusters;
    [SerializeField] Material clusterMaterial;
    private ClusterCell[] clusterCells;
    private Color newColor;
    private bool enableClusters;

    // Terrain
    [Header("Terrain")]
    [SerializeField] float step;
    [SerializeField] float heightFactor;
    [SerializeField] float noiseFactor;
    [SerializeField] Vector3 terrainDir;
    [SerializeField] float terrainOffset;
    private GameObject terrainObj;
    private GenerateTerrain terrainComp;
    private bool enableTerrain;

    // Particles
    [Header("Particles")]
    [SerializeField] ParticleSystem particles;
    private ParticleSystem.EmissionModule emission;
    private ParticleSystem.VelocityOverLifetimeModule velocityModule;
    private bool enableParticles;

    // Post-Processing
    [Header("Post-Processing")]
    [SerializeField] Volume vol;
    [SerializeField] float bloomFactor;
    [SerializeField] Color bloomColor;
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
        bandReact = GetComponent<FreqBandReact>();

        // Post-Processing
        bloom = (Bloom)vol.profile.components[0];
        vignette = (Vignette)vol.profile.components[1];
        ca = (ChromaticAberration)vol.profile.components[2];

        // DNA Trail
        tunnelObjs = new GameObject[4];

        // Cluster Cells
        clusterCells = clusters.GetComponentsInChildren<ClusterCell>();

        // Particles
        emission = particles.emission;
        velocityModule = particles.velocityOverLifetime;

    }

    // Update is called once per frame
    void Update()
    {
        // Camera Rotation
        if (enableCameraRotation)
        {
            ampReact.AmplitudeRotation(cameraObj.transform, Vector3.up, rotationFactor);
        }

        // Atoms Rotation
        if (enableAtomsRotation)
        {
            foreach (GameObject atom in atoms)
            {
                ampReact.AmplitudeRotation(atom.transform, Vector3.right, atomRotationFactor);
            }
        }

        // Atoms Fade
        if (enableAtomsFade)
        {
            atomsParent.transform.localScale = Vector3.Lerp(atomsParent.transform.localScale, Vector3.zero, 0.05f);

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
            cells[0].transform.localScale = Vector3.Lerp(cells[0].transform.localScale, Vector3.one, 0.05f);
        }

        if (enableMultipleCells)
        {
            for (int i = 1; i < cells.Length; i++)
            {
                cells[i].transform.localPosition = Vector3.Lerp(cells[i].transform.localPosition, cellPositions[i - 1].localPosition, 0.1f);
            }
        }

        // Enable cells fade
        if (enableCellsFade)
        {
            cellsParent.transform.localScale = Vector3.Lerp(cellsParent.transform.localScale, Vector3.zero, 0.05f);

            if (cellsParent.transform.localScale == Vector3.zero)
            {
                cellsParent.SetActive(false);
            }
        }

        // DNA Trail
        if (enableDNATrail)
        {
            for (int i = 0; i < tunnelObjs.Length; i++)
            {
                ampReact.AmplitudeRotation(tunnelObjs[i].transform, Vector3.forward, rotationFactor * 2);
            }
        }

        // Cell Clusters
        if (enableClusters)
        {
            ampReact.AmplitudeShaderGraphMatProperty(clusterMaterial, "FresnelPower", GenericSoundReact.MatPropertyType.Float, 4f);

            newColor = new Color(1 - ampReact.audioInput.GetAmplitudeBuffer() * 0.5f, ampReact.audioInput.GetAmplitudeBuffer() * 0.8f, clusterMaterial.GetColor("EmissionColor").b);
            clusterMaterial.SetColor("EmissionColor", Color.Lerp(clusterMaterial.GetColor("EmissionColor"), newColor, 0.1f));
        }

        // Move Camera for terrain
        if (enableTerrain)
        {
            cameraObj.transform.position = new Vector3(cameraObj.transform.position.x, cameraObj.transform.position.y, terrainComp.GetTerrainPos().x + terrainOffset);
        }

        // Particles
        if (enableParticles)
        {
            emission.rateOverTime = ampReact.audioInput.GetAmplitudeBuffer() * 20;
            velocityModule.speedModifierMultiplier = ampReact.audioInput.GetAmplitudeBuffer() * 10;
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

    public void FadeOut()
    {
        fadeIn.CrossFadeAlpha(1, 3, false);
    }

    #endregion

    #region Camera_Rotation

    public void EnableCameraRotation()
    {
        enableCameraRotation = !enableCameraRotation;
        cameraObj.transform.rotation = Quaternion.identity;
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
        for (int i = 1; i < cells.Length; i++)
        {
            cells[i].SetActive(true);
        }

        enableMultipleCells = !enableMultipleCells;
    }

    public void EnableCellsFade()
    {
        enableCellsFade = !enableCellsFade;
    }

    #endregion

    #region DNA_Trail

    public void EnableDNATrail()
    {
        // Red Trails
        DNATrail(0, -6f, new Vector3(-90f, 0, 0), startColorRed, endColorRed);
        DNATrail(1, 6f, new Vector3(-90f, 0, 0), startColorRed, endColorRed);

        // Blue Trail
        DNATrail(2, -6f, new Vector3(-90f, 180f, 0), startColorBlue, endColorBlue);
        DNATrail(3, 6f, new Vector3(-90f, 180f, 0), startColorBlue, endColorBlue);

        enableDNATrail = !enableDNATrail;
    }

    public void DisableDNATrail()
    {
        foreach (GameObject dnaTunnel in tunnelObjs)
        {
            dnaTunnel.SetActive(false);
        }
    }

    private void DNATrail(int trail, float x, Vector3 rotation, Color startColor, Color endColor)
    {
        tunnelObjs[trail] = ampReact.AmplitudePhyllotunnel(tunnelSpeed, degree, speedFactor, 0, 0, null, 1);
        tunnelObjs[trail].transform.position = new Vector3(x, -5.5f, -5);
        tunnelObjs[trail].transform.Rotate(rotation);
        phylloTrail = tunnelObjs[trail].GetComponentInChildren<TrailRenderer>();
        phylloTrail.startWidth = 0.1f;
        phylloTrail.time = 6;
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

    #region Cell_Clusters

    public void EnableAllClusters()
    {
        clusters.SetActive(true);
        enableClusters = !enableClusters;
    }

    public void EnableClusterCellsScale()
    {
        foreach (ClusterCell cell in clusterCells)
        {
            cell.enableScale = true;
        }
    }

    #endregion

    #region Terrain

    public void EnableTerrain()
    {
        terrainObj = bandReact.BandsGenerateTerrain(64, step, heightFactor, noiseFactor, terrainDir, clusterMaterial);
        terrainObj.transform.Translate(32, -11, 0);
        terrainObj.transform.Rotate(0, -90, 0);
        terrainComp = terrainObj.GetComponent<GenerateTerrain>();
        enableTerrain = !enableTerrain;
    }
    #endregion

    #region Particles

    public void EnableParticles()
    {
        particles.Play();
        enableParticles = !enableParticles;
    }

    #endregion

    #region Post-Processing

    public void EnableBloom()
    {
        enableBloom = !enableBloom;
    }

    public void ChangeBloomTint()
    {
        bloom.tint.value = bloomColor;
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
