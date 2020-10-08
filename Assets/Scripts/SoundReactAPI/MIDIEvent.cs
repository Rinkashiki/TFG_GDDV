using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIDIEvent<T>
{
    #region Class_Variables

    private List<T> listEvent;
    private T valueEvent;

    #endregion

    #region Constructors

    // MIDI List Event Constructor
    public MIDIEvent(List<T> listEvent)
    {
        this.listEvent = listEvent;
    }

    // MIDI Value Event Constructor
    public MIDIEvent(T valueEvent)
    {
        this.valueEvent = valueEvent;
    }

    #endregion

    #region MIDI_Events_Getters

    // MIDI List Event Getter
    public List<T> GetListEvent()
    {
        return listEvent;
    }

    // MIDI Value Event Getter
    public T GetValueEvent()
    {
        return valueEvent;
    }

    #endregion
}
