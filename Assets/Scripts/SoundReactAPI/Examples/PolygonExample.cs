using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonExample : MonoBehaviour
{
    private MIDIRecordReact recordReact;
    private AmplitudeReact ampReact;

    private Mesh mesh;

    //Draw Polygon variables
    private Color lineColor = new Color(238, 126, 60);
    private float lineWidth = 0.1f;
    private float drawSpeedFactor = 0.0015f;
    private Vector3[] polygonVert = {new Vector2(-1.5f, -1), new Vector2(-0.5f, 0) , new Vector2(-1.5f, 1) , new Vector2(-0.5f, 1),
                                   new Vector2(0, 2), new Vector2(0.5f, 1), new Vector2(1.5f, 1), new Vector2(0.5f, 0), new Vector2(1.5f, -1),
                                   new Vector2(-1.5f, -1)};

    // Height Map
    Vector3[] initPos;
    [SerializeField] private float noiseFactor;
    [SerializeField] private float reliefFactor;
    [SerializeField] private float waveSpeed;

    //Bright
    private Color startColor;


    // Start is called before the first frame update
    void Start()
    {
        //MidiRecording.RecordingSetUp();
        //MidiRecording.StartRecording();

        recordReact = GetComponent<MIDIRecordReact>();
        ampReact = GetComponent<AmplitudeReact>();

        mesh = GetComponent<MeshFilter>().mesh;
        initPos = mesh.vertices;
        startColor = this.gameObject.GetComponent<MeshRenderer>().material.color;
        //recordReact.Record_VelocityDrawPolygon(polygonVert, lineColor, lineWidth, drawSpeedFactor);
    }

    private void Update()
    {
        //recordReact.Record_VelocityTerrainHeightMap(mesh, 0.4f, 0.03f);
        ampReact.AmplitudeReliefMap(mesh, noiseFactor, reliefFactor, initPos, waveSpeed);
        ampReact.AmplitudeBright(this.gameObject, 0.7f, startColor);
        //this.gameObject.transform.Rotate(new Vector3(1, 1, 0), Time.deltaTime * 100);
    }

    private void OnApplicationQuit()
    {
        //MidiRecording.StopRecording();
    }
}
