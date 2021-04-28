using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phyllotaxis : MonoBehaviour
{
    public AudioInput _audioPeer;
    private Material _trailMat;

    public float degree;
    public int numberStart;
    public int step;
    public Color startColor;
    public Color endColor;

    //Lerping
    private Vector3 startPosition, endPosition;
    private float phylloTimer;
    public float speedFactor;


    private int number;
    private TrailRenderer trailRenderer;

    private Vector2 phyllotaxisPos;

    //Scaling
    public float scaleFactor;
    public float initialScale;
    private float currentScale;

    // Start is called before the first frame update
    void Awake()
    {
        currentScale = initialScale;
        trailRenderer = GetComponent<TrailRenderer>();
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(startColor, 0.0f), new GradientColorKey(endColor, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        trailRenderer.colorGradient = gradient;
        number = numberStart;
        Vector2 initPos = PhyllotaxisCompute(degree, currentScale, number);
        transform.localPosition = new Vector3(initPos.x, initPos.y, transform.localPosition.z);

        SetLerpPositions();
    }

    private void Update()
    {
        // Phyllotaxis Scale
        currentScale = initialScale + _audioPeer.GetAmplitudeBuffer() * scaleFactor;

        // Phyllotaxis Position
        phylloTimer += Time.deltaTime * _audioPeer.GetAmplitudeBuffer() * speedFactor;
        transform.localPosition = Vector3.Lerp(startPosition, endPosition, Mathf.Clamp01(phylloTimer));
        if (phylloTimer >= 1)
        {
            phylloTimer -= 1;
            number += step;
            SetLerpPositions();
        }

    }

    void SetLerpPositions()
    {
        phyllotaxisPos = PhyllotaxisCompute(degree, currentScale, number);
        startPosition = transform.localPosition;
        endPosition = new Vector3(phyllotaxisPos.x, phyllotaxisPos.y, transform.localPosition.z);
    }

    private Vector2 PhyllotaxisCompute(float degree, float scale, int number)
    {
        double angle = number * (degree * Mathf.Deg2Rad);
        float radius = scale * Mathf.Sqrt(number);
        float x = radius * (float)System.Math.Cos(angle);
        float y = radius * (float)System.Math.Sin(angle);
        Vector2 vec2 = new Vector2(x, y);
        return vec2;
    }
}
