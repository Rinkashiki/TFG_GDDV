using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GenericSoundReact : MonoBehaviour
{
    #region Generic_SoundReact_Const

    /// <summary>
    /// Defines musical input data types.
    /// </summary>
    public enum MusicDataType { Amplitude, FreqBand, Play_Velocity, Record_Velocity };

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
    /// by <paramref name="startScale"/> and the the scale amount by <paramref name="scaleFactor"/>.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="axis"></param>
    /// <param name="scaleFactor"></param>
    /// <param name="startScale"></param>
    /// <param name="property"></param>
    public static void ChangeScale(GameObject go, Vector3 axis, float scaleFactor, float startScale, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        go.transform.localScale = new Vector3((value * scaleFactor * axis.x) + startScale,
                                              (value * scaleFactor * axis.y) + startScale,
                                              (value * scaleFactor * axis.z) + startScale);
    }

    /// <summary>
    /// Modifies the bright of the color in the material associated to <paramref name="go"/>. The initial an minimum bright is specified 
    /// by <paramref name="startBright"/> and the the bright amount by <paramref name="brightFactor"/>.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="brightFactor"></param>
    /// <param name="startBrightness"></param>
    /// <param name="property"></param>
    public static void ChangeBright(GameObject go, float brightFactor, Color startColor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        float brightValue = value * brightFactor;
        Color color = new Color(startColor.r + brightValue, startColor.g + brightValue, startColor.b + brightValue);
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
        go.GetComponent<MeshRenderer>().material.color = Color.Lerp(prevColor, color, transitionTime);
    }

    /// <summary>
    /// Modifies <paramref name="mesh"/> vertices height. The amount of height variation is specified by <paramref name="heightFactor"/>
    /// and the amount of noise by <paramref name="noiseFactor"/>.
    /// </summary>
    /// <param name="mesh"></param>
    /// <param name="noiseFactor"></param>
    /// <param name="heightFactor"></param>
    /// <param name="property"></param>
    public static void ChangeTerrainHeightMap(Mesh mesh, float noiseFactor, float heightFactor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        Vector3[] vertices = mesh.vertices;

        int length = (int)(Mathf.Abs(mesh.bounds.max.z) + Mathf.Abs(mesh.bounds.min.z));
        int width = (int)(Mathf.Abs(mesh.bounds.max.x) + Mathf.Abs(mesh.bounds.min.x));

        for (int i = 0, z = 0; z <= length; z++)
        {
            for (int x = 0; x <= width; x++)
            {
                vertices[i].y = value * Mathf.PerlinNoise(x * noiseFactor, z * noiseFactor) * heightFactor;
                i++;
            }
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    /// <summary>
    /// Modifies <paramref name="mesh"/> vertices height. The amount of height variation is specified by <paramref name="heightFactor"/>
    /// and the amount of noise by <paramref name="noiseFactor"/>. Vertices initial positions are defined by <paramref name="initPos"/>.
    /// </summary>
    /// <param name="mesh"></param>
    /// <param name="noiseFactor"></param>
    /// <param name="heightFactor"></param>
    /// <param name="initPos"></param>
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
    /// </summary>
    /// <param name="light"></param>
    /// <param name="intensityFactor"></param>
    /// <param name="property"></param>
    public static void ChangeLightIntensity(Light light, float intensityFactor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        light.intensity = Mathf.Clamp(intensityFactor * value, 0, 8);
    }

    /// <summary>
    /// Modifies <paramref name="light"/> range. The amount of this range is specified by <paramref name="rangeFactor"/>.
    /// </summary>
    /// <param name="light"></param>
    /// <param name="rangeFactor"></param>
    /// <param name="property"></param>
    public static void ChangeLightRange(Light light, float rangeFactor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        light.range = rangeFactor * value;
    }

    /// <summary>
    /// Modifies <paramref name="mat"/> shader/material property named <paramref name="propertyName"/> of type <paramref name="propertyType"/>.
    /// The amount of change is specified by <paramref name="factor"/>.
    /// </summary>
    /// <param name="mat"></param>
    /// <param name="propertyName"></param>
    /// <param name="propertyType"></param>
    /// <param name="factor"></param>
    /// <param name="property"></param>
    public static void ChangeShaderGraphMatProperty(Material mat, string propertyName, MatPropertyType propertyType, float factor, Numeric property)
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
                mat.SetFloat(propertyName, value * factor);
                break;

            case MatPropertyType.FloatArray:
                break;

            case MatPropertyType.Int:
                mat.SetInt(propertyName, (int)(value * factor));
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
    /// Modifies <paramref name="anim"/> animation speed. The amount of change in speed is specified by factor.
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="factor"></param>
    /// <param name="property"></param>
    public static void ChangeAnimationSpeed(Animator anim, float factor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        anim.speed = value * factor;
    }

    #endregion

    #region Generic_Change_Post_Processing_Functions

    /// <summary>
    /// Modifies the <paramref name="bloom"/> of the Global Volume. The amount of change is specified by <paramref name="factor"/>.
    /// </summary>
    /// <param name="bloom"></param>
    /// <param name="factor"></param>
    /// <param name="property"></param>
    public static void ChangeBloom(Bloom bloom, float factor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        value = value * factor;

        bloom.intensity.value = value;
    }

    /// <summary>
    /// Modifies the chromatic aberration of the Global Volume. The amount of change is specified by <paramref name="factor"/>.
    /// </summary>
    /// <param name="ca"></param>
    /// <param name="factor"></param>
    /// <param name="property"></param>
    public static void ChangeChromaticAberration(ChromaticAberration ca, float factor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        value = Mathf.Clamp01(value * factor);

        ca.intensity.value = value;
    }

    #endregion

    #region Generic_Physic_Functions

    /// <summary>
    /// Modifies the specified 3D float physic property of <paramref name="body"/>. The amount of change is specified by <paramref name="fppFactor"/>.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="fpp"></param>
    /// <param name="fppFactor"></param>
    /// <param name="property"></param>
    public static void ChangePhysicProperty(Rigidbody body, FloatPhysicProperties fpp, float fppFactor, float initialValue, Numeric property)
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
    /// Modifies the specified 3D vectorial physic property of <paramref name="body"/> along <paramref name="axis"/>. 
    /// </summary>
    /// <param name="body"></param>
    /// <param name="vpp"></param>
    /// <param name="property"></param>
    /// <param name="axis"></param>
    public static void ChangePhysicProperty(Rigidbody body, VectorPhysicProperties vpp, Vector3 axis, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();
        Vector3 newVpp = axis * value;

        switch (vpp)
        {
            case VectorPhysicProperties.centerOfMass:
                body.centerOfMass += newVpp;
                break;

            case VectorPhysicProperties.inertiaTensor:
                body.inertiaTensor += newVpp;
                break;

            case VectorPhysicProperties.velocity:
                body.velocity += newVpp;
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Modifies the specified 2D float physic property of <paramref name="body"/>. The amount of change is specified by fppFactor.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="fpp"></param>
    /// <param name="fppFactor"></param>
    /// <param name="property"></param>
    public static void ChangePhysicProperty2D(Rigidbody2D body, FloatPhysicProperties fpp, float fppFactor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();
        float newFpp = fppFactor * value;

        switch (fpp)
        {
            case FloatPhysicProperties.angularDrag:
                body.angularDrag *= newFpp;
                break;

            case FloatPhysicProperties.drag:
                body.drag *= newFpp;
                break;

            case FloatPhysicProperties.inertia:
                body.inertia += newFpp;
                break;

            case FloatPhysicProperties.mass:
                body.mass *= newFpp;
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Modifies the specified 2D vectorial physic property of <paramref name="body"/> along <paramref name="axis"/>. 
    /// The amount of change is specified by vppFactor.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="vpp"></param>
    /// <param name="property"></param>
    /// <param name="axis"></param>
    public static void ChangePhysicProperty2D(Rigidbody2D body, VectorPhysicProperties vpp, Vector2 axis, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();
        Vector2 newVpp = axis * value;

        switch (vpp)
        {
            case VectorPhysicProperties.centerOfMass:
                body.centerOfMass += newVpp;
                break;


            case VectorPhysicProperties.velocity:
                body.velocity += newVpp;
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
    /// Returns a GameObject that generates a new line of terrain every frame, increasing its width (in x). 
    /// The <paramref name="length"/> of the terrain (in z) is fixed. The amount of increase in width is specified by <paramref name="step"/>.
    /// Height and noise are determined by <paramref name="heightfactor"/> and <paramref name="noiseFactor"/>, respectively. 
    /// The direction of the terrain is specified by <paramref name="terrainDir"/>.
    /// </summary>
    /// <param name="length"></param>
    /// <param name="startWidth"></param>
    /// <param name="step"></param>
    /// <param name="heightfactor"></param>
    /// <param name="noiseFactor"></param>
    /// <param name="terrainDir"></param>
    /// <returns></returns>
    public static GameObject GenerateTerrain(int length, float startWidth, float step, float heightfactor, float noiseFactor, Vector3 terrainDir)
    {
        GameObject terrainObj = new GameObject();
        terrainObj.name = "terrain";
        terrainObj.AddComponent<MeshFilter>();
        terrainObj.AddComponent<MeshRenderer>();
        GenerateTerrain terrain = terrainObj.AddComponent<GenerateTerrain>();
        terrain.SetParams(terrainObj.GetComponent<MeshFilter>().mesh, terrainObj.GetComponent<MeshRenderer>(), length, startWidth, step, heightfactor, noiseFactor, terrainDir);

        return terrainObj;
    }

    public static GameObject GenerateWaveTerrain(float step, float heightfactor, float noiseFactor)
    {
        GameObject terrainObj = new GameObject();
        terrainObj.name = "waveTerrain";
        terrainObj.AddComponent<MeshFilter>();
        terrainObj.AddComponent<MeshRenderer>();
        GenerateWaveTerrain terrain = terrainObj.AddComponent<GenerateWaveTerrain>();
        terrain.SetParams(terrainObj.GetComponent<MeshFilter>().mesh, terrainObj.GetComponent<MeshRenderer>(), step, heightfactor, noiseFactor);

        return terrainObj;
    }

    /// <summary>
    /// Creates and returns a GameObject, which is an instance of <paramref name="obj"/>, in the position determined by <paramref name="position"/> and
    /// with the rotation specified by <paramref name="rotation"/>. The instantiation is produced when <paramref name="soundOption"/> is true.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="soundOption"></param>
    /// <returns></returns>
    public static GameObject SoundInstantiate(UnityEngine.Object obj, Vector3 position, Quaternion rotation, bool soundOption)
    {
        if (soundOption)
        {
            GameObject go = (GameObject)Instantiate(obj, position, rotation);
            return go;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Returns a GameObject that generate a new piece of trail every frame between the specified vertices of a custom polygon defined 
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
    /// Returns a GameObject that generates a piece of trail when a note of MIDI keyboard is played. 
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
