using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audi;

    public AudioClip inGameSound;
    public AudioClip titleGameSound;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        audi = GetComponent<AudioSource>();
    }

    public void PlayTitleSound()
    {
        audi.clip = titleGameSound;
        audi.Play();
    }

    public void PlayInGameSound()
    {
        audi.clip = inGameSound;
        audi.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
