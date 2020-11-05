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

    private Recording recording;

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
        Debug.Log("Se ha dejado de detectar entrada MIDI");
    }

    private static void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
    {
        var midiDevice = (MidiDevice)sender;
        if (e.Event.EventType.Equals(MidiEventType.NoteOn))
        {
            Debug.Log($"Event received from '{midiDevice.Name}' at {DateTime.Now}: {e.Event}");
        }
    }
}
