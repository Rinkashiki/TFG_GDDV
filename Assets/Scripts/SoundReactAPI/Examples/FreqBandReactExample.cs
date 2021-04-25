using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreqBandReactExample : MonoBehaviour
{

    private FreqBandReact bandReact;

    // Bands Generate Terrain
    [Header("Bands Generate Terrain")]
    [SerializeField] float step;
    [SerializeField] float heightFactor;
    [SerializeField] float noiseFactor;
    [SerializeField] Vector3 terrainDir;
    [SerializeField] Material terrainMat;
    private GameObject terrain;
    private GenerateTerrain terrainComp;

    //Camera Terrain Follow
    private Camera cam;
    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        bandReact = GetComponent<FreqBandReact>();

        //Bands Generate Terrain
        terrain = bandReact.BandsGenerateTerrain(16, step, heightFactor, noiseFactor, terrainDir, terrainMat);
        terrainComp = terrain.GetComponent<GenerateTerrain>();

        //Camera Terrain Follow
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        offset = new Vector3 (0, 10, -10);
    }

    // Update is called once per frame
    void Update()
    {
        //Bands Generate Terrain
        terrainComp.SetStep(step);
        terrainComp.SetHeightFactor(heightFactor);
        terrainComp.SetNoiseFactor(noiseFactor);
        terrainComp.SetTerrainDir(terrainDir);

        CameraFollow();
    }

    private void CameraFollow()
    {
        cam.transform.position = terrainComp.GetAdvanceDir() + offset;
    }
}
