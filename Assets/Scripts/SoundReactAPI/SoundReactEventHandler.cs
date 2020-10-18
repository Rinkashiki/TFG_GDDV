#region Dependencies

using Melanchall.DryWetMidi.Devices;
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
        // VERY IMPORTANT CALL THIS before anything related with MIDI playback
        PlaybackSetUp(midiFile);

        /*
        long BPM = Event_BPMAtTime(midiFile, 0).GetValueEvent();
        Debug.Log(BPM);
        List<int> numbers = Event_NotesNumber(midiFile).GetListEvent();
        List<float> timesOn = Event_NotesOnTime(midiFile).GetListEvent();
        List<float> timesOff = Event_NotesOffTime(midiFile).GetListEvent();
        List<float> lengths = Event_NotesLength(midiFile).GetListEvent();
        List<int> speeds = Event_NotesSpeed(midiFile).GetListEvent();
        for (int i = 0; i < numbers.Count; i++)
        {
            Debug.Log("NoteNumber: " + numbers[i] + "  NoteOnTime: " + timesOn[i] + "  NoteOffTime: " + timesOff[i]+ "  NoteLength: " + lengths[i] + "  NoteSpeed: " + speeds[i]);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        /*
       if(Input.GetKeyDown(KeyCode.A))
        {
            StartPlayback();
        }
       else if (Input.GetKeyDown(KeyCode.S))
        {
            StopPlayback();
        }
       else if (Input.GetKeyDown(KeyCode.D))
        {
            ReleasePlaybackResources();
        }
       else if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log(Event_CurrentNoteName().GetValueEvent());
        }
        */
    }

    #region MIDI_File_Events

    /// <summary>
    /// Returns NotesName event list of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public MIDIEvent<string> Event_NotesName(Object midiFile)
    {
        MIDIEvent<string> midiEvent = new MIDIEvent<string>(MidiFileInput.MidiInputNotesName(AssetDatabase.GetAssetPath(midiFile)));
        return midiEvent;
    }

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
    public MIDIEvent<float> Event_NoteOnTimes(Object midiFile)
    {
        MIDIEvent<float> midiEvent = new MIDIEvent<float>(MidiFileInput.MidiInputNoteOnTimes(AssetDatabase.GetAssetPath(midiFile)));
        return midiEvent;
    }

    /// <summary>
    /// Returns NotesOffTime event list in seconds of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public MIDIEvent<float> Event_NoteOffTimes(Object midiFile)
    {
        MIDIEvent<float> midiEvent = new MIDIEvent<float>(MidiFileInput.MidiInputNoteOffTimes(AssetDatabase.GetAssetPath(midiFile)));
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

    #region MIDI_Play_Events

    /// <summary>
    /// Returns NoteName event value of the current played note
    /// </summary>
    /// <returns></returns>
    public MIDIEvent<string> Event_CurrentNoteName()
    {
        MIDIEvent<string> midiEvent = new MIDIEvent<string>(MidiPlayInput.MidiPlayNoteName());
        return midiEvent;
    }

    /// <summary>
    /// Returns NoteNumber event value of the current played note
    /// </summary>
    /// <returns></returns>
    public MIDIEvent<int> Event_CurrentNoteNumber()
    {
        MIDIEvent<int> midiEvent = new MIDIEvent<int>(MidiPlayInput.MidiPlayNoteNumber());
        return midiEvent;
    }

    /// <summary>
    /// Returns NoteSpeed event value of the current played note
    /// </summary>
    /// <returns></returns>
    public MIDIEvent<int> Event_CurrentNoteSpeed()
    {
        MIDIEvent<int> midiEvent = new MIDIEvent<int>(MidiPlayInput.MidiPlayNoteSpeed());
        return midiEvent;
    }

    /// <summary>
    /// Returns NoteOnTime event value of the current played note
    /// </summary>
    /// <returns></returns>
    public MIDIEvent<float> Event_CurrentNoteOnTime()
    {
        MIDIEvent<float> midiEvent = new MIDIEvent<float>(MidiPlayInput.MidiPlayNoteOnTime());
        return midiEvent;
    }

    /// <summary>
    /// Returns NoteOffTime event value of the current played note
    /// </summary>
    /// <returns></returns>
    public MIDIEvent<float> Event_CurrentNoteOffTime()
    {
        MIDIEvent<float> midiEvent = new MIDIEvent<float>(MidiPlayInput.MidiPlayNoteOffTime());
        return midiEvent;
    }

    /// <summary>
    /// Returns NoteLength event value of the current played note
    /// </summary>
    /// <returns></returns>
    public MIDIEvent<float> Event_CurrentNoteLength()
    {
        MIDIEvent<float> midiEvent = new MIDIEvent<float>(MidiPlayInput.MidiPlayNoteLength());
        return midiEvent;
    }

    /// <summary>
    /// Returns BPM event value at the current playback moment of the specified <paramref name="midiFile"/>
    /// </summary>
    /// <param name="midiFile"></param>
    /// <returns></returns>
    public MIDIEvent<long> Event_CurrentBPM(Object midiFile)
    {
        MIDIEvent<long> midiEvent = new MIDIEvent<long>(MidiPlayInput.MidiPlayBPM(AssetDatabase.GetAssetPath(midiFile)));
        return midiEvent;
    }

    #endregion

    #region MIDI_Playback_Actions

    /// <summary>
    /// Configures MIDI playback fo the specified <paramref name="midiFile"/>. Call this before anything related with MIDI playback
    /// </summary>
    /// <param name="midiFile"></param>
    public void PlaybackSetUp(Object midiFile)
    {
        MidiPlayInput.MidiPlaybackSetUp(AssetDatabase.GetAssetPath(midiFile));
    }

    /// <summary>
    /// Starts playing MIDI
    /// </summary>
    public void StartPlayback()
    {
        MidiPlayInput.PlayMidi();
    }

    /// <summary>
    /// Stops playing MIDI. It does not move playback cursor to the beginning
    /// </summary>
    public void StopPlayback()
    {
        MidiPlayInput.StopMidi();
    }

    /// <summary>
    /// Stops playing MIDI and resets playback cursor to the beginning
    /// </summary>
    public void StopResetPlayback()
    {
        MidiPlayInput.StopResetMidi();
    }

    /// <summary>
    /// Releases output resources taken by the playback
    /// </summary>
    public void ReleasePlaybackResources()
    {
        MidiPlayInput.MidiPlaybackReleaseResources(OutputDevice.GetByName("Microsoft GS Wavetable Synth"));
    }

    #endregion
}
