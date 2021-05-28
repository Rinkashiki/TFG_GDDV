using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhylloTunnel : MonoBehaviour
{
    #region PhylloTunnel_Variables

    [SerializeField] Transform cameraTransform;
    [SerializeField] Vector3 tunnelSpeed;
    [SerializeField] float cameraDistance;

    // Music Data Type
    private AudioInput audioInput;
    private GenericSoundReact.MusicDataType type;
    private int band;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("AudioInput"))
            audioInput = GameObject.FindGameObjectWithTag("AudioInput").GetComponent<AudioInput>();
    }

    // Update is called once per frame
    void Update()
    {
        // Select music value to draw the trail
        float value = 0;

        switch (type)
        {
            case GenericSoundReact.MusicDataType.Amplitude:
                value = audioInput.GetAmplitudeBuffer();
                break;

            case GenericSoundReact.MusicDataType.FreqBand:
                value = audioInput.GetBandBuffer(band);
                break;

            case GenericSoundReact.MusicDataType.MidiPlay:
                if (MidiPlayEventHandler.Event_CurrentNoteOn() != null) { }
                value = MIDIConst.ComputedB(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()) * MIDIConst.NOTES_TO_FREQ[MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber()] / MIDIConst.MAX_FREQ;
                break;

            case GenericSoundReact.MusicDataType.MidiRecord:
                if (MidiRecording.Event_CurrentNoteOn() != null)
                    value = MIDIConst.ComputedB(MidiRecording.Event_CurrentNoteOn().GetNoteVelocity()) * MIDIConst.NOTES_TO_FREQ[MidiRecording.Event_CurrentNoteOn().GetNoteNumber()] / MIDIConst.MAX_FREQ;
                break;

            default:
                break;
        }
        transform.position = new Vector3(transform.localPosition.x + value * tunnelSpeed.x, transform.localPosition.y + value * tunnelSpeed.y, transform.localPosition.z + value * tunnelSpeed.z);

        if (cameraTransform != null)
            cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, transform.position.z + cameraDistance);
    }

    public void SetParams(Vector3 tunnelSpeed, float cameraDistance = -10, Transform cameraTransform = null)
    {
        this.tunnelSpeed = tunnelSpeed;
        this.cameraDistance = cameraDistance;
        this.cameraTransform = cameraTransform;
    }

    public void SetFreqBand(int band)
    {
        this.band = band;
    }
}
