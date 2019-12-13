using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeEffect : MonoBehaviour
{
    public enum FADE_MODE
    {
        Out, In
    }

    public Material material;
    public FADE_MODE fadeMode;
    public float speed = 0.2f;

    public string changeSceneName;

    float gage;
    float fadeOutDef = 1.8f;
    float fadeInDef = 0.8f;
    bool onTrigger;

    float sceneChangeDeltaTime;

    SceneChangeBoxPos sceneChangeBoxPos;

    AsyncOperation async;
    // Start is called before the first frame update
    void Start()
    {
        switch (fadeMode)
        {
            case FADE_MODE.Out:gage = fadeOutDef; break;
            case FADE_MODE.In: gage = fadeInDef; break;
            default: break;
        }
        material.SetFloat("_Gauge", gage);
        async = SceneManager.LoadSceneAsync(changeSceneName);
        async.allowSceneActivation = false;
        sceneChangeBoxPos = GetComponent<SceneChangeBoxPos>();
        sceneChangeDeltaTime = 0;// リアルタイムの初期化
    }

    // Update is called once per frame
    void Update()
    {
        switch (fadeMode)
        {
            case FADE_MODE.Out:
                if (onTrigger)
                {
                    if (fadeInDef < gage)
                    {
                        material.SetFloat("_Gauge", gage -= sceneChangeDeltaTime * speed);
                        sceneChangeDeltaTime += (1.0f / 120);
                    } 
                    else async.allowSceneActivation = true;
                }
                break;

            case FADE_MODE.In:
                if (fadeOutDef > gage)
                {
                    material.SetFloat("_Gauge", gage += sceneChangeDeltaTime * speed);
                    sceneChangeDeltaTime += (1.0f / 120);
                    Debug.Log("sceneChangeDeltaTime : " + sceneChangeDeltaTime);
                }
                break;

            default: break;
        }
    }

    public void OnTrigger()
    {
        sceneChangeBoxPos.BoxPosChange();
        onTrigger = true;
    }
}
