#region Dependencies

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#endregion

public class MidiFileEventHandler
{
    #region MIDI_File_Note_Events

    /// <summary>
    /// Returns Note On events list the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static List<MIDINoteEvent> Event_NoteOnList(Object midiFile, int track)
    {
        List<MIDINoteEvent>[] midiTrackEvent = MidiFileInput.MidiInputNoteOnEvents(AssetDatabase.GetAssetPath(midiFile));
        List<MIDINoteEvent> midiEvent = midiTrackEvent[track];
        return midiEvent;
    }

    /// <summary>
    /// Returns Note Off events list the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static List<MIDINoteEvent> Event_NoteOffList(Object midiFile)
    {
        List<MIDINoteEvent> midiEvent = MidiFileInput.MidiInputNoteOffEvents(AssetDatabase.GetAssetPath(midiFile));
        return midiEvent;
    }

    #endregion

    #region MIDI_File_Chord_Events

    /// <summary>
    /// Returns Chord On events list the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static List<MIDIChordEvent> Event_ChordOnList(Object midiFile)
    {
        List<MIDIChordEvent> midiEvent = MidiFileInput.MidiInputChordOnEvents(AssetDatabase.GetAssetPath(midiFile));
        return midiEvent;
    }

    /// <summary>
    /// Returns Chord Off events list the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static List<MIDIChordEvent> Event_ChordOffList(Object midiFile)
    {
        List<MIDIChordEvent> midiEvent = MidiFileInput.MidiInputChordOffEvents(AssetDatabase.GetAssetPath(midiFile));
        return midiEvent;
    }

    #endregion

    #region MIDI_File_BPM_Event

    /// <summary>
    /// Returns BPM event value at given MIDI <paramref name="time"/> of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static long Event_BPMAtTime(Object midiFile, long time)
    {
        long midiEvent = MidiFileInput.MidiInputBPMAtTime(AssetDatabase.GetAssetPath(midiFile), time);
        return midiEvent;
    }

    #endregion
}
