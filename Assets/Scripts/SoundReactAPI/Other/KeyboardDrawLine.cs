using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardDrawLine : MonoBehaviour
{
    private LineRenderer line;
    private List<Vector3> linePositions;
    private Vector3 lineDirection, lastDirection;
    private int currentPos = 0;
    private float advanceFactor = 00000.1f;

    // Start is called before the first frame update
    void Start()
    {
        linePositions = new List<Vector3>();
        linePositions.Add(Vector3.zero);

        line = GetComponent<LineRenderer>();
        line.positionCount = 1;
        line.widthMultiplier = 0.1f;

        line.SetPositions(linePositions.ToArray());

        lastDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            lineDirection = Vector3.up * advanceFactor;
            UpdateLinePosition();
        }

        if (Input.GetKey(KeyCode.A))
        {
            lineDirection = Vector3.left * advanceFactor;
            UpdateLinePosition();
        }

        if (Input.GetKey(KeyCode.S))
        {
            lineDirection = Vector3.down * advanceFactor;
            UpdateLinePosition();
        }

        if (Input.GetKey(KeyCode.D))
        {
            lineDirection = Vector3.right * advanceFactor;
            UpdateLinePosition();
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

        linePositions[currentPos] += lineDirection;
        line.SetPosition(currentPos, linePositions[currentPos]);
    }
}
