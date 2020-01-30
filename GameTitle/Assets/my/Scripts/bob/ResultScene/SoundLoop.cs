using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLoop : MonoBehaviour
{
    [SerializeField] AudioClip bgmIntroAudioClip;
    [SerializeField] AudioClip bgmLoopAudioClip;

    AudioSource introAudioSource;
    AudioSource loopAudioSource;

    void Start()
    {
        introAudioSource = gameObject.AddComponent <AudioSource>();
        loopAudioSource = gameObject.AddComponent <AudioSource>();

        introAudioSource.clip = bgmIntroAudioClip;
        introAudioSource.loop = false;
        introAudioSource.playOnAwake = false;
        introAudioSource.volume = 0.5f;

        loopAudioSource.clip = bgmLoopAudioClip;
        loopAudioSource.loop = true;
        loopAudioSource.playOnAwake = false;
        loopAudioSource.volume = 0.5f;

        PlayBGM();
    }

    public void PlayBGM()
    {
        if (introAudioSource == null || loopAudioSource == null) {
            return;
        }

        introAudioSource.Play ();
        loopAudioSource.PlayScheduled (AudioSettings.dspTime + bgmIntroAudioClip.length);
    }

    public void StopBGM()
    {
        if (introAudioSource == null || loopAudioSource == null) {
            return;
        }

        if (introAudioSource.isPlaying) {
            introAudioSource.Stop ();
        } else if (loopAudioSource.isPlaying) {
            loopAudioSource.Stop ();
        }
    }
}