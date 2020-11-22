#region Dependencies

using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Interaction;
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

    // Notes collections variables
    private static List<MIDINoteEvent> noteOnEvents;
    private static List<MIDINoteEvent>[] noteOnEventsTracks;
    private static List<MIDINoteEvent> noteOffEvents;
    private static List<MIDINoteEvent>[] noteOffEventsTracks;
    private static List<float> noteOnTimes = new List<float>();

    // Chords collection variables
    private static List<MIDIChordEvent> chordOnEvents;
    private static List<MIDIChordEvent>[] chordOnEventsTracks;
    private static List<MIDIChordEvent> chordOffEvents;
    private static List<MIDIChordEvent>[] chordOffEventsTracks;
    private static List<float> chordOnTimes = new List<float>();

    #endregion

    #region MIDI_Play_Handlers

    /// <summary>
    /// Prepares the variable that controls playback of one track MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    public static void MidiPlaybackSetUp(string midiFilePath)
    {
        // Extract Notes Events from MIDI file
        noteOnEvents = MidiFileInput.MidiInputNoteOnEvents(midiFilePath);
        noteOffEvents = MidiFileInput.MidiInputNoteOffEvents(midiFilePath);

        // Fill noteOnTimes list
        foreach (MIDINoteEvent noteEvent in noteOnEvents)
        {
            noteOnTimes.Add(noteEvent.GetNoteTime());
        }

        // Extract Chords Events from MIDI file
        chordOnEvents = MidiFileInput.MidiInputChordOnEvents(midiFilePath);
        chordOffEvents = MidiFileInput.MidiInputChordOffEvents(midiFilePath);

        // Fill noteOnTimes list
        foreach (MIDIChordEvent chordEvent in chordOnEvents)
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
        // Extract Notes Events from MIDI file
        noteOnEventsTracks = MidiFileInput.MidiInputNoteOnEventsTracks(midiFilePath);
        noteOffEventsTracks = MidiFileInput.MidiInputNoteOffEventsTracks(midiFilePath);

        // Fill noteOnTimes list
        foreach (MIDINoteEvent noteEvent in noteOnEventsTracks[track])
        {
            noteOnTimes.Add(noteEvent.GetNoteTime());
        }

        // Extract Chords Events from MIDI file
        chordOnEventsTracks = MidiFileInput.MidiInputChordOnEventsTracks(midiFilePath);
        chordOffEventsTracks = MidiFileInput.MidiInputChordOffEventsTracks(midiFilePath);

        // Fill noteOnTimes list
        foreach (MIDIChordEvent chordEvent in chordOnEventsTracks[track])
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
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetNoteIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec);

        return noteOnEvents[index];

    }

    /// <summary>
    /// Return the current played Note Off event
    /// </summary>
    /// <returns></returns>
    public static MIDINoteEvent MidiPlayNoteOffEvent()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetNoteIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec);

        return noteOffEvents[index];

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

        return chordOnEvents[index];

    }

    /// <summary>
    /// Return the current played Chord Off event
    /// </summary>
    /// <returns></returns>
    public static MIDIChordEvent MidiPlayChordOffEvent()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetChordIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec);

        return chordOffEvents[index];

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

    #region Other_Utility_Functions

    private static int GetNoteIndex(float targetTime)
    {
        // Compute the corresponding index
        float nearest = noteOnTimes.OrderBy(x => System.Math.Abs(x - targetTime)).First();
        int index = noteOnTimes.IndexOf(nearest);
        index = noteOnTimes[index] > targetTime ? index-- : index;

        return index;
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
