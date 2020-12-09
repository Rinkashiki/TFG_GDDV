using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TerrainGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    private int width = 30;
    private int length = 30;

    public AudioInput audioInput;
    public float scaleFactor;

    private int currentLine = 0;
    private int globalVert = 0;
    private int globalTris = 0;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateTerrain();
        //UpdateTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTerrain();
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

    private void CreateTerrainLine()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int x = 0; x <= width; x++)
            {
                float y = audioInput.GetAmplitudeBuffer() * 0.5f + Mathf.PerlinNoise(x * 0.2f, currentLine * 0.2f) * 2;
                vertices[(currentLine + i) * length + x] = new Vector3(x, 0, currentLine + i);
            }
        }

            for (int x = 0; x < width; x++)
            {
                triangles[globalTris + 0] = globalVert + 0;
                triangles[globalTris + 1] = globalVert + width + 1;
                triangles[globalTris + 2] = globalVert + 1;
                triangles[globalTris + 3] = globalVert + 1;
                triangles[globalTris + 4] = globalVert + width + 1;
                triangles[globalTris + 5] = globalVert + width + 2;

                globalVert++;
                globalTris += 6;
            }

        currentLine = (currentLine + 2) % (width + 1);

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.color = new Color(0, 0, 255);
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
