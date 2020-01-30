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

            SE_Manager.SePlay(SE_Manager.SE_NAME.LoadComplete);

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
