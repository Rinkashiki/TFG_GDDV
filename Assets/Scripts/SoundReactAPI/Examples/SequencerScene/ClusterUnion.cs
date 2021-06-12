using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterUnion : MonoBehaviour
{
    private FreqBandReact bandReact;

    [SerializeField] int band;
    [SerializeField] float scaleFactor;
    [SerializeField] float rotationFactor;

    // Position
    private Vector3 initialPos;
    private Vector3 positionOffset;

    // Rotation
    private Vector3 rotationAxis;

    // Scale
    private Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        bandReact = GetComponent<FreqBandReact>();

        // Position
        initialPos = transform.parent.position;
        positionOffset = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        transform.parent.position = initialPos + positionOffset;

        // Rotation
        rotationAxis = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        // Scale
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        bandReact.BandScale(transform, band, Vector3.up, scaleFactor, initialScale);
        bandReact.BandRotation(transform.parent, band, rotationAxis, rotationFactor);
    }
}
