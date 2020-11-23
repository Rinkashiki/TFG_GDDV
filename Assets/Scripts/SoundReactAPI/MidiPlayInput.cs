﻿#region Dependencies

using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

public class MidiPlayInput
{
    #region Units

    private static float microSecToSec = 0.000001f;

    #endregion

    #region MIDI_Play_Variables

    // Playback variable
    private static Playback midiPlayback;

    // Played Events
    private static MidiEvent playedEventOn;
    private static MidiEvent playedEventOff;

    // Notes variables
    private static MIDINoteEvent currentNoteOnEvent;
    private static MIDINoteEvent currentNoteOffEvent;

    // Chords variables
    private static MIDIChordEvent currentChordOnEvent;
    private static MIDIChordEvent currentChordOffEvent;

    #endregion

    #region MIDI_Play_Handlers

    /// <summary>
    /// Prepares the variable that controls playback of one track MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    public static void MidiPlaybackSetUp(string midiFilePath)
    {
        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Define the output device
        var outputDevice = OutputDevice.GetByName("Microsoft GS Wavetable Synth");

        // Extract playback from MIDI file
        midiPlayback = new Playback(midiFile.GetNotes(), midiFile.GetTempoMap(), outputDevice, new MidiClockSettings
        {
            CreateTickGeneratorCallback = () => new RegularPrecisionTickGenerator()
        });
    }

    /// <summary>
    /// Prepares the variable that controls playback of the multiple track MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    public static void MidiPlaybackSetUp(string midiFilePath, int track)
    {
        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Define the output device
        var outputDevice = OutputDevice.GetByName("Microsoft GS Wavetable Synth");

        List<TrackChunk> midiTracks = midiFile.GetTrackChunks().ToList();
        List<TrackChunk> tracks = new List<TrackChunk>();

        // Initialize each list
        foreach (TrackChunk trk in midiTracks)
        {
            if (trk.Events.Any(x => x is NoteEvent))
            {
                tracks.Add(trk);
            }
        }

        // Create playback from MIDI file
        midiPlayback = new Playback(tracks.ToArray()[track].GetNotes(), midiFile.GetTempoMap(), outputDevice, new MidiClockSettings
        {
            CreateTickGeneratorCallback = () => new RegularPrecisionTickGenerator()
        });
        /*
        midiPlayback = midiFile.GetPlayback(outputDevice, new MidiClockSettings
        {
            CreateTickGeneratorCallback = () => new RegularPrecisionTickGenerator()
        });
        */
    }

    /// <summary>
    /// Releases <paramref name="outputDevice"/> resource taken for MIDI playback
    /// </summary>
    /// <param name="outputDevice"></param>
    public static void MidiPlaybackReleaseResources(OutputDevice outputDevice)
    {
        outputDevice.Dispose();
        midiPlayback.Dispose();
    }

    /// <summary>
    /// Starts playing MIDI from the last stopped
    /// </summary>
    public static void PlayMidi()
    {
        midiPlayback.EventPlayed += OnEventPlayed;
        midiPlayback.Start();
    }

    /// <summary>
    /// Stops playing MIDI
    /// </summary>
    public static void StopMidi()
    {
        midiPlayback.Stop();
    }

    /// <summary>
    /// Stops playing MIDI and resets it to its start position
    /// </summary>
    public static void StopResetMidi()
    {
        midiPlayback.Stop();
        midiPlayback.MoveToStart();
    }

    #endregion

    #region MIDI_Play_Notes_Input

    /// <summary>
    /// Return the current played Note On event
    /// </summary>
    /// <returns></returns>
    public static MIDINoteEvent MidiPlayNoteOnEvent()
    {
        currentNoteOnEvent = buildEvent(playedEventOn.ToString());
        return currentNoteOnEvent;
    }

    /// <summary>
    /// Return the current played Note Off event
    /// </summary>
    /// <returns></returns>
    public static MIDINoteEvent MidiPlayNoteOffEvent()
    {
        currentNoteOffEvent = buildEvent(playedEventOff.ToString());
        return currentNoteOffEvent;
    }

    #endregion

    #region MIDI_Play_Chords_Input

    /// <summary>
    /// Return the current played Chord On event
    /// </summary>
    /// <returns></returns>
    public static MIDIChordEvent MidiPlayChordOnEvent()
    {
        return currentChordOnEvent;
    }

    /// <summary>
    /// Return the current played Chord Off event
    /// </summary>
    /// <returns></returns>
    public static MIDIChordEvent MidiPlayChordOffEvent()
    {
        return currentChordOffEvent;
    }

    #endregion

    #region MIDI_Play_BPM_Input

    /// <summary>
    /// Returns the current BPM of the MIDI file stores in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static long MidiPlayBPM(string midiFilePath)
    {
        long currentBPM = MidiFileInput.MidiInputBPMAtTime(midiFilePath, midiPlayback.GetCurrentTime<MidiTimeSpan>().TimeSpan);
        return currentBPM;
    }

    #endregion

    private static void OnEventPlayed(object sender, MidiEventPlayedEventArgs e)
    {
        float time = midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec;
        //Debug.Log($"{time} {e.Event.ToString()}");
        if (e.Event.EventType.Equals(MidiEventType.NoteOn))
        {
            playedEventOn = e.Event;
        }
        else if (e.Event.EventType.Equals(MidiEventType.NoteOff))
        {
            playedEventOff = e.Event;
        }
    }

    #region Other_Utility_Functions

    private static MIDINoteEvent buildEvent(string inputEvent)
    {
        MIDINoteEvent noteEvent;

        // Note Time
        float time = midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec;

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
