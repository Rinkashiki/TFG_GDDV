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
    private float movementAmount;
    private int currentPos = 0;
    private int noteNumber;
    private float velocity;

    // Start is called before the first frame update
    void Start()
    {
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
        if (MidiRecording.Event_CurrentNoteOn() != null)
        {
            noteNumber = MidiRecording.Event_CurrentNoteOn().GetNoteNumber();  

            if (numberDirAssociation.ContainsKey(noteNumber))
            {
                velocity = MIDIConst.ComputedB(MidiRecording.Event_CurrentNoteOn().GetNoteVelocity());
                lineDirection = numberDirAssociation[noteNumber];
                movementAmount = velocity * drawSpeedFactor;
                UpdateLinePosition();
            }
        }
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

        linePositions[currentPos] += lineDirection * movementAmount;
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
