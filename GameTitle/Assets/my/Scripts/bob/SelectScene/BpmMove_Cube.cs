using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BpmMove_Cube : MonoBehaviour
{
    public int bpm = 120;
    private int bpmOld;
    private int bpmTiming = 0;
    private bool moveSwitch = true;
    public enum BOXORIENTATION { soundBox_1, soundBox_2, soundBox_3, soundBox_4, SoundName, Bottom, Other }// 0:正面 1:右面 2:背後 3:左面 4:曲名とか 5:底面 6:その他
    public static BOXORIENTATION boxOrientation { get; set; }
    public BOXORIENTATION boxOrientation_Old;
    float value_Old;    //回転した差分を引く用
    private MusicManagement musicManagement;
    private CanvasAlpha canvasAlpha;

    void Start()
    {
        boxOrientation = BOXORIENTATION.soundBox_1;
        boxOrientation_Old = boxOrientation;
        bpmOld = bpm;
        musicManagement = GetComponent<MusicManagement>();
        GameObject childObject = transform.Find("SoundInformation").gameObject;
        canvasAlpha = childObject.GetComponent<CanvasAlpha>();
    }
    
    void Update()
    {
        if(moveSwitch)
        {
            // セレクトボックス回転
            if (boxOrientation != BOXORIENTATION.SoundName)
            {
                if (Input.GetKeyDown(KeyCode.A))// 上から見て時計回転
                {
                    DOTween
                        .To(value => Y_AxisRotate(value, 1), 0, 90, 0.4f)
                        .SetEase(Ease.OutElastic)
                        .OnStart(() => {// アニメーション開始時によばれる
                            Y_AxisRotate_Number(1);
                            musicManagement.nomberOfMusic++;
                            if (musicManagement.nomberOfMusic == musicManagement.musicInfoList.Count)
                                musicManagement.nomberOfMusic = 0;
                            musicManagement.MusicInformationSet();

                            moveSwitch = false;
                            value_Old = 0;
                        })
                        .OnUpdate(() => {// 対象の値が変更される度によばれる
                        })
                        .OnComplete(() => {// アニメーションが終了時によばれる
                            moveSwitch = true;
                        });
                }
                else if (Input.GetKeyDown(KeyCode.D))// 上から見て反時計回転
                {
                    DOTween
                        .To(value => Y_AxisRotate(value, -1), 0, 90, 0.4f)
                        .SetEase(Ease.OutElastic)
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
                else if (Input.GetKeyDown(KeyCode.W))// 曲詳細へ
                {
                    DOTween
                        .To(value => SoundName_AxisRotate(value), 0, 270, 0.5f)
                        .SetEase(Ease.OutBack)
                        .OnStart(() => {
                            moveSwitch = false;
                            value_Old = 0;
                        })
                        .OnUpdate(() => {// 対象の値が変更される度によばれる
                            canvasAlpha.MusicInformation_Alpha(0.4f);
                        })
                        .OnComplete(() => {
                            boxOrientation_Old = boxOrientation;
                            boxOrientation = BOXORIENTATION.SoundName;
                            moveSwitch = true;
                        });
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.S))// 戻る
                {
                    DOTween
                        .To(value => Reset_AxisRotate(value), 0, 270, 0.5f)
                        .SetEase(Ease.OutBack)
                        .OnStart(() => {
                            moveSwitch = false;
                            value_Old = 0;
                        })
                        .OnUpdate(() => {// 対象の値が変更される度によばれる
                            canvasAlpha.MusicInformation_Alpha(-0.4f);
                        })
                        .OnComplete(() => {
                            boxOrientation = boxOrientation_Old;
                            moveSwitch = true;
                        });
                }
            }
        }
        else
        {
            //Debug.Log("boxOrientation :" + boxOrientation);
            //Debug.Log("boxOrientation_Old :" + boxOrientation_Old);
            //Debug.Log("moveSwitch :" + moveSwitch);
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
