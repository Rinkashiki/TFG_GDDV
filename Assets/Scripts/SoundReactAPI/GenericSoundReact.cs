using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSoundReact : MonoBehaviour
{
    #region Generic_SoundReact_Const

    public enum MusicDataType { Amplitude, FreqBand, NoteEvent, ChordEvent};

    public enum FloatPhysicProperties { angularDrag, angularVelocity, drag, mass, inertia };
    public enum VectorPhysicProperties { centerOfMass, inertiaTensor, velocity };

    #endregion

    #region Generic_Change_Property_Functions

    public static void ChangeScale(GameObject go, Vector3 axis, float scaleFactor, float startScale, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        go.transform.localScale = new Vector3((value * scaleFactor * axis.x) + startScale,
                                              (value * scaleFactor * axis.y) + startScale,
                                              (value * scaleFactor * axis.z) + startScale);
    }

    public static void ChangeBright(GameObject go, float brightFactor, float startBrightness, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        float colorValue = startBrightness + value * brightFactor;
        Color color = new Color(colorValue, colorValue, colorValue);
        go.GetComponent<MeshRenderer>().material.color = color;
    }

    public static void ChangeRotation(GameObject go, Vector3 axis, float rotFactor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        go.transform.Rotate(axis, value * rotFactor);
    }

    public static void ChangeTerrainHeightMap(Mesh mesh, float noiseFactor, float heightFactor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        Vector3[] vertices = mesh.vertices;

        int length = (int) (Mathf.Abs(mesh.bounds.max.z) + Mathf.Abs(mesh.bounds.min.z));
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

    public static void ChangeVolumeHeightMap(Mesh mesh, float noiseFactor, float heightFactor, Vector3[] initPos, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;
        Vector2[] uvs = mesh.uv;

        Vector3 normal = Vector3.zero;
        Vector3 factor = Vector3.zero;
        float x, y, z;
        float waveSpeed = 0.3f;

        for (int i = 0; i < vertices.Length; i++)
        {
            normal = normals[i].normalized;
            factor = value * normal * heightFactor;
            x = Mathf.Cos(uvs[i].x * 2 * Mathf.PI) * Mathf.Cos(uvs[i].y * Mathf.PI - Mathf.PI / 2) + (Time.timeSinceLevelLoad * waveSpeed);
            //x = x * 0.5f + 0.5f;
            y = Mathf.Sin(uvs[i].y * Mathf.PI - Mathf.PI / 2) + (Time.timeSinceLevelLoad * waveSpeed);
            //y = y * 0.5f + 0.5f;
            z = Mathf.Sin(uvs[i].x * 2 * Mathf.PI) * Mathf.Cos(uvs[i].y * Mathf.PI - Mathf.PI / 2) + (Time.timeSinceLevelLoad * waveSpeed);
            //z = z * 0.5f + 0.5f; //PerlinNoise3D(x * noiseFactor, y * noiseFactor, z * noiseFactor)
            vertices[i] = factor * Mathf.Clamp(PerlinNoise3D(x * noiseFactor, y * noiseFactor, z * noiseFactor), 0.4f, 0.8f) + initPos[i];//Mathf.PerlinNoise(Mathf.PerlinNoise(x * noiseFactor, y * noiseFactor), z * noiseFactor)
            //vertices[i].x = value * normal.x * heightFactor * PerlinNoise3D(uvs[i].x, uvs[i].y, 1) * noiseFactor + initPos[i].x;
            //vertices[i].y = value * normal.y * heightFactor * PerlinNoise3D(uvs[i].x, uvs[i].y, 1) * noiseFactor + initPos[i].y; 
            //vertices[i].z = value * normal.z * heightFactor * PerlinNoise3D(uvs[i].x, uvs[i].y, 1) * noiseFactor + initPos[i].z; 
        }

        mesh.vertices = vertices;
        //mesh.RecalculateNormals();
    }

    public static void ChangeLightIntensity(Light light, float intensityFactor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        light.intensity = Mathf.Clamp(intensityFactor * value, 0, 8);
    }

    public static void ChangeLightRange(Light light, float rangeFactor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        light.range = rangeFactor * value;
    }

    #endregion

    #region Generic_Physic_Functions

    public static void ChangePhysicProperty(Rigidbody body, FloatPhysicProperties fpp, float fppFactor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();
        float newFpp = fppFactor * value;

        switch (fpp)
        {
            case FloatPhysicProperties.angularDrag:
                body.angularDrag *= newFpp;
            break;

            case FloatPhysicProperties.angularVelocity:
                body.angularVelocity *= newFpp;
            break;

            case FloatPhysicProperties.drag:
                body.drag *= newFpp;
            break;

            case FloatPhysicProperties.mass:
                body.mass *= newFpp;
            break;

            default:
            break;
        }

    }

    public static void ChangePhysicProperty(Rigidbody body, VectorPhysicProperties vpp, Numeric property, Vector3 axis)
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

    public static void ChangePhysicProperty2D(Rigidbody2D body, FloatPhysicProperties fpp, float fppFactor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();
        float newFpp = fppFactor * value;

        switch (fpp)
        {
            case FloatPhysicProperties.angularDrag:
                body.angularDrag *= newFpp;
                break;

            case FloatPhysicProperties.angularVelocity:
                body.angularVelocity *= newFpp;
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

    public static void ChangePhysicProperty2D(Rigidbody2D body, VectorPhysicProperties vpp, Numeric property, Vector2 axis)
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

    public static void CustomAddForce(Rigidbody body, Vector3 forceDir, ForceMode mode, float forceFactor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();
        body.AddForce(value * forceFactor * forceDir, mode);
    }

    public static void CustomAddForce2D(Rigidbody2D body, Vector2 forceDir, ForceMode2D mode, float forceFactor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();
        body.AddForce(value * forceFactor * forceDir, mode);
    }

    #endregion

    #region Generic_Create_Functions


    public static float CreateTerrainLineAmplitude(Mesh mesh, int length, float currentWidth, float step, float heightFactor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        Vector3[] oldVertices = mesh.vertices;
        int[] oldTriangles = mesh.triangles;
        Vector3[] vertices;
        int[] triangles;

        int oldVertLength = oldVertices.Length;
        int oldTriLength = oldTriangles.Length;

        int xFin = 1;
        int vertIni = 0;
        int trisIni = 0;
        int trisOffset = oldVertLength <= 0 ? trisOffset = 0 : trisOffset = length;

        if (oldVertLength <= 0)
        {
            xFin = 2;
            vertices = new Vector3[2 * length];
            triangles = new int[6 * (length - 1)];
        }
        
        else if (oldVertLength > 500 * length)
        {
            vertices = new Vector3[oldVertLength];
            triangles = new int[oldTriLength];
            vertIni = length;
            trisIni = 6 * (length - 1);
        }
        else
        {
            vertices = new Vector3[length + oldVertLength];
            triangles = new int[6 * (length - 1) + oldTriLength];
        }

        // Old vertices
        for (int i = 0; i < oldVertLength - vertIni; i++)
        {
            vertices[i] = oldVertices[i + vertIni];
        }

        // New vertices 
        for (int i = 0, x = 0; x < xFin; x++)
        {
            for (int z = 0; z < length; z++)
            {
                vertices[i + oldVertLength - vertIni] = new Vector3(x * step + currentWidth, value * heightFactor, z);
                i++;
            }
            currentWidth += step * Mathf.Clamp(value, 0, 1.5f); ;
        }

        // Old triangles
        for (int i = 0; i < oldTriLength - trisIni; i++)
        {
            triangles[i] = oldTriangles[i];
        }

        // New triangles
        int vert = 0;
        int tris = 0;

        //ClockWise
        for (int z = 0; z < length - 1; z++)
        {
            triangles[tris + oldTriLength - trisIni] = vert + oldVertLength - trisOffset - vertIni;
            triangles[tris + 1 + oldTriLength - trisIni] = vert + oldVertLength + 1 - vertIni;
            triangles[tris + 2 + oldTriLength - trisIni] = vert + oldVertLength - vertIni;
            triangles[tris + 3 + oldTriLength - trisIni] = vert + oldVertLength - trisOffset - vertIni;
            triangles[tris + 4 + oldTriLength - trisIni] = vert + oldVertLength - trisOffset + 1 - vertIni;
            triangles[tris + 5 + oldTriLength - trisIni] = vert + oldVertLength + 1 - vertIni;

            vert++;
            tris += 6;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        return currentWidth;
    }
    
    public static float CreateTerrainLineBands(Mesh mesh, int length, float currentWidth, float step, float heightFactor, float noiseFactor, float[] bands)
    {
        float bandSum = 0;
        for(int i = 0; i < bands.Length; i++)
        {
            bandSum += bands[i];
        }

        float advanceFactor = Mathf.Clamp(bandSum / bands.Length, 0, 1.5f);

        Vector3[] oldVertices = mesh.vertices;
        int[] oldTriangles = mesh.triangles;
        Vector3[] vertices;
        int[] triangles;

        int oldVertLength = oldVertices.Length;
        int oldTriLength = oldTriangles.Length;

        int xFin = 1;
        int vertIni = 0;
        int trisIni = 0;
        int trisOffset = oldVertLength <= 0 ? trisOffset = 0 : trisOffset = length;

        if (oldVertLength <= 0)
        {
            xFin = 2;
            vertices = new Vector3[2 * length];
            triangles = new int[6 * (length - 1)];
        }

        else if (oldVertLength > 500 * length)
        {
            vertices = new Vector3[oldVertLength];
            triangles = new int[oldTriLength];
            vertIni = length;
            trisIni = 6 * (length - 1);
        }
        else
        {
            vertices = new Vector3[length + oldVertLength];
            triangles = new int[6 * (length - 1) + oldTriLength];
        }

        // Old vertices
        for (int i = 0; i < oldVertLength - vertIni; i++)
        {
            vertices[i] = oldVertices[i + vertIni];
        }

        // New vertices
        int bandIndex = 0;
        int bandCount = 0;
        float height = 0;
        
        for (int i = 0, x = 0; x < xFin; x++)
        {
            for (int z = 0; z < length; z++)
            {
                if (bandCount == (length / bands.Length))
                {
                    bandCount = 0;
                    bandIndex++;
                }
                bandCount++;
                if (oldVertLength > 0)
                {
                    height = (bands[bandIndex] * heightFactor * Mathf.PerlinNoise(x * noiseFactor, z * noiseFactor) + vertices[i + oldVertLength - vertIni - length].y) / 2;
                    //height = (bands[bandIndex] * heightFactor + vertices[i + oldVertLength - vertIni - length].y) / 2;
                    //height = Mathf.Abs(height - vertices[i + oldVertLength - vertIni - length].y) > 5 ? height - height / 1.5f : height ;
                }
                else
                {
                    height = bands[bandIndex] * heightFactor * Mathf.PerlinNoise(x * noiseFactor, z * noiseFactor);
                    //height = bands[bandIndex] * heightFactor;
                }
                //Debug.Log(height);
                vertices[i + oldVertLength - vertIni] = new Vector3(x * step + currentWidth, height, z);
                i++;
            }
            currentWidth += step * advanceFactor;
            bandCount = 0;
            bandIndex = 0;
        }
        

        // Old triangles
        for (int i = 0; i < oldTriLength - trisIni; i++)
        {
            triangles[i] = oldTriangles[i];
        }

        // New triangles
        int vert = 0;
        int tris = 0;
            
        //ClockWise
        for (int z = 0; z < length - 1; z++)
        {
            triangles[tris + oldTriLength - trisIni] = vert + oldVertLength - trisOffset - vertIni;
            triangles[tris + 1 + oldTriLength - trisIni] = vert + oldVertLength + 1 - vertIni;
            triangles[tris + 2 + oldTriLength - trisIni] = vert + oldVertLength - vertIni;
            triangles[tris + 3 + oldTriLength - trisIni] = vert + oldVertLength - trisOffset - vertIni; 
            triangles[tris + 4 + oldTriLength - trisIni] = vert + oldVertLength - trisOffset + 1 - vertIni;
            triangles[tris + 5 + oldTriLength - trisIni] = vert + oldVertLength + 1 - vertIni;

            vert++;
            tris += 6;
        }
       
        
        //Counter ClockWise
        /*
        for (int z = 0; z < length - 1; z++)
        {
            triangles[tris + oldTriLength - trisIni] = vert + oldVertLength - trisOffset - vertIni;
            triangles[tris + 1 + oldTriLength - trisIni] = vert + oldVertLength - vertIni;
            triangles[tris + 2 + oldTriLength - trisIni] = vert + oldVertLength - trisOffset + 1 - vertIni;
            triangles[tris + 3 + oldTriLength - trisIni] = vert + oldVertLength - trisOffset + 1 - vertIni;
            triangles[tris + 4 + oldTriLength - trisIni] = vert + oldVertLength - vertIni;
            triangles[tris + 5 + oldTriLength - trisIni] = vert + oldVertLength + 1 - vertIni;

            vert++;
            tris += 6;
        }
        */     
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        return currentWidth;
    }

    public static GameObject GenerateTerrain(int length, float startWidth, float step, float heightfactor, float noiseFactor)
    {
        GameObject terrainObj = new GameObject();
        terrainObj.name = "terrain";
        terrainObj.AddComponent<MeshFilter>();
        terrainObj.AddComponent<MeshRenderer>();
        GenerateTerrain terrain = terrainObj.AddComponent<GenerateTerrain>();
        terrain.SetParams(terrainObj.GetComponent<MeshFilter>().mesh, terrainObj.GetComponent<MeshRenderer>(), length, startWidth, step, heightfactor, noiseFactor);

        return terrainObj;
    }

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

    public static GameObject DrawPolygon(Vector3[] polygonVert, Color lineColor, float lineWidth, float drawSpeedFactor, MusicDataType type)
    {
        GameObject polygon = new GameObject();
        polygon.name = "polygon";
        polygon.AddComponent<LineRenderer>();
        DrawPolygon line = polygon.AddComponent<DrawPolygon>();
        line.SetParams(polygonVert, lineColor, lineWidth, drawSpeedFactor, type);

        return polygon;
    }

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

}
