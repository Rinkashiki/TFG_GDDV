using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioExample : MonoBehaviour
{

    public float startScale, scaleMultiplier;
    public float rotMultiplier;
    public float startBrightness, brightnessMultiplier;
    public float heightFactor, noiseFactor;
    private SoundReact soundReact;

    public GameObject[] cubes;

    //public GameObject terrain;
    private Mesh terrainMesh;

    private float currentWidth = 0;

    // Start is called before the first frame update
    void Start()
    {
        soundReact = GetComponent<SoundReact>();
        //this.transform.Rotate(Vector3.right, 180);
        terrainMesh = GetComponent<MeshFilter>().mesh;
    }

    // Update is called once per frame
    void Update()
    {
        CreateTerrainBands();
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
}
