﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FreqBandReact : MonoBehaviour
{
    #region FreqBand_React_Variables

    public AudioInput audioInput;

    #endregion

    #region Frequency_Band_Input_Functions

    /// <summary>
    /// Moves <paramref name="go"/> along <paramref name="axis"/>. The amount of movement is specified by <paramref name="translationFactor"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="band"></param>
    /// <param name="axis"></param>
    /// <param name="translationFactor"></param>
    public void BandTranslation(GameObject go, int band, Vector3 axis, float translationFactor)
    {
        GenericSoundReact.ChangeTranslation(go, axis, translationFactor, new Numeric(audioInput.GetBandBuffer(band)));
    }

    /// <summary>
    /// Rotates <paramref name="go"/> over <paramref name="axis"/>. The amount of rotation is specified by <paramref name="rotFactor"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="band"></param>
    /// <param name="axis"></param>
    /// <param name="rotFactor"></param>
    public void BandRotation(GameObject go, int band, Vector3 axis, float rotFactor)
    {
        GenericSoundReact.ChangeRotation(go, axis, rotFactor, new Numeric(audioInput.GetBandBuffer(band)));
    }

    /// <summary>
    /// Modifies the scale of <paramref name="go"/> along <paramref name="axis"/>. The initial an minimum scale is specified 
    /// by <paramref name="startScale"/> and the the scale amount by <paramref name="scaleFactor"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="band"></param>
    /// <param name="axis"></param>
    /// <param name="scaleFactor"></param>
    /// <param name="startScale"></param>
    public void BandScale(GameObject go, int band, Vector3 axis, float scaleFactor, float startScale)
    {
        GenericSoundReact.ChangeScale(go, axis, scaleFactor, startScale, new Numeric(audioInput.GetBandBuffer(band)));
    }

    /// <summary>
    /// Modifies the bright of the color in the material associated to <paramref name="go"/>. The initial an minimum bright is specified 
    /// by <paramref name="startBright"/> and the the bright amount by <paramref name="brightFactor"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="band"></param>
    /// <param name="brightFactor"></param>
    /// <param name="startBrightness"></param>
    public void BandBright(GameObject go, int band, float brightFactor, Color startColor)
    {
        GenericSoundReact.ChangeBright(go, brightFactor, startColor, new Numeric(audioInput.GetBandBuffer(band)));
    }

    /// <summary>
    /// Modifies the color in the material associated to <paramref name="go"/>. The material changes to <paramref name="color"/> in 
    /// the specified <paramref name="transitionTime"/>.
    /// The change is produced when the frequency band value is greater than <paramref name="bandThreshold"/>.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="color"></param>
    /// <param name="transitionTime"></param>
    /// <param name="amplitudeThreshold"></param>
    public void BandColor(GameObject go, int band, Color color, float transitionTime, float bandThreshold)
    {
        if (audioInput.GetBandBuffer(band) > bandThreshold)
            GenericSoundReact.ChangeColor(go, color, transitionTime);
    }

    /// <summary>
    /// Modifies <paramref name="mesh"/> vertices height. The amount of height variation is specified by <paramref name="heightFactor"/>
    /// and the amount of noise by <paramref name="noiseFactor"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="mesh"></param>
    /// <param name="noiseFactor"></param>
    /// <param name="heightFactor"></param>
    public void BandTerrainHeightMap(Mesh mesh, int band, float noiseFactor, float heightFactor)
    {
        GenericSoundReact.ChangeTerrainHeightMap(mesh, noiseFactor, heightFactor, new Numeric(audioInput.GetBandBuffer(band)));
    }

    /// <summary>
    /// Modifies <paramref name="mesh"/> vertices height. The amount of height variation is specified by <paramref name="heightFactor"/>
    /// and the amount of noise by <paramref name="noiseFactor"/>. Vertices initial positions are defined by <paramref name="initPos"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="mesh"></param>
    /// <param name="noiseFactor"></param>
    /// <param name="heightFactor"></param>
    /// <param name="initPos"></param>
    public void BandReliefMap(Mesh mesh, int band, float noiseFactor, float reliefFactor, float waveSpeed, Vector3[] initPos)
    {
        GenericSoundReact.ChangeReliefMap(mesh, noiseFactor, reliefFactor, initPos, waveSpeed, new Numeric(audioInput.GetBandBuffer(band)));
    }

    /// <summary>
    /// Modifies <paramref name="light"/> intensity. The amount of this intensity is specified by <paramref name="intensityFactor"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="light"></param>
    /// <param name="intensityFactor"></param>
    public void BandLightIntensity(Light light, int band, float intensityFactor)
    {
        GenericSoundReact.ChangeLightIntensity(light, intensityFactor, new Numeric(audioInput.GetBandBuffer(band)));
    }

    /// <summary>
    /// Modifies <paramref name="light"/> range. The amount of this range is specified by <paramref name="rangeFactor"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="light"></param>
    /// <param name="rangeFactor"></param>
    public void BandLightRange(Light light, int band, float rangeFactor)
    {
        GenericSoundReact.ChangeLightRange(light, rangeFactor, new Numeric(audioInput.GetBandBuffer(band)));
    }

    /// <summary>
    /// Modifies <paramref name="mat"/> shader/material property named <paramref name="propertyName"/> of type <paramref name="propertyType"/>.
    /// The amount of change is specified by <paramref name="factor"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="mat"></param>
    /// <param name="band"></param>
    /// <param name="propertyName"></param>
    /// <param name="propertyType"></param>
    /// <param name="factor"></param>
    public void BandShaderGraphMatProperty(Material mat, int band, string propertyName, GenericSoundReact.MatPropertyType propertyType, float factor)
    {
        GenericSoundReact.ChangeShaderGraphMatProperty(mat, propertyName, propertyType, factor, new Numeric(audioInput.GetBandBuffer(band)));
    }

    /// <summary>
    /// Modifies <paramref name="anim"/> animation speed. The amount of change in speed is specified by <paramref name="factor"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="band"></param>
    /// <param name="factor"></param>
    public void BandAnimationSpeed(Animator anim, int band, float factor)
    {
        GenericSoundReact.ChangeAnimationSpeed(anim, factor, new Numeric(audioInput.GetBandBuffer(band)));
    }

    /// <summary>
    /// Modifies the <paramref name="bloom"/> of the Global Volume. The amount of change is specified by <paramref name="factor"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="bloom"></param>
    /// <param name="band"></param>
    /// <param name="factor"></param>
    public void BandChangeBloom(Bloom bloom, int band, float factor)
    {
        GenericSoundReact.ChangeBloom(bloom, factor, new Numeric(audioInput.GetBandBuffer(band)));
    }

    /// <summary>
    /// Modifies the chromatic aberration of the Global Volume. The amount of change is specified by <paramref name="factor"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="ca"></param>
    /// <param name="band"></param>
    /// <param name="factor"></param>
    public void BandChangeChromaticAberration(ChromaticAberration ca, int band, float factor)
    {
        GenericSoundReact.ChangeChromaticAberration(ca, factor, new Numeric(audioInput.GetBandBuffer(band)));
    }

    /// <summary>
    /// Modifies the specified 3D float physic property of <paramref name="body"/>. The amount of change is specified by <paramref name="fppFactor"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="band"></param>
    /// <param name="fpp"></param>
    /// <param name="fppFactor"></param>
    public void BandPhysicProperty(Rigidbody body, int band, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor, float initialValue)
    {
        GenericSoundReact.ChangePhysicProperty(body, fpp, fppFactor, initialValue, new Numeric(audioInput.GetBandBuffer(band)));
    }

    /// <summary>
    /// Modifies the specified 3D vectorial physic property of <paramref name="body"/> along <paramref name="axis"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="band"></param>
    /// <param name="fpp"></param>
    /// <param name="fppFactor"></param>
    public void BandPhysicProperty(Rigidbody body, int band, GenericSoundReact.VectorPhysicProperties vpp, Vector3 axis)
    {
        GenericSoundReact.ChangePhysicProperty(body, vpp, axis, new Numeric(audioInput.GetBandBuffer(band)));
    }

    /// <summary>
    /// Modifies the specified 2D float physic property of <paramref name="body"/>. The amount of change is specified by <paramref name="fppFactor"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="band"></param>
    /// <param name="fpp"></param>
    /// <param name="fppFactor"></param>
    public void BandPhysicProperty2D(Rigidbody2D body, int band, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor)
    {
        GenericSoundReact.ChangePhysicProperty2D(body, fpp, fppFactor, new Numeric(audioInput.GetBandBuffer(band)));
    }

    /// <summary>
    /// Modifies the specified 2D vectorial physic property of <paramref name="body"/> along <paramref name="axis"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="band"></param>
    /// <param name="fpp"></param>
    /// <param name="fppFactor"></param>
    public void BandPhysicProperty2D(Rigidbody2D body, int band, GenericSoundReact.VectorPhysicProperties vpp, Vector2 axis)
    {
        GenericSoundReact.ChangePhysicProperty2D(body, vpp, axis, new Numeric(audioInput.GetBandBuffer(band)));
    }

    /// <summary>
    /// Add a 3D force to <paramref name="body"/> in the direction of <paramref name="forceDir"/>. The type of the applied force 
    /// is specified by <paramref name="mode"/> and the amount of force by <paramref name="forceFactor"/>.
    /// The amount of force is computed using the specified frequency band.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="band"></param>
    /// <param name="forceDir"></param>
    /// <param name="mode"></param>
    /// <param name="forceFactor"></param>
    public void BandAddForce(Rigidbody body, int band, Vector3 forceDir, ForceMode mode, float forceFactor)
    {
        GenericSoundReact.CustomAddForce(body, forceDir, mode, forceFactor, new Numeric(audioInput.GetBandBuffer(band)));

    }

    /// <summary>
    /// Add a 2D force to <paramref name="body"/> in the direction of <paramref name="forceDir"/>. The type of the applied force 
    /// is specified by <paramref name="mode"/> and the amount of force by <paramref name="forceFactor"/>.
    /// The amount of force is computed using the specified frequency band.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="band"></param>
    /// <param name="forceDir"></param>
    /// <param name="mode"></param>
    /// <param name="forceFactor"></param>
    public void BandAddForce2D(Rigidbody2D body, int band, Vector2 forceDir, ForceMode2D mode, float forceFactor)
    {
        GenericSoundReact.CustomAddForce2D(body, forceDir, mode, forceFactor, new Numeric(audioInput.GetBandBuffer(band)));

    }

    /// <summary>
    /// Returns a GameObject that generates a new line of terrain every frame, increasing its width (in x). 
    /// The <paramref name="length"/> of the terrain (in z) is fixed. The amount of increase in width is specified by <paramref name="step"/>. 
    /// Height and noise are determined by <paramref name="heightfactor"/> and <paramref name="noiseFactor"/>, respectively. 
    /// The direction of the terrain is specified by <paramref name="terrainDir"/>.
    /// Creation of new vertices in each frame is according to the specified frequency band.
    /// </summary>
    /// <param name="length"></param>
    /// <param name="startWidth"></param>
    /// <param name="step"></param>
    /// <param name="heightfactor"></param>
    /// <param name="noiseFactor"></param>
    /// <param name="terrainDir"></param>
    /// <returns></returns>
    public GameObject BandsGenerateTerrain(int length, float startWidth, float step, float heightfactor, float noiseFactor, Vector3 terrainDir)
    {
        GameObject terrain = GenericSoundReact.GenerateTerrain(length, startWidth, step, heightfactor, noiseFactor, terrainDir);
        return terrain;
    }

    /// <summary>
    /// Creates and returns a GameObject, which is an instance of <paramref name="obj"/>, in the position determined by <paramref name="position"/> and
    /// with the rotation specified by <paramref name="rotation"/>.
    /// The instantiation is produced when the specified frequency band value is greater than <paramref name="bandThreshold"/>.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="amplitudeThreshold"></param>
    /// <returns></returns>
    public GameObject BandInstantiate(UnityEngine.Object obj, int band, Vector3 position, Quaternion rotation, float bandThreshold, float spawnTime)
    {
        GameObject go = GenericSoundReact.SoundInstantiate(obj, position, rotation, audioInput.GetBandBuffer(band) > bandThreshold, spawnTime);
        return go;
    }

    /// <summary>
    /// Returns a GameObject that generate a new piece of trail every frame between the specified vertices of a custom polygon defined 
    /// by <paramref name="polygonVert"/>.
    /// The amount of the generated piece of trail in every frame is according to the specified frequency band.
    /// </summary>
    /// <param name="polygonVert"></param>
    /// <param name="lineColor"></param>
    /// <param name="lineWidth"></param>
    /// <param name="drawSpeedFactor"></param>
    /// <returns></returns>
    public GameObject BandDrawPolygon(Vector3[] polygonVert, int band, Color lineColor, float lineWidth, float drawSpeedFactor)
    {
        GameObject polygon = GenericSoundReact.DrawPolygon(polygonVert, lineColor, lineWidth, drawSpeedFactor, GenericSoundReact.MusicDataType.FreqBand, band);

        return polygon;
    }

    #endregion
}
