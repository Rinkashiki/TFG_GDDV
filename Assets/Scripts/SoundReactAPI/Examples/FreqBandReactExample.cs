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
    [SerializeField][Range(-1, 1)] float terrainDirY;
    [SerializeField][Range(-1, 1)] float terrainDirZ;
    [SerializeField] Material terrainMat;
    private GameObject terrain;
    private GenerateTerrain terrainComp;
    private Vector3 terrainDir;

    //Camera Terrain Follow
    [Header("Camera Follow")]
    [SerializeField] private Vector3 offset;
    private Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        bandReact = GetComponent<FreqBandReact>();

        //Bands Generate Terrain
        terrainDir = Vector3.right;
        terrain = bandReact.BandsGenerateTerrain(16, step, heightFactor, noiseFactor, terrainDir, terrainMat);
        terrainComp = terrain.GetComponent<GenerateTerrain>();

        //Camera Terrain Follow
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //Bands Generate Terrain
        terrainComp.SetStep(step);
        terrainComp.SetHeightFactor(heightFactor);
        terrainComp.SetNoiseFactor(noiseFactor);

        terrainDir = new Vector3(terrainDir.x, terrainDirY, terrainDirZ);
        terrainComp.SetTerrainDir(terrainDir);

        //Camera Terrain Follow
        CameraFollow();
    }

    private void CameraFollow()
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, terrainComp.GetTerrainPos() + offset, 0.05f);
    }
}
