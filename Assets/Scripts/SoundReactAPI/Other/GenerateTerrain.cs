using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    #region Generate_Terrain_Variables

    //Terrain Variables
    private Mesh terrain;
    private MeshRenderer rend;
    private int length;
    private float currentWidth;
    private float step;
    private float heightFactor, noiseFactor;

    //During Generation Variables
    private float bandSum;
    private float advanceFactor;
    private Vector3[] oldVertices;
    private int[] oldTriangles;
    private Vector3[] vertices;
    private int[] triangles;
    private int oldVertLength;
    private int oldTriLength;
    private int xFin;
    private int vertIni;
    private int trisIni;
    private int trisOffset;

    #endregion

    //Music Data Variables
    private AudioInput audioInput;

    // Start is called before the first frame update
    void Start()
    {
        audioInput = GameObject.FindGameObjectWithTag("AudioInput").GetComponent<AudioInput>();
        

        for (int i = 0; i < 8; i++)
        {
            bandSum += audioInput.GetBandBuffer()[i];
        }

        advanceFactor = Mathf.Clamp(bandSum / 8, 0, 1.5f);
        oldVertices = terrain.vertices;
        oldTriangles = terrain.triangles;
        oldVertLength = oldVertices.Length;
        oldTriLength = oldTriangles.Length;
        xFin = 2;
        vertIni = 0;
        trisIni = 0;
        trisOffset = 0;
        vertices = new Vector3[2 * length];
        triangles = new int[6 * (length - 1)];

        GenerateTerrainLine();

        xFin = 1;
        trisOffset = length;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 8; i++)
        {
            bandSum += audioInput.GetBandBuffer()[i];
        }

        advanceFactor = Mathf.Clamp(bandSum / 8, 0, 1.5f);

        oldVertices = terrain.vertices;
        oldTriangles = terrain.triangles;

        oldVertLength = oldVertices.Length;
        oldTriLength = oldTriangles.Length;

        if (oldVertLength > 500 * length)
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

        GenerateTerrainLine();
    }

    private void GenerateTerrainLine()
    {        
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
                if (bandCount == (length / 8))
                {
                    bandCount = 0;
                    bandIndex++;
                }
                bandCount++;
                if (oldVertLength > 0)
                {
                    height = (audioInput.GetBandBuffer()[bandIndex] * heightFactor * Mathf.PerlinNoise(x * noiseFactor, z * noiseFactor) + vertices[i + oldVertLength - vertIni - length].y) / 2;
                    //height = (bands[bandIndex] * heightFactor + vertices[i + oldVertLength - vertIni - length].y) / 2;
                    //height = Mathf.Abs(height - vertices[i + oldVertLength - vertIni - length].y) > 5 ? height - height / 1.5f : height ;
                }
                else
                {
                    height = audioInput.GetBandBuffer()[bandIndex] * heightFactor * Mathf.PerlinNoise(x * noiseFactor, z * noiseFactor);
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

        terrain.Clear();
        terrain.vertices = vertices;
        terrain.triangles = triangles;

        terrain.RecalculateNormals();
    }

    public void SetParams(Mesh mesh, MeshRenderer rend, int length, float currentWidth, float step, float heightFactor, float noiseFactor)
    {
        terrain = mesh;
        this.rend = rend;
        rend.material = Resources.Load<Material>("Materials/TerrainMat");
        this.length = length;
        this.currentWidth = currentWidth;
        this.step = step;
        this.heightFactor = heightFactor;
        this.noiseFactor = noiseFactor;
    }
}
