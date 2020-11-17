#region Dependencies

using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Interaction;
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
        if (showNoteOnEvents && e.Event.EventType.Equals(MidiEventType.NoteOn))
        {
            string inputEvent = e.Event.ToString();
            currentNoteOnEvent = buildEvent(e.Event.ToString());
            Debug.Log($"Note On event received from '{midiDevice.Name}' at {currentNoteOnEvent.GetNoteTime()}: Note Number: {currentNoteOnEvent.GetNoteNumber()}  Note Velocity: {currentNoteOnEvent.GetNoteVelocity()}");
        }
        else if (showNoteOffEvents && e.Event.EventType.Equals(MidiEventType.NoteOff))
        {
            string inputEvent = e.Event.ToString();
            currentNoteOffEvent = buildEvent(e.Event.ToString());
            Debug.Log($"Note Off event received from '{midiDevice.Name}' at {currentNoteOffEvent.GetNoteTime()}: Note Number: {currentNoteOffEvent.GetNoteNumber()}  Note Velocity: {currentNoteOffEvent.GetNoteVelocity()}");
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

    #endregion

}
