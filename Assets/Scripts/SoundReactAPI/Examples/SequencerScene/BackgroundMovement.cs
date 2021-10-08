﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    //Background variables
    [SerializeField] AudioInput audioInput;
    private MeshRenderer mesh;
    public float xSpeed, ySpeed;

    private Vector2 offset;

    public Vector2 tiling;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();

        mesh.material.SetTextureScale("_MainTex", tiling);
    }

    // Update is called once per frame
    void Update()
    {
        offset = new Vector2(Time.time * xSpeed, Time.time * (ySpeed + audioInput.GetAmplitudeBuffer() * 0.001f));
        mesh.sharedMaterial.SetTextureOffset("_MainTex", offset);
        mesh.material.SetTextureScale("_MainTex", tiling);

    }
}
