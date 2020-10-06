#region Dependencies 
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
#endregion

public class MidiFileInput : MonoBehaviour
{
    #region In_Editor_Variables

    [SerializeField] private Object midiFile;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        /*
        List<float> a = new List<float>();
        a = MidiInputNotesOnTime(AssetDatabase.GetAssetPath(midiFile));
        foreach (float note in a)
        {
            Debug.Log(note);
        }
        */
        Debug.Log(MidiInputBPMAtTime(AssetDatabase.GetAssetPath(midiFile), 0));
    }
    #region MIDI_Notes_Inputs

    //Gets the notes number
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

    //Gets the notes time
    public static List<float> MidiInputNotesOnTime(string midiFilePath)
    {
        // Create NotesTime list
        List<float> NotesTime = new List<float>();

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
            NotesTime.Add(float.Parse(line) * 0.000001f);
        }

        return NotesTime;
    }

    //Gets the notes length
    public static List<int> MidiInputNotesLength(string midiFilePath)
    {
        // Create NotesLength list
        List<int> NotesLength = new List<int>();

        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Copy .mid file into .txt file
        string textFilePath = midiFilePath.Replace(".mid", ".txt");
        File.WriteAllLines(textFilePath, midiFile.GetNotes().Select(n => $"{n.Length}"));

        // Copy .txt content into NotesNumber list
        string[] lines = File.ReadAllLines(textFilePath);
        foreach (string line in lines)
        {
            NotesLength.Add(int.Parse(line));
        }

        return NotesLength;
    }

    //Gets the notes speed
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
