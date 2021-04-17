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
    private Mesh mesh;
    private Vector3[] initPos;


    // Start is called before the first frame update
    void Start()
    {
        MidiRecording.RecordingSetUp();
        MidiRecording.StartRecording();
        recReact = GetComponent<MIDIRecordReact>();
        mesh = this.gameObject.GetComponent<MeshFilter>().mesh;
        initPos = mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            recReact.Record_VelocityReliefMap(mesh, 0.5f, 0.1f, MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() * 0.01f, initPos, 5f);
        }
        else
        {
            recReact.Record_VelocityReliefMap(mesh, 0.5f, 0.1f, 0, initPos, 5f);
        }
        
    }

    private void OnApplicationQuit()
    {
        MidiRecording.StopRecording();
        //MidiPlayEventHandler.ReleasePlaybackResources();
    }
}
