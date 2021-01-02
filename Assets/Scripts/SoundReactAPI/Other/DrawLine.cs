using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private LineRenderer line;
    private Vector3[] positions = {new Vector2(-1.5f, -1), new Vector2(-0.5f, 0) , new Vector2(-1.5f, 1) , new Vector2(-0.5f, 1),
                                   new Vector2(0, 2), new Vector2(0.5f, 1), new Vector2(1.5f, 1), new Vector2(0.5f, 0), new Vector2(1.5f, -1),
                                   new Vector2(-1.5f, -1)};
    private Vector3[] initPos;
    private int currentPos = 0;
    private float counter;
    private float dist;

    public AudioInput audioInput;

    // Start is called before the first frame update
    void Start()
    {
        initPos = new Vector3[positions.Length];
        for (int i = 0; i < initPos.Length; i++)
        {
            initPos[i] = positions[currentPos];
        }
        line = GetComponent<LineRenderer>();
        line.positionCount = positions.Length;
        line.widthMultiplier = 0.1f;
        line.SetPositions(initPos);
        dist = Vector3.Distance(positions[currentPos], positions[currentPos + 1]);
        currentPos++;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentPos < line.positionCount)
        {
            if (counter < dist)
            {
                counter += audioInput.GetAmplitudeBuffer() * 0.015f;

                //line.widthMultiplier = audioInput.GetAmplitudeBuffer() * 0.05f + 0.05f;

                float factor = Mathf.Lerp(0, dist, counter);

                Vector3 pointA = positions[currentPos - 1];
                Vector3 pointB = positions[currentPos];

                Vector3 currentPoint = factor * Vector3.Normalize(pointB - pointA) + pointA;

                line.SetPosition(currentPos, currentPoint);
            }
            else
            {
                counter = 0;
                currentPos++;
                if (currentPos < line.positionCount)
                    dist = Vector3.Distance(positions[currentPos - 1], positions[currentPos]);
                for (int i = currentPos - 1; i < initPos.Length; i++)
                {
                    initPos[i] = positions[currentPos - 1];
                }
                line.SetPositions(initPos);
            }
        }
        
    }
}
