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
    bool onTrigger;

    AsyncOperation async;
    // Start is called before the first frame update
    void Start()
    {
        switch (fadeMode)
        {
            case FADE_MODE.Out:gage = 1.8f; break;
            case FADE_MODE.In: gage = 0.8f; break;
            default: break;
        }
        material.SetFloat("_Gauge", gage);
        async = SceneManager.LoadSceneAsync(changeSceneName);
        async.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        OnTrigger();

        switch (fadeMode)
        {
            case FADE_MODE.Out:
                if (onTrigger)
                {
                    if (0.8f < gage) material.SetFloat("_Gauge", gage -= Time.deltaTime * speed);
                    else async.allowSceneActivation = true;
                }
                break;

            case FADE_MODE.In:
                if (1.8f > gage) material.SetFloat("_Gauge", gage += Time.deltaTime * speed);
                break;

            default: break;
        }
    }

    void OnTrigger()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onTrigger = true;
        }
    }
}
