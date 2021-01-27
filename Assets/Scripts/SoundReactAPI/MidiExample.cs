using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MidiExample : MonoBehaviour
{
    private SoundReact soundReact;
    [SerializeField] private Object midiFile;

    List<int> noteNumbers;
    List<MIDINoteEvent> notes;

    int[] notesColor = new int[3];
    Color[] colors = { new Color(255,0,0), new Color(0,255,0), new Color(0,0,255)};
    Dictionary<int, Color> numberColorAssociation = new Dictionary<int, Color>();

    // Start is called before the first frame update
    void Start()
    {
        MidiPlayEventHandler.PlaybackSetUp(midiFile);

        soundReact = GetComponent<SoundReact>();
        noteNumbers = new List<int>();
        
        notes = MidiFileEventHandler.Event_NoteOnList(midiFile);
        foreach (MIDINoteEvent note in notes)
        {
            noteNumbers.Add(note.GetNoteNumber());
        }

         for (int i = 0; i < notesColor.Length; i++)
        {
            notesColor[i] = noteNumbers.GroupBy(s => s).OrderByDescending(s => s.Count()).Skip(i).First().Key;
            Debug.Log(notesColor[i]);
            numberColorAssociation.Add(notesColor[i], colors[i]);
        }


    }

    // Update is called once per frame
    void Update()
    {
        soundReact.Play_NoteNumberColor(this.gameObject, numberColorAssociation);

        if (Input.GetKeyDown(KeyCode.A))
        {
            MidiPlayEventHandler.StartPlayback();
            Debug.Log("Start playing MIDI");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MidiPlayEventHandler.StopPlayback();
            Debug.Log("Stop playing MIDI");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MidiPlayEventHandler.ReleasePlaybackResources();
            Debug.Log("Release MIDI");
        }
    }
}
