using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Example : MonoBehaviour
{
    [SerializeField]
    private Volume vol;

    private AmplitudeReact ampReact;

    [SerializeField]
    private float noiseFactor;

    [SerializeField]
    private float reliefFactor;

    [SerializeField]
    private float waveSpeed;

    private Vector3[] initPos;

    /*
    private float timer;
    private float spawnTime = 1.0f;
    private Object objToSpawn;
    */


    // Start is called before the first frame update
    void Start()
    {
        //MidiRecording.RecordingSetUp();
        //MidiRecording.StartRecording();
        //recReact = GetComponent<MIDIRecordReact>();
        //objToSpawn = Resources.Load("Prefabs/Sphere");
        ampReact = GetComponent<AmplitudeReact>();
        initPos = this.gameObject.GetComponent<MeshFilter>().mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        //recReact.Record_NoteNumberInstantiate(objToSpawn, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), Quaternion.identity, 69, 1f);
        ampReact.AmplitudeReliefMap(this.gameObject.GetComponent<MeshFilter>().mesh, noiseFactor, reliefFactor, initPos, waveSpeed);
    }

    private void OnApplicationQuit()
    {
        //MidiRecording.StopRecording();
        //MidiPlayEventHandler.ReleasePlaybackResources();
    }
}
