using UnityEngine;

public class MIDIChordEvent : MIDIEvent
{
    #region Chord_Event_Properties

    private Type eventType;
    private string ChordNotesNames;
    private int[] ChordNotesNumbers;
    private int[] ChordNotesVelocities;
    private float ChordTime;
    private float ChordLength;

    #endregion

    #region Chord_Event_Constructor

    public MIDIChordEvent(Type type, string names, int[] numbers, int[] velocities, float time, float length)
    {
        eventType = type;
        ChordNotesNames = names;
        ChordNotesNumbers = numbers;
        ChordNotesVelocities = velocities;
        ChordTime = time;
        ChordLength = length;
    }

    #endregion

    #region Chord_Properties_Getters

    public string GetChordNotesNames()
    {
        return ChordNotesNames;
    }

    public int[] GetChordNotesNumbers()
    {
        return ChordNotesNumbers;
    }

    public int[] GetChordNotesVelocities()
    {
        return ChordNotesVelocities;
    }

    public float GetChordTime()
    {
        return ChordTime;
    }

    public float GetChordLength()
    {
        return ChordLength;
    }

    #endregion

    #region Other_Utility_Functions

    public override void PrintEvent()
    {
        string eventToPrint = "";
        switch (eventType)
        {
            case Type.ChordOn:
                eventToPrint = "Chord On -> ";
                break;

            case Type.ChordOff:
                eventToPrint = "Chord Off -> ";
                break;
        }

        string numbersString = string.Join(" ", ChordNotesNumbers);
        string velocitiesString = string.Join(" ", ChordNotesVelocities);

        eventToPrint += $"ChordNotesNames: {ChordNotesNames}  ChordNotesNumbers: {numbersString}  ChordNotesVelocities: {velocitiesString}  ChordTime: {ChordTime}  ChordLength: {ChordLength}";
        Debug.Log(eventToPrint);
    }

    #endregion

}
