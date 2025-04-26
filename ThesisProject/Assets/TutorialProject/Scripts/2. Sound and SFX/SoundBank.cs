using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBank : MonoBehaviour
{
    public AudioClip stepAudio;
    public AudioClip flute1;
    public AudioClip flute2;
    public AudioClip flute3;
    public static SoundBank Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
