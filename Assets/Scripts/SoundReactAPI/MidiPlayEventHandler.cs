#region Dependencies

using Melanchall.DryWetMidi.Devices;
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

    public static void PlaybackSetUp(Object midiFile, int track)
    {
        MidiPlayInput.MidiPlaybackSetUp(AssetDatabase.GetAssetPath(midiFile), track);
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
    /// Returns Note On event value at current playback time
    /// </summary>
    /// <returns></returns>
    public static MIDINoteEvent Event_CurrentNoteOn()
    {
        MIDINoteEvent midiEvent = MidiPlayInput.MidiPlayNoteOnEvent();
        return midiEvent;
    }

    /// <summary>
    /// Returns Note Off event value at current playback time
    /// </summary>
    /// <returns></returns>
    public static MIDINoteEvent Event_CurrentNoteOff()
    {
        MIDINoteEvent midiEvent = MidiPlayInput.MidiPlayNoteOffEvent();
        return midiEvent;
    }

    #endregion

    #region MIDI_Play_Chord_Events

    /// <summary>
    /// Returns Chord On event value at current playback time
    /// </summary>
    /// <returns></returns>
    public static MIDIChordEvent Event_CurrentChordOn()
    {
        MIDIChordEvent midiEvent = MidiPlayInput.MidiPlayChordOnEvent();
        return midiEvent;
    }

    /// <summary>
    /// Returns Chord Off event value at current playback time
    /// </summary>
    /// <returns></returns>
    public static MIDIChordEvent Event_CurrentChordOff()
    {
        MIDIChordEvent midiEvent = MidiPlayInput.MidiPlayChordOffEvent();
        return midiEvent;
    }

    #endregion

    #region MIDI_Play_BPM_Event

    /// <summary>
    /// Returns BPM event value at the current playback moment of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static long Event_CurrentBPM(Object midiFile)
    {
        long midiEvent = MidiPlayInput.MidiPlayBPM(AssetDatabase.GetAssetPath(midiFile));
        return midiEvent;
    }

    #endregion
}
