#region Dependencies

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#endregion

public class MidiFileEventHandler
{
    #region MIDI_File_Note_Events

    /// <summary>
    /// Returns NotesName event list of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static MIDIEvent<string> Event_NotesName(Object midiFile)
    {
        MIDIEvent<string> midiEvent = new MIDIEvent<string>(MidiFileInput.MidiInputNotesName(AssetDatabase.GetAssetPath(midiFile)));
        return midiEvent;
    }

    /// <summary>
    /// Returns NotesNumber event list of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static MIDIEvent<int> Event_NotesNumber(Object midiFile)
    {
        MIDIEvent<int> midiEvent = new MIDIEvent<int>(MidiFileInput.MidiInputNotesNumber(AssetDatabase.GetAssetPath(midiFile)));
        return midiEvent;
    }

    /// <summary>
    /// Returns NoteOnVelocities event list of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static MIDIEvent<int> Event_NoteOnVelocities(Object midiFile)
    {
        MIDIEvent<int> midiEvent = new MIDIEvent<int>(MidiFileInput.MidiInputNoteOnVelocities(AssetDatabase.GetAssetPath(midiFile)));
        return midiEvent;
    }

    /// <summary>
    /// Returns NoteOffVelocities event list of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static MIDIEvent<int> Event_NoteOffVelocities(Object midiFile)
    {
        MIDIEvent<int> midiEvent = new MIDIEvent<int>(MidiFileInput.MidiInputNoteOffVelocities(AssetDatabase.GetAssetPath(midiFile)));
        return midiEvent;
    }

    /// <summary>
    /// Returns NoteOnTimes event list in seconds of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static MIDIEvent<float> Event_NoteOnTimes(Object midiFile)
    {
        MIDIEvent<float> midiEvent = new MIDIEvent<float>(MidiFileInput.MidiInputNoteOnTimes(AssetDatabase.GetAssetPath(midiFile)));
        return midiEvent;
    }

    /// <summary>
    /// Returns NoteOffTimes event list in seconds of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static MIDIEvent<float> Event_NoteOffTimes(Object midiFile)
    {
        MIDIEvent<float> midiEvent = new MIDIEvent<float>(MidiFileInput.MidiInputNoteOffTimes(AssetDatabase.GetAssetPath(midiFile)));
        return midiEvent;
    }

    /// <summary>
    /// Returns NotesLength event list in seconds of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static MIDIEvent<float> Event_NotesLength(Object midiFile)
    {
        MIDIEvent<float> midiEvent = new MIDIEvent<float>(MidiFileInput.MidiInputNotesLength(AssetDatabase.GetAssetPath(midiFile)));
        return midiEvent;
    }

    #endregion

    #region MIDI_File_Chord_Events

    /// <summary>
    /// Returns ChordsName event list of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static MIDIEvent<string> Event_ChordsNotesName(Object midiFile)
    {
        MIDIEvent<string> midiEvent = new MIDIEvent<string>(MidiFileInput.MidiInputChordsNotesName(AssetDatabase.GetAssetPath(midiFile)));
        return midiEvent;
    }

    /// <summary>
    /// Returns ChordsNumber event list of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static MIDIEvent<int[]> Event_ChordsNotesNumber(Object midiFile)
    {
        MIDIEvent<int[]> midiEvent = new MIDIEvent<int[]>(MidiFileInput.MidiInputChordsNotesNumber(AssetDatabase.GetAssetPath(midiFile)));
        return midiEvent;
    }

    /// <summary>
    /// Returns ChordOnVelocities event list of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static MIDIEvent<int[]> Event_ChordOnVelocities(Object midiFile)
    {
        MIDIEvent<int[]> midiEvent = new MIDIEvent<int[]>(MidiFileInput.MidiInputChordsNoteOnVelocities(AssetDatabase.GetAssetPath(midiFile)));
        return midiEvent;
    }

    /// <summary>
    /// Returns ChordOffVelocities event list of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static MIDIEvent<int[]> Event_ChordOffVelocities(Object midiFile)
    {
        MIDIEvent<int[]> midiEvent = new MIDIEvent<int[]>(MidiFileInput.MidiInputChordsNoteOffVelocities(AssetDatabase.GetAssetPath(midiFile)));
        return midiEvent;
    }

    /// <summary>
    /// Returns ChordOnTimes event list of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static MIDIEvent<float> Event_ChordOnTimes(Object midiFile)
    {
        MIDIEvent<float> midiEvent = new MIDIEvent<float>(MidiFileInput.MidiInputChordOnTimes(AssetDatabase.GetAssetPath(midiFile)));
        return midiEvent;
    }

    /// <summary>
    /// Returns ChordOffTimes event list of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static MIDIEvent<float> Event_ChordOffTimes(Object midiFile)
    {
        MIDIEvent<float> midiEvent = new MIDIEvent<float>(MidiFileInput.MidiInputChordOffTimes(AssetDatabase.GetAssetPath(midiFile)));
        return midiEvent;
    }

    /// <summary>
    /// Returns ChordsLength event list of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static MIDIEvent<float> Event_ChordsLength(Object midiFile)
    {
        MIDIEvent<float> midiEvent = new MIDIEvent<float>(MidiFileInput.MidiInputChordsLength(AssetDatabase.GetAssetPath(midiFile)));
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
    public static MIDIEvent<long> Event_BPMAtTime(Object midiFile, long time)
    {
        MIDIEvent<long> midiEvent = new MIDIEvent<long>(MidiFileInput.MidiInputBPMAtTime(AssetDatabase.GetAssetPath(midiFile), time));
        return midiEvent;
    }

    #endregion
}
