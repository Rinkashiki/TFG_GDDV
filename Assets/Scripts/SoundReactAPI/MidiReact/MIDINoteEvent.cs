using UnityEngine;

public class MIDINoteEvent : MIDIEvent
{

    #region Note_Event_Properties

    private Type eventType;
    private string NoteName;
    private int NoteNumber;
    private int NoteVelocity;
    private float NoteTime;
    private float NoteLength;

    #endregion

    #region Note_Event_Constructors

    public MIDINoteEvent(Type type, string name, int number, int velocity, float time, float length)
    {
        eventType = type;
        NoteName = name;
        NoteNumber = number;
        NoteVelocity = velocity;
        NoteTime = time;
        NoteLength = length;
    }

    // Constructor for MIDIRecording. Can´t know at construction moment the note length
    public MIDINoteEvent(Type type, string name, int number, int velocity, float time)
    {
        eventType = type;
        NoteName = name;
        NoteNumber = number;
        NoteVelocity = velocity;
        NoteTime = time;
    }

    #endregion

    #region Note_Properties_Getters

    public string GetNoteName()
    {
        return NoteName;
    }

    public int GetNoteNumber()
    {
        return NoteNumber;
    }

    public int GetNoteVelocity()
    {
        return NoteVelocity;
    }

    public float GetNoteTime()
    {
        return NoteTime;
    }

    public float GetNoteLength()
    {
        return NoteLength;
    }

    #endregion

    #region Other_Utility_Functions

    public override void PrintEvent()
    {
        string eventToPrint = "";
        switch (eventType)
        {
            case Type.NoteOn:
                eventToPrint = "Note On -> ";
                break;

            case Type.NoteOff:
                eventToPrint = "Note Off -> ";
                break;
        }

        eventToPrint += $"NoteName: {NoteName}  NoteNumber: {NoteNumber}  NoteVelocity: {NoteVelocity}  NoteTime: {NoteTime}  NoteLength: {NoteLength}";
        Debug.Log(eventToPrint);
    }

    #endregion
}
