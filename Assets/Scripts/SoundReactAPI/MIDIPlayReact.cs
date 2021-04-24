using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MIDIPlayReact : MonoBehaviour
{
    #region MIDI_Play_React_Variables

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

    public void Play_VelocityTranslation(GameObject go, Vector3 axis, float translationFactor, float fadeFactor = 0)

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

    public void Play_VelocityRotation(GameObject go, Vector3 axis, float rotFactor, float fadeFactor = 0)
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

    public void Play_VelocityScale(GameObject go, Vector3 axis, float scaleFactor, float initialScale = 1, float fadeFactor = 0)
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

    public void Play_VelocityBright(GameObject go, float brightFactor, Color initialColor, float fadeFactor = 0)
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

    public void Play_VelocityColor(GameObject go, Color color, float transitionTime, int velocityThreshold)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            if (MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() > velocityThreshold)
                GenericSoundReact.ChangeColor(go, color, transitionTime);
        }
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

    public void Play_VelocityLightIntensity(Light light, float intensityFactor, float initialIntensity = 1, float fadeFactor = 0)
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

    public void Play_VelocityLightRange(Light light, float rangeFactor, float initialRange = 1, float fadeFactor = 0)
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

    public void Play_VelocityShaderGraphMatProperty(Material mat, string propertyName, GenericSoundReact.MatPropertyType propertyType, float propertyFactor, float fadeFactor = 0)
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

    public void Play_VelocityAnimationSpeed(Animator anim, float speedFactor, float initialSpeed = 1, float fadeFactor = 0)
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

    public void Play_VelocityBloom(Bloom bloom, float bloomFactor, float initialBloom = 0, float fadeFactor = 0)
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

    public void Play_VelocityChromaticAberration(ChromaticAberration ca, float caFactor, float initialCA = 0, float fadeFactor = 0)
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

    public void Play_VelocityVignette(Vignette vignette, float vignetteFactor, float initialVignette = 0, float fadeFactor = 0)
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
