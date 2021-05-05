using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phyllotaxis : MonoBehaviour
{
    #region Phyllotaxis_Variables

    // Phyllotaxis Trail
    private TrailRenderer trailRenderer;
    private float number;
    private float radiusNumber = 1;
    private bool tunnel;
    private int sign;

    // Phyllotaxis Trail Color
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;
    
    // Phyllotaxis Position
    [SerializeField] float phyllotaxisDegree;
    [SerializeField] float speedFactor;
    private float phylloTimer;
    private Vector3 startPosition, endPosition;
    private Vector2 phyllotaxisPos;

    // Phyllotaxis Scale
    [SerializeField] float scaleFactor;
    [SerializeField] float initialScale;
    private float currentScale;

    // Phyllotaxis Loops
    [SerializeField] int loops;

    // Music Data Type
    private AudioInput audioInput;
    private GenericSoundReact.MusicDataType type;
    private int band;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("AudioInput"))
            audioInput = GameObject.FindGameObjectWithTag("AudioInput").GetComponent<AudioInput>();

        currentScale = initialScale;
        Vector2 initPos = PhyllotaxisCompute(phyllotaxisDegree, currentScale, number);
        transform.localPosition = new Vector3(initPos.x, initPos.y, transform.localPosition.z);

        SetLerpPositions();
    }

    private void Update()
    {
        // Select music value to draw the trail
        float value = 0;

        switch (type)
        {
            case GenericSoundReact.MusicDataType.Amplitude:
                value = audioInput.GetAmplitudeBuffer();
                break;

            case GenericSoundReact.MusicDataType.FreqBand:
                value = audioInput.GetBandBuffer(band);
                break;

            case GenericSoundReact.MusicDataType.MidiPlay:
                if (MidiPlayEventHandler.Event_CurrentNoteOn() != null)
                    value = MIDIConst.ComputedB(MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity()) * MIDIConst.NOTES_TO_FREQ[MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber()] / MIDIConst.MAX_FREQ;
                break;

            case GenericSoundReact.MusicDataType.MidiRecord:
                if (MidiRecording.Event_CurrentNoteOn() != null)
                    value = MIDIConst.ComputedB(MidiRecording.Event_CurrentNoteOn().GetNoteVelocity()) * MIDIConst.NOTES_TO_FREQ[MidiRecording.Event_CurrentNoteOn().GetNoteNumber()] / MIDIConst.MAX_FREQ;
                break;

            default:
                break;
        }

        // Phyllotaxis Scale
        currentScale = initialScale + value * scaleFactor;

        // Phyllotaxis Position
        phylloTimer += Time.deltaTime * value * speedFactor;
        transform.localPosition = Vector3.Lerp(startPosition, endPosition, Mathf.Clamp01(phylloTimer));
        if (phylloTimer >= 1)
        {
            phylloTimer -= 1;

            if (tunnel)
            {
                // For tunnel
                number = number >= (360 / phyllotaxisDegree) ? 1 : number + 1;
            }
            else
            {
                // For phyllotaxis
                
                if (radiusNumber >= (360 / phyllotaxisDegree) * loops)
                {
                    sign = -1;
                }
                else if (radiusNumber <= 1)
                {
                    sign = 1;
                }
                number++;
                radiusNumber += sign;
            } 
            SetLerpPositions();
        }
    }

    void SetLerpPositions()
    {
        phyllotaxisPos = PhyllotaxisCompute(phyllotaxisDegree, currentScale, number);
        startPosition = transform.localPosition;
        endPosition = new Vector3(phyllotaxisPos.x, phyllotaxisPos.y, transform.localPosition.z);
    }

    private Vector2 PhyllotaxisCompute(float phyllotaxisDegree, float scale, float number)
    {
        double angle = number * (phyllotaxisDegree * Mathf.Deg2Rad);
        float radius;
        if (tunnel)
        {
            // For tunnel
            radius = scale;
        }
        else
        {
            // For phyllotaxis
            radius = scale * Mathf.Sqrt(radiusNumber);
        }
        float x = radius * (float)System.Math.Cos(angle);
        float y = radius * (float)System.Math.Sin(angle);
        Vector2 vec2 = new Vector2(x, y);
        return vec2;
    }

    public void SetParams(float phyllotaxisDegree, float speedFactor, float scaleFactor, GenericSoundReact.MusicDataType type, float initialScale = 0, int loops = 10)
    {
        this.phyllotaxisDegree = phyllotaxisDegree;
        this.speedFactor = speedFactor;
        this.scaleFactor = scaleFactor;
        this.type = type;
        this.initialScale = initialScale;
        this.loops = loops;
    }

    public void SetFreqBand(int band)
    {
        this.band = band;
    }

    public void SetTunnel(bool tunnel)
    {
        this.tunnel = tunnel;
    }
}
