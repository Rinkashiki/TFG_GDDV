using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GenericSoundReact : MonoBehaviour
{
    #region Generic_Sound_React_Variables
    private static float timeStamp;
    #endregion

    #region Generic_Sound_React_Const

    /// <summary>
    /// Defines musical input data types.
    /// </summary>
    public enum MusicDataType { Amplitude, FreqBand, MidiPlay, MidiRecord };

    /// <summary>
    /// Defines Rigigbody properties that are floats
    /// </summary>
    public enum FloatPhysicProperties { angularDrag, drag, mass, inertia };

    /// <summary>
    /// Defines Rigidbody properties that are vectors.
    /// </summary>
    public enum VectorPhysicProperties { angularVelocity, centerOfMass, inertiaTensor, velocity };

    /// <summary>
    /// Defines property types tha can be changed in a shader/material.
    /// </summary>
    public enum MatPropertyType { ComputeBuffer, Color, ColorArray, Float, FloatArray, Int, Matrix4x4, Matrix4x4Array, Texture, Vector4, Vector4Array };

    #endregion

    #region Generic_Change_Property_Functions

    /// <summary>
    /// Moves <paramref name="go"/> along <paramref name="axis"/>. The amount of movement is specified by <paramref name="translationFactor"/>.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="axis"></param>
    /// <param name="translationFactor"></param>
    /// <param name="property"></param>
    public static void ChangeTranslation(GameObject go, Vector3 axis, float translationFactor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        go.transform.Translate(new Vector3((value * translationFactor * axis.x),
                                              (value * translationFactor * axis.y),
                                              (value * translationFactor * axis.z)));
    }

    /// <summary>
    /// Rotates <paramref name="go"/> over <paramref name="axis"/>. The amount of rotation is specified by <paramref name="rotFactor"/>.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="axis"></param>
    /// <param name="rotFactor"></param>
    /// <param name="property"></param>
    public static void ChangeRotation(GameObject go, Vector3 axis, float rotFactor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        go.transform.Rotate(axis, value * rotFactor);
    }

    /// <summary>
    /// Modifies the scale of <paramref name="go"/> along <paramref name="axis"/>. The initial an minimum scale is specified 
    /// by <paramref name="initialScale"/> and the the scale amount by <paramref name="scaleFactor"/>.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="axis"></param>
    /// <param name="scaleFactor"></param>
    /// <param name="property"></param>
    /// <param name="initialScale"></param>
    public static void ChangeScale(GameObject go, Vector3 axis, float scaleFactor, Numeric property, float initialScale = 1)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        go.transform.localScale = new Vector3((value * scaleFactor * axis.x) + initialScale,
                                              (value * scaleFactor * axis.y) + initialScale,
                                              (value * scaleFactor * axis.z) + initialScale);
    }

    /// <summary>
    /// Modifies the bright of the color in the material associated to <paramref name="go"/>. The initial an minimum bright is specified 
    /// by <paramref name="initialColor"/> and the the bright amount by <paramref name="brightFactor"/>.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="brightFactor"></param>
    /// <param name="initialColor"></param>
    /// <param name="property"></param>
    public static void ChangeBright(GameObject go, float brightFactor, Color initialColor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        float brightValue = value * brightFactor;
        Color color = new Color(initialColor.r + brightValue, initialColor.g + brightValue, initialColor.b + brightValue);
        go.GetComponent<MeshRenderer>().material.color = color;
    }

    /// <summary>
    /// Modifies the color in the material associated to <paramref name="go"/>. The material changes to <paramref name="color"/> in 
    /// the specified <paramref name="transitionTime"/>.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="color"></param>
    /// <param name="transitionTime"></param>
    public static void ChangeColor(GameObject go, Color color, float transitionTime)
    {
        Color prevColor = go.GetComponent<MeshRenderer>().material.color;
        go.GetComponent<MeshRenderer>().material.color = Color.Lerp(prevColor, color, Mathf.Clamp01(transitionTime));
    }

    /// <summary>
    /// Modifies <paramref name="mesh"/> vertices height. The amount of height variation is specified by <paramref name="reliefFactor"/>
    /// and the amount of noise by <paramref name="noiseFactor"/>. Vertices initial positions are defined by <paramref name="initPos"/>.
    /// Vertices animation speed is specified by <paramref name="waveSpeed"/>
    /// </summary>
    /// <param name="mesh"></param>
    /// <param name="noiseFactor"></param>
    /// <param name="reliefFactor"></param>
    /// <param name="initPos"></param>
    /// <param name="waveSpeed"></param>
    /// <param name="property"></param>
    public static void ChangeReliefMap(Mesh mesh, float noiseFactor, float reliefFactor, Vector3[] initPos, float waveSpeed, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;
        Vector2[] uvs = mesh.uv;

        Vector3 normal = Vector3.zero;
        Vector3 factor = Vector3.zero;
        float x, y, z;

        // For dispair normals and uvs in repeated vertices
        List<int> checkedVerts = new List<int>();

        for (int i = 0; i < vertices.Length; i++)
        {
            List<int> repeatedVerts = new List<int>();
            List<Vector3> initialNormals = new List<Vector3>();
            List<Vector2> initialUvs = new List<Vector2>();
            repeatedVerts.Add(i);
            initialNormals.Add(normals[i]);
            initialUvs.Add(uvs[i]);
            if (!checkedVerts.Contains(i))
                checkedVerts.Add(i);

            for (int j = 0; j < vertices.Length; j++)
            {
                if (vertices[i] == vertices[j])
                {
                    if (!repeatedVerts.Contains(j) && !checkedVerts.Contains(j))
                    {
                        repeatedVerts.Add(j);
                        checkedVerts.Add(j);
                        initialNormals.Add(normals[j]);
                        initialUvs.Add(uvs[j]);
                    }
                }
            }

            if (repeatedVerts.Count < 2)
                repeatedVerts.Clear();

            for (int k = 0; k < repeatedVerts.Count; k++)
            {
                for (int l = 0; l < repeatedVerts.Count; l++)
                {
                    if (l != k)
                    {
                        normals[repeatedVerts[k]] += initialNormals[l];
                        uvs[repeatedVerts[k]] += initialUvs[l];
                    }
                }

                normals[repeatedVerts[k]] = normals[repeatedVerts[k]].normalized;
                uvs[repeatedVerts[k]] = Vector2.ClampMagnitude(uvs[repeatedVerts[k]], 1);
            }
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            normal = normals[i].normalized;
            factor = value * normal * reliefFactor;
            x = Mathf.Cos(uvs[i].x * 2 * Mathf.PI) * Mathf.Cos(uvs[i].y * Mathf.PI - Mathf.PI / 2) + (Time.timeSinceLevelLoad * waveSpeed);
            y = Mathf.Sin(uvs[i].y * Mathf.PI - Mathf.PI / 2) + (Time.timeSinceLevelLoad * waveSpeed);
            z = Mathf.Sin(uvs[i].x * 2 * Mathf.PI) * Mathf.Cos(uvs[i].y * Mathf.PI - Mathf.PI / 2) + (Time.timeSinceLevelLoad * waveSpeed);
            //vertices[i] = factor * Mathf.Clamp(PerlinNoise3D(x * noiseFactor, y * noiseFactor, z * noiseFactor), 0.4f, 0.8f) + initPos[i];
            //vertices[i] = factor * Mathf.PerlinNoise(Mathf.PerlinNoise(x * noiseFactor, y * noiseFactor), z * noiseFactor) + initPos[i];
            vertices[i] = factor * PerlinNoise3D(x * noiseFactor, y * noiseFactor, z * noiseFactor) + initPos[i];
        }

        mesh.vertices = vertices;
    }

    /// <summary>
    /// Modifies <paramref name="light"/> intensity. The amount of this intensity is specified by <paramref name="intensityFactor"/>.
    /// The initial an minimum intensity is specified by <paramref name="initialIntensity"/>.
    /// </summary>
    /// <param name="light"></param>
    /// <param name="intensityFactor"></param>
    /// <param name="property"></param>
    /// <param name="initialIntensity"></param>
    public static void ChangeLightIntensity(Light light, float intensityFactor, Numeric property, float initialIntensity = 1)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        light.intensity = initialIntensity + intensityFactor * value;
    }

    /// <summary>
    /// Modifies <paramref name="light"/> range. The amount of this range is specified by <paramref name="rangeFactor"/>.
    /// The initial an minimum range is specified by <paramref name="initialRange"/>.
    /// </summary>
    /// <param name="light"></param>
    /// <param name="rangeFactor"></param>
    /// <param name="property"></param>
    /// <param name="initialRange"></param>
    public static void ChangeLightRange(Light light, float rangeFactor, Numeric property, float initialRange = 1)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        light.range = initialRange + rangeFactor * value;
    }

    /// <summary>
    /// Modifies <paramref name="mat"/> shader/material property named <paramref name="propertyName"/> of type <paramref name="propertyType"/>.
    /// The amount of change is specified by <paramref name="propertyFactor"/>.
    /// </summary>
    /// <param name="mat"></param>
    /// <param name="propertyName"></param>
    /// <param name="propertyType"></param>
    /// <param name="propertyFactor"></param>
    /// <param name="property"></param>
    public static void ChangeShaderGraphMatProperty(Material mat, string propertyName, MatPropertyType propertyType, float propertyFactor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        switch (propertyType)
        {
            case MatPropertyType.ComputeBuffer:
                break;

            case MatPropertyType.Color:
                break;

            case MatPropertyType.ColorArray:
                break;

            case MatPropertyType.Float:
                mat.SetFloat(propertyName, value * propertyFactor);
                break;

            case MatPropertyType.FloatArray:
                break;

            case MatPropertyType.Int:
                mat.SetInt(propertyName, (int)(value * propertyFactor));
                break;

            case MatPropertyType.Matrix4x4:
                break;

            case MatPropertyType.Matrix4x4Array:
                break;

            case MatPropertyType.Texture:
                break;

            case MatPropertyType.Vector4:
                break;

            case MatPropertyType.Vector4Array:
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Modifies <paramref name="anim"/> animation speed. The amount of change in speed is specified by <paramref name="speedFactor"/>.
    /// The initial an minimum speed is specified by <paramref name="initialSpeed"/>.
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="speedFactor"></param>
    /// <param name="property"></param>
    /// <param name="initialSpeed"></param>
    public static void ChangeAnimationSpeed(Animator anim, float speedFactor, Numeric property, float initialSpeed = 1)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        anim.speed = initialSpeed + value * speedFactor;
    }

    #endregion

    #region Generic_Change_Post_Processing_Functions

    /// <summary>
    /// Modifies the <paramref name="bloom"/> of the Global Volume. The amount of change is specified by <paramref name="bloomFactor"/>.
    /// The initial an minimum bloom is specified by <paramref name="initialBloom"/>.
    /// </summary>
    /// <param name="bloom"></param>
    /// <param name="bloomFactor"></param>
    /// <param name="property"></param>
    /// <param name="initialBloom"></param>
    public static void ChangeBloom(Bloom bloom, float bloomFactor, Numeric property, float initialBloom = 0)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        value = initialBloom + value * bloomFactor;

        bloom.intensity.value = value;
    }

    /// <summary>
    /// Modifies the chromatic aberration of the Global Volume. The amount of change is specified by <paramref name="caFactor"/>.
    /// The initial an minimum chromatic aberration is specified by <paramref name="initialCA"/>.
    /// </summary>
    /// <param name="ca"></param>
    /// <param name="caFactor"></param>
    /// <param name="property"></param>
    /// <param name="initialCA"></param>
    public static void ChangeChromaticAberration(ChromaticAberration ca, float caFactor, Numeric property, float initialCA = 0)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        value = Mathf.Clamp01(initialCA + value * caFactor);

        ca.intensity.value = value;
    }

    /// <summary>
    /// Modifies the <paramref name="vignette"/> of the Global Volume. The amount of change is specified by <paramref name="vignetteFactor"/>.
    /// The initial an minimum vignette is specified by <paramref name="initialVignette"/>.
    /// </summary>
    /// <param name="vignette"></param>
    /// <param name="vignetteFactor"></param>
    /// <param name="property"></param>
    /// <param name="initialVignette"></param>
    public static void ChangeVignette(Vignette vignette, float vignetteFactor, Numeric property, float initialVignette = 0)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        value = initialVignette + (1 - value * vignetteFactor);

        vignette.intensity.value = value;
    }

    #endregion

    #region Generic_Physic_Functions

    /// <summary>
    /// Modifies 3D float physic property specified by  <paramref name="fpp"/> of <paramref name="body"/>. The amount of change is 
    /// specified by <paramref name="fppFactor"/>. The initial an minimum property value is specified by <paramref name="initialValue"/>.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="fpp"></param>
    /// <param name="fppFactor"></param>
    /// <param name="property"></param>
    /// <param name="initialValue"></param>
    public static void ChangePhysicProperty(Rigidbody body, FloatPhysicProperties fpp, float fppFactor, Numeric property, float initialValue = 0)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();
        float newFpp = fppFactor * value;

        switch (fpp)
        {
            case FloatPhysicProperties.angularDrag:
                body.angularDrag = initialValue + newFpp;
                break;

            case FloatPhysicProperties.drag:
                body.drag = initialValue + newFpp;
                break;

            case FloatPhysicProperties.mass:
                body.mass = initialValue + newFpp;
                break;

            default:
                break;
        }

    }

    /// <summary>
    /// Modifies 3D vectorial physic property specified by <paramref name="vpp"/> of <paramref name="body"/>. The amount of change is 
    /// specified by <paramref name="vppFactor"/>. The initial an minimum property value is specified by <paramref name="initialValue"/>.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="vpp"></param>
    /// <param name="vppFactor"></param>
    /// <param name="property"></param>
    /// <param name="initialValue"></param>
    public static void ChangePhysicProperty(Rigidbody body, VectorPhysicProperties vpp, Vector3 vppFactor, Numeric property, Vector3 initialValue = new Vector3())
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();
        Vector3 newVpp = value * vppFactor;

        switch (vpp)
        {
            case VectorPhysicProperties.centerOfMass:
                body.centerOfMass = initialValue + newVpp;
                break;

            case VectorPhysicProperties.inertiaTensor:
                body.inertiaTensor = initialValue + newVpp;
                break;

            case VectorPhysicProperties.velocity:
                body.velocity = initialValue + newVpp;
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Modifies 2D float physic property specified by <paramref name="fpp"/> of <paramref name="body"/>. The amount of change is 
    /// specified by <paramref name="fppFactor"/>. The initial an minimum property value is specified by <paramref name="initialValue"/>.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="fpp"></param>
    /// <param name="fppFactor"></param>
    /// <param name="property"></param>
    /// <param name="initialValue"></param>
    public static void ChangePhysicProperty2D(Rigidbody2D body, FloatPhysicProperties fpp, float fppFactor, Numeric property, float initialValue = 0)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();
        float newFpp = fppFactor * value;

        switch (fpp)
        {
            case FloatPhysicProperties.angularDrag:
                body.angularDrag = initialValue + newFpp;
                break;

            case FloatPhysicProperties.drag:
                body.drag = initialValue + newFpp;
                break;

            case FloatPhysicProperties.inertia:
                body.inertia = initialValue + newFpp;
                break;

            case FloatPhysicProperties.mass:
                body.mass = initialValue + newFpp;
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Modifies 2D vectorial physic property specified by <paramref name="vpp"/> of <paramref name="body"/>. The amount of change is 
    /// specified by <paramref name="vppFactor"/>. The initial an minimum property value is specified by <paramref name="initialValue"/>.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="vpp"></param>
    /// <param name="vppFactor"></param>
    /// <param name="property"></param>
    /// <param name="initialValue"></param>
    public static void ChangePhysicProperty2D(Rigidbody2D body, VectorPhysicProperties vpp, Vector2 vppFactor, Numeric property, Vector2 initialValue = new Vector2())
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();
        Vector2 newVpp = vppFactor * value;

        switch (vpp)
        {
            case VectorPhysicProperties.centerOfMass:
                body.centerOfMass = initialValue + newVpp;
                break;


            case VectorPhysicProperties.velocity:
                body.velocity = initialValue + newVpp;
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Add a 3D force to <paramref name="body"/> in the direction of <paramref name="forceDir"/>. The type of the applied force 
    /// is specified by <paramref name="mode"/> and the amount of force by <paramref name="forceFactor"/>.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="forceDir"></param>
    /// <param name="mode"></param>
    /// <param name="forceFactor"></param>
    /// <param name="property"></param>
    public static void CustomAddForce(Rigidbody body, Vector3 forceDir, ForceMode mode, float forceFactor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();
        body.AddForce(value * forceFactor * forceDir, mode);
    }

    /// <summary>
    /// Add a 2D force to <paramref name="body"/> in the direction of <paramref name="forceDir"/>. The type of the applied force 
    /// is specified by <paramref name="mode"/> and the amount of force by <paramref name="forceFactor"/>.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="forceDir"></param>
    /// <param name="mode"></param>
    /// <param name="forceFactor"></param>
    /// <param name="property"></param>
    public static void CustomAddForce2D(Rigidbody2D body, Vector2 forceDir, ForceMode2D mode, float forceFactor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();
        body.AddForce(value * forceFactor * forceDir, mode);
    }

    #endregion

    #region Generic_Create_Functions

    /// <summary>
    /// Creates and returns a GameObject that generates a new line of terrain every frame, increasing its width (in x). 
    /// The <paramref name="length"/> of the terrain (in z) is fixed. The amount of increase in width is specified by <paramref name="step"/>.
    /// Height and noise are determined by <paramref name="heightfactor"/> and <paramref name="noiseFactor"/>, respectively. 
    /// The direction of the terrain is specified by <paramref name="terrainDir"/>. The terrain´s material is specified by <paramref name="mat"/>
    /// </summary>
    /// <param name="length"></param>
    /// <param name="step"></param>
    /// <param name="heightfactor"></param>
    /// <param name="noiseFactor"></param>
    /// <param name="terrainDir"></param>
    /// <param name="mat"></param>
    /// <returns></returns>
    public static GameObject GenerateTerrain(int length, float step, float heightfactor, float noiseFactor, Vector3 terrainDir, Material mat)
    {
        GameObject terrainObj = new GameObject();
        terrainObj.name = "terrain";
        terrainObj.AddComponent<MeshFilter>();
        terrainObj.AddComponent<MeshRenderer>();
        GenerateTerrain terrain = terrainObj.AddComponent<GenerateTerrain>();
        terrain.SetParams(terrainObj.GetComponent<MeshFilter>().mesh, terrainObj.GetComponent<MeshRenderer>(), mat, length, step, heightfactor, noiseFactor, terrainDir);

        return terrainObj;
    }

    /// <summary>
    /// Creates and returns a GameObject, which is an instance of <paramref name="obj"/>, in the position determined by <paramref name="position"/> and
    /// with the rotation specified by <paramref name="rotation"/>. The instantiation is produced when <paramref name="soundOption"/> is true and 
    /// <paramref name="spawnTime"/> has passed between instances.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="soundOption"></param>
    /// <param name="spawnTime"></param>
    /// <returns></returns>
    public static GameObject SoundInstantiate(UnityEngine.Object obj, Vector3 position, Quaternion rotation, bool soundOption, float spawnTime)
    {
        bool timeOption = Time.time - timeStamp >= spawnTime;
        if (soundOption && timeOption)
        {
            timeStamp = Time.time;
            GameObject go = (GameObject)Instantiate(obj, position, rotation);
            return go;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Creates and returns a GameObject that generate a new piece of trail every frame between the specified vertices of a custom polygon defined 
    /// by <paramref name="polygonVert"/>.
    /// </summary>
    /// <param name="polygonVert"></param>
    /// <param name="lineColor"></param>
    /// <param name="lineWidth"></param>
    /// <param name="drawSpeedFactor"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static GameObject DrawPolygon(Vector3[] polygonVert, Color lineColor, float lineWidth, float drawSpeedFactor, MusicDataType type, int band = -1)
    {
        GameObject polygon = new GameObject();
        polygon.name = "polygon";
        polygon.AddComponent<LineRenderer>();
        DrawPolygon line = polygon.AddComponent<DrawPolygon>();
        line.SetParams(polygonVert, lineColor, lineWidth, drawSpeedFactor, type);
        line.SetFreqBand(band);

        return polygon;
    }

    /// <summary>
    /// Creates and returns a GameObject that generates a piece of trail when a note of MIDI keyboard is played. 
    /// The direction of the trail is determined by the association with MIDI notes in <paramref name="numberDirAssociation"/>.
    /// </summary>
    /// <param name="numberDirAssociation"></param>
    /// <param name="lineColor"></param>
    /// <param name="lineWidth"></param>
    /// <param name="drawSpeedFactor"></param>
    /// <returns></returns>
    public static GameObject KeyboardDrawPolygon(Dictionary<int, Vector2> numberDirAssociation, Color lineColor, float lineWidth, float drawSpeedFactor)
    {
        GameObject keyboardPolygon = new GameObject();
        keyboardPolygon.name = "keyboardPolygon";
        keyboardPolygon.AddComponent<LineRenderer>();
        KeyBoardDrawPolygon line = keyboardPolygon.AddComponent<KeyBoardDrawPolygon>();
        line.SetParams(numberDirAssociation, lineColor, lineWidth, drawSpeedFactor);

        return keyboardPolygon;
    }

    /// <summary>
    /// Creates and returns a GameObject that generates a phyllotaxis trail. Degree between one line and the next is specified by
    /// <paramref name="phyllotaxisDegree"/>. Speed of trail is specified by <paramref name="speedFactor"/> and the amount of scale is determined by
    /// <paramref name="scaleFactor"/>. The initial and minimum scale is determined by <paramref name="initialScale"/>. The phyllotaxis changes between
    /// opening and closing when reaching the specified <paramref name="loops"/>.
    /// </summary>
    /// <param name="phyllotaxisDegree"></param>
    /// <param name="speedFactor"></param>
    /// <param name="scaleFactor"></param>
    /// <param name="type"></param>
    /// <param name="initialScale"></param>
    /// <param name="loops"></param>
    /// <param name="band"></param>
    /// <returns></returns>
    public static GameObject GeneratePhyllotaxis(float phyllotaxisDegree, float speedFactor, float scaleFactor, MusicDataType type, float initialScale = 0, int loops = 10, int band = -1)
    {
        GameObject phylloObj = new GameObject();
        phylloObj.name = "phyllotaxis";
        phylloObj.AddComponent<TrailRenderer>();
        Phyllotaxis phyllotaxis = phylloObj.AddComponent<Phyllotaxis>();
        phyllotaxis.SetParams(phyllotaxisDegree, speedFactor, scaleFactor, type, initialScale, loops);
        phyllotaxis.SetFreqBand(band);

        return phylloObj;
    }

    /// <summary>
    /// Creates and returns a GameObject that generates a tunnel using a phyllotaxis trail. The advance speed inside the tunnel is specified by 
    /// <paramref name="tunnelSpeed"/>. The <paramref name="cameraTransform"/> refers to the camera that we want to follows inside the tunnel,
    /// always keeping <paramref name="cameraDistance"/> with the end of it. If <paramref name="cameraTransform"/> is not specified,
    /// the camera will not follow inside the tunnel.
    /// </summary>
    /// <param name="tunnelSpeed"></param>
    /// <param name="phyllotaxisDegree"></param>
    /// <param name="speedFactor"></param>
    /// <param name="scaleFactor"></param>
    /// <param name="type"></param>
    /// <param name="cameraDistance"></param>
    /// <param name="cameraTransform"></param>
    /// <param name="initialScale"></param>
    /// <param name="band"></param>
    /// <returns></returns>
    public static GameObject GeneratePhyllotunnel(float tunnelSpeed, float phyllotaxisDegree, float speedFactor, float scaleFactor, MusicDataType type, float cameraDistance = -10, Transform cameraTransform = null, float initialScale = 0, int band = -1)
    {
        // Generate PhylloTunnel 
        GameObject tunnelObj = new GameObject();
        tunnelObj.name = "phyllotunnel";

        PhylloTunnel phyllotunnel = tunnelObj.AddComponent<PhylloTunnel>();
        phyllotunnel.SetParams(tunnelSpeed, cameraDistance, cameraTransform);
        phyllotunnel.SetFreqBand(band);

        // Generate the Phyllotaxis for the tunnel
        GameObject phylloObj = new GameObject();
        phylloObj.name = "phyllotaxis";
        phylloObj.AddComponent<TrailRenderer>();

        Phyllotaxis phyllotaxis = phylloObj.AddComponent<Phyllotaxis>();
        phyllotaxis.SetParams(phyllotaxisDegree, speedFactor, scaleFactor, type, initialScale);
        phyllotaxis.SetTunnel(true);
        phyllotaxis.SetFreqBand(band);

        // Assign phyllotaxis as tunnel's child
        phylloObj.transform.SetParent(tunnelObj.transform);

        return tunnelObj;
    }

    #endregion

    #region Other_Utility_Functions

    private static float PerlinNoise3D(float x, float y, float z)
    {
        y += 1;
        z += 2;
        float xy = _perlin3DFixed(x, y);
        float xz = _perlin3DFixed(x, z);
        float yz = _perlin3DFixed(y, z);
        float yx = _perlin3DFixed(y, x);
        float zx = _perlin3DFixed(z, x);
        float zy = _perlin3DFixed(z, y);

        return xy * xz * yz * yx * zx * zy;
    }

    static float _perlin3DFixed(float a, float b)
    {
        return Mathf.Sin(Mathf.PI * Mathf.PerlinNoise(a, b));
    }

    #endregion
}
