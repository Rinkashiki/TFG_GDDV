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

    public static float CreateTerrainLine(Mesh mesh, int length, float currentWidth, float step, float heightFactor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        Vector3[] oldVertices = mesh.vertices;
        int[] oldTriangles = mesh.triangles;
        int oldVertLength = oldVertices.Length;
        int oldTriLength = oldTriangles.Length;

        Vector3[] vertices = new Vector3[2 * length + oldVertLength];
        int[] triangles = new int[6 * (length - 1) + oldTriLength];

        // Old vertices
        for (int i = 0; i < oldVertLength; i++)
        {
            vertices[i] = oldVertices[i];
        }

        // New vertices 
        for (int i = 0, x = 0; x < 2; x++)
        {
            for (int z = 0; z < length; z++)
            {
                vertices[i + oldVertLength] = new Vector3(x * step * value + currentWidth, value * heightFactor, z);
                if (x == 0 && oldVertLength != 0)
                {
                    vertices[i + oldVertLength - length] = vertices[i + oldVertLength];
                }
                i++;
            }
            currentWidth += step;
        }

        // Old triangles
        for (int i = 0; i < oldTriLength; i++)
        {
            triangles[i] = oldTriangles[i];
        }

        // New triangles
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < length - 1; z++)
        {
            triangles[tris + oldTriLength] = vert + oldVertLength;
            triangles[tris + 1 + oldTriLength] = vert + oldVertLength + length;
            triangles[tris + 2 + oldTriLength] = vert + oldVertLength + 1;
            triangles[tris + 3 + oldTriLength] = vert + oldVertLength + 1;
            triangles[tris + 4 + oldTriLength] = vert + oldVertLength + length;
            triangles[tris + 5 + oldTriLength] = vert + oldVertLength + length + 1;

            vert++;
            tris += 6;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        return currentWidth;
    }

    public static float CreateTerrainLine2(Mesh mesh, int length, float currentWidth, float step, float heightFactor, Numeric property)
    {
        var value = property.GetNumericInt() != 0 ? property.GetNumericInt() : property.GetNumericFloat();

        Vector3[] oldVertices = mesh.vertices;
        int[] oldTriangles = mesh.triangles;
        List<Vector3> oldVerticesList = new List<Vector3>();
        List<int> oldTrianglesList = new List<int>();

        if (oldVertices[oldVertices.Length - 1].x - oldVertices[0].x > 20)
        {
            for(int i = length * 10; i < oldVertices.Length; i++)
            {
                oldVerticesList.Add(oldVertices[i]);
            }

            for(int i = 6 * (length - 1) * 10; i < oldTriangles.Length; i++)
            {
                oldTrianglesList.Add(oldTriangles[i]);
            }

            oldVertices = oldVerticesList.ToArray();
            oldTriangles = oldTrianglesList.ToArray();
        }

        int oldVertLength = oldVertices.Length;
        int oldTriLength = oldTriangles.Length;
        
        Vector3[] vertices = new Vector3[2 * length + oldVertLength];
        int[] triangles = new int[6 * (length - 1) + oldTriLength];

        // Old vertices
        for (int i = 0; i < oldVertLength; i++)
        {
            vertices[i] = oldVertices[i];
        }

        // New vertices 
        for (int i = 0, x = 0; x < 2; x++)
        {
            for (int z = 0; z < length; z++)
            {
                vertices[i + oldVertLength] = new Vector3(x * step * value + currentWidth, value * heightFactor, z);
                if (x == 0 && oldVertLength != 0)
                {
                    vertices[i + oldVertLength - length] = vertices[i + oldVertLength];
                }
                i++;
            }
            currentWidth += step;
        }

        // Old triangles
        for (int i = 0; i < oldTriLength; i++)
        {
            triangles[i] = oldTriangles[i];
        }

        // New triangles
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < length - 1; z++)
        {
            triangles[tris + oldTriLength] = vert + oldVertLength;
            triangles[tris + 1 + oldTriLength] = vert + oldVertLength + length;
            triangles[tris + 2 + oldTriLength] = vert + oldVertLength + 1;
            triangles[tris + 3 + oldTriLength] = vert + oldVertLength + 1;
            triangles[tris + 4 + oldTriLength] = vert + oldVertLength + length;
            triangles[tris + 5 + oldTriLength] = vert + oldVertLength + length + 1;

            vert++;
            tris += 6;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        return currentWidth;
    }

    #endregion

}
