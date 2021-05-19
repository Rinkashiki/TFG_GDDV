using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
class ScheduledEvent
{
    public float scheduleTime;
    public UnityEvent callback;
}

public class Sequencer : MonoBehaviour
{
    [SerializeField]
    List<ScheduledEvent> scheduledEvents;

    private int current;

    // Start is called before the first frame update
    void Start()
    {
        current = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (current < scheduledEvents.Count && Time.time >= scheduledEvents[current].scheduleTime)
        {
            scheduledEvents[current].callback.Invoke();
            current++;
        }
        
    }
}


