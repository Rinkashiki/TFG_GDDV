using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterCell : MonoBehaviour
{
    [SerializeField] Transform cellPosition;
    [SerializeField] Transform unionTransform;
    [SerializeField] bool reverse;

    private AmplitudeReact ampReact;
    private Vector3 initialScale;
    public bool enableScale;
    private float offset;

    // Start is called before the first frame update
    void Start()
    {
        ampReact = GetComponent<AmplitudeReact>();
        offset = 0.5f;

        // Scale
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Maintain Position
        if (reverse)
        {
            transform.position = cellPosition.position - offset * unionTransform.up;
        }
        else
        {
            transform.position = cellPosition.position + offset * unionTransform.up;
        }

        // Scale
        ampReact.AmplitudeScale(transform, Vector3.one, 1.5f, initialScale);
    }

}
