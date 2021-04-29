using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phyllotaxis : MonoBehaviour
{
    #region Phyllotaxis_Variables

    private const float NOTES = 128.0f;

    // Phyllotaxis Trail
    private TrailRenderer trailRenderer;
    private int number;

    // Phyllotaxis Trail Color
    public Color startColor;
    public Color endColor;
    
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
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.startWidth = 0.1f;
        trailRenderer.material = new Material(Shader.Find("Sprites/Default"));
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(startColor, 0.0f), new GradientColorKey(endColor, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        trailRenderer.colorGradient = gradient;
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
                if (MidiPlayEventHandler.Event_CurrentNoteOn() != null) { }
                value = MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteVelocity() * MidiPlayEventHandler.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
                break;

            case GenericSoundReact.MusicDataType.MidiRecord:
                if (MidiRecording.Event_CurrentNoteOn() != null)
                    value = MidiRecording.Event_CurrentNoteOn().GetNoteVelocity() * MidiRecording.Event_CurrentNoteOn().GetNoteNumber() / NOTES;
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
            number = (number + 1) % 50;
            SetLerpPositions();
        }
    }

    void SetLerpPositions()
    {
        phyllotaxisPos = PhyllotaxisCompute(phyllotaxisDegree, currentScale, number);
        startPosition = transform.localPosition;
        endPosition = new Vector3(phyllotaxisPos.x, phyllotaxisPos.y, transform.localPosition.z);
    }

    private Vector2 PhyllotaxisCompute(float phyllotaxisDegree, float scale, int number)
    {
        double angle = number * (phyllotaxisDegree * Mathf.Deg2Rad);
        float radius = scale * Mathf.Sqrt(number);
        float x = radius * (float)System.Math.Cos(angle);
        float y = radius * (float)System.Math.Sin(angle);
        Vector2 vec2 = new Vector2(x, y);
        return vec2;
    }

    public void SetParams(float phyllotaxisDegree, Color startColor, Color endColor, float speedFactor, float scaleFactor, GenericSoundReact.MusicDataType type, float initialScale = 0)
    {
        this.phyllotaxisDegree = phyllotaxisDegree;
        this.startColor = startColor;
        this.endColor = endColor;
        this.speedFactor = speedFactor;
        this.scaleFactor = scaleFactor;
        this.type = type;
        this.initialScale = initialScale;
    }

    public void SetFreqBand(int band)
    {
        this.band = band;
    }
}
