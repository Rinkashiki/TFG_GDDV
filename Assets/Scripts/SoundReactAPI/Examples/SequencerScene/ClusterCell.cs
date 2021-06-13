using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterCell : MonoBehaviour
{
    [SerializeField] Transform cellPosition;
    [SerializeField] Transform unionTransform;
    [SerializeField] bool reverse;

    private AmplitudeReact ampReact;
    private float offset;

    // Start is called before the first frame update
    void Start()
    {
        ampReact = GetComponent<AmplitudeReact>();
        offset = 0.5f;
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

        // Control Material
        //ampReact.AmplitudeShaderGraphMatProperty(gameObject.GetComponent<MeshRenderer>().material,"FresnelPower", GenericSoundReact.MatPropertyType.Float, 5f);
    }

}
