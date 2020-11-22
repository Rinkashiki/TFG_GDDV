#region Dependencies 

using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

public class MidiFileInput
{
    #region Units

    private static float microSecToSec = 0.000001f;

    #endregion

    #region MIDI_Notes_Inputs

    /// <summary>
    /// Returns all Note On events from one track MIDI file stored in <paramref name="midiFilePath"/>
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

        // Build note events from notes list
        foreach (Note note in notes)
        {
            NoteOnEvents.Add(BuildNoteEvent(MIDIEvent.Type.NoteOn, note, tempo));
        }

        return NoteOnEvents;
    }

    /// <summary>
    /// Returns all Note On events from multiple track MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static List<MIDINoteEvent>[] MidiInputNoteOnEventsTracks(string midiFilePath)
    {
        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Extract notes from .mid file
        TempoMap tempo = midiFile.GetTempoMap();
        List<Note> notes = midiFile.GetNotes().ToList();

        // Extract tracks collection
        List<TrackChunk> midiTracks = midiFile.GetTrackChunks().ToList();
        List<TrackChunk> tracks = new List<TrackChunk>();

        // Initialize each list
        foreach (TrackChunk track in midiTracks)
        {
            if(track.Events.Any(x => x is NoteEvent))
            {
                tracks.Add(track);
            }
        }

        // Declare Create NoteOnEvents collection track
        List<MIDINoteEvent>[] NoteOnEvents = new List<MIDINoteEvent>[tracks.Count()];

        // Build note events from notes list
        for (int i = 0; i < tracks.Count(); i++)
        {
            NoteOnEvents[i] = new List<MIDINoteEvent>();

            foreach (Note note in notes)
            {
                NoteOnEvents[i].Add(BuildNoteEvent(MIDIEvent.Type.NoteOn, note, tempo));
            }
        }
        return NoteOnEvents;
    }

    /// <summary>
    /// Returns all Note Off events from one track MIDI file stored in <paramref name="midiFilePath"/>
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

        // Build note events from notes list
        foreach (Note note in notes)
        {
            NoteOffEvents.Add(BuildNoteEvent(MIDIEvent.Type.NoteOff, note, tempo));
        }

        return NoteOffEvents;
    }

    /// <summary>
    /// Returns all Note Off events from multiple track MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static List<MIDINoteEvent>[] MidiInputNoteOffEventsTracks(string midiFilePath)
    {
        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Extract notes from .mid file
        TempoMap tempo = midiFile.GetTempoMap();
        List<Note> notes = midiFile.GetNotes().ToList();

        // Create NoteOffEvents collection
        List<FourBitNumber> channels = midiFile.GetChannels().ToList();
        List<MIDINoteEvent>[] NoteOffEvents = new List<MIDINoteEvent>[channels.Count()];

        // Initialize each list
        for (int i = 0; i < NoteOffEvents.Length; i++)
        {
            NoteOffEvents[i] = new List<MIDINoteEvent>();
        }

        // Declare Note track
        int NoteTrack;;

        // Build note events from notes list
        foreach (Note note in notes)
        {
            NoteTrack = channels.IndexOf(note.Channel);
            NoteOffEvents[NoteTrack].Add(BuildNoteEvent(MIDIEvent.Type.NoteOff, note, tempo));
        }

        return NoteOffEvents;
    }

    #endregion

    #region MIDI_Chords_Inputs

    /// <summary>
    /// Returns all Chord On events from one track MIDI file stored in <paramref name="midiFilePath"/>
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

        // Build chord events from chords list
        foreach (Chord chord in chords)
        {
            ChordOnEvents.Add(BuildChordEvent(MIDIEvent.Type.ChordOn, chord, tempo));
        }

        return ChordOnEvents;
    }

    /// <summary>
    /// Returns all Chord On events from multiple track MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static List<MIDIChordEvent>[] MidiInputChordOnEventsTracks(string midiFilePath)
    {
        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Extract chords from .mid file
        TempoMap tempo = midiFile.GetTempoMap();
        List<Chord> chords = midiFile.GetChords().ToList();

        // Create ChordOnEvents collection
        List<FourBitNumber> channels = midiFile.GetChannels().ToList();
        List<MIDIChordEvent>[] ChordOnEvents = new List<MIDIChordEvent>[channels.Count()];

        // Initialize each list
        for (int i = 0; i < ChordOnEvents.Length; i++)
        {
            ChordOnEvents[i] = new List<MIDIChordEvent>();
        }

        // Declare Chord track
        int ChordTrack;

        // Build chord events from chords list
        foreach (Chord chord in chords)
        {
            ChordTrack = channels.IndexOf(chord.Channel);
            ChordOnEvents[ChordTrack].Add(BuildChordEvent(MIDIEvent.Type.ChordOn, chord, tempo));
        }

        return ChordOnEvents;
    }

    /// <summary>
    /// Returns all Chord Off events from one track MIDI file stored in <paramref name="midiFilePath"/>
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

        // Build chord events from chords list
        foreach (Chord chord in chords)
        {
            ChordOffEvents.Add(BuildChordEvent(MIDIEvent.Type.ChordOff, chord, tempo));
        }

        return ChordOffEvents;
    }

    /// <summary>
    /// Returns all Chord Off events from multiple track MIDI file stored in <paramref name="midiFilePath"/>
    /// </summary>
    /// <param name="midiFilePath"></param>
    /// <returns></returns>
    public static List<MIDIChordEvent>[] MidiInputChordOffEventsTracks(string midiFilePath)
    {
        // Read MIDI file
        var midiFile = MidiFile.Read(midiFilePath);

        // Extract chords from .mid file
        TempoMap tempo = midiFile.GetTempoMap();
        List<Chord> chords = midiFile.GetChords().ToList();

        // Create ChordOnEvents collection
        List<FourBitNumber> channels = midiFile.GetChannels().ToList();
        List<MIDIChordEvent>[] ChordOffEvents = new List<MIDIChordEvent>[channels.Count()];

        // Initialize each list
        for (int i = 0; i < ChordOffEvents.Length; i++)
        {
            ChordOffEvents[i] = new List<MIDIChordEvent>();
        }

        // Declare Chord track
        int ChordTrack;

        // Build chord events from chords list
        foreach (Chord chord in chords)
        {
            ChordTrack = channels.IndexOf(chord.Channel);
            ChordOffEvents[ChordTrack].Add(BuildChordEvent(MIDIEvent.Type.ChordOff, chord, tempo));
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

    #region Other_Utility_Functions

    private static MIDINoteEvent BuildNoteEvent(MIDIEvent.Type type, Note note, TempoMap tempo)
    {
        string NoteName = note.NoteName.ToString() + note.Octave;
        NoteName = NoteName.Contains("Sharp") ? NoteName.Replace("Sharp", "#") : NoteName;
        int NoteNumber = note.NoteNumber;
        int NoteVelocity;
        float NoteTime;
        if (type == MIDIEvent.Type.NoteOn)
        {
            NoteVelocity = note.Velocity;
            NoteTime = note.TimeAs<MetricTimeSpan>(tempo).TotalMicroseconds * microSecToSec;
        }
        else
        {
            NoteVelocity = note.OffVelocity;
            NoteTime = note.EndTimeAs<MetricTimeSpan>(tempo).TotalMicroseconds * microSecToSec;
        }
        float NoteLength = note.LengthAs<MetricTimeSpan>(tempo).TotalMicroseconds * microSecToSec;

        MIDINoteEvent noteEvent = new MIDINoteEvent(type, NoteName, NoteNumber, NoteVelocity, NoteTime, NoteLength);
        return noteEvent;
    }

    private static MIDIChordEvent BuildChordEvent(MIDIEvent.Type type, Chord chord, TempoMap tempo)
    {
        // Extract notes numbers and velocities
        Note[] chordNotes = chord.Notes.ToArray();
        int[] notes = new int[chord.Notes.Count()];
        int[] velocities = new int[chord.Notes.Count()];

        for (int i = 0; i < chord.Notes.Count(); i++)
        {
            notes[i] = chordNotes[i].NoteNumber;
            if (type == MIDIEvent.Type.ChordOn)
            {
                velocities[i] = chordNotes[i].Velocity;
            }
            else
            {
                velocities[i] = chordNotes[i].OffVelocity;
            }
        }

        string ChordNotesNames = chord.ToString();
        int[] ChordNotesNumbers = notes;
        int[] ChordNotesVelocities = velocities;
        float ChordTime;
        if (type == MIDIEvent.Type.ChordOn)
        {
            ChordTime = chord.TimeAs<MetricTimeSpan>(tempo).TotalMicroseconds * microSecToSec;
        }
        else
        {
            ChordTime = chord.EndTimeAs<MetricTimeSpan>(tempo).TotalMicroseconds * microSecToSec;
        }
        float ChordLength = chord.LengthAs<MetricTimeSpan>(tempo).TotalMicroseconds * microSecToSec;

        MIDIChordEvent chordEvent = new MIDIChordEvent(type, ChordNotesNames, ChordNotesNumbers, ChordNotesVelocities, ChordTime, ChordLength);
        return chordEvent;
    }

    #endregion
}
