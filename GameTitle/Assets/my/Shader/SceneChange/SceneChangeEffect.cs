using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// フェイドインフェイドアウトする
/// (シーンチェンジとタイムスケールをいじる)
/// </summary>
public class SceneChangeEffect : MonoBehaviour
{
    public enum FADE_MODE
    {
        Out, In
    }

    public Material material;
    public FADE_MODE fadeMode;
    public float speed = 0.2f;

    //フェイドインしたとき、タイムスケールをもとに戻すときに間を開ける用
    public float timeScaleWaitTime = 0;

    public string changeSceneName;

    float gage;
    float fadeOutDef = 1.8f;
    float fadeInDef = 0.8f;
    bool onTrigger;

    float sceneChangeDeltaTime;

    private bool onlyOne = true;

    SceneChangeBoxPos sceneChangeBoxPos;
    //AsyncOperation async;

    // Start is called before the first frame update
    void Start()
    {
        switch (fadeMode)
        {
            case FADE_MODE.Out:
                gage = fadeOutDef;
                break;

            case FADE_MODE.In:
                gage = fadeInDef;
                break;

            default: break;
        }
        material.SetFloat("_Gauge", gage);
        //async = SceneManager.LoadSceneAsync(changeSceneName);
        //async.allowSceneActivation = false;
        sceneChangeBoxPos = GetComponent<SceneChangeBoxPos>();
        sceneChangeDeltaTime = 0;// リアルタイムの初期化
        onlyOne = true;
        onTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (fadeMode)
        {
            //フェイドアウトの処理の場合
            case FADE_MODE.Out:
                if (onTrigger)
                {
                    if (fadeInDef < gage)
                    {
                        material.SetFloat("_Gauge", gage -= sceneChangeDeltaTime * speed);
                        sceneChangeDeltaTime += (1.0f / 120);
                    }
                    else
                    {
                        //async.allowSceneActivation = true;
                        SceneManager.LoadScene(changeSceneName);
                        onlyOne = true;
                    }
                }
                break;

            //フェイドインの処理の場合
            case FADE_MODE.In:
                if (fadeOutDef > gage)
                {
                    material.SetFloat("_Gauge", gage += sceneChangeDeltaTime * speed);
                    sceneChangeDeltaTime += (1.0f / 120);
                    Debug.Log("sceneChangeDeltaTime : " + sceneChangeDeltaTime);
                }
                else
                {
                    if(onlyOne)
                    {
                        //何秒か待ってからタイムスケールを戻す
                        StartCoroutine(TimeScaleWait(timeScaleWaitTime));
                        onlyOne = false;
                    }
                }
                break;

            default: break;
        }
    }

    //フェイドアウトとフェイトインを切り替える
    public void ChangeFadeMode()
    {
        switch (fadeMode)
        {
            case FADE_MODE.Out:
                gage = fadeInDef;
                fadeMode = FADE_MODE.In;
                break;

            case FADE_MODE.In:
                gage = fadeOutDef;
                fadeMode = FADE_MODE.Out;
                break;

            default: break;
        }
    }

    //タイムスケールを戻す
    IEnumerator TimeScaleWait(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);

        Time.timeScale = 1;
        PlayMusic.playMusic = true;
    }

    public void OnTrigger()
    {
        sceneChangeBoxPos.BoxPosChange();
        onTrigger = true;
    }
}
