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
        List<int> a = new List<int>();
        a = MidiInputNotesNumber(AssetDatabase.GetAssetPath(midiFile));
        foreach (int note in a)
        {
            Debug.Log(note);
        }
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
    public static List<int> MidiInputNotesTime(string midiFilePath)
    {
        // Create NotesTime list
        List<int> NotesTime = new List<int>();

        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Copy .mid file into .txt file
        string textFilePath = midiFilePath.Replace(".mid", ".txt");
        File.WriteAllLines(textFilePath, midiFile.GetNotes().Select(n => $"{n.Time}"));

        // Copy .txt content into NotesTime list
        string[] lines = File.ReadAllLines(textFilePath);
        foreach (string line in lines)
        {
            NotesTime.Add(int.Parse(line));
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
}
