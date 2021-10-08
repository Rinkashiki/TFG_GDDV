using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIDIConst
{
    #region MIDI_Constants

    private const float NOTES = 128.0f;
    public static float MAX_FREQ = 12543.9f;
    public static float[] NOTES_TO_FREQ = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16.4f, 17.3f, 18.4f, 19.4f, 20.6f, 21.8f, 23.1f, 24.5f, 26, 27.5f, 29.1f, 30.9f,
                                     32.7f, 34.6f, 36.7f, 38.9f, 41.2f, 43.7f, 46.2f, 49, 51.9f, 55, 58.3f, 61.7f, 65.4f, 69.3f, 73.4f, 77.8f, 82.4f, 87.3f,
                                     92.5f, 98, 103.8f, 110, 116.5f, 123.5f, 130.8f, 138.6f, 146.8f, 155.6f, 164.8f, 174.6f, 185, 196, 207.7f, 220, 233.1f,
                                     246.9f, 261.6f, 277.2f, 293.7f, 311.1f, 329.6f, 349.2f, 370, 392, 415.3f, 440, 466.2f, 493.9f, 523.3f, 554.4f, 587.3f,
                                     622.3f, 659.3f, 698.5f, 740, 784, 830.6f, 880, 932.3f, 987.8f, 1046.5f, 1108.7f, 1174.7f, 1244.5f, 1318.5f, 1396.9f,
                                     1480, 1568, 1661.2f, 1760, 1864.7f, 1975.5f, 2093, 2217.5f, 2349.3f, 2489, 2637, 2793.8f, 2960, 3136, 3322.4f, 3520,
                                     3729.3f, 3951.1f, 4186, 4434.9f, 4698.6f, 4978, 5274, 5587.7f, 5919.9f, 6271.9f, 6644.9f, 7040, 7458.6f, 7902.1f, 8372,
                                     8869.8f, 9397.3f, 9956.1f, 10548.1f, 11175.3f, 11839.8f, 12543.9f};

    #endregion

    #region MIDI_Functions

    public static float ComputedB(int velocity)
    {
        float dB = Mathf.Abs(120 * Mathf.Log10(1 - velocity / NOTES));

        return dB;
    }

    #endregion
}
