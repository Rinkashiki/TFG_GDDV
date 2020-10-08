#region Dependencies

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#endregion

public class SoundReactEventHandler : MonoBehaviour
{

    #region In_Editor_Variables

    [SerializeField] private Object midiFile;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        long BPM = Event_BPMAtTime(midiFile, 0).GetValueEvent();
        Debug.Log(BPM);
        List<int> numbers = Event_NotesNumber(midiFile).GetListEvent();
        List<float> times = Event_NotesOnTime(midiFile).GetListEvent();
        List<float> lengths = Event_NotesLength(midiFile).GetListEvent();
        List<int> speeds = Event_NotesSpeed(midiFile).GetListEvent();
        for (int i = 0; i < numbers.Count; i++)
        {
            Debug.Log("NoteNumber: " + numbers[i] + "  NoteOnTime: " + times[i] + "  NoteLength: " + lengths[i] + "  NoteSpeed: " + speeds[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region MIDI_Events

    /// <summary>
    /// Returns NotesNumber event list of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public MIDIEvent<int> Event_NotesNumber(Object midiFile)
    {
        MIDIEvent<int> midiEvent = new MIDIEvent<int>(MidiFileInput.MidiInputNotesNumber(AssetDatabase.GetAssetPath(midiFile)));
        return midiEvent;
    }

    /// <summary>
    /// Returns NotesSpeed event list of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public MIDIEvent<int> Event_NotesSpeed(Object midiFile)
    {
        MIDIEvent<int> midiEvent = new MIDIEvent<int>(MidiFileInput.MidiInputNotesSpeed(AssetDatabase.GetAssetPath(midiFile)));
        return midiEvent;
    }

    /// <summary>
    /// Returns NotesOnTime event list in seconds of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public MIDIEvent<float> Event_NotesOnTime(Object midiFile)
    {
        MIDIEvent<float> midiEvent = new MIDIEvent<float>(MidiFileInput.MidiInputNotesOnTime(AssetDatabase.GetAssetPath(midiFile)));
        return midiEvent;
    }

    /// <summary>
    /// Returns NotesLength event list in seconds of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public MIDIEvent<float> Event_NotesLength(Object midiFile)
    {
        MIDIEvent<float> midiEvent = new MIDIEvent<float>(MidiFileInput.MidiInputNotesLength(AssetDatabase.GetAssetPath(midiFile)));
        return midiEvent;
    }

    /// <summary>
    /// Returns BPM event value at given MIDI <paramref name="time"/> of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public MIDIEvent<long> Event_BPMAtTime(Object midiFile, long time)
    {
        MIDIEvent<long> midiEvent = new MIDIEvent<long>(MidiFileInput.MidiInputBPMAtTime(AssetDatabase.GetAssetPath(midiFile), time));
        return midiEvent;
    }

    #endregion
}
