using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinSounds : MonoBehaviour
{
    public AudioClip dive;
    public AudioClip walk;
    PenguinController penguin;
    AudioSource audioWalk;
    AudioSource audioDive;

    void Awake()
    {
        penguin = GetComponent<PenguinController>();
        audioWalk = AddAudio(walk);
        audioDive = AddAudio(dive);
    }
    void FixedUpdate()
    {
        PlaySound();
    }

    void PlaySound()
    {
        switch(penguin.currentState)
        {
            case State.WALK:
                if (audioWalk.isPlaying == false && audioDive.isPlaying == false) 
                {
                    audioWalk.volume = Random.Range(0.1f, 0.3f);
                    audioWalk.pitch = Random.Range(0.8f, 1f);
                    audioWalk.Play();
                }
                break;
            case State.IDLE:
                audioWalk.Stop();
                break;
            case State.DIVE:
                audioDive.Play();
                break;
        }
    }

    AudioSource AddAudio(AudioClip clip)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        return newAudio;
    }
}
