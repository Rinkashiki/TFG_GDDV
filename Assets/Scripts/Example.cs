using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Example : MonoBehaviour
{
    [SerializeField]
    private Volume vol;

    //Reacts
    private AmplitudeReact ampReact;
    private FreqBandReact bandReact;

    // For ReliefMap
    /*
    [SerializeField] private float noiseFactor;
    [SerializeField] private float reliefFactor;
    [SerializeField] private float waveSpeed;
    private Vector3[] initPos;
    */

    // For MIDIRecord Instantiate
    /* 
    private float timer;
    private float spawnTime = 1.0f;
    private Object objToSpawn;
    */

    // For Bands Generate Terrain
    /*
    [SerializeField] private float noiseFactor;
    [SerializeField] private float heightFactor;
    [SerializeField] private Material mat;
    private GameObject terrain;
    */

    // For Generate Phyllotaxis
    [SerializeField] float phyllotaxisDegree;
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;
    [SerializeField] float speedFactor;
    [SerializeField] float scaleFactor;
    private GameObject phylloObj;
    private Phyllotaxis phyllotaxis;


    // Start is called before the first frame update
    void Start()
    {
        // For MIDIRecord Instantiate
        /*
        MidiRecording.RecordingSetUp();
        MidiRecording.StartRecording();
        recReact = GetComponent<MIDIRecordReact>();
        objToSpawn = Resources.Load("Prefabs/Sphere");
        */

        // For ReliefMap
        /*
        ampReact = GetComponent<AmplitudeReact>();
        initPos = this.gameObject.GetComponent<MeshFilter>().mesh.vertices;
        */

        // For Bands Generate Terrain
        /*
        bandReact = GetComponent<FreqBandReact>();
        terrain = bandReact.BandsGenerateTerrain(16, 0.05f, heightFactor, noiseFactor, Vector3.right, mat);
        */

        // For Shader Material Property
        /*
        ampReact = GetComponent<AmplitudeReact>();
        */

        // For Generate Phyllotaxis
        ampReact = GetComponent<AmplitudeReact>();
        ampReact.AmplitudePhyllotaxis(phyllotaxisDegree, startColor, endColor, speedFactor, scaleFactor, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // For MIDIRecord Instantiate
        /*
        recReact.Record_NoteNumberInstantiate(objToSpawn, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), Quaternion.identity, 69, 1f);
        */

        // For ReliefMap
        /*
        ampReact.AmplitudeReliefMap(this.gameObject.GetComponent<MeshFilter>().mesh, noiseFactor, reliefFactor, initPos, waveSpeed);
        */

        // For Shader Material Property
        /*
        ampReact.AmplitudeShaderGraphMatProperty(terrain.GetComponent<MeshRenderer>().material, "DissolveFactor", GenericSoundReact.MatPropertyType.Float, 1f);
        */

    }

    private void OnApplicationQuit()
    {
        // For MIDIRecord Instantiate
        /*
        MidiRecording.StopRecording();
        */
    }
}
