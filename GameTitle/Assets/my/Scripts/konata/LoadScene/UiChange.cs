using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (LoadSceneManager.GetEndProcess)
        {
            processNow.SetActive(false);
            processEnd.SetActive(true);
            Invoke("DissolveControl", 2f);
        }
        else
        {
            processEnd.SetActive(false);
            processNow.SetActive(true);
        }
    }

    void DissolveControl()
    {
        sceneChangeObj.GetComponent<SceneChangeEffect>().fadeMode = SceneChangeEffect.FADE_MODE.Out;
        sceneChangeObj.GetComponent<SceneChangeEffect>().OnTrigger();
    }
}
