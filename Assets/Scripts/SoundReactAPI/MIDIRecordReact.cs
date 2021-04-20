﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MIDIRecordReact : MonoBehaviour
{

    #region MIDI_Record_React_Variables

    private float lastVelocity;
    const float NOTES = 128.0f;

    #endregion


    #region MIDI_Record_NoteNumber_Input_Functions

    public void Record_NoteNumberColor(GameObject go, Dictionary<int, Color> numberColorAssociation, float transitionTime)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            int number = MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber();

            if (numberColorAssociation.ContainsKey(number))
            {
                GenericSoundReact.ChangeColor(go, numberColorAssociation[number], transitionTime);
            }

        }
    }

    public GameObject Record_NoteNumberInstantiate(Object obj, Vector3 position, Quaternion rotation, int noteNumber, float spawnTime)
    {
        GameObject go = null;

        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            int number = MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber();
            Debug.Log(number);

            go = GenericSoundReact.SoundInstantiate(obj, position, rotation, number == noteNumber, spawnTime);
        }
        return go;
    }

    public GameObject Record_KeyboardDrawPolygon(Dictionary<int, Vector2> numberDirAssociation, Color lineColor, float lineWidth, float drawSpeedFactor)
    {
        GameObject polygon = GenericSoundReact.KeyboardDrawPolygon(numberDirAssociation, lineColor, lineWidth, drawSpeedFactor);
        return polygon;
    }


    #endregion

    #region MIDI_Record_Velocity_Input_Functions

    public void Record_VelocityTranslation(GameObject go, Vector3 axis, float translationFactor, float fadeFactor = 0)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            float velocity = MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() * MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeTranslation(go, axis, translationFactor, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
            lastVelocity = velocity;
        } 
        else
        {
            float velocityDelta = fadeFactor == 0 ? lastVelocity : Time.deltaTime * lastVelocity * fadeFactor;
            lastVelocity = lastVelocity <= 0 ? 0 : lastVelocity - velocityDelta;
            GenericSoundReact.ChangeTranslation(go, axis, translationFactor, new Numeric(lastVelocity));
        }
    }

    public void Record_VelocityRotation(GameObject go, Vector3 axis, float rotFactor, float fadeFactor = 0)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            float velocity = MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() * MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeRotation(go, axis, rotFactor, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
            lastVelocity = velocity;
        }
        else
        {
            float velocityDelta = fadeFactor == 0 ? lastVelocity : Time.deltaTime * lastVelocity * fadeFactor;
            lastVelocity = lastVelocity <= 0 ? 0 : lastVelocity - velocityDelta;
            GenericSoundReact.ChangeRotation(go, axis, rotFactor, new Numeric(lastVelocity));
        }

    }

    public void Record_VelocityScale(GameObject go, Vector3 axis, float scaleFactor, float startScale, float fadeFactor = 0)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            float velocity = MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() * MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeScale(go, axis, scaleFactor, startScale, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
            lastVelocity = velocity;
        }
        else
        {
            float velocityDelta = fadeFactor == 0 ? lastVelocity : Time.deltaTime * lastVelocity * fadeFactor;
            lastVelocity = lastVelocity <= 0 ? 0 : lastVelocity - velocityDelta;
            GenericSoundReact.ChangeScale(go, axis, scaleFactor, startScale, new Numeric(lastVelocity));
        }
    }

    public void Record_VelocityBright(GameObject go, float brightFactor, Color startColor, float fadeFactor = 0)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            float velocity = MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() * MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeBright(go, brightFactor, startColor, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
            lastVelocity = velocity;
        }
        else
        {
            float velocityDelta = fadeFactor == 0 ? lastVelocity : Time.deltaTime * lastVelocity * fadeFactor;
            lastVelocity = lastVelocity <= 0 ? 0 : lastVelocity - velocityDelta;
            GenericSoundReact.ChangeBright(go, brightFactor, startColor, new Numeric(lastVelocity));
        }
    }

    public void Record_VelocityColor(GameObject go, Color color, float transitionTime, float velocityThreshold)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            if (MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() > velocityThreshold)
                GenericSoundReact.ChangeColor(go, color, transitionTime);
        }
    }

    public void Record_VelocityReliefMap(Mesh mesh, float noiseFactor, float reliefFactor, float waveSpeed, Vector3[] initPos, float fadeFactor = 0)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            float velocity = MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() * MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeReliefMap(mesh, noiseFactor, reliefFactor, initPos, waveSpeed, new Numeric(velocity));
            lastVelocity = velocity;
        }
        else
        {
            float velocityDelta = fadeFactor == 0 ? lastVelocity : Time.deltaTime * lastVelocity * fadeFactor;
            lastVelocity = lastVelocity <= 0 ? 0 : lastVelocity - velocityDelta;
            GenericSoundReact.ChangeReliefMap(mesh, noiseFactor, reliefFactor, initPos, waveSpeed, new Numeric(lastVelocity));
        }   
    }

    public void Record_VelocityLightIntensity(Light light, float intensityFactor)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.ChangeLightIntensity(light, intensityFactor, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
    }

    public void Record_VelocityLightRange(Light light, float rangeFactor)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.ChangeLightRange(light, rangeFactor, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
    }

    public void Record_VelocityShaderGraphMatProperty(Material mat, string propertyName, GenericSoundReact.MatPropertyType propertyType, float factor, float fadeFactor = 0)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            float velocity = MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() * MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeShaderGraphMatProperty(mat, propertyName, propertyType, factor, new Numeric(velocity));
        }
        else
        {
            float propertyIntensity = mat.GetFloat(propertyName);
            float propertyDelta = fadeFactor == 0 ? propertyIntensity : Time.deltaTime * propertyIntensity / fadeFactor;
            GenericSoundReact.ChangeShaderGraphMatProperty(mat, propertyName, propertyType, 1, new Numeric(propertyIntensity - propertyDelta));
        }
    }

    public void Record_VelocityAnimationSpeed(Animator anim, float factor)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.ChangeAnimationSpeed(anim, factor, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
    }

    public void Record_VelocityBloom(Bloom bloom, float factor, float fadeFactor = 0)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            float velocity = MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() * MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeBloom(bloom, factor, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
        }
        else
        {
            float bloomIntensity = bloom.intensity.value;
            float bloomDelta = fadeFactor == 0 ? bloomIntensity : Time.deltaTime * bloomIntensity / fadeFactor;
            GenericSoundReact.ChangeBloom(bloom, factor, new Numeric(bloom.intensity.value = bloomIntensity - bloomDelta));
        }    
    }

    public void Record_VelocityChromaticAberration(ChromaticAberration ca, float factor)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            GenericSoundReact.ChangeChromaticAberration(ca, factor, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
        }
        else
        {
            GenericSoundReact.ChangeChromaticAberration(ca, factor, new Numeric(0));
        }
    }

    public void Record_VelocityPhysicProperty(Rigidbody body, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor, float initialValue, float fadeFactor = 0)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            float velocity = MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() * MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangePhysicProperty(body, fpp, fppFactor, initialValue, new Numeric(velocity));
            lastVelocity = velocity;
        }
        else
        {
            float velocityDelta = fadeFactor == 0 ? lastVelocity : Time.deltaTime * lastVelocity * fadeFactor;
            lastVelocity = lastVelocity <= 0 ? 0 : lastVelocity - velocityDelta;
            GenericSoundReact.ChangePhysicProperty(body, fpp, fppFactor, initialValue, new Numeric(lastVelocity));
        }
    }

    public void Record_VelocityPhysicProperty(Rigidbody body, GenericSoundReact.VectorPhysicProperties vpp, Vector3 axis)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.ChangePhysicProperty(body, vpp, axis, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
    }

    public void Record_VelocityPhysicProperty2D(Rigidbody2D body, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.ChangePhysicProperty2D(body, fpp, fppFactor, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
    }

    public void Record_VelocityPhysicProperty2D(Rigidbody2D body, GenericSoundReact.VectorPhysicProperties vpp, Vector2 axis)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.ChangePhysicProperty2D(body, vpp, axis, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
    }

    public void Record_VelocityAddForce(Rigidbody body, Vector3 forceDir, ForceMode mode, float forceFactor)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.CustomAddForce(body, forceDir, mode, forceFactor, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
    }

    public void Record_VelocityAddForce2D(Rigidbody2D body, Vector2 forceDir, ForceMode2D mode, float forceFactor)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.CustomAddForce2D(body, forceDir, mode, forceFactor, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
    }

    public GameObject Record_VelocityInstantiate(Object obj, Vector3 position, Quaternion rotation, float velocityThreshold, float spawnTime)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            GameObject go = GenericSoundReact.SoundInstantiate(obj, position, rotation, MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() > velocityThreshold, spawnTime);
            return go;
        }
        else
        {
            return null;
        }
    }

    public GameObject Record_VelocityDrawPolygon(Vector3[] polygonVert, Color lineColor, float lineWidth, float drawSpeedFactor)
    {
        GameObject polygon = GenericSoundReact.DrawPolygon(polygonVert, lineColor, lineWidth, drawSpeedFactor, GenericSoundReact.MusicDataType.Record_Velocity);
        return polygon;
    }

    #endregion
}
