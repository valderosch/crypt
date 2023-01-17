using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource _audio;

    public AudioClip ClickSound;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();

    }

    public void OnButtonClickSound()
    {
        _audio.PlayOneShot(ClickSound);
    }
}

