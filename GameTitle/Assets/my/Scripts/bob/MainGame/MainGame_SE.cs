using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame_SE : MonoBehaviour
{
    private AudioSource sounds_Player;
    public AudioClip sword_SE;
    public AudioClip step_SE;
    void Start()
    {
        sounds_Player = gameObject.GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))// デバッグ用
        {
            SwordSound();
            //Debug.Log("けんがふらい");
        }
        if (Input.GetKeyDown(KeyCode.P))// デバッグ用
        {
            StepSound();
            //Debug.Log("りずむのってる");
        }
    }
    public void SwordSound()// 剣の効果音
    {
        sounds_Player.PlayOneShot(sword_SE);
    }
    public void StepSound ()// 足音の効果音
    {
        sounds_Player.PlayOneShot(step_SE);

    }
}
