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

}
