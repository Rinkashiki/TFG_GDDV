using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioExample : MonoBehaviour
{

    public float startScale, scaleMultiplier;
    public AudioInput audioInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      transform.localScale = new Vector3((audioInput.AmplitudeBuffer * scaleMultiplier) + startScale, (audioInput.AmplitudeBuffer * scaleMultiplier)
                                          + startScale, (audioInput.AmplitudeBuffer * scaleMultiplier) + startScale);
    }
}
