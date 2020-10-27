﻿#region Dependencies

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
    private static List<string> notesName = new List<string>();
    private static List<int> notesNumber = new List<int>();
    private static List<int> noteOnSpeeds = new List<int>();
    private static List<int> noteOffSpeeds = new List<int>();
    private static List<float> noteOnTimes = new List<float>();
    private static List<float> noteOffTimes = new List<float>();
    private static List<float> notesLength = new List<float>();
    
    // Chords collection variables
    private static List<string> chordsName = new List<string>();
    private static List<int[]> chordsNumber = new List<int[]>();
    private static List<int[]> chordOnSpeeds = new List<int[]>();
    private static List<int[]> chordOffSpeeds = new List<int[]>();
    private static List<float> chordOnTimes = new List<float>();
    private static List<float> chordOffTimes = new List<float>();
    private static List<float> chordsLength = new List<float>();

    #endregion

    #region MIDI_Play_Handlers

    /// <summary>
    /// Prepares the variable that controls playback of the MIDI file stores in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    public static void MidiPlaybackSetUp(string midiFilePath)
    {
        // Extract Chord Events from MIDI file
        notesName = MidiFileInput.MidiInputNotesName(midiFilePath);
        notesNumber = MidiFileInput.MidiInputNotesNumber(midiFilePath);
        noteOnSpeeds = MidiFileInput.MidiInputNoteOnSpeeds(midiFilePath);
        noteOffSpeeds = MidiFileInput.MidiInputNoteOffSpeeds(midiFilePath);
        noteOnTimes = MidiFileInput.MidiInputNoteOnTimes(midiFilePath);
        noteOffTimes = MidiFileInput.MidiInputNoteOffTimes(midiFilePath);
        notesLength = MidiFileInput.MidiInputNotesLength(midiFilePath);

        // Extract Note Events from MIDI file
        chordsName = MidiFileInput.MidiInputChordsNotesName(midiFilePath);
        chordsNumber = MidiFileInput.MidiInputChordsNotesNumber(midiFilePath);
        chordOnSpeeds = MidiFileInput.MidiInputChordsNoteOnSpeeds(midiFilePath);
        chordOffSpeeds = MidiFileInput.MidiInputChordsNoteOffSpeeds(midiFilePath);
        chordOnTimes = MidiFileInput.MidiInputChordOnTimes(midiFilePath);
        chordOffTimes = MidiFileInput.MidiInputChordOffTimes(midiFilePath);
        chordsLength = MidiFileInput.MidiInputChordsLength(midiFilePath);
        
        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Define the output device
        var outputDevice = OutputDevice.GetByName("Microsoft GS Wavetable Synth");

        // Extract playback from MIDI file
        midiPlayback = midiFile.GetPlayback(outputDevice, new MidiClockSettings
        {
            CreateTickGeneratorCallback = () => new RegularPrecisionTickGenerator()
        });
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
    /// Returns the current played note name
    /// </summary>
    /// <returns></returns>
    public static string MidiPlayNoteName()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetNoteIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec);

        return notesName[index];
    }

    /// <summary>
    /// Returns the current played note number
    /// </summary>
    /// <returns></returns>
    public static int MidiPlayNoteNumber()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetNoteIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec);

        return notesNumber[index];
    }

    /// <summary>
    /// Returns the current played note on speed
    /// </summary>
    /// <returns></returns>
    public static int MidiPlayNoteOnSpeed()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetNoteIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec);

        return noteOnSpeeds[index];
    }

    /// <summary>
    /// Returns the current played note off speed
    /// </summary>
    /// <returns></returns>
    public static int MidiPlayNoteOffSpeed()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetNoteIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec);

        return noteOffSpeeds[index];
    }

    /// <summary>
    /// Returns the current played note on time
    /// </summary>
    /// <returns></returns>
    public static float MidiPlayNoteOnTime()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetNoteIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec);

        return noteOnTimes[index];
    }

    /// <summary>
    /// Returns the current played note off time
    /// </summary>
    /// <returns></returns>
    public static float MidiPlayNoteOffTime()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetNoteIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec);

        return noteOffTimes[index];
    }

    /// <summary>
    /// Returns the current played note length
    /// </summary>
    /// <returns></returns>
    public static float MidiPlayNoteLength()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetNoteIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec);

        return notesLength[index];
    }


    #endregion

    #region MIDI_Play_Chords_Input

    /// <summary>
    /// Returns the current played chord notes name
    /// </summary>
    /// <returns></returns>
    public static string MidiPlayChordNotesName()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetChordIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec);

        return chordsName[index];
    }

    /// <summary>
    /// Returns the current played chord notes number
    /// </summary>
    /// <returns></returns>
    public static int[] MidiPlayChordNotesNumber()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetChordIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec);

        return chordsNumber[index];
    }

    /// <summary>
    /// Returns the current played chord note on speeds
    /// </summary>
    /// <returns></returns>
    public static int[] MidiPlayChordOnSpeed()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetChordIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec);

        return chordOnSpeeds[index];
    }

    /// <summary>
    /// Returns the current played chord note off speeds
    /// </summary>
    /// <returns></returns>
    public static int[] MidiPlayChordOffSpeed()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetChordIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec);

        return chordOnSpeeds[index];
    }

    /// <summary>
    /// Returns the current played chord on time
    /// </summary>
    /// <returns></returns>
    public static float MidiPlayChordOnTime()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetChordIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec);

        return chordOnTimes[index];
    }

    /// <summary>
    /// Returns the current played chord off time
    /// </summary>
    /// <returns></returns>
    public static float MidiPlayChordOffTime()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetChordIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec);

        return chordOffTimes[index];
    }

    /// <summary>
    /// Returns the current played chord length
    /// </summary>
    /// <returns></returns>
    public static float MidiPlayChordLength()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetChordIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * microSecToSec);

        return chordsLength[index];
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
