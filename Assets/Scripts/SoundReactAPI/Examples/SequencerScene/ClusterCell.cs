using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterCell : MonoBehaviour
{
    [SerializeField] Transform cellPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cellPosition.position;
    }

}
