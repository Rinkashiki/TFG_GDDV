using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Example : MonoBehaviour
{

    [SerializeField]
    private Volume vol;

    private MIDIRecordReact ampReact;
    private Bloom bloom;
    private ChromaticAberration ca;


    // Start is called before the first frame update
    void Start()
    {
        MidiRecording.RecordingSetUp();
        MidiRecording.StartRecording();
        ampReact = GetComponent<MIDIRecordReact>();
        bloom = (Bloom)vol.profile.components[0];
        ca = (ChromaticAberration)vol.profile.components[1];
    }

    // Update is called once per frame
    void Update()
    {
        ampReact.Record_VelocityShaderGraphMatProperty(this.gameObject.GetComponent<MeshRenderer>().material, "DissolveFactor", GenericSoundReact.MatPropertyType.Float, 0.01f, 1f);
    }

    private void OnApplicationQuit()
    {
        MidiRecording.StopRecording();
    }
}
