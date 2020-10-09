#region Dependencies 

using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

#endregion

public class MidiFileInput
{
    #region Units

    private static float microSecToSec = 0.000001f;

    #endregion

    #region MIDI_Notes_Inputs

    //Gets the notes number
    /// <summary>
    /// Returns all notes number of MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static List<int> MidiInputNotesNumber(string midiFilePath)
    {
        // Create NotesNumber list
        List<int> NotesNumber = new List<int>();

        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Copy .mid file into .txt file
        string textFilePath = midiFilePath.Replace(".mid", ".txt");
        File.WriteAllLines(textFilePath, midiFile.GetNotes().Select(n => $"{n.NoteNumber}"));

        // Copy .txt content into NotesNumber list
        string[] lines = File.ReadAllLines(textFilePath);
        foreach (string line in lines)
        {
            NotesNumber.Add(int.Parse(line));
        }

        return NotesNumber;
    }

    //Gets the notes on time
    /// <summary>
    /// Returns all noteOn times in seconds of MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static List<float> MidiInputNotesOnTime(string midiFilePath)
    {
        // Create NotesTime list
        List<float> NotesOnTime = new List<float>();

        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Copy .mid file into .txt file
        TempoMap tempo = midiFile.GetTempoMap();
        string textFilePath = midiFilePath.Replace(".mid", ".txt");
        File.WriteAllLines(textFilePath, midiFile.GetNotes().Select(n => $"{n.TimeAs<MetricTimeSpan>(tempo).TotalMicroseconds}"));

        // Copy .txt content into NotesTime list
        string[] lines = File.ReadAllLines(textFilePath);
        foreach (string line in lines)
        {
            NotesOnTime.Add(float.Parse(line) * microSecToSec);
        }

        return NotesOnTime;
    }

    //Gets the notes off time
    /// <summary>
    /// Returns all noteOff times in seconds of MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static List<float> MidiInputNotesOffTime(string midiFilePath)
    {
        // Create NotesTime list
        List<float> NotesOffTime = new List<float>();

        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Copy .mid file into .txt file
        TempoMap tempo = midiFile.GetTempoMap();
        string textFilePath = midiFilePath.Replace(".mid", ".txt");
        File.WriteAllLines(textFilePath, midiFile.GetNotes().Select(n => $"{n.EndTimeAs<MetricTimeSpan>(tempo).TotalMicroseconds}"));

        // Copy .txt content into NotesTime list
        string[] lines = File.ReadAllLines(textFilePath);
        foreach (string line in lines)
        {
            NotesOffTime.Add(float.Parse(line) * microSecToSec);
        }

        return NotesOffTime;

    }

    //Gets the notes length
    /// <summary>
    /// Returns all notes length in seconds of MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static List<float> MidiInputNotesLength(string midiFilePath)
    {
        // Create NotesLength list
        List<float> NotesLength = new List<float>();

        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Copy .mid file into .txt file
        TempoMap tempo = midiFile.GetTempoMap();
        string textFilePath = midiFilePath.Replace(".mid", ".txt");
        File.WriteAllLines(textFilePath, midiFile.GetNotes().Select(n => $"{n.LengthAs<MetricTimeSpan>(tempo).TotalMicroseconds}"));

        // Copy .txt content into NotesNumber list
        string[] lines = File.ReadAllLines(textFilePath);
        foreach (string line in lines)
        {
            NotesLength.Add(float.Parse(line) * microSecToSec);
        }

        return NotesLength;
    }

    //Gets the notes speed
    /// <summary>
    /// Returns all notes speed of MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static List<int> MidiInputNotesSpeed(string midiFilePath)
    {
        // Create NotesSpeed list
        List<int> NotesSpeed = new List<int>();

        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Copy .mid file into .txt file
        string textFilePath = midiFilePath.Replace(".mid", ".txt");
        File.WriteAllLines(textFilePath, midiFile.GetNotes().Select(n => $"{n.Velocity}"));

        // Copy .txt content into NotesNumber list
        string[] lines = File.ReadAllLines(textFilePath);
        foreach (string line in lines)
        {
            NotesSpeed.Add(int.Parse(line));
        }

        return NotesSpeed;
    }

    #endregion

    #region MIDI_BPM_Input

    //Gets BPM
    /// <summary>
    /// Returns BPM in a specified <paramref name="time"/> of MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static long MidiInputBPMAtTime(string midiFilePath, long time)
    {
        // Create BPM variable
        long BPM = 0;

        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Extract TempoMap Variable
        TempoMap tempo = midiFile.GetTempoMap();

        // Extract Tempo convert in BPM
        BPM = tempo.TempoLine.GetValueAtTime(time).BeatsPerMinute;

        return BPM;
    }

    #endregion
}
