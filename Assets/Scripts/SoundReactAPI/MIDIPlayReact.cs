using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MIDIPlayReact : MonoBehaviour
{
    #region MIDI_Play_NoteNumber_Input_Functions

    public void Play_NoteNumberColor(GameObject go, Dictionary<int, Color> numberColorAssociation, float transitionTime)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            int number = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber();

            if (numberColorAssociation.ContainsKey(number))
            {
                GenericSoundReact.ChangeColor(go, numberColorAssociation[number], transitionTime);
            }

        }
    }

    public GameObject Play_NoteNumberInstantiate(Object obj, Vector3 position, Quaternion rotation, int noteNumber)
    {
        GameObject go = null;

        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            int number = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber();

            GenericSoundReact.SoundInstantiate(obj, position, rotation, number == noteNumber);
        }

        return go;
    }

    #endregion

    #region MIDI_Play_Velocity_Input_Functions

    public void Play_VelocityTranslation(GameObject go, Vector3 axis, float translationFactor)
    {
        GenericSoundReact.ChangeTranslation(go, axis, translationFactor, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityRotation(GameObject go, Vector3 axis, float rotFactor)
    {
        GenericSoundReact.ChangeRotation(go, axis, rotFactor, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityScale(GameObject go, Vector3 axis, float scaleFactor, float startScale)
    {
        GenericSoundReact.ChangeScale(go, axis, scaleFactor, startScale, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityBright(GameObject go, float brightFactor, float startBrightness)
    {
        GenericSoundReact.ChangeBright(go, brightFactor, startBrightness, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityColor(GameObject go, Color color, float transitionTime, float velocityThreshold)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() > velocityThreshold)
            GenericSoundReact.ChangeColor(go, color, transitionTime);
    }

    public void Play_VelocityTerrainHeightMap(Mesh mesh, float noiseFactor, float heightFactor)
    {
        GenericSoundReact.ChangeTerrainHeightMap(mesh, noiseFactor, heightFactor, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityHeightMap(Mesh mesh, float noiseFactor, float heightFactor, float waveSpeed, Vector3[] initPos)
    {
        GenericSoundReact.ChangeHeightMap(mesh, noiseFactor, heightFactor, initPos, waveSpeed, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityLightIntensity(Light light, float intensityFactor)
    {
        GenericSoundReact.ChangeLightIntensity(light, intensityFactor, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityLightRange(Light light, float rangeFactor)
    {
        GenericSoundReact.ChangeLightRange(light, rangeFactor, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityShaderGraphMatProperty(Material mat, string propertyName, GenericSoundReact.MatPropertyType propertyType, float factor)
    {
        GenericSoundReact.ChangeShaderGraphMatProperty(mat, propertyName, propertyType, factor, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityAnimationSpeed(Animator anim, float factor)
    {
        GenericSoundReact.ChangeAnimationSpeed(anim, factor, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityBloom(Bloom bloom, float factor)
    {
        GenericSoundReact.ChangeBloom(bloom, factor, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityChromaticAberration(ChromaticAberration ca, float factor)
    {
        GenericSoundReact.ChangeChromaticAberration(ca, factor, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityPhysicProperty(Rigidbody body, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor)
    {
        GenericSoundReact.ChangePhysicProperty(body, fpp, fppFactor, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityPhysicProperty(Rigidbody body, GenericSoundReact.VectorPhysicProperties vpp, Vector3 axis)
    {
        GenericSoundReact.ChangePhysicProperty(body, vpp, axis, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityPhysicProperty2D(Rigidbody2D body, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor)
    {
        GenericSoundReact.ChangePhysicProperty2D(body, fpp, fppFactor, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityPhysicProperty2D(Rigidbody2D body, GenericSoundReact.VectorPhysicProperties vpp, Vector2 axis)
    {
        GenericSoundReact.ChangePhysicProperty2D(body, vpp, axis, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityAddForce(Rigidbody body, Vector3 forceDir, ForceMode mode, float forceFactor)
    {
        GenericSoundReact.CustomAddForce(body, forceDir, mode, forceFactor, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityAddForce2D(Rigidbody2D body, Vector2 forceDir, ForceMode2D mode, float forceFactor)
    {
        GenericSoundReact.CustomAddForce2D(body, forceDir, mode, forceFactor, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public GameObject Play_VelocityInstantiate(Object obj, Vector3 position, Quaternion rotation, float velocityThreshold)
    {
        GameObject go = GenericSoundReact.SoundInstantiate(obj, position, rotation, MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() > velocityThreshold);
        return go;
    }

    public GameObject Play_VelocityDrawPolygon(Vector3[] polygonVert, Color lineColor, float lineWidth, float drawSpeedFactor)
    {
        GameObject polygon = GenericSoundReact.DrawPolygon(polygonVert, lineColor, lineWidth, drawSpeedFactor, GenericSoundReact.MusicDataType.Play_Velocity);
        return polygon;
    }

    #endregion
}
