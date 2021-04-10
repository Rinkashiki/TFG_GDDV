using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardDrawPolygon : MonoBehaviour
{
    //Keyboard polygon variables
    private Dictionary<int, Vector2> numberDirAssociation;
    private Color lineColor;
    private float lineWidth;
    private float drawSpeedFactor;

    //During drawing variables 
    private LineRenderer line;
    private List<Vector3> linePositions;
    private Vector3 lineDirection, lastDirection;
    private int currentPos = 0;
    private int noteNumber;
    //private float advanceFactor = 00000.1f;

    // Start is called before the first frame update
    void Start()
    {
        //MidiRecording.RecordingSetUp();
        //MidiRecording.StartRecording();

        linePositions = new List<Vector3>();
        linePositions.Add(Vector3.zero);

        line = GetComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.positionCount = 1;
        line.startColor = lineColor;
        line.widthMultiplier = lineWidth;

        line.SetPositions(linePositions.ToArray());

        lastDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        noteNumber = MidiRecording.GetCurrentNoteOnEvent() != null ? MidiRecording.GetCurrentNoteOnEvent().GetNoteNumber(): 0;

        if (noteNumber > 0 && numberDirAssociation.ContainsKey(noteNumber))
        {
            lineDirection = numberDirAssociation[noteNumber] * drawSpeedFactor;
            UpdateLinePosition();
        }
    }

    private void OnApplicationQuit()
    {
        MidiRecording.StopRecording();
    }

    private void UpdateLinePosition()
    {
        if (lastDirection != lineDirection)
        {
            linePositions.Add(linePositions[currentPos]);
            line.positionCount++;
            currentPos++;
            lastDirection = lineDirection;
        }

        linePositions[currentPos] += lineDirection;
        line.SetPosition(currentPos, linePositions[currentPos]);
    }

    public void SetParams(Dictionary<int, Vector2> numberDirAssociation, Color lineColor, float lineWidth, float drawSpeedFactor)
    {
        this.numberDirAssociation = numberDirAssociation;
        this.lineColor = lineColor;
        this.lineWidth = lineWidth;
        this.drawSpeedFactor = drawSpeedFactor;
    }
}
