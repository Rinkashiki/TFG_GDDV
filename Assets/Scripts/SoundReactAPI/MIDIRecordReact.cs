using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MIDIRecordReact : MonoBehaviour
{
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

    public GameObject Record_NoteNumberInstantiate(Object obj, Vector3 position, Quaternion rotation, int noteNumber)
    {
        GameObject go = null;

        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            int number = MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber();

            go = GenericSoundReact.SoundInstantiate(obj, position, rotation, number == noteNumber);
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

    public void Record_VelocityTranslation(GameObject go, Vector3 axis, float translationFactor)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.ChangeTranslation(go, axis, translationFactor, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
    }

    public void Record_VelocityRotation(GameObject go, Vector3 axis, float rotFactor)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.ChangeRotation(go, axis, rotFactor, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
    }

    public void Record_VelocityScale(GameObject go, Vector3 axis, float scaleFactor, float startScale)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.ChangeScale(go, axis, scaleFactor, startScale, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
    }

    public void Record_VelocityBright(GameObject go, float brightFactor, float startBrightness)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.ChangeBright(go, brightFactor, startBrightness, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
    }

    public void Record_VelocityColor(GameObject go, Color color, float transitionTime, float velocityThreshold)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            if (MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() > velocityThreshold)
                GenericSoundReact.ChangeColor(go, color, transitionTime);
        }
    }

    public void Record_VelocityTerrainHeightMap(Mesh mesh, float noiseFactor, float heightFactor)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.ChangeTerrainHeightMap(mesh, noiseFactor, heightFactor, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
    }

    public void Record_VelocityHeightMap(Mesh mesh, float noiseFactor, float heightFactor, float waveSpeed, Vector3[] initPos)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.ChangeHeightMap(mesh, noiseFactor, heightFactor, initPos, waveSpeed, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
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

    public void Record_VelocityShaderGraphMatProperty(Material mat, string propertyName, GenericSoundReact.MatPropertyType propertyType, float factor)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.ChangeShaderGraphMatProperty(mat, propertyName, propertyType, factor, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
    }

    public void Record_VelocityAnimationSpeed(Animator anim, float factor)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.ChangeAnimationSpeed(anim, factor, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
    }

    public void Record_VelocityBloom(Bloom bloom, float factor)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.ChangeBloom(bloom, factor, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
    }

    public void Record_VelocityChromaticAberration(ChromaticAberration ca, float factor)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.ChangeChromaticAberration(ca, factor, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
    }

    public void Record_VelocityPhysicProperty(Rigidbody body, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.ChangePhysicProperty(body, fpp, fppFactor, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
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

    public GameObject Record_VelocityInstantiate(Object obj, Vector3 position, Quaternion rotation, float velocityThreshold)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            GameObject go = GenericSoundReact.SoundInstantiate(obj, position, rotation, MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() > velocityThreshold);
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
