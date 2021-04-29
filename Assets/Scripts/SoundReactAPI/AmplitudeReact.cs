﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AmplitudeReact : MonoBehaviour
{
    #region Amplitude_React_Variables

    public AudioInput audioInput;

    #endregion

    #region Amplitude_Input_Functions

    /// <summary>
    /// Moves <paramref name="go"/> along <paramref name="axis"/>. The amount of movement is specified by <paramref name="translationFactor"/>.
    /// Changes are made using amplitude.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="axis"></param>
    /// <param name="translationFactor"></param>
    public void AmplitudeTranslation(GameObject go, Vector3 axis, float translationFactor)
    {
        GenericSoundReact.ChangeTranslation(go, axis, translationFactor, new Numeric(audioInput.GetAmplitudeBuffer()));
    }

    /// <summary>
    /// Rotates <paramref name="go"/> over <paramref name="axis"/>. The amount of rotation is specified by <paramref name="rotFactor"/>.
    /// Changes are made using amplitude.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="axis"></param>
    /// <param name="rotFactor"></param>
    public void AmplitudeRotation(GameObject go, Vector3 axis, float rotFactor)
    {
        GenericSoundReact.ChangeRotation(go, axis, rotFactor, new Numeric(audioInput.GetAmplitudeBuffer()));
    }

    /// <summary>
    /// Modifies the scale of <paramref name="go"/> along <paramref name="axis"/>. The initial an minimum scale is specified 
    /// by <paramref name="startScale"/> and the the scale amount by <paramref name="scaleFactor"/>.
    /// Changes are made using amplitude.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="axis"></param>
    /// <param name="scaleFactor"></param>
    /// <param name="startScale"></param>
    public void AmplitudeScale(GameObject go, Vector3 axis, float scaleFactor, float initialScale = 1)
    {
        GenericSoundReact.ChangeScale(go, axis, scaleFactor, new Numeric(audioInput.GetAmplitudeBuffer()), initialScale);
    }

    /// <summary>
    /// Modifies the bright of the color in the material associated to <paramref name="go"/>. The initial an minimum bright is specified 
    /// by <paramref name="startColor"/> and the the bright amount by <paramref name="brightFactor"/>.
    /// Changes are made using amplitude.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="brightFactor"></param>
    /// <param name="startColor"></param>
    public void AmplitudeBright(GameObject go, float brightFactor, Color initialColor)
    {
        GenericSoundReact.ChangeBright(go, brightFactor, initialColor, new Numeric(audioInput.GetAmplitudeBuffer()));
    }

    /// <summary>
    /// Modifies the color in the material associated to <paramref name="go"/>. The material changes to <paramref name="color"/> in 
    /// the specified <paramref name="transitionTime"/>.
    /// The change is produced when the amplitude value is greater than <paramref name="amplitudeThreshold"/>.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="color"></param>
    /// <param name="transitionTime"></param>
    /// <param name="amplitudeThreshold"></param>
    public void AmplitudeColor(GameObject go, Color color, float transitionTime, float amplitudeThreshold)
    {
        if (audioInput.GetAmplitudeBuffer() > amplitudeThreshold)
            GenericSoundReact.ChangeColor(go, color, transitionTime);
    }

    /// <summary>
    /// Modifies <paramref name="mesh"/> vertices height. The amount of height variation is specified by <paramref name="heightFactor"/>
    /// and the amount of noise by <paramref name="noiseFactor"/>. Vertices initial positions are defined by <paramref name="initPos"/>.
    /// Changes are made using amplitude.
    /// </summary>
    /// <param name="mesh"></param>
    /// <param name="noiseFactor"></param>
    /// <param name="heightFactor"></param>
    /// <param name="initPos"></param>
    public void AmplitudeReliefMap(Mesh mesh, float noiseFactor, float reliefFactor, Vector3[] initPos, float waveSpeed)
    {
        GenericSoundReact.ChangeReliefMap(mesh, noiseFactor, reliefFactor, initPos, waveSpeed, new Numeric(audioInput.GetAmplitudeBuffer()));
    }

    /// <summary>
    /// Modifies <paramref name="light"/> intensity. The amount of this intensity is specified by <paramref name="intensityFactor"/>.
    /// Changes are made using amplitude.
    /// </summary>
    /// <param name="light"></param>
    /// <param name="intensityFactor"></param>
    public void AmplitudeLightIntensity(Light light, float intensityFactor, float initialIntensity = 1)
    {
        GenericSoundReact.ChangeLightIntensity(light, intensityFactor, new Numeric(audioInput.GetAmplitudeBuffer()), initialIntensity);
    }

    /// <summary>
    /// Modifies <paramref name="light"/> range. The amount of this range is specified by <paramref name="rangeFactor"/>.
    /// Changes are made using amplitude.
    /// </summary>
    /// <param name="light"></param>
    /// <param name="rangeFactor"></param>
    public void AmplitudeLightRange(Light light, float rangeFactor, float initialRange = 1)
    {
        GenericSoundReact.ChangeLightRange(light, rangeFactor, new Numeric(audioInput.GetAmplitudeBuffer()), initialRange);
    }

    /// <summary>
    /// Modifies <paramref name="mat"/> shader/material property named <paramref name="propertyName"/> of type <paramref name="propertyType"/>.
    /// The amount of change is specified by <paramref name="factor"/>.
    /// Changes are made using amplitude.
    /// </summary>
    /// <param name="mat"></param>
    /// <param name="propertyName"></param>
    /// <param name="propertyType"></param>
    /// <param name="factor"></param>
    public void AmplitudeShaderGraphMatProperty(Material mat, string propertyName, GenericSoundReact.MatPropertyType propertyType, float propertyFactor)
    {
        GenericSoundReact.ChangeShaderGraphMatProperty(mat, propertyName, propertyType, propertyFactor, new Numeric(audioInput.GetAmplitudeBuffer()));
    }

    /// <summary>
    /// Modifies <paramref name="anim"/> animation speed. The amount of change in speed is specified by <paramref name="factor"/>.
    /// Changes are made using amplitude.
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="factor"></param>
    public void AmplitudeAnimationSpeed(Animator anim, float speedFactor, float initialSpeed = 1)
    {
        GenericSoundReact.ChangeAnimationSpeed(anim, speedFactor, new Numeric(audioInput.GetAmplitudeBuffer()), initialSpeed);
    }

    /// <summary>
    /// Modifies the <paramref name="bloom"/> of the Global Volume. The amount of change is specified by <paramref name="factor"/>.
    /// Changes are made using amplitude.
    /// </summary>
    /// <param name="bloom"></param>
    /// <param name="factor"></param>
    public void AmplitudeBloom(Bloom bloom, float bloomFactor, float initialBloom = 0)
    {
        GenericSoundReact.ChangeBloom(bloom, bloomFactor, new Numeric(audioInput.GetAmplitudeBuffer()), initialBloom);
    }

    /// <summary>
    /// Modifies the chromatic aberration of the Global Volume. The amount of change is specified by <paramref name="factor"/>.
    /// Changes are made using amplitude.
    /// </summary>
    /// <param name="ca"></param>
    /// <param name="factor"></param>
    public void AmplitudeChromaticAberration(ChromaticAberration ca, float caFactor, float initialCA = 0)
    {
        GenericSoundReact.ChangeChromaticAberration(ca, caFactor, new Numeric(audioInput.GetAmplitudeBuffer()), initialCA);
    }

    /// <summary>
    /// Modifies the vignette of the Global Volume. The amount of change is specified by <paramref name="factor"/>.
    /// Changes are made using amplitude.
    /// </summary>
    /// <param name="ca"></param>
    /// <param name="factor"></param>
    public void AmplitudeVignette(Vignette vignette, float vignetteFactor, float initialVignette = 0)
    {
        GenericSoundReact.ChangeVignette(vignette, vignetteFactor, new Numeric(audioInput.GetAmplitudeBuffer()), initialVignette);
    }

    /// <summary>
    /// Modifies the specified 3D float physic property of <paramref name="body"/>. The amount of change is specified by <paramref name="fppFactor"/>.
    /// Changes are made using amplitude.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="fpp"></param>
    /// <param name="fppFactor"></param>
    public void AmplitudePhysicProperty(Rigidbody body, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor, float initialValue = 0)
    {
        GenericSoundReact.ChangePhysicProperty(body, fpp, fppFactor, new Numeric(audioInput.GetAmplitudeBuffer()), initialValue);
    }

    /// <summary>
    /// Modifies the specified 3D vectorial physic property of <paramref name="body"/> along <paramref name="axis"/>.
    /// Changes are made using amplitude.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="vpp"></param>
    /// <param name="axis"></param>
    public void AmplitudePhysicProperty(Rigidbody body, GenericSoundReact.VectorPhysicProperties vpp, Vector3 vppFactor, Vector3 initialValue = new Vector3())
    {
        GenericSoundReact.ChangePhysicProperty(body, vpp, vppFactor, new Numeric(audioInput.GetAmplitudeBuffer()), initialValue);
    }

    /// <summary>
    /// Modifies the specified 2D float physic property of <paramref name="body"/>. The amount of change is specified by <paramref name="fppFactor"/>.
    /// Changes are made using amplitude.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="fpp"></param>
    /// <param name="fppFactor"></param>
    public void AmplitudePhysicProperty2D(Rigidbody2D body, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor, float initialValue = 0)
    {
        GenericSoundReact.ChangePhysicProperty2D(body, fpp, fppFactor, new Numeric(audioInput.GetAmplitudeBuffer()), initialValue);
    }

    /// <summary>
    /// Modifies the specified 2D vectorial physic property of <paramref name="body"/> along <paramref name="axis"/>.
    /// Changes are made using amplitude.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="vpp"></param>
    /// <param name="axis"></param>
    public void AmplitudePhysicProperty2D(Rigidbody2D body, GenericSoundReact.VectorPhysicProperties vpp, Vector2 vppFactor, Vector2 initialValue = new Vector2())
    {
        GenericSoundReact.ChangePhysicProperty2D(body, vpp, vppFactor, new Numeric(audioInput.GetAmplitudeBuffer()), initialValue);
    }

    /// <summary>
    /// Add a 3D force to <paramref name="body"/> in the direction of <paramref name="forceDir"/>. The type of the applied force 
    /// is specified by <paramref name="mode"/> and the amount of force by <paramref name="forceFactor"/>.
    /// The amount of force is computed using amplitude.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="forceDir"></param>
    /// <param name="mode"></param>
    /// <param name="forceFactor"></param>
    public void AmplitudeAddForce(Rigidbody body, Vector3 forceDir, ForceMode mode, float forceFactor)
    {
        GenericSoundReact.CustomAddForce(body, forceDir, mode, forceFactor, new Numeric(audioInput.GetAmplitudeBuffer()));
    }

    /// <summary>
    /// Add a 2D force to <paramref name="body"/> in the direction of <paramref name="forceDir"/>. The type of the applied force 
    /// is specified by <paramref name="mode"/> and the amount of force by <paramref name="forceFactor"/>.
    /// The amount of force is computed using amplitude.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="forceDir"></param>
    /// <param name="mode"></param>
    /// <param name="forceFactor"></param>
    public void AmplitudeAddForce2D(Rigidbody2D body, Vector2 forceDir, ForceMode2D mode, float forceFactor)
    {
        GenericSoundReact.CustomAddForce2D(body, forceDir, mode, forceFactor, new Numeric(audioInput.GetAmplitudeBuffer()));
    }

    /// <summary>
    /// Creates and returns a GameObject, which is an instance of <paramref name="obj"/>, in the position determined by <paramref name="position"/> and
    /// with the rotation specified by <paramref name="rotation"/>.
    /// The instantiation is produced when the amplitude value is greater than <paramref name="amplitudeThreshold"/>.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="amplitudeThreshold"></param>
    /// <returns></returns>
    public GameObject AmplitudeInstantiate(UnityEngine.Object obj, Vector3 position, Quaternion rotation, float amplitudeThreshold, float spawnTime)
    {
        GameObject go = GenericSoundReact.SoundInstantiate(obj, position, rotation, audioInput.GetAmplitudeBuffer() > amplitudeThreshold, spawnTime);
        return go;
    }

    /// <summary>
    /// Returns a GameObject that generate a new piece of trail every frame between the specified vertices of a custom polygon defined 
    /// by <paramref name="polygonVert"/>.
    /// The amount of the generated piece of trail in every frame is according to the amplitude.
    /// </summary>
    /// <param name="polygonVert"></param>
    /// <param name="lineColor"></param>
    /// <param name="lineWidth"></param>
    /// <param name="drawSpeedFactor"></param>
    /// <returns></returns>
    public GameObject AmplitudeDrawPolygon(Vector3[] polygonVert, Color lineColor, float lineWidth, float drawSpeedFactor)
    {
        GameObject polygon = GenericSoundReact.DrawPolygon(polygonVert, lineColor, lineWidth, drawSpeedFactor, GenericSoundReact.MusicDataType.Amplitude);
        return polygon;
    }

    public GameObject AmplitudePhyllotaxis(float phyllotaxisDegree, float speedFactor, float scaleFactor, float initialScale = 0, int loops = 10)
    {
        GameObject phylloObj = GenericSoundReact.GeneratePhyllotaxis(phyllotaxisDegree, speedFactor, scaleFactor, GenericSoundReact.MusicDataType.Amplitude, initialScale, loops);
        return phylloObj;
    }

    public GameObject AmplitudePhyllotunnel(float tunnelSpeed, float phyllotaxisDegree, float speedFactor, float scaleFactor, float cameraDistance = -10, Transform cameraTransform = null, float initialScale = 0)
    {
        GameObject tunnelObj = GenericSoundReact.GeneratePhyllotunnel(tunnelSpeed, phyllotaxisDegree, speedFactor, scaleFactor, GenericSoundReact.MusicDataType.Amplitude, cameraDistance, cameraTransform, initialScale);
        return tunnelObj;
    }

    #endregion

}
