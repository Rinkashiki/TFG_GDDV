#region Dependencies 

using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using System.Linq;

#endregion

public class MidiFileInput
{
    #region Units

    private static float microSecToSec = 0.000001f;

    #endregion

    #region MIDI_Notes_Inputs

    /// <summary>
    /// Returns all Note On events of MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static List<MIDINoteEvent> MidiInputNoteOnEvents(string midiFilePath)
    {
        // Create NoteOnEvents list
        List<MIDINoteEvent> NoteOnEvents = new List<MIDINoteEvent>();

        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Extract notes from .mid file
        TempoMap tempo = midiFile.GetTempoMap();
        List<Note> notes = midiFile.GetNotes().ToList();

        // Declare Note properties
        string NoteName;
        int NoteNumber;
        int NoteVelocity;
        float NoteTime;
        float NoteLength;

        // Extract notes properties from notes list
        foreach (Note note in notes)
        {
            NoteName = note.NoteName.ToString() + note.Octave;
            NoteNumber = note.NoteNumber;
            NoteVelocity = note.Velocity;
            NoteTime = note.TimeAs<MetricTimeSpan>(tempo).TotalMicroseconds * microSecToSec;
            NoteLength = note.LengthAs<MetricTimeSpan>(tempo).TotalMicroseconds * microSecToSec;
            NoteOnEvents.Add(new MIDINoteEvent(MIDIEvent.Type.NoteOn, NoteName, NoteNumber, NoteVelocity, NoteTime, NoteLength));
        }

        return NoteOnEvents;
    }

    /// <summary>
    /// Returns all Note Off events of MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static List<MIDINoteEvent> MidiInputNoteOffEvents(string midiFilePath)
    {
        // Create NoteOffEvents list
        List<MIDINoteEvent> NoteOffEvents = new List<MIDINoteEvent>();

        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Extract notes from .mid file
        TempoMap tempo = midiFile.GetTempoMap();
        List<Note> notes = midiFile.GetNotes().ToList();

        // Declare Note properties
        string NoteName;
        int NoteNumber;
        int NoteVelocity;
        float NoteTime;
        float NoteLength;

        // Extract notes properties from notes list
        foreach (Note note in notes)
        {
            NoteName = note.NoteName.ToString() + note.Octave;
            NoteNumber = note.NoteNumber;
            NoteVelocity = note.OffVelocity;
            NoteTime = note.EndTimeAs<MetricTimeSpan>(tempo).TotalMicroseconds * microSecToSec;
            NoteLength = note.LengthAs<MetricTimeSpan>(tempo).TotalMicroseconds * microSecToSec;
            NoteOffEvents.Add(new MIDINoteEvent(MIDIEvent.Type.NoteOff, NoteName, NoteNumber, NoteVelocity, NoteTime, NoteLength));
        }

        return NoteOffEvents;
    }

    #endregion

    #region MIDI_Chords_Inputs

    /// <summary>
    /// Returns all Chord On events of MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static List<MIDIChordEvent> MidiInputChordOnEvents(string midiFilePath)
    {
        // Create ChordOnEvents list
        List<MIDIChordEvent> ChordOnEvents = new List<MIDIChordEvent>();

        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Extract notes from .mid file
        TempoMap tempo = midiFile.GetTempoMap();
        List<Chord> chords = midiFile.GetChords().ToList();

        // Declare Note properties
        string ChordNotesNames;
        int[] ChordNotesNumbers;
        int[] ChordNotesVelocities;
        float ChordTime;
        float ChordLength;

        // Extract chords properties from chords list
        foreach (Chord chord in chords)
        {
            // Extract notes numbers and velocities
            Note[] chordNotes = chord.Notes.ToArray();
            int[] notes = new int[chord.Notes.Count()];
            int[] velocities = new int[chord.Notes.Count()];

            for (int i = 0; i < chord.Notes.Count(); i++)
            {
                notes[i] = chordNotes[i].NoteNumber;
                velocities[i] = chordNotes[i].Velocity;
            }

            ChordNotesNames = chord.ToString();
            ChordNotesNumbers = notes;
            ChordNotesVelocities = velocities;
            ChordTime = chord.TimeAs<MetricTimeSpan>(tempo).TotalMicroseconds * microSecToSec;
            ChordLength = chord.LengthAs<MetricTimeSpan>(tempo).TotalMicroseconds * microSecToSec;
            ChordOnEvents.Add(new MIDIChordEvent(MIDIEvent.Type.ChordOn, ChordNotesNames, ChordNotesNumbers, ChordNotesVelocities, ChordTime, ChordLength));
        }

        return ChordOnEvents;
    }

    /// <summary>
    /// Returns all Chord On events of MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static List<MIDIChordEvent> MidiInputChordOffEvents(string midiFilePath)
    {
        // Create ChordOffEvents list
        List<MIDIChordEvent> ChordOffEvents = new List<MIDIChordEvent>();

        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Extract notes from .mid file
        TempoMap tempo = midiFile.GetTempoMap();
        List<Chord> chords = midiFile.GetChords().ToList();

        // Declare Note properties
        string ChordNotesNames;
        int[] ChordNotesNumbers;
        int[] ChordNotesVelocities;
        float ChordTime;
        float ChordLength;

        // Extract chords properties from chords list
        foreach (Chord chord in chords)
        {
            // Extract notes numbers and velocities
            Note[] chordNotes = chord.Notes.ToArray();
            int[] notes = new int[chord.Notes.Count()];
            int[] velocities = new int[chord.Notes.Count()];

            for (int i = 0; i < chord.Notes.Count(); i++)
            {
                notes[i] = chordNotes[i].NoteNumber;
                velocities[i] = chordNotes[i].OffVelocity;
            }

            ChordNotesNames = chord.ToString();
            ChordNotesNumbers = notes;
            ChordNotesVelocities = velocities;
            ChordTime = chord.EndTimeAs<MetricTimeSpan>(tempo).TotalMicroseconds * microSecToSec;
            ChordLength = chord.LengthAs<MetricTimeSpan>(tempo).TotalMicroseconds * microSecToSec;
            ChordOffEvents.Add(new MIDIChordEvent(MIDIEvent.Type.ChordOff, ChordNotesNames, ChordNotesNumbers, ChordNotesVelocities, ChordTime, ChordLength));
        }

        return ChordOffEvents;
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
