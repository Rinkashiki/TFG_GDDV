using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhylloTunnel : MonoBehaviour
{
    public Transform cameraTransform;
    public AudioInput audioPeer;
    public float tunnelSpeed, cameraDistance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + audioPeer.GetAmplitudeBuffer() * tunnelSpeed);

        if (cameraTransform != null)
            cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, transform.position.z + cameraDistance);
    }
}
