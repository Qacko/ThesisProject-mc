using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSounds : MonoBehaviour
{
    private AudioSource source;
    private float timer;
    private int randomSound;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5)
        {
            if (randomSound == 1)
            {
                source.clip = SoundBank.Instance.flute1;
            }
            else if (randomSound == 2)
            {
                source.clip = SoundBank.Instance.flute2;
            }
            else
            {
                source.clip = SoundBank.Instance.flute3;
            }
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
    }

    private void OnMovement()
    {
        if (source.clip != SoundBank.Instance.stepAudio)
        {
            source.clip = SoundBank.Instance.stepAudio;
            source.loop = true;
        }

        if (!source.isPlaying)
        {
            source.Play();
        }
        timer = 0;
    }

    private void OnMovementStop()
    {
        source.Stop();
        randomSound = Random.Range(1, 4);
    }
}
