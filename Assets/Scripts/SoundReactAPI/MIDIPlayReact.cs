﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MIDIPlayReact : MonoBehaviour
{
    #region MIDI_Play_React_Variables

    private float lastVelocity;
    const float NOTES = 128.0f;

    #endregion

    #region MIDI_Play_Velocity_Input_Functions

    public void Play_Translation(GameObject go, Vector3 axis, float translationFactor, float fadeFactor = 0)

    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeTranslation(go, axis, translationFactor, new Numeric(velocity));
            lastVelocity = velocity;
        }
        else
        {
            float velocityDelta = fadeFactor == 0 ? lastVelocity : Time.deltaTime * lastVelocity * fadeFactor;
            lastVelocity = lastVelocity <= 0 ? 0 : lastVelocity - velocityDelta;
            GenericSoundReact.ChangeTranslation(go, axis, translationFactor, new Numeric(lastVelocity));
        }
    }

    public void Play_Rotation(GameObject go, Vector3 axis, float rotFactor, float fadeFactor = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeRotation(go, axis, rotFactor, new Numeric(velocity));
            lastVelocity = velocity;
        }
        else
        {
            float velocityDelta = fadeFactor == 0 ? lastVelocity : Time.deltaTime * lastVelocity * fadeFactor;
            lastVelocity = lastVelocity <= 0 ? 0 : lastVelocity - velocityDelta;
            GenericSoundReact.ChangeRotation(go, axis, rotFactor, new Numeric(lastVelocity));
        }
    }

    public void Play_Scale(GameObject go, Vector3 axis, float scaleFactor, float initialScale = 1, float fadeFactor = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeScale(go, axis, scaleFactor, new Numeric(velocity), initialScale);
            lastVelocity = velocity;
        }
        else
        {
            float velocityDelta = fadeFactor == 0 ? lastVelocity : Time.deltaTime * lastVelocity * fadeFactor;
            lastVelocity = lastVelocity <= 0 ? 0 : lastVelocity - velocityDelta;
            GenericSoundReact.ChangeScale(go, axis, scaleFactor, new Numeric(lastVelocity), initialScale);
        }
    }

    public void Play_Bright(GameObject go, float brightFactor, Color initialColor, float fadeFactor = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeBright(go, brightFactor, initialColor, new Numeric(velocity));
            lastVelocity = velocity;
        }
        else
        {
            float velocityDelta = fadeFactor == 0 ? lastVelocity : Time.deltaTime * lastVelocity * fadeFactor;
            lastVelocity = lastVelocity <= 0 ? 0 : lastVelocity - velocityDelta;
            GenericSoundReact.ChangeBright(go, brightFactor, initialColor, new Numeric(lastVelocity));
        }
    }

    public void Play_Color(GameObject go, Dictionary<int, Color> numberColorAssociation, float transitionTime)
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

    public void Play_ReliefMap(Mesh mesh, float noiseFactor, float reliefFactor, float waveSpeed, Vector3[] initPos, float fadeFactor = 0)
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

    public void Play_LightIntensity(Light light, float intensityFactor, float initialIntensity = 1, float fadeFactor = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeLightIntensity(light, intensityFactor, new Numeric(velocity), initialIntensity);
            lastVelocity = velocity;
        }
        else
        {
            float velocityDelta = fadeFactor == 0 ? lastVelocity : Time.deltaTime * lastVelocity * fadeFactor;
            lastVelocity = lastVelocity <= 0 ? 0 : lastVelocity - velocityDelta;
            GenericSoundReact.ChangeLightIntensity(light, intensityFactor, new Numeric(lastVelocity), initialIntensity);
        }
    }

    public void Play_LightRange(Light light, float rangeFactor, float initialRange = 1, float fadeFactor = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeLightRange(light, rangeFactor, new Numeric(velocity), initialRange);
            lastVelocity = velocity;
        }
        else
        {
            float velocityDelta = fadeFactor == 0 ? lastVelocity : Time.deltaTime * lastVelocity * fadeFactor;
            lastVelocity = lastVelocity <= 0 ? 0 : lastVelocity - velocityDelta;
            GenericSoundReact.ChangeLightRange(light, rangeFactor, new Numeric(lastVelocity), initialRange);
        } 
    }

    public void Play_ShaderGraphMatProperty(Material mat, string propertyName, GenericSoundReact.MatPropertyType propertyType, float propertyFactor, float fadeFactor = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / 128.0f;
            GenericSoundReact.ChangeShaderGraphMatProperty(mat, propertyName, propertyType, propertyFactor, new Numeric(velocity));
        }
        else
        {
            float propertyIntensity = mat.GetFloat(propertyName);
            float propertyDelta = fadeFactor == 0 ? propertyIntensity : Time.deltaTime * propertyIntensity / fadeFactor;
            GenericSoundReact.ChangeShaderGraphMatProperty(mat, propertyName, propertyType, 1, new Numeric(propertyIntensity - propertyDelta));
        }
    }

    public void Play_AnimationSpeed(Animator anim, float speedFactor, float initialSpeed = 1, float fadeFactor = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeAnimationSpeed(anim, speedFactor, new Numeric(velocity), initialSpeed);
            lastVelocity = velocity;
        }
        else
        {
            float velocityDelta = fadeFactor == 0 ? lastVelocity : Time.deltaTime * lastVelocity * fadeFactor;
            lastVelocity = lastVelocity <= 0 ? 0 : lastVelocity - velocityDelta;
            GenericSoundReact.ChangeAnimationSpeed(anim, speedFactor, new Numeric(lastVelocity), initialSpeed);
        }
    }

    public void Play_Bloom(Bloom bloom, float bloomFactor, float initialBloom = 0, float fadeFactor = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / 128.0f;
            //float dB = Mathf.Pow(40 * Mathf.Log10(velocity / 127), 4);
            GenericSoundReact.ChangeBloom(bloom, bloomFactor, new Numeric(velocity), initialBloom);
        }
        else
        {
            float bloomIntensity = bloom.intensity.value;
            float bloomDelta = fadeFactor == 0 ? bloomIntensity : Time.deltaTime * bloomIntensity / fadeFactor;
            GenericSoundReact.ChangeBloom(bloom, bloomFactor, new Numeric(bloom.intensity.value = bloomIntensity - bloomDelta), initialBloom);
        }
    }

    public void Play_ChromaticAberration(ChromaticAberration ca, float caFactor, float initialCA = 0, float fadeFactor = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeChromaticAberration(ca, caFactor, new Numeric(velocity), initialCA);
            lastVelocity = velocity;
        }
        else

        {
            float caIntensity = ca.intensity.value;
            float caDelta = fadeFactor == 0 ? caIntensity : Time.deltaTime * caIntensity / fadeFactor;
            GenericSoundReact.ChangeChromaticAberration(ca, caFactor, new Numeric(ca.intensity.value = caIntensity - caDelta), initialCA);
        }
    }

    public void Play_Vignette(Vignette vignette, float vignetteFactor, float initialVignette = 0, float fadeFactor = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeVignette(vignette, vignetteFactor, new Numeric(velocity), initialVignette);
            lastVelocity = velocity;
        }
        else

        {
            float vignetteIntensity = vignette.intensity.value;
            float caDelta = fadeFactor == 0 ? vignetteIntensity : Time.deltaTime * vignetteIntensity / fadeFactor;
            GenericSoundReact.ChangeVignette(vignette, vignetteFactor, new Numeric(vignette.intensity.value = vignetteIntensity - caDelta), initialVignette);
        }
    }

    public void Play_PhysicProperty(Rigidbody body, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor, float initialValue = 0, float fadeFactor = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangePhysicProperty(body, fpp, fppFactor, new Numeric(velocity), initialValue);
            lastVelocity = velocity;
        }
        else

        {
            float velocityDelta = fadeFactor == 0 ? lastVelocity : Time.deltaTime * lastVelocity * fadeFactor;
            lastVelocity = lastVelocity <= 0 ? 0 : lastVelocity - velocityDelta;
            GenericSoundReact.ChangePhysicProperty(body, fpp, fppFactor, new Numeric(lastVelocity), initialValue);
        }   
    }

    public void Play_PhysicProperty(Rigidbody body, GenericSoundReact.VectorPhysicProperties vpp, Vector3 vppFactor, Vector3 initialValue = new Vector3(), float fadeFactor = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangePhysicProperty(body, vpp, vppFactor, new Numeric(velocity), initialValue);
            lastVelocity = velocity;
        }
        else

        {
            float velocityDelta = fadeFactor == 0 ? lastVelocity : Time.deltaTime * lastVelocity * fadeFactor;
            lastVelocity = lastVelocity <= 0 ? 0 : lastVelocity - velocityDelta;
            GenericSoundReact.ChangePhysicProperty(body, vpp, vppFactor, new Numeric(lastVelocity), initialValue);
        }   
    }

    public void Play_PhysicProperty2D(Rigidbody2D body, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor, float initialValue = 0, float fadeFactor = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangePhysicProperty2D(body, fpp, fppFactor, new Numeric(velocity), initialValue);
            lastVelocity = velocity;
        }
        else

        {
            float velocityDelta = fadeFactor == 0 ? lastVelocity : Time.deltaTime * lastVelocity * fadeFactor;
            lastVelocity = lastVelocity <= 0 ? 0 : lastVelocity - velocityDelta;
            GenericSoundReact.ChangePhysicProperty2D(body, fpp, fppFactor, new Numeric(lastVelocity), initialValue);
        }   
    }

    public void Play_PhysicProperty2D(Rigidbody2D body, GenericSoundReact.VectorPhysicProperties vpp, Vector2 vppFactor, Vector2 initialValue = new Vector2(), float fadeFactor = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangePhysicProperty2D(body, vpp, vppFactor, new Numeric(velocity), initialValue);
            lastVelocity = velocity;
        }
        else

        {
            float velocityDelta = fadeFactor == 0 ? lastVelocity : Time.deltaTime * lastVelocity * fadeFactor;
            lastVelocity = lastVelocity <= 0 ? 0 : lastVelocity - velocityDelta;
            GenericSoundReact.ChangePhysicProperty2D(body, vpp, vppFactor, new Numeric(lastVelocity), initialValue);
        }   
    }

    public void Play_AddForce(Rigidbody body, Vector3 forceDir, ForceMode mode, float forceFactor)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
            GenericSoundReact.CustomAddForce(body, forceDir, mode, forceFactor, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public void Play_AddForce2D(Rigidbody2D body, Vector2 forceDir, ForceMode2D mode, float forceFactor)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
            GenericSoundReact.CustomAddForce2D(body, forceDir, mode, forceFactor, new Numeric(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()));
    }

    public GameObject Play_Instantiate(Object obj, Vector3 position, Quaternion rotation, List<int> noteNumbers, float spawnTime)
    {
        GameObject go = null;

        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            int number = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber();

            GenericSoundReact.SoundInstantiate(obj, position, rotation, noteNumbers.Contains(number), spawnTime);
        }

        return go;
    }

    public GameObject Play_DrawPolygon(Vector3[] polygonVert, Color lineColor, float lineWidth, float drawSpeedFactor)
    {
        GameObject polygon = GenericSoundReact.DrawPolygon(polygonVert, lineColor, lineWidth, drawSpeedFactor, GenericSoundReact.MusicDataType.Play_Velocity);
        return polygon;
    }

    #endregion
}
