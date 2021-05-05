using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MIDIPlayReact : MonoBehaviour
{
    #region MIDI_Play_Velocity_Input_Functions

    /// <summary>
    /// Moves <paramref name="go"/> along <paramref name="axis"/>. The amount of movement is specified by <paramref name="translationFactor"/>.
    /// When there are no incoming events, the effect is fading by <paramref name="fadeFactor"/> amount.
    /// Changes are made using the played event.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="axis"></param>
    /// <param name="translationFactor"></param>
    /// <param name="fadeFactor"></param>
    public void Play_Translation(GameObject go, Vector3 axis, float translationFactor)

    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MIDIConst.ComputedB(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()) * MIDIConst.NOTES_TO_FREQ[MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber()] / MIDIConst.MAX_FREQ;
            GenericSoundReact.ChangeTranslation(go, axis, translationFactor, new Numeric(velocity));
        }
        else
        {
            GenericSoundReact.ChangeTranslation(go, axis, translationFactor, new Numeric(0));
        }
    }

    /// <summary>
    /// Rotates <paramref name="go"/> over <paramref name="axis"/>. The amount of rotation is specified by <paramref name="rotFactor"/>.
    /// When there are no incoming events, the effect is fading by <paramref name="fadeFactor"/> amount.
    /// Changes are made using the played event.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="axis"></param>
    /// <param name="rotFactor"></param>
    /// <param name="fadeFactor"></param>
    public void Play_Rotation(GameObject go, Vector3 axis, float rotFactor)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MIDIConst.ComputedB(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()) * MIDIConst.NOTES_TO_FREQ[MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber()] / MIDIConst.MAX_FREQ;
            GenericSoundReact.ChangeRotation(go, axis, rotFactor, new Numeric(velocity));
        }
        else
        {
            GenericSoundReact.ChangeRotation(go, axis, rotFactor, new Numeric(0));
        }
    }

    /// <summary>
    /// Modifies the scale of <paramref name="go"/> along <paramref name="axis"/>. The initial an minimum scale is specified 
    /// by <paramref name="initialScale"/> and the the scale amount by <paramref name="scaleFactor"/>.
    /// When there are no incoming events, the effect is fading by <paramref name="fadeFactor"/> amount.
    /// Changes are made using the played event.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="axis"></param>
    /// <param name="scaleFactor"></param>
    /// <param name="initialScale"></param>
    /// <param name="fadeFactor"></param>
    public void Play_Scale(GameObject go, Vector3 axis, float scaleFactor, float initialScale = 1)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MIDIConst.ComputedB(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()) * MIDIConst.NOTES_TO_FREQ[MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber()] / MIDIConst.MAX_FREQ;
            GenericSoundReact.ChangeScale(go, axis, scaleFactor, new Numeric(velocity), initialScale);
        }
        else
        {
            GenericSoundReact.ChangeScale(go, axis, scaleFactor, new Numeric(0), initialScale);
        }
    }

    /// <summary>
    /// Modifies the bright of the color in the material associated to <paramref name="go"/>. The initial an minimum bright is specified 
    /// by <paramref name="initialColor"/> and the the bright amount by <paramref name="brightFactor"/>.
    /// When there are no incoming events, the effect is fading by <paramref name="fadeFactor"/> amount.
    /// Changes are made using the played event.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="brightFactor"></param>
    /// <param name="initialColor"></param>
    /// <param name="fadeFactor"></param>
    public void Play_Bright(GameObject go, float brightFactor, Color initialColor)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MIDIConst.ComputedB(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()) * MIDIConst.NOTES_TO_FREQ[MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber()] / MIDIConst.MAX_FREQ;
            GenericSoundReact.ChangeBright(go, brightFactor, initialColor, new Numeric(velocity));
        }
        else
        {
            GenericSoundReact.ChangeBright(go, brightFactor, initialColor, new Numeric(0));
        }
    }

    /// <summary>
    /// Modifies the color in the material associated to <paramref name="go"/>. The material changes to the color that is associated with the number
    /// of the played event in <paramref name="numberColorAssociation"/>. The transition last <paramref name="transitionTime"/>.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="numberColorAssociation"></param>
    /// <param name="transitionTime"></param>
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

    /// <summary>
    /// Modifies <paramref name="mesh"/> vertices height. The amount of height variation is specified by <paramref name="heightFactor"/>
    /// and the amount of noise by <paramref name="noiseFactor"/>. Vertices initial positions are defined by <paramref name="initPos"/>.
    /// When there are no incoming events, the effect is fading by <paramref name="fadeFactor"/> amount.
    /// Changes are made using the played event.
    /// </summary>
    /// <param name="mesh"></param>
    /// <param name="noiseFactor"></param>
    /// <param name="reliefFactor"></param>
    /// <param name="waveSpeed"></param>
    /// <param name="initPos"></param>
    /// <param name="fadeFactor"></param>
    public void Play_ReliefMap(Mesh mesh, float noiseFactor, float reliefFactor, float waveSpeed, Vector3[] initPos)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MIDIConst.ComputedB(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()) * MIDIConst.NOTES_TO_FREQ[MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber()] / MIDIConst.MAX_FREQ;
            GenericSoundReact.ChangeReliefMap(mesh, noiseFactor, reliefFactor, initPos, waveSpeed, new Numeric(velocity));
        }
        else
        {
            GenericSoundReact.ChangeReliefMap(mesh, noiseFactor, reliefFactor, initPos, waveSpeed, new Numeric(0));
        }
    }

    /// <summary>
    /// Modifies <paramref name="light"/> intensity. The amount of this intensity is specified by <paramref name="intensityFactor"/>.
    /// The initial an minimum intensity is specified by <paramref name="initialIntensity"/>.
    /// When there are no incoming events, the effect is fading by <paramref name="fadeFactor"/> amount.
    /// Changes are made using the played event.
    /// </summary>
    /// <param name="light"></param>
    /// <param name="intensityFactor"></param>
    /// <param name="initialIntensity"></param>
    /// <param name="fadeFactor"></param>
    public void Play_LightIntensity(Light light, float intensityFactor, float initialIntensity = 1)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MIDIConst.ComputedB(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()) * MIDIConst.NOTES_TO_FREQ[MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber()] / MIDIConst.MAX_FREQ;
            GenericSoundReact.ChangeLightIntensity(light, intensityFactor, new Numeric(velocity), initialIntensity);
        }
        else
        {
            GenericSoundReact.ChangeLightIntensity(light, intensityFactor, new Numeric(0), initialIntensity);
        }
    }

    /// <summary>
    /// Modifies <paramref name="light"/> range. The amount of this range is specified by <paramref name="rangeFactor"/>.
    /// The initial an minimum intensity is specified by <paramref name="initialRange"/>.
    /// When there are no incoming events, the effect is fading by <paramref name="fadeFactor"/> amount.
    /// Changes are made using the played event.
    /// </summary>
    /// <param name="light"></param>
    /// <param name="rangeFactor"></param>
    /// <param name="initialRange"></param>
    /// <param name="fadeFactor"></param>
    public void Play_LightRange(Light light, float rangeFactor, float initialRange = 1)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MIDIConst.ComputedB(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()) * MIDIConst.NOTES_TO_FREQ[MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber()] / MIDIConst.MAX_FREQ;
            GenericSoundReact.ChangeLightRange(light, rangeFactor, new Numeric(velocity), initialRange);
        }
        else
        {
            GenericSoundReact.ChangeLightRange(light, rangeFactor, new Numeric(0), initialRange);
        } 
    }

    /// <summary>
    /// Modifies <paramref name="mat"/> shader/material property named <paramref name="propertyName"/> of type <paramref name="propertyType"/>.
    /// The amount of change is specified by <paramref name="propertyFactor"/>.
    /// When there are no incoming events, the effect is fading by <paramref name="fadeFactor"/> amount.
    /// Changes are made using the played event.
    /// </summary>
    /// <param name="mat"></param>
    /// <param name="propertyName"></param>
    /// <param name="propertyType"></param>
    /// <param name="propertyFactor"></param>
    /// <param name="fadeFactor"></param>
    public void Play_ShaderGraphMatProperty(Material mat, string propertyName, GenericSoundReact.MatPropertyType propertyType, float propertyFactor)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MIDIConst.ComputedB(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()) * MIDIConst.NOTES_TO_FREQ[MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber()] / MIDIConst.MAX_FREQ;
            GenericSoundReact.ChangeShaderGraphMatProperty(mat, propertyName, propertyType, propertyFactor, new Numeric(velocity));
        }
        else
        {
            GenericSoundReact.ChangeShaderGraphMatProperty(mat, propertyName, propertyType, 1, new Numeric(0));
        }
    }

    /// <summary>
    /// Modifies <paramref name="anim"/> animation speed. The amount of change in speed is specified by <paramref name="speedFactor"/>.
    /// The initial an minimum intensity is specified by <paramref name="initialSpeed"/>.
    /// When there are no incoming events, the effect is fading by <paramref name="fadeFactor"/> amount.
    /// Changes are made using the played event.
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="speedFactor"></param>
    /// <param name="initialSpeed"></param>
    /// <param name="fadeFactor"></param>
    public void Play_AnimationSpeed(Animator anim, float speedFactor, float initialSpeed = 1)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MIDIConst.ComputedB(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()) * MIDIConst.NOTES_TO_FREQ[MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber()] / MIDIConst.MAX_FREQ;
            GenericSoundReact.ChangeAnimationSpeed(anim, speedFactor, new Numeric(velocity), initialSpeed);
        }
        else
        {
            GenericSoundReact.ChangeAnimationSpeed(anim, speedFactor, new Numeric(0), initialSpeed);
        }
    }

    /// <summary>
    /// Modifies the <paramref name="bloom"/> of the Global Volume. The amount of change is specified by <paramref name="bloomFactor"/>.
    /// The initial an minimum bloom is specified by <paramref name="initialBloom"/>.
    /// When there are no incoming events, the effect is fading by <paramref name="fadeFactor"/> amount.
    /// Changes are made using the played event.
    /// </summary>
    /// <param name="bloom"></param>
    /// <param name="bloomFactor"></param>
    /// <param name="initialBloom"></param>
    /// <param name="fadeFactor"></param>
    public void Play_Bloom(Bloom bloom, float bloomFactor, float initialBloom = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MIDIConst.ComputedB(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()) * MIDIConst.NOTES_TO_FREQ[MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber()] / MIDIConst.MAX_FREQ;
            GenericSoundReact.ChangeBloom(bloom, bloomFactor, new Numeric(velocity), initialBloom);
        }
        else
        {
            GenericSoundReact.ChangeBloom(bloom, bloomFactor, new Numeric(0), initialBloom);
        }
    }

    /// <summary>
    /// Modifies the chromatic aberration of the Global Volume. The amount of change is specified by <paramref name="caFactor"/>.
    /// The initial an minimum chromatic aberration is specified by <paramref name="initialCA"/>.
    /// When there are no incoming events, the effect is fading by <paramref name="fadeFactor"/> amount.
    /// Changes are made using the played event.
    /// </summary>
    /// <param name="ca"></param>
    /// <param name="caFactor"></param>
    /// <param name="initialCA"></param>
    /// <param name="fadeFactor"></param>
    public void Play_ChromaticAberration(ChromaticAberration ca, float caFactor, float initialCA = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MIDIConst.ComputedB(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()) * MIDIConst.NOTES_TO_FREQ[MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber()] / MIDIConst.MAX_FREQ;
            GenericSoundReact.ChangeChromaticAberration(ca, caFactor, new Numeric(velocity), initialCA);
        }
        else

        {
            GenericSoundReact.ChangeChromaticAberration(ca, caFactor, new Numeric(0), initialCA);
        }
    }

    /// <summary>
    /// Modifies the <paramref name="vignette"/> of the Global Volume. The amount of change is specified by <paramref name="vignetteFactor"/>.
    /// The initial an minimum vignette is specified by <paramref name="initialVignette"/>.
    /// When there are no incoming events, the effect is fading by <paramref name="fadeFactor"/> amount.
    /// Changes are made using the played event.
    /// </summary>
    /// <param name="vignette"></param>
    /// <param name="vignetteFactor"></param>
    /// <param name="initialVignette"></param>
    /// <param name="fadeFactor"></param>
    public void Play_Vignette(Vignette vignette, float vignetteFactor, float initialVignette = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MIDIConst.ComputedB(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()) * MIDIConst.NOTES_TO_FREQ[MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber()] / MIDIConst.MAX_FREQ;
            GenericSoundReact.ChangeVignette(vignette, vignetteFactor, new Numeric(velocity), initialVignette);
        }
        else

        {
            GenericSoundReact.ChangeVignette(vignette, vignetteFactor, new Numeric(0), initialVignette);
        }
    }

    /// <summary>
    /// Modifies 3D float physic property specified by  <paramref name="fpp"/> of <paramref name="body"/>. The amount of change is 
    /// specified by <paramref name="fppFactor"/>. The initial an minimum property value is specified by <paramref name="initialValue"/>.
    /// When there are no incoming events, the effect is fading by <paramref name="fadeFactor"/> amount.
    /// Changes are made using the played event.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="fpp"></param>
    /// <param name="fppFactor"></param>
    /// <param name="initialValue"></param>
    /// <param name="fadeFactor"></param>
    public void Play_PhysicProperty(Rigidbody body, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor, float initialValue = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MIDIConst.ComputedB(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()) * MIDIConst.NOTES_TO_FREQ[MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber()] / MIDIConst.MAX_FREQ;
            GenericSoundReact.ChangePhysicProperty(body, fpp, fppFactor, new Numeric(velocity), initialValue);
        }
        else

        {
            GenericSoundReact.ChangePhysicProperty(body, fpp, fppFactor, new Numeric(0), initialValue);
        }   
    }

    /// <summary>
    /// Modifies 3D vectorial physic property specified by <paramref name="vpp"/> of <paramref name="body"/>. The amount of change is 
    /// specified by <paramref name="vppFactor"/>. The initial an minimum property value is specified by <paramref name="initialValue"/>.
    /// When there are no incoming events, the effect is fading by <paramref name="fadeFactor"/> amount.
    /// Changes are made using the played event.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="vpp"></param>
    /// <param name="vppFactor"></param>
    /// <param name="initialValue"></param>
    /// <param name="fadeFactor"></param>
    public void Play_PhysicProperty(Rigidbody body, GenericSoundReact.VectorPhysicProperties vpp, Vector3 vppFactor, Vector3 initialValue = new Vector3())
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MIDIConst.ComputedB(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()) * MIDIConst.NOTES_TO_FREQ[MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber()] / MIDIConst.MAX_FREQ;
            GenericSoundReact.ChangePhysicProperty(body, vpp, vppFactor, new Numeric(velocity), initialValue);
        }
        else

        {
            GenericSoundReact.ChangePhysicProperty(body, vpp, vppFactor, new Numeric(0), initialValue);
        }   
    }

    /// <summary>
    /// Modifies 2D float physic property specified by <paramref name="fpp"/> of <paramref name="body"/>. The amount of change is 
    /// specified by <paramref name="fppFactor"/>. The initial an minimum property value is specified by <paramref name="initialValue"/>.
    /// When there are no incoming events, the effect is fading by <paramref name="fadeFactor"/> amount.
    /// Changes are made using the played event.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="fpp"></param>
    /// <param name="fppFactor"></param>
    /// <param name="initialValue"></param>
    /// <param name="fadeFactor"></param>
    public void Play_PhysicProperty2D(Rigidbody2D body, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor, float initialValue = 0)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MIDIConst.ComputedB(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()) * MIDIConst.NOTES_TO_FREQ[MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber()] / MIDIConst.MAX_FREQ;
            GenericSoundReact.ChangePhysicProperty2D(body, fpp, fppFactor, new Numeric(velocity), initialValue);
        }
        else

        {
            GenericSoundReact.ChangePhysicProperty2D(body, fpp, fppFactor, new Numeric(0), initialValue);
        }   
    }

    /// <summary>
    /// Modifies 2D vectorial physic property specified by <paramref name="vpp"/> of <paramref name="body"/>. The amount of change is 
    /// specified by <paramref name="vppFactor"/>. The initial an minimum property value is specified by <paramref name="initialValue"/>.
    /// When there are no incoming events, the effect is fading by <paramref name="fadeFactor"/> amount.
    /// Changes are made using the played event.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="vpp"></param>
    /// <param name="vppFactor"></param>
    /// <param name="initialValue"></param>
    /// <param name="fadeFactor"></param>
    public void Play_PhysicProperty2D(Rigidbody2D body, GenericSoundReact.VectorPhysicProperties vpp, Vector2 vppFactor, Vector2 initialValue = new Vector2())
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MIDIConst.ComputedB(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()) * MIDIConst.NOTES_TO_FREQ[MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber()] / MIDIConst.MAX_FREQ;
            GenericSoundReact.ChangePhysicProperty2D(body, vpp, vppFactor, new Numeric(velocity), initialValue);
        }
        else

        {
            GenericSoundReact.ChangePhysicProperty2D(body, vpp, vppFactor, new Numeric(0), initialValue);
        }   
    }

    /// <summary>
    /// Add a 3D force to <paramref name="body"/> in the direction of <paramref name="forceDir"/>. The type of the applied force 
    /// is specified by <paramref name="mode"/> and the amount of force by <paramref name="forceFactor"/>.
    /// The amount of force is computed using the played event.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="forceDir"></param>
    /// <param name="mode"></param>
    /// <param name="forceFactor"></param>
    public void Play_AddForce(Rigidbody body, Vector3 forceDir, ForceMode mode, float forceFactor)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MIDIConst.ComputedB(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()) * MIDIConst.NOTES_TO_FREQ[MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber()] / MIDIConst.MAX_FREQ;
            GenericSoundReact.CustomAddForce(body, forceDir, mode, forceFactor, new Numeric(velocity));
        }   
    }

    /// <summary>
    /// Add a 2D force to <paramref name="body"/> in the direction of <paramref name="forceDir"/>. The type of the applied force 
    /// is specified by <paramref name="mode"/> and the amount of force by <paramref name="forceFactor"/>.
    /// The amount of force is computed using the played event.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="forceDir"></param>
    /// <param name="mode"></param>
    /// <param name="forceFactor"></param>
    public void Play_AddForce2D(Rigidbody2D body, Vector2 forceDir, ForceMode2D mode, float forceFactor)
    {
        if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
        {
            float velocity = MIDIConst.ComputedB(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()) * MIDIConst.NOTES_TO_FREQ[MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber()] / MIDIConst.MAX_FREQ;
            GenericSoundReact.CustomAddForce2D(body, forceDir, mode, forceFactor, new Numeric(velocity));
        }
    }

    /// <summary>
    /// Creates and returns a GameObject, which is an instance of <paramref name="obj"/>, in the position determined by <paramref name="position"/> and
    /// with the rotation specified by <paramref name="rotation"/>.
    /// The instantiation is produced when the NoteNumber of played event is contained in <paramref name="noteNumbers"/> and 
    /// <paramref name="spawnTime"/> has passed between instances.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="noteNumbers"></param>
    /// <param name="spawnTime"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Creates and returns a GameObject that generate a new piece of trail every frame between the specified vertices of a custom polygon defined 
    /// by <paramref name="polygonVert"/>.
    /// The amount of the generated piece of trail in every frame is according to the played event.
    /// </summary>
    /// <param name="polygonVert"></param>
    /// <param name="lineColor"></param>
    /// <param name="lineWidth"></param>
    /// <param name="drawSpeedFactor"></param>
    /// <returns></returns>
    public GameObject Play_DrawPolygon(Vector3[] polygonVert, Color lineColor, float lineWidth, float drawSpeedFactor)
    {
        GameObject polygon = GenericSoundReact.DrawPolygon(polygonVert, lineColor, lineWidth, drawSpeedFactor, GenericSoundReact.MusicDataType.MidiPlay);
        return polygon;
    }

    /// <summary>
    /// Creates and returns a GameObject that generates a phyllotaxis trail. Degree between one line and the next is specified by
    /// <paramref name="phyllotaxisDegree"/>. Speed of trail is specified by <paramref name="speedFactor"/> and the amount of scale is determined by
    /// <paramref name="scaleFactor"/>. The initial and minimum scale is determined by <paramref name="initialScale"/>. The phyllotaxis changes between
    /// opening and closing when reaching the specified <paramref name="loops"/>.
    /// The amount of the generated piece of trail in every frame is according to the played event.
    /// </summary>
    /// <param name="phyllotaxisDegree"></param>
    /// <param name="speedFactor"></param>
    /// <param name="scaleFactor"></param>
    /// <param name="initialScale"></param>
    /// <param name="loops"></param>
    /// <returns></returns>
    public GameObject Play_Phyllotaxis(float phyllotaxisDegree, float speedFactor, float scaleFactor, float initialScale = 0, int loops = 10)
    {
        GameObject phylloObj = GenericSoundReact.GeneratePhyllotaxis(phyllotaxisDegree, speedFactor, scaleFactor, GenericSoundReact.MusicDataType.MidiPlay, initialScale, loops);
        return phylloObj;
    }

    /// <summary>
    /// Creates and returns a GameObject that generates a tunnel using a phyllotaxis trail. The advance speed inside the tunnel is specified by 
    /// <paramref name="tunnelSpeed"/>. The <paramref name="cameraTransform"/> refers to the camera that we want to follows inside the tunnel,
    /// always keeping <paramref name="cameraDistance"/> with the end of it. If <paramref name="cameraTransform"/> is not specified,
    /// the camera will not follow inside the tunnel.
    /// The amount of the generated piece of trail in every frame is according to the played event.
    /// </summary>
    /// <param name="tunnelSpeed"></param>
    /// <param name="phyllotaxisDegree"></param>
    /// <param name="speedFactor"></param>
    /// <param name="scaleFactor"></param>
    /// <param name="cameraDistance"></param>
    /// <param name="cameraTransform"></param>
    /// <param name="initialScale"></param>
    /// <returns></returns>
    public GameObject Play_Phyllotunnel(float tunnelSpeed, float phyllotaxisDegree, float speedFactor, float scaleFactor, float cameraDistance = -10, Transform cameraTransform = null, float initialScale = 0)
    {
        GameObject tunnelObj = GenericSoundReact.GeneratePhyllotunnel(tunnelSpeed, phyllotaxisDegree, speedFactor, scaleFactor, GenericSoundReact.MusicDataType.MidiPlay, cameraDistance, cameraTransform, initialScale);
        return tunnelObj;
    }

    #endregion
}
