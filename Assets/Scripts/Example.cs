using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Example : MonoBehaviour
{

    [SerializeField]
    private Object midiFile;

    [SerializeField]
    private Volume vol;

    private MIDIRecordReact recReact;
    private float timer;
    private float spawnTime = 1.0f;
    private Object objToSpawn;


    // Start is called before the first frame update
    void Start()
    {
        MidiRecording.RecordingSetUp();
        MidiRecording.StartRecording();
        recReact = GetComponent<MIDIRecordReact>();
        objToSpawn = Resources.Load("Prefabs/Sphere");
    }

    // Update is called once per frame
    void Update()
    {
        recReact.Record_NoteNumberInstantiate(objToSpawn, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), Quaternion.identity, 69, 1f);
    }

    private void OnApplicationQuit()
    {
        MidiRecording.StopRecording();
        //MidiPlayEventHandler.ReleasePlaybackResources();
    }
}
