using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioExample : MonoBehaviour
{   
    private SoundReact soundReact;

    // Cubes variables
    [SerializeField] private GameObject[] cubes;
    [SerializeField] private float startScale, scaleMultiplier;
    [SerializeField] private float rotMultiplier;
    [SerializeField] private float startBrightness, brightnessMultiplier;

    // Terrain variables
    [SerializeField] private float heightFactor, noiseFactor;
    private Mesh terrainMesh;
    private float currentWidth = 0;
    private Vector3[] initPos;

    //Light variables
    private Light sceneLight;
    [SerializeField] private float intensityFactor;
    [SerializeField] private float rangeFactor;

    //Draw Polygon variables
    private Color lineColor = new Color(238, 126, 60);
    private float lineWidth = 0.1f;
    private float drawSpeedFactor = 0.015f;
    private Vector3[] polygonVert = {new Vector2(-1.5f, -1), new Vector2(-0.5f, 0) , new Vector2(-1.5f, 1) , new Vector2(-0.5f, 1),
                                   new Vector2(0, 2), new Vector2(0.5f, 1), new Vector2(1.5f, 1), new Vector2(0.5f, 0), new Vector2(1.5f, -1),
                                   new Vector2(-1.5f, -1)};

    //Keyboard draw polygon variables
    private Color klineColor = new Color(238, 126, 60);
    private float klineWidth = 0.1f;
    private float kdrawSpeedFactor = 00000.1f;
    private Dictionary<int, Vector2> numberDirAssociation = new Dictionary<int, Vector2>();

    //Add forces
    private Rigidbody body;
    private Vector3 forceDir = Vector3.right;
    private float forceFactor = 0.01f;
    private float mu = 0.4f;
    private Vector3 fRoz;

    // Start is called before the first frame update
    void Start()
    {
        soundReact = GetComponent<SoundReact>();

        //sceneLight = GetComponent<Light>();

        //soundReact.AmplitudeDrawPolygon(polygonVert, lineColor, lineWidth, drawSpeedFactor);

        //InitDic();
        //soundReact.NoteNumberDrawPolygon(numberDirAssociation, klineColor, klineWidth, kdrawSpeedFactor);

        //soundReact.BandsGenerateTerrain(16, currentWidth, 0.1f, heightFactor, noiseFactor);

        /*
        MidiRecording.RecordingSetUp();
        MidiRecording.StartRecording();
        body = GetComponent<Rigidbody>();
        fRoz = new Vector3(mu * body.mass * Physics.gravity.y, 0, 0);
        */

        terrainMesh = GetComponent<MeshFilter>().mesh;
        initPos = new Vector3[terrainMesh.vertices.Length];
        for (int i = 0; i < initPos.Length; i++)
        {
            initPos[i] = terrainMesh.vertices[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        //LightIntensity();

        //LightRange();

        //UpdateTerrain();

        //UpdateCubes();

        /*
        soundReact.Record_NoteNumberAddForce(body, forceDir, ForceMode.Impulse, forceFactor);
        if (body.velocity.x > 0)
            body.AddForce(fRoz);

        if (body.velocity.x <= 0)
            body.velocity = Vector3.zero;
        */

        soundReact.AmplitudeVolumeHeightMap(terrainMesh, noiseFactor, heightFactor, initPos);
    }

    private void OnApplicationQuit()
    {
        //MidiRecording.StopRecording();
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

    private void LightIntensity()
    {
        soundReact.AmplitudeLightIntensity(sceneLight, intensityFactor);
    }

    private void LightRange()
    {
        soundReact.AmplitudeLightRange(sceneLight, rangeFactor);
    }

    private void InitDic()
    {
        float x, y;
        for (int i = 1; i <= 127; i++)
        {
            x = Random.Range(-10, 10);
            y = Random.Range(-10, 10);

            numberDirAssociation.Add(i, new Vector2(x, y).normalized);
        }
    }
}
