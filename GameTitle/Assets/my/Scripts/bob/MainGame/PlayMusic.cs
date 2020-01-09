using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public static bool playMusic;
    [SerializeField] AudioSource music;
    void Start()
    {
        playMusic = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playMusic)
        {
            music.Play();
            playMusic = false;
        }
    }
}
