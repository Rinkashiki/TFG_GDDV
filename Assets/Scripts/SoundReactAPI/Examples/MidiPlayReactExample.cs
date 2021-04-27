using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidiPlayReactExample : MonoBehaviour
{

    [SerializeField] Object midiFile;

    MIDIPlayReact playReact;

    List<MIDINoteEvent> events;

    // Play Add Force
    [Header("Play Add Force")]
    [SerializeField] GameObject sphereNotes;
    [SerializeField] float forceFactor;
    private Rigidbody[] bodies;

    // Light
    [Header("Light")]
    [SerializeField] Light[] focusLights;

    // Play Light Intensity
    [Header("Play Light Intensity")]
    [SerializeField] float intensityFactor;

    // Play Light Range
    [Header("Play Light Range")]
    [SerializeField] float rangeFactor;

    // Start is called before the first frame update
    void Start()
    {
        playReact = GetComponent<MIDIPlayReact>();

        events = MidiFileEventHandler.Event_NoteOnListDistinct(midiFile);

        //Playback callbacks for start playing
        MidiPlayEventHandler.PlaybackSetUp(midiFile);
        MidiPlayEventHandler.StartPlayback();

        // Play Add Force
        bodies = sphereNotes.GetComponentsInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Play Add Force
        for (int i = 0; i < bodies.Length; i++)
        {
            if(MidiPlayEventHandler.Event_CurrentNoteOn() != null && MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() == events[i].GetNoteNumber())
            {
                playReact.Play_AddForce(bodies[i], Vector3.up, ForceMode.Impulse, forceFactor);
            }
        }

        // Play Light Intensity
        playReact.Play_LightIntensity(focusLights[0], intensityFactor);
        playReact.Play_LightIntensity(focusLights[1], intensityFactor);

        // Play Light Range
        playReact.Play_LightRange(focusLights[0], rangeFactor);
        playReact.Play_LightRange(focusLights[1], rangeFactor);
    }

    private void OnApplicationQuit()
    {
        //Playback callbacks for stop playing
        MidiPlayEventHandler.StopPlayback();
        MidiPlayEventHandler.ReleasePlaybackResources();
    }

}
