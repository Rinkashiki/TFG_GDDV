using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPolygon : MonoBehaviour
{
    #region Draw_Polygon_Variables

    private LineRenderer line;

    //Line attributes
    private Vector3[] polygonVert; 
    private Color lineColor;
    private float lineWidth;
    private float drawSpeedFactor;
    private GenericSoundReact.MusicDataType type;

    //Auxiliar variables
    private Vector3[] initPos;
    private int currentPos = 0;
    private float counter;
    private float dist;
    private float value;

    #endregion

    //Music Data Types
    private AudioInput audioInput;

    // Start is called before the first frame update
    void Start()
    {
        audioInput = GameObject.FindGameObjectWithTag("AudioInput").GetComponent<AudioInput>();

        initPos = new Vector3[polygonVert.Length];
        for (int i = 0; i < initPos.Length; i++)
        {
           initPos[i] = polygonVert[currentPos];
        }
        line = GetComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.positionCount = polygonVert.Length;
        Debug.Log(line.startColor);
        line.startColor = lineColor;
        line.endColor = lineColor;
        Debug.Log(line.startColor);
        line.widthMultiplier = lineWidth;
        line.SetPositions(initPos);
        dist = Vector3.Distance(polygonVert[currentPos], polygonVert[currentPos + 1]);
        currentPos++;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentPos < line.positionCount)
        {
            if (counter < dist)
            {
                switch (type)
                {
                    case GenericSoundReact.MusicDataType.Amplitude:
                        value = audioInput.GetAmplitudeBuffer();
                        break;

                    case GenericSoundReact.MusicDataType.FreqBand:
                        value = audioInput.GetBandBuffer(1);
                        break;

                    case GenericSoundReact.MusicDataType.NoteEvent:
                        break;

                    case GenericSoundReact.MusicDataType.ChordEvent:
                        break;

                    default:
                        break;
                }
                counter += value * drawSpeedFactor;

                //line.widthMultiplier = audioInput.GetAmplitudeBuffer() * 0.05f + 0.05f;

                float factor = Mathf.Lerp(0, dist, counter);

                Vector3 pointA = polygonVert[currentPos - 1];
                Vector3 pointB = polygonVert[currentPos];

                Vector3 currentPoint = factor * Vector3.Normalize(pointB - pointA) + pointA;

                //for(int i = currentPos; i < initPos.Length; i++)
                line.SetPosition(currentPos, currentPoint);
            }
            else
            {
                counter = 0;
                currentPos++;
                if (currentPos < line.positionCount)
                    dist = Vector3.Distance(polygonVert[currentPos - 1], polygonVert[currentPos]);
                for (int i = currentPos - 1; i < initPos.Length; i++)
                {
                    initPos[i] = polygonVert[currentPos - 1];
                }
                line.SetPositions(initPos);
            }
        }
        
    }

    public void SetParams(Vector3[] polygonVert, Color lineColor, float lineWidth, float drawSpeedFactor, GenericSoundReact.MusicDataType type)
    {
        this.polygonVert = polygonVert;
        this.lineColor = lineColor;
        this.lineWidth = lineWidth;
        this.drawSpeedFactor = drawSpeedFactor;
        this.type = type;
    }
}
