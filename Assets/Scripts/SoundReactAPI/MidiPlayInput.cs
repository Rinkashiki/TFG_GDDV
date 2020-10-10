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

    private Playback midiPlayback;
    private List<int> notesNumber = new List<int>();
    private List<float> notesOnTime = new List<float>();
    [SerializeField] private Object midiFile;
    
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
        notesOnTime = MidiFileInput.MidiInputNotesOnTime(midiFilePath);

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

    #region MIDI_Play_Events

    /// <summary>
    /// Returns the current played note
    /// </summary>
    /// <returns></returns>
    public int MidiPlayNoteNumber()
    {
        float targetTime = midiPlayback.GetCurrentTime<MetricTimeSpan>().TotalMicroseconds * 0.000001f;
        float nearest = notesOnTime.OrderBy(x => System.Math.Abs(x - targetTime)).First();
        int index = notesOnTime.IndexOf(nearest);
        index = notesOnTime[index] > targetTime ? index-- : index;

        return notesNumber[index];
    }

    #endregion
}
