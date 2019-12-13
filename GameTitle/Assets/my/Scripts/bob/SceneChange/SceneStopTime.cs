using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStopTime : MonoBehaviour
{
    float realDeltaTime;
    float sceneChangeDeltaTime;
    void Start()
    {
        Time.timeScale = 0;
    }
    
    void Update()
    {

    }
}
