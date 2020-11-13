using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidiRecording : MonoBehaviour
{
    private InputDevice inputDevice;
    private OutputDevice outputDevice;

    private static Recording recording;
    private DevicesConnector connector;

    private MIDINoteEvent currentNoteOnEvent;
    private MIDINoteEvent currentNoteOffEvent;

    public static bool showNoteOnEvents = true;
    public static bool showNoteOffEvents = true;

    #region Units

    private static float microSecToSec = 0.000001f;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Pulsa A para empezar a detectar entrada MIDI");
        RecordingSetUp();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartRecording();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            StopRecording();
        }
    }

    private void RecordingSetUp()
    {
        inputDevice = InputDevice.GetById(0);
        outputDevice = OutputDevice.GetByName("Microsoft GS Wavetable Synth");
        recording = new Recording(TempoMap.Default, inputDevice);
    }

    private void StartRecording()
    {
        connector = inputDevice.Connect(outputDevice);
        inputDevice.EventReceived += OnEventReceived;
        inputDevice.StartEventsListening();
        recording.Start();
        Debug.Log("Pulsa S para dejar de detectar entrada MIDI");
    }

    private void StopRecording()
    {
        recording.Stop();
        recording.Dispose();
        inputDevice.Dispose();
        connector.Disconnect();
        Debug.Log("Se ha dejado de detectar entrada MIDI");
    }

    /*Función a modificar. Declarar dos variables, currentNoteOnEvent y currentNoteOffEvent. Cuando se procese un evento de alguno de esos tipos,
    almaceno en la variable correspondiente dicho evento. Al proporcionarse getters, podré accederlas desde una clase superior*/
    private static void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
    {
        var midiDevice = (MidiDevice)sender;
        if (showNoteOnEvents && e.Event.EventType.Equals(MidiEventType.NoteOn))
        {
            string inputEvent = e.Event.ToString();
            MIDINoteEvent receivedNoteEvent = buildEvent(e.Event.ToString());
            Debug.Log($"Note On event received from '{midiDevice.Name}' at {receivedNoteEvent.GetNoteTime()}: Note Number: {receivedNoteEvent.GetNoteNumber()}  Note Velocity: {receivedNoteEvent.GetNoteVelocity()}");
        }
        else if (showNoteOffEvents && e.Event.EventType.Equals(MidiEventType.NoteOff))
        {
            string inputEvent = e.Event.ToString();
            MIDINoteEvent receivedNoteEvent = buildEvent(e.Event.ToString());
            Debug.Log($"Note Off event received from '{midiDevice.Name}' at {receivedNoteEvent.GetNoteTime()}: Note Number: {receivedNoteEvent.GetNoteNumber()}  Note Velocity: {receivedNoteEvent.GetNoteVelocity()}");
        }
    }

    public MIDINoteEvent getCurrentNoteOnEvent()
    {
        return currentNoteOnEvent;
    }

    public MIDINoteEvent getCurrentNoteOffEvent()
    {
        return currentNoteOffEvent;
    }

    private static MIDINoteEvent buildEvent(string inputEvent)
    {
        MIDINoteEvent noteEvent;

        // Note Time
        float time = recording.GetDuration<MetricTimeSpan>().TotalMicroseconds * microSecToSec;

        // Note Number
        int length = (inputEvent.IndexOf(",")) - (inputEvent.IndexOf("(") + 1);
        int noteNumber = int.Parse(inputEvent.Substring(inputEvent.IndexOf("(") + 1, length));

        //Note Velocity
        length = (inputEvent.IndexOf(")")) - (inputEvent.IndexOf(",") + 2);
        int noteVelocity = int.Parse(inputEvent.Substring(inputEvent.IndexOf(",") + 2, length));

        //Create the MIDI Event
        if (inputEvent.Contains("On"))
        {
            noteEvent = new MIDINoteEvent(MIDIEvent.Type.NoteOn, noteNumber.ToString(), noteNumber, noteVelocity, time, 0);
        }
        else
        {
            noteEvent = new MIDINoteEvent(MIDIEvent.Type.NoteOff, noteNumber.ToString(), noteNumber, noteVelocity, time, 0);
        }

        return noteEvent;
    }

}
