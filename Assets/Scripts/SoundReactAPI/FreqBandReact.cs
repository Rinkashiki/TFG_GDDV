using System.Collections;
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
    /// by <paramref name="initialScale"/> and the the scale amount by <paramref name="scaleFactor"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="band"></param>
    /// <param name="axis"></param>
    /// <param name="scaleFactor"></param>
    /// <param name="initialScale"></param>
    public void BandScale(GameObject go, int band, Vector3 axis, float scaleFactor, float initialScale = 1)
    {
        GenericSoundReact.ChangeScale(go, axis, scaleFactor, new Numeric(audioInput.GetBandBuffer(band)), initialScale);
    }

    /// <summary>
    /// Modifies the bright of the color in the material associated to <paramref name="go"/>. The initial an minimum bright is specified 
    /// by <paramref name="initialColor"/> and the the bright amount by <paramref name="brightFactor"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="band"></param>
    /// <param name="brightFactor"></param>
    /// <param name="initialColor"></param>
    public void BandBright(GameObject go, int band, float brightFactor, Color initialColor)
    {
        GenericSoundReact.ChangeBright(go, brightFactor, initialColor, new Numeric(audioInput.GetBandBuffer(band)));
    }

    /// <summary>
    /// Modifies the color in the material associated to <paramref name="go"/>. The material changes to <paramref name="color"/> in 
    /// the specified <paramref name="transitionTime"/>.
    /// The change is produced when the frequency band value is greater than <paramref name="bandThreshold"/>.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="color"></param>
    /// <param name="transitionTime"></param>
    /// <param name="bandThreshold"></param>
    public void BandColor(GameObject go, int band, Color color, float transitionTime, float bandThreshold)
    {
        if (audioInput.GetBandBuffer(band) > bandThreshold)
            GenericSoundReact.ChangeColor(go, color, transitionTime);
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
    /// The initial an minimum intensity is specified by <paramref name="initialIntensity"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="light"></param>
    /// <param name="intensityFactor"></param>
    /// <param name="initialIntensity"></param>
    public void BandLightIntensity(Light light, int band, float intensityFactor, float initialIntensity = 1)
    {
        GenericSoundReact.ChangeLightIntensity(light, intensityFactor, new Numeric(audioInput.GetBandBuffer(band)), initialIntensity);
    }

    /// <summary>
    /// Modifies <paramref name="light"/> range. The amount of this range is specified by <paramref name="rangeFactor"/>.
    /// The initial an minimum range is specified by <paramref name="initialRange"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="light"></param>
    /// <param name="rangeFactor"></param>
    /// <param name="initialRange"></param>
    public void BandLightRange(Light light, int band, float rangeFactor, float initialRange)
    {
        GenericSoundReact.ChangeLightRange(light, rangeFactor, new Numeric(audioInput.GetBandBuffer(band)), initialRange);
    }

    /// <summary>
    /// Modifies <paramref name="mat"/> shader/material property named <paramref name="propertyName"/> of type <paramref name="propertyType"/>.
    /// The amount of change is specified by <paramref name="propertyFactor"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="mat"></param>
    /// <param name="band"></param>
    /// <param name="propertyName"></param>
    /// <param name="propertyType"></param>
    /// <param name="propertyFactor"></param>
    public void BandShaderGraphMatProperty(Material mat, int band, string propertyName, GenericSoundReact.MatPropertyType propertyType, float propertyFactor)
    {
        GenericSoundReact.ChangeShaderGraphMatProperty(mat, propertyName, propertyType, propertyFactor, new Numeric(audioInput.GetBandBuffer(band)));
    }

    /// <summary>
    /// Modifies <paramref name="anim"/> animation speed. The amount of change in speed is specified by <paramref name="speedFactor"/>.
    /// The initial an minimum intensity is specified by <paramref name="initialSpeed"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="band"></param>
    /// <param name="speedFactor"></param>
    /// <param name="initialSpeed"></param>
    public void BandAnimationSpeed(Animator anim, int band, float speedFactor, float initialSpeed = 0)
    {
        GenericSoundReact.ChangeAnimationSpeed(anim, speedFactor, new Numeric(audioInput.GetBandBuffer(band)), initialSpeed);
    }

    /// <summary>
    /// Modifies the <paramref name="bloom"/> of the Global Volume. The amount of change is specified by <paramref name="bloomFactor"/>.
    /// The initial an minimum bloom is specified by <paramref name="initialBloom"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="bloom"></param>
    /// <param name="band"></param>
    /// <param name="bloomFactor"></param>
    /// <param name="initialBloom"></param>
    public void BandBloom(Bloom bloom, int band, float bloomFactor, float initialBloom = 0)
    {
        GenericSoundReact.ChangeBloom(bloom, bloomFactor, new Numeric(audioInput.GetBandBuffer(band)), initialBloom);
    }

    /// <summary>
    /// Modifies the chromatic aberration of the Global Volume. The amount of change is specified by <paramref name="caFactor"/>.
    /// The initial an minimum chromatic aberration is specified by <paramref name="initialCA"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="ca"></param>
    /// <param name="band"></param>
    /// <param name="caFactor"></param>
    /// <param name="initialCA"></param>
    public void BandChromaticAberration(ChromaticAberration ca, int band, float caFactor, float initialCA = 0)
    {
        GenericSoundReact.ChangeChromaticAberration(ca, caFactor, new Numeric(audioInput.GetBandBuffer(band)), initialCA);
    }

    /// <summary>
    /// Modifies the <paramref name="vignette"/> of the Global Volume. The amount of change is specified by <paramref name="vignetteFactor"/>.
    /// The initial an minimum vignette is specified by <paramref name="initialVignette"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="vignette"></param>
    /// <param name="band"></param>
    /// <param name="vignetteFactor"></param>
    /// <param name="initialVignette"></param>
    public void BandVignette(Vignette vignette, int band, float vignetteFactor, float initialVignette = 0)
    {
        GenericSoundReact.ChangeVignette(vignette, vignetteFactor, new Numeric(audioInput.GetBandBuffer(band)), initialVignette);
    }

    /// <summary>
    ///Modifies 3D float physic property specified by  <paramref name="fpp"/> of <paramref name="body"/>. The amount of change is 
    /// specified by <paramref name="fppFactor"/>. The initial an minimum property value is specified by <paramref name="initialValue"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="band"></param>
    /// <param name="fpp"></param>
    /// <param name="fppFactor"></param>
    /// <param name="initialValue"></param>
    public void BandPhysicProperty(Rigidbody body, int band, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor, float initialValue = 0)
    {
        GenericSoundReact.ChangePhysicProperty(body, fpp, fppFactor, new Numeric(audioInput.GetBandBuffer(band)), initialValue);
    }

    /// <summary>
    /// Modifies 3D vectorial physic property specified by <paramref name="vpp"/> of <paramref name="body"/>. The amount of change is 
    /// specified by <paramref name="vppFactor"/>. The initial an minimum property value is specified by <paramref name="initialValue"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="band"></param>
    /// <param name="vpp"></param>
    /// <param name="vppFactor"></param>
    /// <param name="initialValue"></param>
    public void BandPhysicProperty(Rigidbody body, int band, GenericSoundReact.VectorPhysicProperties vpp, Vector3 vppFactor, Vector3 initialValue = new Vector3())
    {
        GenericSoundReact.ChangePhysicProperty(body, vpp, vppFactor, new Numeric(audioInput.GetBandBuffer(band)), initialValue);
    }

    /// <summary>
    /// Modifies 2D float physic property specified by <paramref name="fpp"/> of <paramref name="body"/>. The amount of change is 
    /// specified by <paramref name="fppFactor"/>. The initial an minimum property value is specified by <paramref name="initialValue"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="band"></param>
    /// <param name="fpp"></param>
    /// <param name="fppFactor"></param>
    /// <param name="initialValue"></param>
    public void BandPhysicProperty2D(Rigidbody2D body, int band, GenericSoundReact.FloatPhysicProperties fpp, float fppFactor, float initialValue = 0)
    {
        GenericSoundReact.ChangePhysicProperty2D(body, fpp, fppFactor, new Numeric(audioInput.GetBandBuffer(band)), initialValue);
    }

    /// <summary>
    /// Modifies 2D vectorial physic property specified by <paramref name="vpp"/> of <paramref name="body"/>. The amount of change is 
    /// specified by <paramref name="vppFactor"/>. The initial an minimum property value is specified by <paramref name="initialValue"/>.
    /// Changes are made using the specified frequency band.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="band"></param>
    /// <param name="vpp"></param>
    /// <param name="vppFactor"></param>
    /// <param name="initialValue"></param>
    public void BandPhysicProperty2D(Rigidbody2D body, int band, GenericSoundReact.VectorPhysicProperties vpp, Vector2 vppFactor, Vector2 initialValue = new Vector2())
    {
        GenericSoundReact.ChangePhysicProperty2D(body, vpp, vppFactor, new Numeric(audioInput.GetBandBuffer(band)), initialValue);
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
    /// Creates and returns a GameObject that generates a new line of terrain every frame, increasing its width (in x). 
    /// The <paramref name="length"/> of the terrain (in z) is fixed. The amount of increase in width is specified by <paramref name="step"/>. 
    /// Height and noise are determined by <paramref name="heightfactor"/> and <paramref name="noiseFactor"/>, respectively. 
    /// The direction of the terrain is specified by <paramref name="terrainDir"/>. The terrain´s material is specified by <paramref name="mat"/>
    /// Creation of new vertices in each frame is according to all frequency bands.
    /// </summary>
    /// <param name="length"></param>
    /// <param name="step"></param>
    /// <param name="heightfactor"></param>
    /// <param name="noiseFactor"></param>
    /// <param name="terrainDir"></param>
    /// <param name="mat"></param>
    /// <returns></returns>
    public GameObject BandsGenerateTerrain(int length, float step, float heightfactor, float noiseFactor, Vector3 terrainDir, Material mat)
    {
        GameObject terrain = GenericSoundReact.GenerateTerrain(length, step, heightfactor, noiseFactor, terrainDir, mat);
        return terrain;
    }

    /// <summary>
    /// Creates and returns a GameObject, which is an instance of <paramref name="obj"/>, in the position determined by <paramref name="position"/> and
    /// with the rotation specified by <paramref name="rotation"/>.
    /// The instantiation is produced when the specified frequency band value is greater than <paramref name="bandThreshold"/> and 
    /// <paramref name="spawnTime"/> has passed between instances.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="amplitudeThreshold"></param>
    /// <param name="spawnTime"></param>
    /// <returns></returns>
    public GameObject BandInstantiate(UnityEngine.Object obj, int band, Vector3 position, Quaternion rotation, float bandThreshold, float spawnTime)
    {
        GameObject go = GenericSoundReact.SoundInstantiate(obj, position, rotation, audioInput.GetBandBuffer(band) > bandThreshold, spawnTime);
        return go;
    }

    /// <summary>
    /// Creates and returns a GameObject that generate a new piece of trail every frame between the specified vertices of a custom polygon defined 
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

    /// <summary>
    /// Creates and returns a GameObject that generates a phyllotaxis trail. Degree between one line and the next is specified by
    /// <paramref name="phyllotaxisDegree"/>. Speed of trail is specified by <paramref name="speedFactor"/> and the amount of scale is determined by
    /// <paramref name="scaleFactor"/>. The initial and minimum scale is determined by <paramref name="initialScale"/>. The phyllotaxis changes between
    /// opening and closing when reaching the specified <paramref name="loops"/>.
    /// The amount of the generated piece of trail in every frame is according to the specified frequency band.
    /// </summary>
    /// <param name="band"></param>
    /// <param name="phyllotaxisDegree"></param>
    /// <param name="speedFactor"></param>
    /// <param name="scaleFactor"></param>
    /// <param name="initialScale"></param>
    /// <param name="loops"></param>
    /// <returns></returns>
    public GameObject BandPhyllotaxis(int band, float phyllotaxisDegree, float speedFactor, float scaleFactor, float initialScale = 0, int loops = 10)
    {
        GameObject phylloObj = GenericSoundReact.GeneratePhyllotaxis(phyllotaxisDegree, speedFactor, scaleFactor, GenericSoundReact.MusicDataType.FreqBand, initialScale, loops, band);
        return phylloObj;
    }

    /// <summary>
    /// Creates and returns a GameObject that generates a tunnel using a phyllotaxis trail. The advance speed inside the tunnel is specified by 
    /// <paramref name="tunnelSpeed"/>. The <paramref name="cameraTransform"/> refers to the camera that we want to follows inside the tunnel,
    /// always keeping <paramref name="cameraDistance"/> with the end of it. If <paramref name="cameraTransform"/> is not specified,
    /// the camera will not follow inside the tunnel.
    /// The amount of the generated piece of trail in every frame is according to the specified frequency band.
    /// </summary>
    /// <param name="band"></param>
    /// <param name="tunnelSpeed"></param>
    /// <param name="phyllotaxisDegree"></param>
    /// <param name="speedFactor"></param>
    /// <param name="scaleFactor"></param>
    /// <param name="cameraDistance"></param>
    /// <param name="cameraTransform"></param>
    /// <param name="initialScale"></param>
    /// <returns></returns>
    public GameObject BandPhyllotunnel(int band, float tunnelSpeed, float phyllotaxisDegree, float speedFactor, float scaleFactor, float cameraDistance = -10, Transform cameraTransform = null, float initialScale = 0)
    {
        GameObject tunnelObj = GenericSoundReact.GeneratePhyllotunnel(tunnelSpeed, phyllotaxisDegree, speedFactor, scaleFactor, GenericSoundReact.MusicDataType.FreqBand, cameraDistance, cameraTransform, initialScale, band);
        return tunnelObj;
    }

    #endregion
}
