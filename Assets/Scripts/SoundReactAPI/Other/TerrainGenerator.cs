using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TerrainGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    private int width = 30; // Number of quads in x-direction
    private int length = 8; 

    public AudioInput audioInput;
    public float scaleFactor;

    private float currentLine = 0;
    private int globalVert = 0;
    private int globalTris = 0;

    // Start is called before the first frame update
    void Start()
    {
        //mesh = new Mesh();
        //GetComponent<MeshFilter>().mesh = mesh;
        //CreateTerrainLine2();
        //CreateTerrain();
        //UpdateTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        CreateTerrainLine();
        //UpdateTerrain();
        //UpdateTerrainLine();
    }

    private void CreateTerrain()
    {
        vertices = new Vector3[(width + 1) * (length + 1)];

        for (int i = 0, z = 0; z <= length; z++)
        {
            for (int x = 0; x <= width; x++)
            {
                //float y = Mathf.PerlinNoise(x * 0.2f, z * 0.2f) * 2;
                vertices[i] = new Vector3(x, 0, z);
                i++;
            } 
        }

        triangles = new int[width * length * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < length; z++)
        {
            for (int x = 0; x < width; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + width + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + width + 1;
                triangles[tris + 5] = vert + width + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    private void UpdateTerrain()
    {
        for (int i = 0, z = 0; z <= length; z++)
        {
            for (int x = 0; x <= width; x++)
            {
                vertices[i].y = audioInput.GetAmplitudeBuffer() * Mathf.PerlinNoise(x * 0.2f, z * 0.2f) * scaleFactor;
                i++;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    /*
    private void UpdateTerrainLine()
    {
       for (int z = 0; z <= length; z++)
       {
            vertices[currentLine * width + z].y = audioInput.GetAmplitudeBuffer() * 0.5f + Mathf.PerlinNoise(currentLine * 0.2f, z * 0.2f) * 2;
       }
        
        currentLine = (currentLine + 1) % (width + 2);

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
    */

    private void CreateTerrainLine()
    {
        Mesh newMesh = new Mesh();

        Vector3[] oldVertices = GetComponent<MeshFilter>().mesh.vertices;
        int[] oldTriangles = GetComponent<MeshFilter>().mesh.triangles;
        int oldVertLength = oldVertices.Length;
        int oldTriLength = oldTriangles.Length;

        Vector3[] vertices = new Vector3[2 * length + oldVertLength];
        int[] triangles = new int[6 * (length -1) + oldTriLength];

        // Old vertices
        for (int i = 0; i < oldVertLength; i++)
        {
            vertices[i] = oldVertices[i];
        }

        // New vertices 
        for(int i = 0, x = 0; x < 2; x++)
        {
            for (int z = 0; z < length; z++)
            {
                vertices[i + oldVertLength] = new Vector3(x * 0.01f * audioInput.GetAmplitudeBuffer() + currentLine, audioInput.GetAmplitudeBuffer(), z);
                if (x == 0 && oldVertLength != 0)
                {
                    vertices[i + oldVertLength - length] = vertices[i + oldVertLength];
                }
                i++;
            }
            currentLine += 0.01f;
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

        newMesh.vertices = vertices;
        newMesh.triangles = triangles;

        GetComponent<MeshFilter>().mesh.Clear();
        GetComponent<MeshFilter>().mesh = newMesh;

       //newMesh.RecalculateNormals();
    }

    /*
    private void OnDrawGizmos()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.color = new Color(0, 0, 255);
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
    */
}
