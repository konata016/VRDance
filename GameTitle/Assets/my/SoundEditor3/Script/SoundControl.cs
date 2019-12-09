using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    public Slider slider;   //再生バー用

    AudioSource source;     //サウンド再生環境
    AudioClip clip;         //サウンドデータ

    public static bool onMusic { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        clip = source.clip;
        source.Stop();

        //再生バーの終了位置セット
        slider.maxValue = clip.length;
    }

    // Update is called once per frame
    void Update()
    {
        //BGMの再生時間と再生バーをリンク
        if (onMusic) slider.value = source.time;

        //マウスクリックをするとBGMが止まる
        if (Input.GetMouseButtonDown(0))
        {
            source.Stop();
            onMusic = false;
        }

        //スペースキーで再生
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int num = StepData.GetTimeNearBeatTime(slider.value);
            slider.value = StepData.GetStepData[num].musicScore;

            source.time = slider.value; //再生バーの位置とBGM再生位置をリンク
            onMusic = true;
            source.Play();              //BGM再生
        }
    }
}
