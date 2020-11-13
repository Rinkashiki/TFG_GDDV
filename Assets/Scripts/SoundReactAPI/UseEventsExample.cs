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
        // VERY IMPORTANT CALL THIS before anything related with MIDI playback
        MidiPlayEventHandler.PlaybackSetUp(midiFile);
        
        long BPM = MidiFileEventHandler.Event_BPMAtTime(midiFile, 0);
        Debug.Log("BPM : " + BPM);
        List<MIDIChordEvent> NoteOnEvents = MidiFileEventHandler.Event_ChordOnList(midiFile);
        foreach (MIDIChordEvent chordEvent in NoteOnEvents)
        {
            chordEvent.PrintEvent();
        }  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MidiPlayEventHandler.StartPlayback();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MidiPlayEventHandler.StopPlayback();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MidiPlayEventHandler.ReleasePlaybackResources();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber());
        }
    }
}
