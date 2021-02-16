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

        wl = 4;

        oldVertices = terrain.vertices;
        oldTriangles = terrain.triangles;
        oldVertLength = oldVertices.Length;
        oldTriLength = oldTriangles.Length;

        vertices = new Vector3[16];
        triangles = new int[54];

        GenerateWaveTerrainLine();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        oldVertices = terrain.vertices;
        oldTriangles = terrain.triangles;

        oldVertLength = oldVertices.Length;
        oldTriLength = oldTriangles.Length;

        vertices = new Vector3[oldVertLength + (vertInc + 8)];
        triangles = new int[oldTriLength + trisInc];

        vertInc = 0;
        */
        //GenerateWaveTerrainLine();
    }

    private void GenerateWaveTerrainLine()
    {
        // Vertices
        float height = 0;
        int vertCount = 0;

        int zCount = 0;
        int xCount = 0;

        advanceFactor = Mathf.Clamp(audioInput.GetAmplitudeBuffer(), 0, 1.5f);

        currentWL += step * advanceFactor;

        for (int i = 0, x = 0; x < wl; x++)
        {
            for (int z = 0; z < wl; z++)
            {
                height = audioInput.GetAmplitudeBuffer() * heightFactor * Mathf.PerlinNoise(x * noiseFactor, z * noiseFactor);
                vertices[i] = new Vector3(x, 0, z);
                i++;
                vertInc++;
            }
            zCount = 0;

            if (x > 0)
                xCount += wl - 2;
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

        trisInc += 16 * 3;

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
