using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReact : MonoBehaviour
{
    #region Sound_React_Variables

    public AudioInput audioInput;

    #endregion

    #region Audio_Functions

    public void AmplitudeScale(GameObject go, Vector3 axis, float scaleFactor, float startScale)
    {
        GenericSoundReact.ChangeScale(go, axis, scaleFactor, startScale, new Numeric(audioInput.GetAmplitudeBuffer()));
    }

    public void AmplitudeRotate(GameObject go, Vector3 axis, float rotFactor)
    {
        GenericSoundReact.ChangeRotation(go, axis, rotFactor, new Numeric(audioInput.GetAmplitudeBuffer()));
    }

    public void AmplitudeBright(GameObject go, float brightFactor, float startBrightness)
    {
        GenericSoundReact.ChangeBright(go, brightFactor, startBrightness, new Numeric(audioInput.GetAmplitudeBuffer()));
    }

    public void AmplitudeTerrainHeightMap(Mesh mesh, float noiseFactor, float heightFactor)
    {
        GenericSoundReact.ChangeTerrainHeightMap(mesh, noiseFactor, heightFactor, new Numeric(audioInput.GetAmplitudeBuffer()));
    }

    public void BandScale(GameObject go, int band, Vector3 axis, float scaleFactor, float startScale)
    {
        GenericSoundReact.ChangeScale(go, axis, scaleFactor, startScale, new Numeric(audioInput.GetBandBuffer(band)));
    }

    public void BandRotate(GameObject go, int band, Vector3 axis, float rotFactor)
    {
        GenericSoundReact.ChangeRotation(go, axis, rotFactor, new Numeric(audioInput.GetBandBuffer(band)));
    }

    public void BandBright(GameObject go, int band, float brightFactor, float startBrightness)
    {
        GenericSoundReact.ChangeBright(go, brightFactor, startBrightness, new Numeric(audioInput.GetBandBuffer(band)));
    }

    #endregion

    #region MIDI_Functions

    public void NoteNumberColor(GameObject go, Dictionary<int, Color> numberColorAssociation)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            int number = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber();

            if (numberColorAssociation.ContainsKey(number))
            {
                go.GetComponent<MeshRenderer>().material.color = numberColorAssociation[number];
            }

        }
    }

    #endregion
}
