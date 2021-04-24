using System.Collections;
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

    /// <summary>
    /// Modifies the color in the material associated to <paramref name="go"/>. The material changes to <paramref name="color"/> in 
    /// the specified <paramref name="transitionTime"/>.
    /// The change is produced using <paramref name="numberColorAssociation"/>.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="color"></param>
    /// <param name="transitionTime"></param>
    /// <param name="numberColorAssociation"></param>
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

    /// <summary>
    /// Creates and returns a GameObject, which is an instance of <paramref name="obj"/>, in the position determined by <paramref name="position"/> and
    /// with the rotation specified by <paramref name="rotation"/>.
    /// The instantiation is produced when the NoteNumber of recorded event is equals to <paramref name="noteNumber"/>..
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="noteNumber"></param>
    /// <returns></returns>
    public GameObject Record_NoteNumberInstantiate(Object obj, Vector3 position, Quaternion rotation, int noteNumber, float spawnTime)
    {
        GameObject go = null;

        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            int number = MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber();

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

    /// <summary>
    /// Moves <paramref name="go"/> along <paramref name="axis"/>. The amount of movement is specified by <paramref name="translationFactor"/>.
    /// Changes are made using NoteVelocity of recorded event.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="axis"></param>
    /// <param name="translationFactor"></param>
    public void Record_VelocityTranslation(GameObject go, Vector3 axis, float translationFactor, float fadeFactor = 0)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            float velocity = MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() * MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber() / NOTES;
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

    /// <summary>
    /// Rotates <paramref name="go"/> over <paramref name="axis"/>. The amount of rotation is specified by <paramref name="rotFactor"/>.
    /// Changes are made using NoteVelocity of recorded event.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="axis"></param>
    /// <param name="rotFactor"></param>
    public void Record_VelocityRotation(GameObject go, Vector3 axis, float rotFactor, float fadeFactor = 0)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            float velocity = MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() * MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber() / NOTES;
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

    /// <summary>
    /// Modifies the scale of <paramref name="go"/> along <paramref name="axis"/>. The initial an minimum scale is specified 
    /// by <paramref name="startScale"/> and the the scale amount by <paramref name="scaleFactor"/>.
    /// Changes are made using NoteVelocity of recorded event.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="axis"></param>
    /// <param name="scaleFactor"></param>
    /// <param name="startScale"></param>
    public void Record_VelocityScale(GameObject go, Vector3 axis, float scaleFactor, float initialScale = 1, float fadeFactor = 0)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            float velocity = MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() * MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber() / NOTES;
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

    /// <summary>
    /// Modifies the bright of the color in the material associated to <paramref name="go"/>. The initial an minimum bright is specified 
    /// by <paramref name="startColor"/> and the the bright amount by <paramref name="brightFactor"/>.
    /// Changes are made using NoteVelocity of recorded event.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="brightFactor"></param>
    /// <param name="startColor"></param>
    public void Record_VelocityBright(GameObject go, float brightFactor, Color initialColor, float fadeFactor = 0)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            float velocity = MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() * MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber() / NOTES;
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

    ///<summary>
    /// Modifies the color in the material associated to <paramref name="go"/>. The material changes to <paramref name="color"/> in 
    /// the specified <paramref name="transitionTime"/>.
    /// 
    /// </summary>
    /// <param name="go"></param>
    /// <param name="color"></param>
    /// <param name="transitionTime"></param>
    /// <param name="amplitudeThreshold"></param>
    public void Record_VelocityColor(GameObject go, Color color, float transitionTime, float velocityThreshold)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            if (MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() > velocityThreshold)
                GenericSoundReact.ChangeColor(go, color, transitionTime);
        }
    }

    /// <summary>
    /// Modifies <paramref name="mesh"/> vertices height. The amount of height variation is specified by <paramref name="heightFactor"/>
    /// and the amount of noise by <paramref name="noiseFactor"/>. Vertices initial positions are defined by <paramref name="initPos"/>.
    /// Changes are made using NoteVelocity of recorded event.
    /// </summary>
    /// <param name="mesh"></param>
    /// <param name="noiseFactor"></param>
    /// <param name="heightFactor"></param>
    /// <param name="initPos"></param>
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

    /// <summary>
    /// Modifies <paramref name="light"/> intensity. The amount of this intensity is specified by <paramref name="intensityFactor"/>.
    /// Changes are made using NoteVelocity of recorded event.
    /// </summary>
    /// <param name="light"></param>
    /// <param name="intensityFactor"></param>
    public void Record_VelocityLightIntensity(Light light, float intensityFactor, float initialIntensity = 1, float fadeFactor = 0)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            float velocity = MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() * MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber() / NOTES;
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

    /// <summary>
    /// Modifies <paramref name="light"/> range. The amount of this range is specified by <paramref name="rangeFactor"/>.
    /// Changes are made using NoteVelocity of recorded event.
    /// </summary>
    /// <param name="light"></param>
    /// <param name="rangeFactor"></param>
    public void Record_VelocityLightRange(Light light, float rangeFactor, float initialRange = 1, float fadeFactor = 0)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            float velocity = MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() * MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber() / NOTES;
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

    /// <summary>
    /// Modifies <paramref name="mat"/> shader/material property named <paramref name="propertyName"/> of type <paramref name="propertyType"/>.
    /// The amount of change is specified by <paramref name="factor"/>.
    /// Changes are made using NoteVelocity of recorded event.
    /// </summary>
    /// <param name="mat"></param>
    /// <param name="propertyName"></param>
    /// <param name="propertyType"></param>
    /// <param name="factor"></param>
    public void Record_VelocityShaderGraphMatProperty(Material mat, string propertyName, GenericSoundReact.MatPropertyType propertyType, float propertyFactor, float fadeFactor = 0)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            float velocity = MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() * MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeShaderGraphMatProperty(mat, propertyName, propertyType, propertyFactor, new Numeric(velocity));
        }
        else
        {
            float propertyIntensity = mat.GetFloat(propertyName);
            float propertyDelta = fadeFactor == 0 ? propertyIntensity : Time.deltaTime * propertyIntensity / fadeFactor;
            GenericSoundReact.ChangeShaderGraphMatProperty(mat, propertyName, propertyType, 1, new Numeric(propertyIntensity - propertyDelta));
        }
    }

    /// <summary>
    /// Modifies <paramref name="anim"/> animation speed. The amount of change in speed is specified by <paramref name="factor"/>.
    /// Changes are made using NoteVelocity of recorded event.
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="factor"></param>
    public void Record_VelocityAnimationSpeed(Animator anim, float speedFactor, float initialSpeed = 1, float fadeFactor = 0)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            float velocity = MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() * MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber() / NOTES;
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

    /// <summary>
    /// Modifies the <paramref name="bloom"/> of the Global Volume. The amount of change is specified by <paramref name="factor"/>.
    /// Changes are made using NoteVelocity of recorded event.
    /// </summary>
    /// <param name="bloom"></param>
    /// <param name="factor"></param>
    public void Record_VelocityBloom(Bloom bloom, float bloomFactor, float initialBloom = 0, float fadeFactor = 0)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            float velocity = MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() * MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeBloom(bloom, bloomFactor, new Numeric(velocity), initialBloom);
        }
        else
        {
            float bloomIntensity = bloom.intensity.value;
            float bloomDelta = fadeFactor == 0 ? bloomIntensity : Time.deltaTime * bloomIntensity / fadeFactor;
            GenericSoundReact.ChangeBloom(bloom, bloomFactor, new Numeric(bloom.intensity.value = bloomIntensity - bloomDelta), initialBloom);
        }    
    }

    /// <summary>
    /// Modifies the chromatic aberration of the Global Volume. The amount of change is specified by <paramref name="factor"/>.
    /// Changes are made using NoteVelocity of recorded event.
    /// </summary>
    /// <param name="ca"></param>
    /// <param name="factor"></param>
    public void Record_VelocityChromaticAberration(ChromaticAberration ca, float caFactor, float initialCA = 0, float fadeFactor = 0)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            float velocity = MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity() * MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber() / NOTES;
            GenericSoundReact.ChangeChromaticAberration(ca, caFactor, new Numeric(velocity), initialCA);
        }
        else
        {
            float caIntensity = ca.intensity.value;
            float caDelta = fadeFactor == 0 ? caIntensity : Time.deltaTime * caIntensity / fadeFactor;
            GenericSoundReact.ChangeChromaticAberration(ca, caFactor, new Numeric(ca.intensity.value = caIntensity - caDelta), initialCA);
        }
    }

    public void Record_VelocityVignette(Vignette vignette, float vignetteFactor, float initialVignette = 0, float fadeFactor = 0)
    {
        if (MidiRecording.GetCurrentNoteOffEvent() != null)
        {
            float velocity = MidiRecording.GetCurrentNoteOffEvent().GetNoteVelocity() * MidiRecording.GetCurrentNoteOffEvent().GetNoteNumber() / NOTES;
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

    /// <summary>
    /// Modifies the specified 3D float physic property of <paramref name="body"/>. The amount of change is specified by <paramref name="fppFactor"/>.
    /// Changes are made using NoteVelocity of recorded event.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="fpp"></param>
    /// <param name="fppFactor"></param>
    public void Record_VelocityPhysicProperty(Rigidbody body, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor, float initialValue = 0, float fadeFactor = 0)
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

    /// <summary>
    /// Modifies the specified 3D vectorial physic property of <paramref name="body"/> along <paramref name="axis"/>.
    /// Changes are made using NoteVelocity of recorded event.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="vpp"></param>
    /// <param name="axis"></param>
    public void Record_VelocityPhysicProperty(Rigidbody body, GenericSoundReact.VectorPhysicProperties vpp, Vector3 axis)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.ChangePhysicProperty(body, vpp, axis, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
    }

    /// <summary>
    /// Modifies the specified 2D float physic property of <paramref name="body"/>. The amount of change is specified by <paramref name="fppFactor"/>.
    /// Changes are made using NoteVelocity of recorded event.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="fpp"></param>
    /// <param name="fppFactor"></param>
    public void Record_VelocityPhysicProperty2D(Rigidbody2D body, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null) { }
            GenericSoundReact.ChangePhysicProperty2D(body, fpp, fppFactor, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
    }

    /// <summary>
    /// Modifies the specified 2D vectorial physic property of <paramref name="body"/> along <paramref name="axis"/>.
    /// Changes are made using NoteVelocity of recorded event.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="vpp"></param>
    /// <param name="axis"></param>
    public void Record_VelocityPhysicProperty2D(Rigidbody2D body, GenericSoundReact.VectorPhysicProperties vpp, Vector2 axis)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.ChangePhysicProperty2D(body, vpp, axis, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
    }

    /// <summary>
    /// Add a 3D force to <paramref name="body"/> in the direction of <paramref name="forceDir"/>. The type of the applied force 
    /// is specified by <paramref name="mode"/> and the amount of force by <paramref name="forceFactor"/>.
    /// The amount of force is computed using NoteVelocity of recorded event.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="forceDir"></param>
    /// <param name="mode"></param>
    /// <param name="forceFactor"></param>
    public void Record_VelocityAddForce(Rigidbody body, Vector3 forceDir, ForceMode mode, float forceFactor)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.CustomAddForce(body, forceDir, mode, forceFactor, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
    }

    /// <summary>
    /// Add a 2D force to <paramref name="body"/> in the direction of <paramref name="forceDir"/>. The type of the applied force 
    /// is specified by <paramref name="mode"/> and the amount of force by <paramref name="forceFactor"/>.
    /// The amount of force is computed using NoteVelocity of recorded event.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="forceDir"></param>
    /// <param name="mode"></param>
    /// <param name="forceFactor"></param>
    public void Record_VelocityAddForce2D(Rigidbody2D body, Vector2 forceDir, ForceMode2D mode, float forceFactor)
    {
        if (MidiRecording.GetCurrentNoteOnEvent() != null)
            GenericSoundReact.CustomAddForce2D(body, forceDir, mode, forceFactor, new Numeric(MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity()));
    }

    /// <summary>
    /// Creates and returns a GameObject, which is an instance of <paramref name="obj"/>, in the position determined by <paramref name="position"/> and
    /// with the rotation specified by <paramref name="rotation"/>.
    /// The instantiation is produced when the NoteVelocity of recorded event value is greater than <paramref name="velocityThreshold"/>.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="velocityThreshold"></param>
    /// <returns></returns>
    public GameObject Record_VelocityInstantiate(Object obj, Vector3 position, Quaternion rotation, int velocityThreshold, float spawnTime)
    {
        GameObject go = null;

        if (MidiRecording.GetCurrentNoteOnEvent() != null)
        {
            int velocity = MidiRecording.GetCurrentNoteOnEvent().GetNoteVelocity();

            go = GenericSoundReact.SoundInstantiate(obj, position, rotation, velocity > velocityThreshold, spawnTime);
        }
        return go;
    }

    /// <summary>
    /// Returns a GameObject that generate a new piece of trail every frame between the specified vertices of a custom polygon defined 
    /// by <paramref name="polygonVert"/>.
    /// The amount of the generated piece of trail in every frame is according to the NoteVelocity of recorded event.
    /// </summary>
    /// <param name="polygonVert"></param>
    /// <param name="lineColor"></param>
    /// <param name="lineWidth"></param>
    /// <param name="drawSpeedFactor"></param>
    /// <returns></returns>
    public GameObject Record_VelocityDrawPolygon(Vector3[] polygonVert, Color lineColor, float lineWidth, float drawSpeedFactor)
    {
        GameObject polygon = GenericSoundReact.DrawPolygon(polygonVert, lineColor, lineWidth, drawSpeedFactor, GenericSoundReact.MusicDataType.Record_Velocity);
        return polygon;
    }

    #endregion
}
