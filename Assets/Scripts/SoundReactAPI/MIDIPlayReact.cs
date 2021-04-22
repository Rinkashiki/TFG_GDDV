using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MIDIPlayReact : MonoBehaviour
{
    #region MIDI_Record_React_Variables

    private float lastVelocity;
    const float NOTES = 128.0f;

    #endregion

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

    public GameObject Play_NoteNumberInstantiate(Object obj, Vector3 position, Quaternion rotation, int noteNumber, float spawnTime)
    {
        GameObject go = null;

        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            int number = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber();

            GenericSoundReact.SoundInstantiate(obj, position, rotation, number == noteNumber, spawnTime);
        }

        return go;
    }

    #endregion

    #region MIDI_Play_Velocity_Input_Functions

    public void Play_VelocityTranslation(GameObject go, Vector3 axis, float translationFactor)

    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeTranslation(go, axis, translationFactor, new Numeric(velocity));
        }
        else
        {
            GenericSoundReact.ChangeTranslation(go, axis, translationFactor, new Numeric(0));
        }
    }

    public void Play_VelocityRotation(GameObject go, Vector3 axis, float rotFactor)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeRotation(go, axis, rotFactor, new Numeric(velocity));
        }
        else
        {
            GenericSoundReact.ChangeRotation(go, axis, rotFactor, new Numeric(0));
        }
    }

    public void Play_VelocityScale(GameObject go, Vector3 axis, float scaleFactor, float startScale)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeScale(go, axis, scaleFactor, startScale, new Numeric(velocity));
        }
        else
        {
            GenericSoundReact.ChangeScale(go, axis, scaleFactor, startScale, new Numeric(0));
        }
    }

    public void Play_VelocityBright(GameObject go, float brightFactor, Color startColor)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeBright(go, brightFactor, startColor, new Numeric(velocity));
        }
        else
        {
            GenericSoundReact.ChangeBright(go, brightFactor, startColor, new Numeric(0));
        }
    }

    public void Play_VelocityColor(GameObject go, Color color, float transitionTime, int velocityThreshold)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            if (MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() > velocityThreshold)
                GenericSoundReact.ChangeColor(go, color, transitionTime);
        }
    }

    public void Play_VelocityTerrainHeightMap(Mesh mesh, float noiseFactor, float heightFactor)
    {
        GenericSoundReact.ChangeTerrainHeightMap(mesh, noiseFactor, heightFactor, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityReliefMap(Mesh mesh, float noiseFactor, float reliefFactor, float waveSpeed, Vector3[] initPos, float fadeFactor = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
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

    public void Play_VelocityLightIntensity(Light light, float intensityFactor)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeLightIntensity(light, intensityFactor, new Numeric(velocity));
        }
        else
        {
            GenericSoundReact.ChangeLightIntensity(light, intensityFactor, new Numeric(0));
        }
    }

    public void Play_VelocityLightRange(Light light, float rangeFactor)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeLightRange(light, rangeFactor, new Numeric(velocity));
        }
        else
        {
            GenericSoundReact.ChangeLightRange(light, rangeFactor, new Numeric(0));
        } 
    }

    public void Play_VelocityShaderGraphMatProperty(Material mat, string propertyName, GenericSoundReact.MatPropertyType propertyType, float factor, float fadeFactor = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / 128.0f;
            GenericSoundReact.ChangeShaderGraphMatProperty(mat, propertyName, propertyType, factor, new Numeric(velocity));
        }
        else
        {
            float propertyIntensity = mat.GetFloat(propertyName);
            float propertyDelta = fadeFactor == 0 ? propertyIntensity : Time.deltaTime * propertyIntensity / fadeFactor;
            GenericSoundReact.ChangeShaderGraphMatProperty(mat, propertyName, propertyType, 1, new Numeric(propertyIntensity - propertyDelta));
        }
    }

    public void Play_VelocityAnimationSpeed(Animator anim, float factor)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeAnimationSpeed(anim, factor, new Numeric(velocity));
        }
        else
        {
            GenericSoundReact.ChangeAnimationSpeed(anim, factor, new Numeric(0));
        }
    }

    public void Play_VelocityBloom(Bloom bloom, float factor, float fadeFactor = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / 128.0f;
            //float dB = Mathf.Pow(40 * Mathf.Log10(velocity / 127), 4);
            GenericSoundReact.ChangeBloom(bloom, factor, new Numeric(velocity));
        }
        else
        {
            float bloomIntensity = bloom.intensity.value;
            float bloomDelta = fadeFactor == 0 ? bloomIntensity : Time.deltaTime * bloomIntensity / fadeFactor;
            GenericSoundReact.ChangeBloom(bloom, factor, new Numeric(bloom.intensity.value = bloomIntensity - bloomDelta));
        }
    }

    public void Play_VelocityChromaticAberration(ChromaticAberration ca, float factor)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeChromaticAberration(ca, factor, new Numeric(velocity));
        }
        else
        {
            GenericSoundReact.ChangeChromaticAberration(ca, factor, new Numeric(0));
        }
    }

    public void Play_VelocityPhysicProperty(Rigidbody body, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor, float initialValue = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
            GenericSoundReact.ChangePhysicProperty(body, fpp, fppFactor, initialValue, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityPhysicProperty(Rigidbody body, GenericSoundReact.VectorPhysicProperties vpp, Vector3 axis)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
            GenericSoundReact.ChangePhysicProperty(body, vpp, axis, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityPhysicProperty2D(Rigidbody2D body, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
            GenericSoundReact.ChangePhysicProperty2D(body, fpp, fppFactor, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityPhysicProperty2D(Rigidbody2D body, GenericSoundReact.VectorPhysicProperties vpp, Vector2 axis)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
            GenericSoundReact.ChangePhysicProperty2D(body, vpp, axis, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityAddForce(Rigidbody body, Vector3 forceDir, ForceMode mode, float forceFactor)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
            GenericSoundReact.CustomAddForce(body, forceDir, mode, forceFactor, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_VelocityAddForce2D(Rigidbody2D body, Vector2 forceDir, ForceMode2D mode, float forceFactor)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
            GenericSoundReact.CustomAddForce2D(body, forceDir, mode, forceFactor, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public GameObject Play_VelocityInstantiate(Object obj, Vector3 position, Quaternion rotation, float velocityThreshold, float spawnTime)
    {
        GameObject go = null;

        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            int velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity();

            go = GenericSoundReact.SoundInstantiate(obj, position, rotation, velocity > velocityThreshold, spawnTime);
        }
        return go;
    }

    public GameObject Play_VelocityDrawPolygon(Vector3[] polygonVert, Color lineColor, float lineWidth, float drawSpeedFactor)
    {
        GameObject polygon = GenericSoundReact.DrawPolygon(polygonVert, lineColor, lineWidth, drawSpeedFactor, GenericSoundReact.MusicDataType.Play_Velocity);
        return polygon;
    }

    #endregion
}
