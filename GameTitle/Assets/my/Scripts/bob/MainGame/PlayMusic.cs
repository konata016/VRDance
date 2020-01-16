using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public static bool playMusic;
    [SerializeField] AudioSource music;
    private bool changeScene;
    private SceneChangeEffect sceneChangeEffect;
    private bool onlyOne = true;

    void Start()
    {
        playMusic = false;
        changeScene = false;
        GameObject anotherObject = GameObject.Find("SceneChangeBox");
        sceneChangeEffect = anotherObject.GetComponent<SceneChangeEffect>();
        onlyOne = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(playMusic)
        {
            music.Play();
            playMusic = false;
            changeScene = true;
        }
        if(changeScene)
        {
            if(!music.isPlaying)
            {
                if(onlyOne)
                {
                    sceneChangeEffect.ChangeFadeMode();
                    sceneChangeEffect.OnTrigger();
                    onlyOne = false;
                }
            }
        }
    }
}