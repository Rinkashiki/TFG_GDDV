#region Dependencies

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

#endregion

public class MidiFileEventHandler
{
    #region MIDI_File_Note_Events

    /// <summary>
    /// Returns Note On events list from the specified one track <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static List<MIDINoteEvent> Event_NoteOnList(Object midiFile)
    {
        List<MIDINoteEvent> midiEvent = MidiFileInput.MidiInputNoteOnEvents(AssetDatabase.GetAssetPath(midiFile));
        return midiEvent;
    }

    /// <summary>
    /// Returns distinct Note On events list from the specified one track <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static List<MIDINoteEvent> Event_NoteOnListDistinct(Object midiFile)
    {
        List<MIDINoteEvent> midiEvent = MidiFileInput.MidiInputNoteOnEvents(AssetDatabase.GetAssetPath(midiFile));
        List<MIDINoteEvent> distinctMIDIEvent = midiEvent.GroupBy(x => x.GetNoteNumber()).Select(y => y.First()).ToList();
        return distinctMIDIEvent;
    }

    /// <summary>
    /// Returns Note On events list of the specified track from multiple track <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static List<MIDINoteEvent> Event_NoteOnList(Object midiFile, int track)
    {
        List<MIDINoteEvent>[] midiTrackEvent = MidiFileInput.MidiInputNoteOnEventsTracks(AssetDatabase.GetAssetPath(midiFile));
        List<MIDINoteEvent> midiEvent = midiTrackEvent[track];
        return midiEvent;
    }

    /// <summary>
    /// Returns Note Off events list from the specified one track <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static List<MIDINoteEvent> Event_NoteOffList(Object midiFile)
    {
        List<MIDINoteEvent> midiEvent = MidiFileInput.MidiInputNoteOffEvents(AssetDatabase.GetAssetPath(midiFile));
        return midiEvent;
    }

    /// <summary>
    /// Returns Note Off events list of the specified track from multiple track <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <param name="track"></param>
    /// <returns></returns>
    public static List<MIDINoteEvent> Event_NoteOffList(Object midiFile, int track)
    {
        List<MIDINoteEvent>[] midiTrackEvent = MidiFileInput.MidiInputNoteOffEventsTracks(AssetDatabase.GetAssetPath(midiFile));
        List<MIDINoteEvent> midiEvent = midiTrackEvent[track];
        return midiEvent;
    }

    #endregion

    #region MIDI_File_Chord_Events

    /// <summary>
    /// Returns Chord On events list from the specified one track <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static List<MIDIChordEvent> Event_ChordOnList(Object midiFile)
    {
        List<MIDIChordEvent> midiEvent = MidiFileInput.MidiInputChordOnEvents(AssetDatabase.GetAssetPath(midiFile));
        return midiEvent;
    }

    /// <summary>
    /// Returns Chord On events list of the specified track from multiple track <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <param name="track"></param>
    /// <returns></returns>
    public static List<MIDIChordEvent> Event_ChordOnList(Object midiFile, int track)
    {
        List<MIDIChordEvent>[] midiTrackEvent = MidiFileInput.MidiInputChordOnEventsTracks(AssetDatabase.GetAssetPath(midiFile));
        List<MIDIChordEvent> midiEvent = midiTrackEvent[track];
        return midiEvent;
    }

    /// <summary>
    /// Returns Chord Off events list from the specified one track <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public static List<MIDIChordEvent> Event_ChordOffList(Object midiFile)
    {
        List<MIDIChordEvent> midiEvent = MidiFileInput.MidiInputChordOffEvents(AssetDatabase.GetAssetPath(midiFile));
        return midiEvent;
    }

    /// <summary>
    /// Returns Chord Off events list of the specified track from multiple track <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <param name="track"></param>
    /// <returns></returns>
    public static List<MIDIChordEvent> Event_ChordOffList(Object midiFile, int track)
    {
        List<MIDIChordEvent>[] midiTrackEvent = MidiFileInput.MidiInputChordOffEventsTracks(AssetDatabase.GetAssetPath(midiFile));
        List<MIDIChordEvent> midiEvent = midiTrackEvent[track];
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
