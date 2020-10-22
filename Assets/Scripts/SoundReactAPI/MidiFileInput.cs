#region Dependencies 

using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

public class MidiFileInput
{
    #region Units

    private static float microSecToSec = 0.000001f;

    #endregion

    #region MIDI_Notes_Inputs

    /// <summary>
    /// Returns all notes name of MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static List<string> MidiInputNotesName(string midiFilePath)
    {
        // Create NotesNumber list
        List<string> NotesName = new List<string>();

        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Extract notes from .mid file
        List<Note> notes = midiFile.GetNotes().ToList();

        // Extract notes name from notes list
        foreach (Note note in notes)
        {
            NotesName.Add(note.NoteName.ToString() + note.Octave);
        }

        return NotesName;
    }

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

        // Extract notes from .mid file
        List<Note> notes = midiFile.GetNotes().ToList();

        // Extract notes number from notes list
        foreach (Note note in notes)
        {
            NotesNumber.Add(note.NoteNumber);
        }

        return NotesNumber;
    }

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

        // Extract notes from .mid file
        List<Note> notes = midiFile.GetNotes().ToList();

        // Extract notes velocity from notes list
        foreach (Note note in notes)
        {
            NotesSpeed.Add(note.Velocity);
        }

        return NotesSpeed;
    }

    /// <summary>
    /// Returns all noteOn times in seconds of MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static List<float> MidiInputNoteOnTimes(string midiFilePath)
    {
        // Create NotesTime list
        List<float> NoteOnTimes = new List<float>();

        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Extract notes from .mid file
        TempoMap tempo = midiFile.GetTempoMap();
        List<Note> notes = midiFile.GetNotes().ToList();

        // Extract noteOn times from notes list
        foreach (Note note in notes)
        {
            NoteOnTimes.Add(note.TimeAs<MetricTimeSpan>(tempo).TotalMicroseconds * microSecToSec);
        }

        return NoteOnTimes;
    }

    /// <summary>
    /// Returns all noteOff times in seconds of MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static List<float> MidiInputNoteOffTimes(string midiFilePath)
    {
        // Create NotesTime list
        List<float> NoteOffTimes = new List<float>();

        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Extract notes from .mid file
        TempoMap tempo = midiFile.GetTempoMap();
        List<Note> notes = midiFile.GetNotes().ToList();

        // Extract noteOff times from notes list
        foreach (Note note in notes)
        {
            NoteOffTimes.Add(note.EndTimeAs<MetricTimeSpan>(tempo).TotalMicroseconds * microSecToSec);
        }

        return NoteOffTimes;

    }

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

        // Extract notes from .mid file
        TempoMap tempo = midiFile.GetTempoMap();
        List<Note> notes = midiFile.GetNotes().ToList();

        // Extract notes length from notes list
        foreach (Note note in notes)
        {
            NotesLength.Add(note.LengthAs<MetricTimeSpan>(tempo).TotalMicroseconds * microSecToSec);
        }

        return NotesLength;
    }

    #endregion

    #region MIDI_Chords_Inputs

    /// <summary>
    /// Returns all chords notes names of every chord in MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static List<string> MidiInputChordsNotesName(string midiFilePath)
    {
        // Create ChordsNames list
        List<string> ChordsNames = new List<string>();

        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Extract chords from .mid file
        List<Chord> chordsList = midiFile.GetChords().ToList();

        // Extract chords notes name from chords list
        foreach (Chord chord in chordsList)
        {
            ChordsNames.Add(chord.ToString());
        }

        return ChordsNames;
    }

    /// <summary>
    /// Returns all chords speed of MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static List<int> MidiInputChordsSpeed(string midiFilePath)
    {
        // Create ChordsSpeed list
        List<int> ChordsSpeed = new List<int>();

        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Extract chords from .mid file
        List<Chord> chordsList = midiFile.GetChords().ToList();

        // Extract chords velocity from chords list
        foreach (Chord chord in chordsList)
        {
            ChordsSpeed.Add(chord.Velocity);
        }

        return ChordsSpeed;
    }

    /// <summary>
    /// Returns all chords notes number of every chord in MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static List<int[]> MidiInputChordsNotesNumber(string midiFilePath)
    {
        // Create ChordsNumbers list
        List<int[]> ChordsNumbers = new List<int[]>();

        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Copy .mid file chords into a list
        List<Chord> chordsList = midiFile.GetChords().ToList();

        // Extract chords notes number from chords list
        foreach (Chord chord in chordsList)
        {
            Note[] chordNotes = chord.Notes.ToArray();
            int[] notes = new int[chord.Notes.Count()];

            for (int i = 0; i < chord.Notes.Count(); i++)
            {
                notes[i] = chordNotes[i].NoteNumber;
            }

            ChordsNumbers.Add(notes);
        }

        return ChordsNumbers;
    }

    /// <summary>
    /// Returns all chordOn times in seconds of MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static List<float> MidiInputChordOnTimes(string midiFilePath)
    {
        // Create ChordOnTimes list
        List<float> ChordOnTimes = new List<float>();

        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Extract chords from .mid file
        TempoMap tempo = midiFile.GetTempoMap();
        List<Chord> chordsList = midiFile.GetChords().ToList();

        // Extract chords note on times from chords list
        foreach (Chord chord in chordsList)
        {
            ChordOnTimes.Add(chord.TimeAs<MetricTimeSpan>(tempo).TotalMicroseconds * microSecToSec);
        }

        return ChordOnTimes;
    }

    /// <summary>
    /// Returns all chordOff times in seconds of MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static List<float> MidiInputChordOffTimes(string midiFilePath)
    {
        // Create ChordOffTimes list
        List<float> ChordOffTimes = new List<float>();

        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Extract chords from .mid file
        TempoMap tempo = midiFile.GetTempoMap();
        List<Chord> chordsList = midiFile.GetChords().ToList();

        // Extract chords note off times from chords list
        foreach (Chord chord in chordsList)
        {
            ChordOffTimes.Add(chord.EndTimeAs<MetricTimeSpan>(tempo).TotalMicroseconds * microSecToSec);
        }

        return ChordOffTimes;
    }

    /// <summary>
    /// Returns all chords length in seconds of MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static List<float> MidiInputChordsLength(string midiFilePath)
    {
        // Create ChordsLength list
        List<float> ChordsLength = new List<float>();

        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Extract chords from .mid file
        TempoMap tempo = midiFile.GetTempoMap();
        List<Chord> chordsList = midiFile.GetChords().ToList();

        // Extract chords length from chords list
        foreach (Chord chord in chordsList)
        {
            ChordsLength.Add(chord.LengthAs<MetricTimeSpan>(tempo).TotalMicroseconds * microSecToSec);
        }

        return ChordsLength;
    }

    #endregion

    #region MIDI_BPM_Input

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
