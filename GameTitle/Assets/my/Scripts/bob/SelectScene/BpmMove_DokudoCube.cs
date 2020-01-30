using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BpmMove_DokudoCube : MonoBehaviour
{
    private int bpmTiming = 0;
    private bool moveSwitch = true;
    public enum BOXORIENTATION { soundBox_1, soundBox_2, soundBox_3, soundBox_4, SoundName, Bottom, Other }// 0:正面 1:右面 2:背後 3:左面 4:曲名とか 5:底面 6:その他
    public static BOXORIENTATION boxOrientation { get; set; }
    public  BOXORIENTATION boxOrientation_Old;
    float value_Old;    //回転した差分を引く用
    public static bool Set_LeftJudgment { private get; set; }
    public static bool Set_RightJudgment { private get; set; }
    public static bool Set_JumpJudgment { private get; set; }

    void Start()
    {
        boxOrientation = BOXORIENTATION.soundBox_1;
        boxOrientation_Old = boxOrientation;
        Set_LeftJudgment = false;
        Set_RightJudgment = false;
        Set_JumpJudgment = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Set_LeftJudgment)// 上から見て時計回転
        {
            LeftJudgment();
            Set_LeftJudgment = false;

            SelectSceneUiManager.checkScene = false;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Set_RightJudgment)// 上から見て反時計回転
        {
            RightJudgment();
            Set_RightJudgment = false;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Set_JumpJudgment)// 曲詳細へ
        {
            JumpJudgment();
            Set_JumpJudgment = false;
            SelectSceneUiManager.checkScene = true;
        }
        else if (Input.GetKeyDown(KeyCode.S))// 戻る
        {
            LeftJudgment();
            SelectSceneUiManager.checkScene = false;
        }
    }

    public void JumpJudgment()
    {
        if (moveSwitch && BpmMove_Cube.Set_sceneChange)
        {
            // セレクトボックス回転
            if (boxOrientation != BOXORIENTATION.SoundName)
            {
                SE_Manager.SePlay(SE_Manager.SE_NAME.Jump);

                DOTween
                    .To(value => SoundName_AxisRotate(value), 0, 270, 0.5f)
                    .SetEase(Ease.OutBack)
                    .OnStart(() => {
                        moveSwitch = false;
                        value_Old = 0;
                    })
                    .OnComplete(() => {
                        boxOrientation_Old = boxOrientation;
                        boxOrientation = BOXORIENTATION.SoundName;
                        moveSwitch = true;
                    });
            }
        }
    }
    public void LeftJudgment()
    {
        if (moveSwitch && BpmMove_Cube.Set_sceneChange)
        {
            // セレクトボックス回転
            if (boxOrientation != BOXORIENTATION.SoundName)
            {
                SE_Manager.SePlay(SE_Manager.SE_NAME.Step);

                DOTween
                    .To(value => Y_AxisRotate(value, -1), 0, 90, 0.4f)
                    .SetEase(Ease.OutBack)
                    .OnStart(() => {
                        moveSwitch = false;
                        value_Old = 0;
                    })
                    .OnComplete(() => {
                        moveSwitch = true;
                    });
            }
            else
            {
                SE_Manager.SePlay(SE_Manager.SE_NAME.Cancel);

                DOTween
                    .To(value => Reset_AxisRotate(value), 0, 270, 0.5f)
                    .SetEase(Ease.OutBack)
                    .OnStart(() => {
                        moveSwitch = false;
                        value_Old = 0;
                    })
                    .OnComplete(() => {
                        boxOrientation = boxOrientation_Old;
                        moveSwitch = true;
                    });
            }
        }
    }
    public void RightJudgment()
    {
        if (moveSwitch && BpmMove_Cube.Set_sceneChange)
        {
            SE_Manager.SePlay(SE_Manager.SE_NAME.Step);

            // セレクトボックス回転
            if (boxOrientation != BOXORIENTATION.SoundName)
            {
                DOTween
                    .To(value => Y_AxisRotate(value, 1), 0, 90, 0.4f)
                    .SetEase(Ease.OutBack)
                    .OnStart(() => {// アニメーション開始時によばれる
                            moveSwitch = false;
                        value_Old = 0;
                    })
                    .OnComplete(() => {// アニメーションが終了時によばれる
                            moveSwitch = true;
                    });
            }
        }
        else if(moveSwitch)
        {
            // 曲選択
            SE_Manager.SePlay(SE_Manager.SE_NAME.SceneChange);
        }
    }

    private void Y_AxisRotate(float value, int a)
    {
        transform.Rotate(new Vector3(0, a, 0), value - value_Old, Space.World);
        value_Old = value;
    }
    private void SoundName_AxisRotate(float value)
    {
        transform.Rotate(new Vector3(1, 0, 0), value - value_Old, Space.World);
        value_Old = value;
    }
    private void Reset_AxisRotate(float value)
    {
        transform.Rotate(new Vector3(-1, 0, 0), value - value_Old, Space.World);
        value_Old = value;
    }
}