using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSoundReact : MonoBehaviour
{
    #region Generic_Change_Propertiy_Functions

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
        
        else if (oldVertLength > 100000)
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
                vertices[i + oldVertLength - vertIni] = new Vector3(x * step * value + currentWidth, -value * heightFactor, z);
                i++;
            }
            currentWidth += step;
        }

        // Old triangles
        for (int i = 0; i < oldTriLength - trisIni; i++)
        {
            triangles[i] = oldTriangles[i + trisIni];
        }

        // New triangles
        int vert = 0;
        int tris = 0;
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

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        return currentWidth;
    }

    public static float CreateTerrainLineBands(Mesh mesh, int length, float currentWidth, float step, float heightFactor, float[] bands)
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

        else if (oldVertLength > 3000 * length)
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
                    //height = (bands[bandIndex] * heightFactor * Mathf.PerlinNoise(x * 0.2f, z * 0.2f) + vertices[i + oldVertLength - vertIni - length].y) / 2;
                    height = (bands[bandIndex] * heightFactor + vertices[i + oldVertLength - vertIni - length].y) / 2;
                }
                else
                {
                    //height = (bands[bandIndex] * heightFactor * Mathf.PerlinNoise(x * 0.2f, z * 0.2f));
                    height = bands[bandIndex] * heightFactor;
                }
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
            triangles[i] = oldTriangles[i + trisIni];
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
        //mesh.RecalculateTangents();
   
        return currentWidth;
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

    #endregion

}
