﻿using System.Collections;
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
        
        long BPM = MidiFileEventHandler.Event_BPMAtTime(midiFile, 0).GetValueEvent();
        Debug.Log("BPM : " + BPM);
        List<string> names = MidiFileEventHandler.Event_NotesName(midiFile).GetListEvent();
        List<int> numbers = MidiFileEventHandler.Event_NotesNumber(midiFile).GetListEvent();
        List<float> timesOn = MidiFileEventHandler.Event_NoteOnTimes(midiFile).GetListEvent();
        List<float> timesOff = MidiFileEventHandler.Event_NoteOffTimes(midiFile).GetListEvent();
        List<float> lengths = MidiFileEventHandler.Event_NotesLength(midiFile).GetListEvent();
        List<int> velocitiesOn = MidiFileEventHandler.Event_NoteOnVelocities(midiFile).GetListEvent();
        List<int> velocitiesOff = MidiFileEventHandler.Event_NoteOffVelocities(midiFile).GetListEvent();
        for (int i = 0; i < numbers.Count; i++)
        {
            Debug.Log("NoteName: " + names[i] + "  NoteNumber: " + numbers[i] + "  NoteOnTime: " + timesOn[i] + "  NoteOffTime: " + timesOff[i] + "  NoteLength: " + lengths[i] + "  NoteOnVelocity: " + velocitiesOn[i] + "  NoteOffVelocity: " + velocitiesOff[i]);
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
            Debug.Log(MidiPlayEventHandler.Event_CurrentNoteName().GetValueEvent());
        }
    }
}
