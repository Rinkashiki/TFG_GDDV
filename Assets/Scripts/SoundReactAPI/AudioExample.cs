using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioExample : MonoBehaviour
{

    [SerializeField] private float startScale, scaleMultiplier;
    [SerializeField] private float rotMultiplier;
    [SerializeField] private float startBrightness, brightnessMultiplier;
    
    private SoundReact soundReact;

    // Cubes variables
    [SerializeField] private GameObject[] cubes;

    // Terrain variables
    [SerializeField] private float heightFactor, noiseFactor;
    private Mesh terrainMesh;
    private float currentWidth = 0;

    //Light variables
    private Light sceneLight;
    [SerializeField] private float intensityFactor;
    [SerializeField] private float rangeFactor;


    // Start is called before the first frame update
    void Start()
    {
        soundReact = GetComponent<SoundReact>();
        //terrainMesh = GetComponent<MeshFilter>().mesh;
        sceneLight = GetComponent<Light>();

    }

    // Update is called once per frame
    void Update()
    {
        LightIntensity();
        LightRange();
        //CreateTerrainBands();
        //CreateTerrainAmplitude();
        //UpdateTerrain();
        //UpdateCubes();
    }

    private void UpdateCubes()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            soundReact.BandScale(cubes[i], i, Vector3.up, scaleMultiplier, startScale);
            soundReact.BandBright(cubes[i], i, brightnessMultiplier, startBrightness);
        }
    }

    private void UpdateTerrain()
    {
        soundReact.AmplitudeTerrainHeightMap(terrainMesh, 0.5f, scaleMultiplier);
    }

    private void CreateTerrainAmplitude()
    {
        currentWidth = soundReact.AmplitudeGenerateTerrainLine(terrainMesh, 16, currentWidth, 0.1f, 10);
    }

    private void CreateTerrainBands()
    {
        currentWidth = soundReact.BandGenerateTerrainLine(terrainMesh, 16, currentWidth, 0.1f, heightFactor, noiseFactor);
    }

    private void LightIntensity()
    {
        soundReact.AmplitudeLightIntensity(sceneLight, intensityFactor);
    }

    private void LightRange()
    {
        soundReact.AmplitudeLightRange(sceneLight, rangeFactor);
    }
}
