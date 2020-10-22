﻿#region Dependencies

using Melanchall.DryWetMidi.Devices;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#endregion

public class MidiPlayEventHandler
{
    #region MIDI_Playback_Actions

    /// <summary>
    /// Configures MIDI playback fo the specified <paramref name="midiFile"/>. Call this before anything related with MIDI playback
    /// </summary>
    /// <param name="midiFile"></param>
    public static void PlaybackSetUp(Object midiFile)
    {
        MidiPlayInput.MidiPlaybackSetUp(AssetDatabase.GetAssetPath(midiFile));
    }

    /// <summary>
    /// Starts playing MIDI
    /// </summary>
    public static void StartPlayback()
    {
        MidiPlayInput.PlayMidi();
    }

    /// <summary>
    /// Stops playing MIDI. It does not move playback cursor to the beginning
    /// </summary>
    public static void StopPlayback()
    {
        MidiPlayInput.StopMidi();
    }

    /// <summary>
    /// Stops playing MIDI and resets playback cursor to the beginning
    /// </summary>
    public static void StopResetPlayback()
    {
        MidiPlayInput.StopResetMidi();
    }

    /// <summary>
    /// Releases output resources taken by the playback
    /// </summary>
    public static void ReleasePlaybackResources()
    {
        MidiPlayInput.MidiPlaybackReleaseResources(OutputDevice.GetByName("Microsoft GS Wavetable Synth"));
    }

    #endregion

    #region MIDI_Play_Note_Events

    /// <summary>
    /// Returns NoteName event value of the current played note
    /// </summary>
    /// <returns></returns>
    public static MIDIEvent<string> Event_CurrentNoteName()
    {
        MIDIEvent<string> midiEvent = new MIDIEvent<string>(MidiPlayInput.MidiPlayNoteName());
        return midiEvent;
    }

    /// <summary>
    /// Returns NoteNumber event value of the current played note
    /// </summary>
    /// <returns></returns>
    public static MIDIEvent<int> Event_CurrentNoteNumber()
    {
        MIDIEvent<int> midiEvent = new MIDIEvent<int>(MidiPlayInput.MidiPlayNoteNumber());
        return midiEvent;
    }

    /// <summary>
    /// Returns NoteSpeed event value of the current played note
    /// </summary>
    /// <returns></returns>
    public static MIDIEvent<int> Event_CurrentNoteSpeed()
    {
        MIDIEvent<int> midiEvent = new MIDIEvent<int>(MidiPlayInput.MidiPlayNoteSpeed());
        return midiEvent;
    }

    /// <summary>
    /// Returns NoteOnTime event value of the current played note
    /// </summary>
    /// <returns></returns>
    public static MIDIEvent<float> Event_CurrentNoteOnTime()
    {
        MIDIEvent<float> midiEvent = new MIDIEvent<float>(MidiPlayInput.MidiPlayNoteOnTime());
        return midiEvent;
    }

    /// <summary>
    /// Returns NoteOffTime event value of the current played note
    /// </summary>
    /// <returns></returns>
    public static MIDIEvent<float> Event_CurrentNoteOffTime()
    {
        MIDIEvent<float> midiEvent = new MIDIEvent<float>(MidiPlayInput.MidiPlayNoteOffTime());
        return midiEvent;
    }

    /// <summary>
    /// Returns NoteLength event value of the current played note
    /// </summary>
    /// <returns></returns>
    public static MIDIEvent<float> Event_CurrentNoteLength()
    {
        MIDIEvent<float> midiEvent = new MIDIEvent<float>(MidiPlayInput.MidiPlayNoteLength());
        return midiEvent;
    }

    #endregion

    #region MIDI_Play_Chord_Events

    /// <summary>
    /// Returns ChordNotesName event value of the current played chord
    /// </summary>
    /// <returns></returns>
    public static MIDIEvent<string> Event_CurrentChordNotesName()
    {
        MIDIEvent<string> midiEvent = new MIDIEvent<string>(MidiPlayInput.MidiPlayChordNotesName());
        return midiEvent;
    }

    /// <summary>
    /// Returns ChordNotesNumber event value of the current played chord
    /// </summary>
    /// <returns></returns>
    public static MIDIEvent<int[]> Event_CurrentChordNotesNumber()
    {
        MIDIEvent<int[]> midiEvent = new MIDIEvent<int[]>(MidiPlayInput.MidiPlayChordNotesNumber());
        return midiEvent;
    }

    /// <summary>
    /// Returns ChordSpeed event value of the current played chord
    /// </summary>
    /// <returns></returns>
    public static MIDIEvent<int> Event_CurrentChordSpeed()
    {
        MIDIEvent<int> midiEvent = new MIDIEvent<int>(MidiPlayInput.MidiPlayChordSpeed());
        return midiEvent;
    }

    /// <summary>
    /// Returns ChordOnTime event value of the current played chord
    /// </summary>
    /// <returns></returns>
    public static MIDIEvent<float> Event_CurrentChordOnTime()
    {
        MIDIEvent<float> midiEvent = new MIDIEvent<float>(MidiPlayInput.MidiPlayChordOnTime());
        return midiEvent;
    }

    /// <summary>
    /// Returns ChordOffTime event value of the current played chord
    /// </summary>
    /// <returns></returns>
    public static MIDIEvent<float> Event_CurrentChordOffTime()
    {
        MIDIEvent<float> midiEvent = new MIDIEvent<float>(MidiPlayInput.MidiPlayChordOffTime());
        return midiEvent;
    }

    /// <summary>
    /// Returns ChordLength event value of the current played chord
    /// </summary>
    /// <returns></returns>
    public static MIDIEvent<float> Event_CurrentChordLength()
    {
        MIDIEvent<float> midiEvent = new MIDIEvent<float>(MidiPlayInput.MidiPlayChordLength());
        return midiEvent;
    }

    #endregion

    #region MIDI_Play_BPM_Event

    /// <summary>
    /// Returns BPM event value at the current playback moment of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static MIDIEvent<long> Event_CurrentBPM(Object midiFile)
    {
        MIDIEvent<long> midiEvent = new MIDIEvent<long>(MidiPlayInput.MidiPlayBPM(AssetDatabase.GetAssetPath(midiFile)));
        return midiEvent;
    }

    #endregion
}