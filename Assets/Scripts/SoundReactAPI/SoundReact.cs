﻿using System.Collections;
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

    public void AmplitudePhysicProperty(Rigidbody body, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor)
    {
        GenericSoundReact.ChangePhysicProperty(body, fpp, fppFactor, new Numeric(audioInput.GetAmplitudeBuffer()));
    }

    public void AmplitudePhysicProperty2D(Rigidbody2D body, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor)
    {
        GenericSoundReact.ChangePhysicProperty2D(body, fpp, fppFactor, new Numeric(audioInput.GetAmplitudeBuffer()));
    }

    public void AmplitudeAddForce(Rigidbody body, Vector3 forceDir, ForceMode mode, float forceFactor)
    {
        GenericSoundReact.CustomAddForce(body, forceDir, mode, forceFactor, new Numeric(audioInput.GetAmplitudeBuffer()));

    }

    public void AmplitudeAddForce2D(Rigidbody2D body, Vector2 forceDir, ForceMode2D mode, float forceFactor)
    {
        GenericSoundReact.CustomAddForce2D(body, forceDir, mode, forceFactor, new Numeric(audioInput.GetAmplitudeBuffer()));

    }



    /*
    public float AmplitudeGenerateTerrainLine(Mesh mesh, int length, float currentWidth, float step, float heightFactor)
    {
        currentWidth = GenericSoundReact.CreateTerrainLineAmplitude(mesh, length, currentWidth, step, heightFactor, new Numeric(audioInput.GetAmplitudeBuffer()));
        return currentWidth;
    }
    */

    public void AmplitudeLightIntensity(Light light, float intensityFactor)
    {

        GenericSoundReact.ChangeLightIntensity(light, intensityFactor, new Numeric(audioInput.GetAmplitudeBuffer()));
    }

    public void AmplitudeLightRange(Light light, float rangeFactor)
    {

        GenericSoundReact.ChangeLightRange(light, rangeFactor, new Numeric(audioInput.GetAmplitudeBuffer()));
    }

    public GameObject AmplitudeDrawPolygon(Vector3[] polygonVert, Color lineColor, float lineWidth, float drawSpeedFactor)
    {
        GameObject polygon = GenericSoundReact.DrawPolygon(polygonVert, lineColor, lineWidth, drawSpeedFactor, GenericSoundReact.MusicDataType.Amplitude);
        return polygon;
    }

    public GameObject NoteNumberDrawPolygon(Dictionary<int, Vector2> numberDirAssociation, Color lineColor, float lineWidth, float drawSpeedFactor)
    {
        GameObject polygon = GenericSoundReact.KeyboardDrawPolygon(numberDirAssociation, lineColor, lineWidth, drawSpeedFactor);
        return polygon;
    }

    public GameObject BandsGenerateTerrain(int length, float startWidth, float step, float heightfactor, float noiseFactor)
    {
        GameObject terrain = GenericSoundReact.GenerateTerrain(length, startWidth, step, heightfactor, noiseFactor);
        return terrain;
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

    public void BandPhysicProperty(Rigidbody body, int band, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor)
    {
        GenericSoundReact.ChangePhysicProperty(body, fpp, fppFactor, new Numeric(audioInput.GetBandBuffer(band)));
    }

    public void BandPhysicProperty2D(Rigidbody2D body, int band, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor)
    {
        GenericSoundReact.ChangePhysicProperty2D(body, fpp, fppFactor, new Numeric(audioInput.GetBandBuffer(band)));
    }

    public void BandAddForce(Rigidbody body, int band, Vector3 forceDir, ForceMode mode, float forceFactor)
    {
        GenericSoundReact.CustomAddForce(body, forceDir, mode, forceFactor, new Numeric(audioInput.GetBandBuffer(band)));

    }

    public void BandAddForce2D(Rigidbody2D body, int band, Vector2 forceDir, ForceMode2D mode, float forceFactor)
    {
        GenericSoundReact.CustomAddForce2D(body, forceDir, mode, forceFactor, new Numeric(audioInput.GetBandBuffer(band)));

    }

    /*
    public float BandGenerateTerrainLine(Mesh mesh, int length, float currentWidth, float step, float heightFactor, float noiseFactor)
    {
        currentWidth = GenericSoundReact.CreateTerrainLineBands(mesh, length, currentWidth, step, heightFactor, noiseFactor, audioInput.GetBandBuffer());
        return currentWidth;
    }
    */

    #endregion

    #region MIDI_Functions

    public void Play_NoteNumberColor(GameObject go, Dictionary<int, Color> numberColorAssociation)
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

    public void Play_NoteNumberInstantiate(Object obj, Vector3 position, Quaternion rotation, int noteNumber)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            int number = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber();

            GenericSoundReact.SoundInstantiate(obj, position, rotation, number == noteNumber);
        }
    }

    public void Play_NoteNumberPhysicProperty(Rigidbody body, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            int number = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber();

            GenericSoundReact.ChangePhysicProperty(body, fpp,fppFactor, new Numeric(number));

        }
    }

    public void Play_NoteNumberPhysicProperty2D(Rigidbody2D body, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            int number = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber();

            GenericSoundReact.ChangePhysicProperty2D(body, fpp, fppFactor, new Numeric(number));

        }
    }

    public void Play_NoteNumberAddForce(Rigidbody body, Vector3 forceDir, ForceMode mode, float forceFactor)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            int number = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber();

            GenericSoundReact.CustomAddForce(body, forceDir, mode, forceFactor, new Numeric(number));

        }
    }

    public void Play_NoteNumberAddForce2D(Rigidbody2D body, Vector2 forceDir, ForceMode2D mode, float forceFactor)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            int number = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber();

            GenericSoundReact.CustomAddForce2D(body, forceDir, mode, forceFactor, new Numeric(number));

        }
    }

    public void Record_NoteNumberPhysicProperty(Rigidbody body, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            int number = MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber();

            GenericSoundReact.ChangePhysicProperty(body, fpp, fppFactor, new Numeric(number));

        }
    }

    public void Record_NoteNumberPhysicProperty2D(Rigidbody2D body, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            int number = MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber();

            GenericSoundReact.ChangePhysicProperty2D(body, fpp, fppFactor, new Numeric(number));

        }
    }

    public void Record_NoteNumberAddForce(Rigidbody body, Vector3 forceDir, ForceMode mode, float forceFactor)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            int number = MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber();

            GenericSoundReact.CustomAddForce(body, forceDir, mode, forceFactor, new Numeric(number));

        }
    }

    public void Record_NoteNumberAddForce2D(Rigidbody2D body, Vector2 forceDir, ForceMode2D mode, float forceFactor)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            int number = MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber();

            GenericSoundReact.CustomAddForce2D(body, forceDir, mode, forceFactor, new Numeric(number));

        }
    }

    #endregion
}
