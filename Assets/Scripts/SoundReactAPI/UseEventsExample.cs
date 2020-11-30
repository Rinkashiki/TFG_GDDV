using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UseEventsExample : MonoBehaviour
{
    [SerializeField] private Object midiFile;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Playback Controls -> Start: 'A'  Stop: 'S'  Release Resources: 'D'  Get Event: 'W'");
        Debug.Log("Recording Controls -> Start: 'I'  Stop: 'O'  Get Event: 'P'");

        // VERY IMPORTANT CALL THIS before anything related with MIDI playback
        MidiPlayEventHandler.PlaybackSetUp(midiFile, 6);

        // VERY IMPORTANT CALL THIS before anything related with MIDI recording
        //MidiRecording.RecordingSetUp();
        MidiRecording.ShowNoteOnEvents(true);
        MidiRecording.ShowNoteOffEvents(true);

        long BPM = MidiFileEventHandler.Event_BPMAtTime(midiFile, 0);
        Debug.Log("BPM : " + BPM);
        List<MIDINoteEvent> NoteOnEvents = MidiFileEventHandler.Event_NoteOnList(midiFile, 6);
        foreach (MIDINoteEvent noteEvent in NoteOnEvents)
        {
            //noteEvent.PrintEvent();
        }  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MidiPlayEventHandler.StartPlayback();
            Debug.Log("Start playing MIDI");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MidiPlayEventHandler.StopPlayback();
            Debug.Log("Stop playing MIDI");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MidiPlayEventHandler.ReleasePlaybackResources();
            Debug.Log("Release MIDI");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber());
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            MidiRecording.StartRecording();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            MidiRecording.StopRecording();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            MidiRecording.GetCurrentNoteOnEvent().PrintEvent();
        }
    }
}
