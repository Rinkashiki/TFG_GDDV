public abstract class MIDIEvent
{
    #region Class_Variables

    public enum Type {NoteOn, NoteOff, ChordOn, ChordOff};

    #endregion

    #region Other_Utility_Functions

    public abstract void PrintEvent();

    #endregion
}
