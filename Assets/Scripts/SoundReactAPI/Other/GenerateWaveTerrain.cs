using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWaveTerrain : MonoBehaviour
{

    #region Generate_Wave_Terrain_Variables

    //Music Data Variables
    private AudioInput audioInput;

    //Terrain Variables
    private Mesh terrain;
    private MeshRenderer rend;
    private int wl;
    private float currentWL;
    private float step;
    private float heightFactor, noiseFactor;

    //During Generation Variables
    private float advanceFactor;
    private Vector3[] oldVertices;
    private int[] oldTriangles;
    private Vector3[] vertices;
    private int[] triangles;
    private int oldVertLength;
    private int oldTriLength;
    private int vertInc, trisInc;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        audioInput = GameObject.FindGameObjectWithTag("AudioInput").GetComponent<AudioInput>();

        wl = 2;

        oldVertices = terrain.vertices;
        oldTriangles = terrain.triangles;
        oldVertLength = oldVertices.Length;
        oldTriLength = oldTriangles.Length;

        vertices = new Vector3[4];
        triangles = new int[6];

        GenerateWaveTerrainLine();
    }

    // Update is called once per frame
    void Update()
    {
        
        oldVertices = terrain.vertices;
        oldTriangles = terrain.triangles;

        oldVertLength = oldVertices.Length;
        oldTriLength = oldTriangles.Length;

        vertices = new Vector3[oldVertLength + (vertInc + 8)];
        triangles = new int[oldTriLength + trisInc];

        vertInc = 0;
        
        GenerateWaveTerrainLine();
    }

    private void GenerateWaveTerrainLine()
    {
        int wlOffset = 0;
        int count = 0;

        // Old vertices
        for (int i = 0; i < oldVertLength; i++)
        {
            if (count == (wl - 2))
            {
                wlOffset += 2;
                count = 0;
            }      

            vertices[i + wl + wlOffset + 1] = oldVertices[i];

            count++;
        }

        // New vertices
        float height = 0;

        //advanceFactor = Mathf.Clamp(audioInput.GetAmplitudeBuffer(), 0, 1.5f);
        advanceFactor = 1;
        currentWL += step * advanceFactor;
        float currentWLStep = step * advanceFactor;

        // Left-Right columns
        for (int i = 0, x = 0; x < wl; x += wl - 1)
        {
            for (int z = 0; z < wl; z++)
            {
                height = audioInput.GetAmplitudeBuffer() * heightFactor * Mathf.PerlinNoise(x * noiseFactor, z * noiseFactor);

                //Left column
                if(x == 0)
                {
                    vertices[i] = new Vector3(-currentWL, height, (z + 1 - wl/2 ) * currentWLStep);
                }

                //Right column
                else
                {
                    vertices[i + vertices.Length - wl * 2] = new Vector3(currentWL, height, (z + 1 - wl / 2) * currentWLStep);
                }
                i++;
                vertInc++;
            }
        }

        if (oldVertLength > 0)
        {
            // Bottom-Top rows
            int zOffset = wl;

            for (int i = 0, x = 1; x < wl - 1; x++)
            {
                for (int z = 0; z < wl; z += wl - 1)
                {
                    height = audioInput.GetAmplitudeBuffer() * heightFactor * Mathf.PerlinNoise(x * noiseFactor, z * noiseFactor);

                    //Bottom row
                    if (z == 0)
                    {
                        vertices[zOffset] = new Vector3((x + 1 - wl / 2) * currentWLStep, height, -currentWL);
                        zOffset += wl - 1;
                    }

                    //Top row
                    else
                    {
                        vertices[zOffset] = new Vector3((x + 1 - wl / 2) * currentWLStep, height, currentWL);
                        zOffset++;
                    }
                    i++;
                    vertInc++;
                }
            }
        }
        // New triangles
        int vert = 0;
        int tris = 0;

        //ClockWise
        for (int i = 0; i < wl * (wl - 2) + 1; i++)
        {
            triangles[tris] = vert;
            triangles[tris + 1] = vert + 1;
            triangles[tris + 2] = vert + wl;
            triangles[tris + 3] = vert + 1;
            triangles[tris + 4] = vert + wl + 1;
            triangles[tris + 5] = vert + wl;

            vert++;
            tris += 6;
        }

        trisInc += 8 * wl * 3;

        wl += 2;

        terrain.Clear();
        terrain.vertices = vertices;
        terrain.triangles = triangles;

        terrain.RecalculateNormals();
    }

    public void SetParams(Mesh mesh, MeshRenderer rend, float step, float heightFactor, float noiseFactor)
    {
        terrain = mesh;
        this.rend = rend;
        rend.material = Resources.Load<Material>("Materials/TerrainMat");
        this.step = step;
        this.heightFactor = heightFactor;
        this.noiseFactor = noiseFactor;
    }
}
