using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BpmMove_Cube : MonoBehaviour
{
    private int bpmTiming = 0;
    private bool moveSwitch = true;
    public enum BOXORIENTATION { soundBox_1, soundBox_2, soundBox_3, soundBox_4, SoundName }// 0:正面 1:右面 2:背後 3:左面 4:曲名とか
    public static BOXORIENTATION boxOrientation { get; set; }
    public BOXORIENTATION boxOrientation_Old;
    float value_Old;    //回転した差分を引く用
    private MusicManagement musicManagement;
    private SceneChangeEffect sceneChangeEffect;
    public static bool Set_LeftJudgment {private get; set; }
    public static bool Set_RightJudgment { private get; set; }
    public static bool Set_JumpJudgment { private get; set; }
    public static bool Set_sceneChange { get; set; }// シーン移行時に操作をしないようにする

    [SerializeField] GameObject SoundInformation;
    [SerializeField] GameObject soundBoxes;

    void Start()
    {
        boxOrientation = BOXORIENTATION.soundBox_1;
        boxOrientation_Old = boxOrientation;
        musicManagement = GetComponent<MusicManagement>();
        GameObject childObject = transform.Find("SoundInformation").gameObject;
        GameObject anotherObject = GameObject.Find("SceneChangeBox");
        sceneChangeEffect = anotherObject.GetComponent<SceneChangeEffect>();
        Set_LeftJudgment = false;
        Set_RightJudgment = false;
        Set_JumpJudgment = false;
        Set_sceneChange = true;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Set_LeftJudgment)// 上から見て時計回転
        {
            LeftJudgment();
            Set_LeftJudgment = false;
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
        }
        else if (Input.GetKeyDown(KeyCode.S))// 戻る
        {
            LeftJudgment();
        }
    }
    public void JumpJudgment()
    {
        if (moveSwitch && Set_sceneChange)
        {
            // セレクトボックス回転
            if (boxOrientation != BOXORIENTATION.SoundName)
            {
                DOTween
                    .To(value => SoundName_AxisRotate(value), 0, 270, 0.5f)
                    .SetEase(Ease.OutBack)
                    .OnStart(() => {
                        moveSwitch = false;
                        value_Old = 0;
                    })


                    .OnUpdate(() => {// 対象の値が変更される度によばれる
                        SoundInformation.SetActive(true);
                        soundBoxes.SetActive(false);
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
        if (moveSwitch && Set_sceneChange)
        {
            // セレクトボックス回転
            if (boxOrientation != BOXORIENTATION.SoundName)
            {
                DOTween
                    .To(value => Y_AxisRotate(value, -1), 0, 90, 0.4f)
                    .SetEase(Ease.OutBack)
                    .OnStart(() => {
                        Y_AxisRotate_Number(-1);
                        musicManagement.nomberOfMusic--;
                        if (musicManagement.nomberOfMusic == -1)
                            musicManagement.nomberOfMusic = musicManagement.musicInfoList.Count - 1;
                        musicManagement.MusicInformationSet();

                        moveSwitch = false;
                        value_Old = 0;
                    })
                    .OnComplete(() => {
                        moveSwitch = true;
                    });
            }
            else
            {
                DOTween
                    .To(value => Reset_AxisRotate(value), 0, 270, 0.5f)
                    .SetEase(Ease.OutBack)
                    .OnStart(() => {
                        moveSwitch = false;
                        value_Old = 0;
                    })
                    .OnUpdate(() => {// 対象の値が変更される度によばれる
                        SoundInformation.SetActive(false);
                        soundBoxes.SetActive(true);
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
        if (moveSwitch && Set_sceneChange)
        {
            // セレクトボックス回転
            if (boxOrientation != BOXORIENTATION.SoundName)
            {
                DOTween
                    .To(value => Y_AxisRotate(value, 1), 0, 90, 0.4f)
                    .SetEase(Ease.OutBack)
                    .OnStart(() => {// アニメーション開始時によばれる
                            Y_AxisRotate_Number(1);
                        musicManagement.nomberOfMusic++;
                        if (musicManagement.nomberOfMusic == musicManagement.musicInfoList.Count)
                            musicManagement.nomberOfMusic = 0;
                        musicManagement.MusicInformationSet();

                        moveSwitch = false;
                        value_Old = 0;
                    })
                    .OnComplete(() => {// アニメーションが終了時によばれる
                            moveSwitch = true;
                    });
            }
            else
            {
                // 曲選択
                sceneChangeEffect.ChangeFadeMode();
                sceneChangeEffect.OnTrigger();
                Set_sceneChange = false;
            }
        }
    }

    public void Y_AxisRotate(float value, int a)
    {
        transform.Rotate(new Vector3(0, a, 0), value - value_Old, Space.World);
        value_Old = value;
    }
    public void SoundName_AxisRotate(float value)
    {
        transform.Rotate(new Vector3(1, 0, 0), value - value_Old, Space.World);
        value_Old = value;
    }
    public void Reset_AxisRotate(float value)
    {
        transform.Rotate(new Vector3(-1, 0, 0), value - value_Old, Space.World);
        value_Old = value;
    }
    private void Y_AxisRotate_Number(int a)
    {
        if (boxOrientation == BOXORIENTATION.soundBox_1)
        {
            if (a == 1)
                boxOrientation = BOXORIENTATION.soundBox_2;
            else
                boxOrientation = BOXORIENTATION.soundBox_4;
        }
        else if (boxOrientation == BOXORIENTATION.soundBox_2)
        {
            if (a == 1)
                boxOrientation = BOXORIENTATION.soundBox_3;
            else
                boxOrientation = BOXORIENTATION.soundBox_1;
        }
        else if (boxOrientation == BOXORIENTATION.soundBox_3)
        {
            if (a == 1)
                boxOrientation = BOXORIENTATION.soundBox_4;
            else
                boxOrientation = BOXORIENTATION.soundBox_2;
        }
        else if (boxOrientation == BOXORIENTATION.soundBox_4)
        {
            if (a == 1)
                boxOrientation = BOXORIENTATION.soundBox_1;
            else
                boxOrientation = BOXORIENTATION.soundBox_3;
        }
    }
}
