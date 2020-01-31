using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// LoadSceneのUIの切り替えをする
/// </summary>
public class UiChange : MonoBehaviour
{
    public GameObject processNow;
    public GameObject processEnd;

    public GameObject sceneChangeObj;
    bool on;

    // Start is called before the first frame update
    void Start()
    {
        processEnd.SetActive(false);
        processNow.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //UIの切り替え
        if (LoadSceneManager.GetEndProcess)
        {
            processNow.SetActive(false);
            processEnd.SetActive(true);

            if (!on)
            {
                SE_Manager.SePlay(SE_Manager.SE_NAME.LoadComplete);
                on = true;
            }

            //何秒か待ってからフェイドアウトする
            Invoke("DissolveControl", 2f);
        }
        else
        {
            processEnd.SetActive(false);
            processNow.SetActive(true);
        }
    }

    //フェイドアウトする
    void DissolveControl()
    {
        sceneChangeObj.GetComponent<SceneChangeEffect>().fadeMode = SceneChangeEffect.FADE_MODE.Out;
        sceneChangeObj.GetComponent<SceneChangeEffect>().OnTrigger();
    }

}
