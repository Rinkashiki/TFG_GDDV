#region Dependencies

using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

#endregion

public class MidiPlayInput : MonoBehaviour
{
    #region MIDI_Play_Variables

    [HideInInspector] public Playback midiPlayback;
    private List<int> notesNumber = new List<int>();
    private List<float> noteOnTimes = new List<float>();
    private List<float> noteOffTimes = new List<float>();
    private List<float> notesLength = new List<float>();
    private List<int> notesSpeed = new List<int>();
    public Object midiFile;
    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        MidiPlaybackSetUp(AssetDatabase.GetAssetPath(midiFile));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StopMidi();
        }

        else if (Input.GetKeyDown(KeyCode.I))
        {
            StopResetMidi();
        }

        else if (Input.GetKeyDown(KeyCode.O))
        {
            PlayMidi();
        }

        else if (Input.GetKeyDown(KeyCode.R))
        {
            MidiPlaybackReleaseResources(OutputDevice.GetByName("Microsoft GS Wavetable Synth"));
        }

        else if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log(MidiPlayNoteNumber());
        }
    }

    #region MIDI_Play_Handlers

    /// <summary>
    /// Prepares the variable that controls playback of the MIDI file stores in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    public void MidiPlaybackSetUp(string midiFilePath)
    {
        // Extract MIDI Events from file
        notesNumber = MidiFileInput.MidiInputNotesNumber(midiFilePath);
        noteOnTimes = MidiFileInput.MidiInputNoteOnTimes(midiFilePath);
        noteOffTimes = MidiFileInput.MidiInputNoteOffTimes(midiFilePath);
        notesLength = MidiFileInput.MidiInputNotesLength(midiFilePath);
        notesSpeed = MidiFileInput.MidiInputNotesSpeed(midiFilePath);

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
    public void MidiPlaybackReleaseResources(OutputDevice outputDevice)
    {
        outputDevice.Dispose();
        midiPlayback.Dispose();
    }

    /// <summary>
    /// Starts playing MIDI from the last stopped
    /// </summary>
    public void PlayMidi()
    {
        midiPlayback.Start();
    }

    /// <summary>
    /// Stops playing MIDI
    /// </summary>
    public void StopMidi()
    {
        midiPlayback.Stop();
    }

    /// <summary>
    /// Stops playing MIDI and resets it to its start position
    /// </summary>
    public void StopResetMidi()
    {
        midiPlayback.Stop();
        midiPlayback.MoveToStart();
    }

    #endregion

    #region MIDI_Play_Notes_Input

    /// <summary>
    /// Returns the current played note number
    /// </summary>
    /// <returns></returns>
    public int MidiPlayNoteNumber()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * 0.000001f);

        return notesNumber[index];
    }

    /// <summary>
    /// Returns the current played note on time
    /// </summary>
    /// <returns></returns>
    public float MidiPlayNoteOnTime()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * 0.000001f);

        return noteOnTimes[index];
    }

    /// <summary>
    /// Returns the current played note off time
    /// </summary>
    /// <returns></returns>
    public float MidiPlayNoteOffTime()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * 0.000001f);

        return noteOffTimes[index];
    }

    /// <summary>
    /// Returns the current played note length
    /// </summary>
    /// <returns></returns>
    public float MidiPlayNoteLength()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * 0.000001f);

        return notesLength[index];
    }

    /// <summary>
    /// Returns the current played note speed
    /// </summary>
    /// <returns></returns>
    public int MidiPlayNoteSpeed()
    {
        // Compute the corresponding index with current time of the MIDI playback
        int index = GetIndex(midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * 0.000001f);

        return notesSpeed[index];
    }

    #endregion

    #region MIDI_Play_BPM_Input

    /// <summary>
    /// Returns the current BPM of the MIDI file stores in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public long MidiPlayBPM(string midiFilePath)
    {
        long currentBPM = MidiFileInput.MidiInputBPMAtTime(midiFilePath, midiPlayback.GetCurrentTime<MidiTimeSpan>().TimeSpan);
        return currentBPM;
    }

    #endregion

    #region Other_Utility_Functions

    private int GetIndex(float targetTime)
    {
        // Compute the corresponding index
        float nearest = noteOnTimes.OrderBy(x => System.Math.Abs(x - targetTime)).First();
        int index = noteOnTimes.IndexOf(nearest);
        index = noteOnTimes[index] > targetTime ? index-- : index;

        return index;
    }

    #endregion
}
