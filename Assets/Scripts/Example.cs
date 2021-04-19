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
    private Rigidbody body;
    private float initialMass;


    // Start is called before the first frame update
    void Start()
    {
        MidiRecording.RecordingSetUp();
        MidiRecording.StartRecording();
        recReact = GetComponent<MIDIRecordReact>();
        body = this.GetComponent<Rigidbody>();
        initialMass = body.mass;
    }

    // Update is called once per frame
    void Update()
    {

        recReact.Record_VelocityPhysicProperty(body, GenericSoundReact.FloatPhysicProperties.mass, 0.1f, initialMass, 5f);
        body.AddForce(new Vector3(0, -Physics.gravity.y, 0));
    }

    private void OnApplicationQuit()
    {
        MidiRecording.StopRecording();
        //MidiPlayEventHandler.ReleasePlaybackResources();
    }
}
