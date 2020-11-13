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

    private MidiEvent currentNoteOnEvent;
    private MidiEvent currentNoteOffEvent;

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
        if (e.Event.EventType.Equals(MidiEventType.NoteOn))
        {
            float time = recording.GetDuration<MetricTimeSpan>().TotalMicroseconds * microSecToSec;
            string inputEvent = e.Event.ToString();
            int length = (inputEvent.IndexOf(",")) - (inputEvent.IndexOf("(") + 1);
            int noteNumber = int.Parse(inputEvent.Substring(inputEvent.IndexOf("(") + 1, length));
            length = (inputEvent.IndexOf(")")) - (inputEvent.IndexOf(",") + 2);
            int noteVelocity = int.Parse(inputEvent.Substring(inputEvent.IndexOf(",") + 2, length));
            Debug.Log($"Note On event received from '{midiDevice.Name}' at {time}: Note Number : {noteNumber}  Note Velocity : {noteVelocity}");
        }
        else if (e.Event.EventType.Equals(MidiEventType.NoteOff))
        {
            float time = recording.GetDuration<MetricTimeSpan>().TotalMicroseconds * microSecToSec;
            string inputEvent = e.Event.ToString();
            int length = (inputEvent.IndexOf(",")) - (inputEvent.IndexOf("(") + 1);
            int noteNumber = int.Parse(inputEvent.Substring(inputEvent.IndexOf("(") + 1, length));
            length = (inputEvent.IndexOf(")")) - (inputEvent.IndexOf(",") + 2);
            int noteVelocity = int.Parse(inputEvent.Substring(inputEvent.IndexOf(",") + 2, length));
            Debug.Log($"Note Off event received from '{midiDevice.Name}' at {time}: Note Number : {noteNumber}  Note Velocity : {noteVelocity}");
        }
    }

    public MidiEvent getCurrentNoteOnEvent()
    {
        return currentNoteOnEvent;
    }

    public MidiEvent getCurrentNoteOffEvent()
    {
        return currentNoteOffEvent;
    }

}
