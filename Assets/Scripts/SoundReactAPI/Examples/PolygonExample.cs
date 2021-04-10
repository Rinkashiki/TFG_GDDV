using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonExample : MonoBehaviour
{
    private MIDIRecordReact recordReact;

    private Mesh mesh;

    //Draw Polygon variables
    private Color lineColor = new Color(238, 126, 60);
    private float lineWidth = 0.1f;
    private float drawSpeedFactor = 0.0015f;
    private Vector3[] polygonVert = {new Vector2(-1.5f, -1), new Vector2(-0.5f, 0) , new Vector2(-1.5f, 1) , new Vector2(-0.5f, 1),
                                   new Vector2(0, 2), new Vector2(0.5f, 1), new Vector2(1.5f, 1), new Vector2(0.5f, 0), new Vector2(1.5f, -1),
                                   new Vector2(-1.5f, -1)};


    // Start is called before the first frame update
    void Start()
    {
        MidiRecording.RecordingSetUp();
        MidiRecording.StartRecording();

        recordReact = GetComponent<MIDIRecordReact>();

        mesh = GetComponent<MeshFilter>().mesh;
        //recordReact.Record_VelocityDrawPolygon(polygonVert, lineColor, lineWidth, drawSpeedFactor);
    }

    private void Update()
    {
        recordReact.Record_VelocityTerrainHeightMap(mesh, 0.4f, 0.03f);
    }

    private void OnApplicationQuit()
    {
        MidiRecording.StopRecording();
    }
}
