using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidiRecordReactExample : MonoBehaviour
{

    private const int MIDI_NOTES_COUNT = 128;

    private MIDIRecordReact recReact;

    // React objects from scene. First one is the plane, second one is the big sphere. Third and fourth ones are the small spheres.
    [Header("React Objects")]
    [SerializeField] GameObject[] reacts;

    // Record Relief Map. For plane and big sphere.
    [Header("Record Relief Map")]
    [SerializeField] float planeNoiseFactor;
    [SerializeField] float planeHeightFactor;
    [SerializeField] float planeWaveSpeed;
    private Mesh planeMesh;
    private Vector3[] planeInitPos;

    // Record Color. For big sphere.
    [Header("Record Color")]
    [Range(0,1)][SerializeField] float transitionSpeed;
    private Dictionary<int, Color> numberColorAssociation;
    private MeshRenderer bigSphereRend;

    // Record Animarion Speed. For small spheres.
    [Header("Record Animation Speed")]
    [SerializeField] float speedFactor;
    private Animator anim1, anim2;

    // Record Bright. For small spheres.
    [Header("Record Bright")]
    [SerializeField] float brightFactor;
    private MeshRenderer sphereRend1, sphereRend2;
    private Color sphere1Color, sphere2Color;


    // Start is called before the first frame update
    void Start()
    {
        recReact = GetComponent<MIDIRecordReact>();

        // Record callbacks for start recording
        MidiRecording.RecordingSetUp();
        MidiRecording.StartRecording();

        // Record Relief Map
        planeMesh = reacts[0].GetComponent<MeshFilter>().mesh;
        planeInitPos = planeMesh.vertices;

        // Record Color
        bigSphereRend = reacts[1].GetComponent<MeshRenderer>();
        numberColorAssociation = InitDict();

        // Record Animation Speed
        anim1 = reacts[2].GetComponent<Animator>();
        anim2 = reacts[3].GetComponent<Animator>();

        // Record Bright
        sphereRend1 = reacts[2].GetComponent<MeshRenderer>();
        sphereRend2 = reacts[3].GetComponent<MeshRenderer>();
        sphere1Color = sphereRend1.material.color;
        sphere2Color = sphereRend2.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        // Record Relief Map
        recReact.Record_ReliefMap(planeMesh, planeNoiseFactor, planeHeightFactor, planeWaveSpeed, planeInitPos);

        // Record Color
        recReact.Record_Color(bigSphereRend, numberColorAssociation, transitionSpeed);

        // Record Animation Speed
        recReact.Record_AnimationSpeed(anim1, speedFactor, 0);
        recReact.Record_AnimationSpeed(anim2, speedFactor, 0);

        // Record Bright
        recReact.Record_Bright(sphereRend1, brightFactor, sphere1Color);
        recReact.Record_Bright(sphereRend2, brightFactor, sphere2Color);
    }

    private void OnApplicationQuit()
    {
        // Record callbacks for stop playing
        MidiRecording.StopRecording();
    }
  
    private Dictionary<int, Color> InitDict()
    {
        Dictionary<int, Color> dict = new Dictionary<int, Color>();

        for(int i = 0; i < MIDI_NOTES_COUNT; i++)
        {
            dict.Add(i, Random.ColorHSV());
        }

        return dict;
    }
}
