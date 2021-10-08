#region Dependencies

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
    private static float SecToMicorsec = 1000000;

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
    private static List<MIDIChordEvent> ChordOnCollection;
    private static List<MIDIChordEvent>[] ChordOnCollectionTracks;
    private static List<MIDIChordEvent> ChordOffCollection;
    private static List<MIDIChordEvent>[] ChordOffCollectionTracks;
    private static List<float> chordOnTimes = new List<float>();

    #endregion

    #region MIDI_Play_Handlers

    /// <summary>
    /// Prepares the variable that controls playback of one track MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    public static void MidiPlaybackSetUp(string midiFilePath)
    {
        ChordOnCollection = MidiFileInput.MidiInputChordOnEvents(midiFilePath);
        ChordOffCollection = MidiFileInput.MidiInputChordOffEvents(midiFilePath);

        // Fill noteOnTimes list
        foreach (MIDIChordEvent chordEvent in ChordOnCollection)
        {
            chordOnTimes.Add(chordEvent.GetChordTime());
        }

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
        ChordOnCollectionTracks = MidiFileInput.MidiInputChordOnEventsTracks(midiFilePath);
        ChordOffCollectionTracks = MidiFileInput.MidiInputChordOffEventsTracks(midiFilePath);

        // Fill noteOnTimes list
        foreach (MIDIChordEvent chordEvent in ChordOnCollectionTracks[track])
        {
            chordOnTimes.Add(chordEvent.GetChordTime());
        }

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
        midiPlayback.MoveToTime(new MetricTimeSpan(System.Convert.ToInt64(chordOnTimes[0] * SecToMicorsec)));
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
        if (playedEventOn != null)
        {
            currentNoteOnEvent = buildEvent(playedEventOn.ToString());
            return currentNoteOnEvent;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Return the current played Note Off event
    /// </summary>
    /// <returns></returns>
    public static MIDINoteEvent MidiPlayNoteOffEvent()
    {
        if (playedEventOn != null)
        {
            currentNoteOffEvent = buildEvent(playedEventOff.ToString());
            return currentNoteOffEvent;
        }
        else
        {
            return null;
        }
    }

    #endregion

    #region MIDI_Play_Chords_Input

    /// <summary>
    /// Return the current played Chord On event
    /// </summary>
    /// <returns></returns>
    public static MIDIChordEvent MidiPlayChordOnEvent()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetChordIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec);
        return ChordOnCollection[index];
    }

    public static MIDIChordEvent MidiPlayChordOnEvent(int track)
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetChordIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec);
        return ChordOnCollectionTracks[track][index];
    }

    /// <summary>
    /// Return the current played Chord Off event
    /// </summary>
    /// <returns></returns>
    public static MIDIChordEvent MidiPlayChordOffEvent()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetChordIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec);
        return ChordOffCollection[index];
    }

    public static MIDIChordEvent MidiPlayChordOffEvent(int track)
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetChordIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec);
        return ChordOffCollectionTracks[track][index];
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

    private static int GetChordIndex(float targetTime)
    {
        // Compute the corresponding index
        float nearest = chordOnTimes.OrderBy(x => System.Math.Abs(x - targetTime)).First();
        int index = chordOnTimes.IndexOf(nearest);
        index = chordOnTimes[index] > targetTime ? index-- : index;

        return index;
    }

        #endregion
    }
