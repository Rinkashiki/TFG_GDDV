﻿#region Dependencies

using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using UnityEngine;

#endregion

public class MidiRecording
{
    #region MIDI_Recording_Variables

    private static InputDevice inputDevice;
    private static OutputDevice outputDevice;

    private static Recording recording;
    private static DevicesConnector connector;

    private static MIDINoteEvent currentNoteOnEvent;
    private static MIDINoteEvent currentNoteOffEvent;

    private static bool showNoteOnEvents;
    private static bool showNoteOffEvents;

    #endregion

    #region Units

    private static float microSecToSec = 0.000001f;

    #endregion

    #region MIDI_Recording_Actions

    public static void RecordingSetUp()
    {
        inputDevice = InputDevice.GetById(0);
        outputDevice = OutputDevice.GetByName("Microsoft GS Wavetable Synth");
        recording = new Recording(TempoMap.Default, inputDevice);
    }

    public static void StartRecording()
    {
        connector = inputDevice.Connect(outputDevice);
        inputDevice.EventReceived += OnEventReceived;
        inputDevice.StartEventsListening();
        recording.Start();
        Debug.Log("Detectando Entrada MIDI");
    }

    public static void StopRecording()
    {
        recording.Stop();
        recording.Dispose();
        inputDevice.Dispose();
        connector.Disconnect();
        Debug.Log("Se ha dejado de detectar entrada MIDI");
    }

    #endregion

    /*Función a modificar. Declarar dos variables, currentNoteOnEvent y currentNoteOffEvent. Cuando se procese un evento de alguno de esos tipos,
    almaceno en la variable correspondiente dicho evento. Al proporcionarse getters, podré accederlas desde una clase superior*/
    private static void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
    {
        var midiDevice = (MidiDevice)sender;
        if (e.Event.EventType.Equals(MidiEventType.NoteOn))
        {
            string inputEvent = e.Event.ToString();
            currentNoteOnEvent = buildEvent(e.Event.ToString());
            currentNoteOffEvent = null;
            if (showNoteOnEvents)
            Debug.Log($"Note On event received from '{midiDevice.Name}' at {currentNoteOnEvent.GetNoteTime()}: Note Name: {currentNoteOnEvent.GetNoteName()}  Note Number: {currentNoteOnEvent.GetNoteNumber()}  Note Velocity: {currentNoteOnEvent.GetNoteVelocity()}");
        }
        else if (e.Event.EventType.Equals(MidiEventType.NoteOff))
        {
            string inputEvent = e.Event.ToString();
            currentNoteOffEvent = buildEvent(e.Event.ToString());
            currentNoteOnEvent = null;
            if(showNoteOffEvents)
            Debug.Log($"Note Off event received from '{midiDevice.Name}' at {currentNoteOffEvent.GetNoteTime()}: Note Name: {currentNoteOnEvent.GetNoteName()}  Note Number: {currentNoteOffEvent.GetNoteNumber()}  Note Velocity: {currentNoteOffEvent.GetNoteVelocity()}");
        }
        else if (e.Event.EventType.Equals(MidiEventType.PitchBend))
        {
            float time = recording.GetDuration<MetricTimeSpan>().TotalMicroseconds * microSecToSec;
            Debug.Log($"Event received from '{midiDevice.Name}' at {time}: {e.Event.ToString()}");
        }

    }

    #region MIDI_Recording_Getters

    public static MIDINoteEvent GetCurrentNoteOnEvent()
    {
        return currentNoteOnEvent;
    }

    public static MIDINoteEvent GetCurrentNoteOffEvent()
    {
        return currentNoteOffEvent;
    }

    #endregion

    #region MIDI_Recording_Shows

    public static void ShowNoteOnEvents(bool show)
    {
        showNoteOnEvents = show;
    }

    public static void ShowNoteOffEvents(bool show)
    {
        showNoteOffEvents = show;
    }

    #endregion

    #region Other_Utility_Functions

    private static MIDINoteEvent buildEvent(string inputEvent)
    {
        MIDINoteEvent noteEvent;

        // Note Time
        float time = recording.GetDuration<MetricTimeSpan>().TotalMicroseconds * microSecToSec;

        // Note Number
        int length = (inputEvent.IndexOf(",")) - (inputEvent.IndexOf("(") + 1);
        string noteText = inputEvent.Substring(inputEvent.IndexOf("(") + 1, length);
        int noteNumber = int.Parse(noteText);

        // Note Name
        string noteName = NoteUtilities.GetNoteName(SevenBitNumber.Parse(noteText)).ToString() + NoteUtilities.GetNoteOctave(SevenBitNumber.Parse(noteText));
        noteName = noteName.Contains("Sharp") ? noteName.Replace("Sharp", "#") : noteName;

        //Note Velocity
        length = (inputEvent.IndexOf(")")) - (inputEvent.IndexOf(",") + 2);
        int noteVelocity = int.Parse(inputEvent.Substring(inputEvent.IndexOf(",") + 2, length));

        //Create the MIDI Event
        if (inputEvent.Contains("On"))
        {
            noteEvent = new MIDINoteEvent(MIDIEvent.Type.NoteOn, noteName, noteNumber, noteVelocity, time);
        }
        else
        {
            noteEvent = new MIDINoteEvent(MIDIEvent.Type.NoteOff, noteName, noteNumber, noteVelocity, time);
        }

        return noteEvent;
    }

    #endregion

}
