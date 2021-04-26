using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidiPlayReactExample : MonoBehaviour
{

    [SerializeField] Object midiFile;

    MIDIPlayReact playReact;

    List<MIDINoteEvent> events;

    // Start is called before the first frame update
    void Start()
    {
        playReact = GetComponent<MIDIPlayReact>();

        //Playback callbacks for start playing
        /*
        MidiPlayEventHandler.PlaybackSetUp(midiFile);
        MidiPlayEventHandler.StartPlayback();
        */

        events = MidiFileEventHandler.Event_NoteOnListDistinct(midiFile);
        foreach(MIDINoteEvent e in events)
        {
            Debug.Log(e.GetNoteNumber());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        //Playback callbacks for stop playing
        /*
        MidiPlayEventHandler.StopPlayback();
        MidiPlayEventHandler.ReleasePlaybackResources();
        */
    }
}
