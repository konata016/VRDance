using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private SceneChangeEffect sceneChangeEffect;
    public static bool Set_sceneChange { get; set; }// シーン移行時に操作をしないようにする
    public static bool Set_JumpJudgment { private get; set; }
    public static bool moveSwitch { private get; set; }
    void Start()
    {
        GameObject anotherObject = GameObject.Find("SceneChangeBox");
        sceneChangeEffect = anotherObject.GetComponent<SceneChangeEffect>();

        Set_sceneChange = true;
        moveSwitch = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Set_JumpJudgment)// セレクト画面へ
        {
            JumpJudgment();
            Set_JumpJudgment = false;
        }
    }

    public void JumpJudgment()
    {
        if (moveSwitch　&& Set_sceneChange)
        {
            SE_Manager.SePlay(SE_Manager.SE_NAME.SceneChange);
            sceneChangeEffect.ChangeFadeMode();
            sceneChangeEffect.OnTrigger();
            Set_sceneChange = false;
        }
    }
}
