using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BpmMove : MonoBehaviour
{
    public int bpm = 120;
    private int bpmOld;
    private int bpmTiming = 0;
    private bool moveSwitch = true;
    public enum BOXORIENTATION { Front, Right, Back, Left, SoundName, Bottom, Other }// 0:正面 1:右面 2:背後 3:左面 4:曲名とか 5:底面 6:その他
    private BOXORIENTATION boxOrientation { get; set; }
    private BOXORIENTATION boxOrientation_Old;

    float value_Old;    //回転した差分を引く用

    void Start()
    {
        boxOrientation = BOXORIENTATION.Front;
        bpmOld = bpm;
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
                        .To(value => Y_AxisRotate(value, 1), 0, 1, 0.5f)
                        .SetEase(Ease.OutCubic)
                        .OnStart(() => moveSwitch = false)
                        .OnComplete(() => {
                            Y_AxisRotate_Number(1);
                            moveSwitch = true;
                        });
                }
                else if (Input.GetKeyDown(KeyCode.D))// 上から見て反時計回転
                {
                    DOTween
                        .To(value => Y_AxisRotate(value, -1), 0, 1, 0.5f)
                        .SetEase(Ease.OutCubic)
                        .OnStart(() => moveSwitch = false)
                        .OnComplete(() => {
                            Y_AxisRotate_Number(-1);
                            moveSwitch = true;
                        });
                }
                else if (Input.GetKeyDown(KeyCode.W))// 曲詳細へ
                {
                    value_Old = 0;
                    DOTween
                        .To(value => SoundName_AxisRotate(value), 0, 360, 0.5f)
                        .SetEase(Ease.OutCubic)
                        .OnStart(() => {
                            boxOrientation_Old = boxOrientation;
                            moveSwitch = false;
                        })
                        .OnComplete(() => {
                            boxOrientation = BOXORIENTATION.SoundName;
                            moveSwitch = true;
                        });
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.S))// 戻る
                {
                    value_Old = 0;
                    DOTween
                        .To(value => Reset_AxisRotate(value), 0, 360, 0.5f)
                        .SetEase(Ease.OutCubic)
                        .OnStart(() => moveSwitch = false)
                        .OnComplete(() => {
                            boxOrientation = boxOrientation_Old;
                            moveSwitch = true;
                        });
                }
            }
        }
        else
        {
            Debug.Log("boxOrientation :" + boxOrientation);
            //Debug.Log("boxOrientation_Old :" + boxOrientation_Old);
            //Debug.Log("moveSwitch :" + moveSwitch);
        }
    }
    
    private void Y_AxisRotate(float value, int a)
    {
        var rot = transform.localEulerAngles;
        rot.y = Mathf.Lerp((int)boxOrientation * 90, (int)boxOrientation * 90 + 90 * a, value);
        transform.localEulerAngles = rot;
    }
    private void SoundName_AxisRotate(float value)
    {
        transform.Rotate(new Vector3(1, 0, 0), value - value_Old, Space.World);
        value_Old = value;
        //Debug.Log(value);
    }
    private void Reset_AxisRotate(float value)
    {
        transform.Rotate(new Vector3(-1, 0, 0), value - value_Old, Space.World);
        value_Old = value;
    }
    private void Y_AxisRotate_Number(int a)
    {
        if (boxOrientation == BOXORIENTATION.Front)
        {
            if (a == 1)
                boxOrientation = BOXORIENTATION.Right;
            else
                boxOrientation = BOXORIENTATION.Left;
        }
        else if (boxOrientation == BOXORIENTATION.Right)
        {
            if (a == 1)
                boxOrientation = BOXORIENTATION.Back;
            else
                boxOrientation = BOXORIENTATION.Front;
        }
        else if (boxOrientation == BOXORIENTATION.Back)
        {
            if (a == 1)
                boxOrientation = BOXORIENTATION.Left;
            else
                boxOrientation = BOXORIENTATION.Right;
        }
        else if (boxOrientation == BOXORIENTATION.Left)
        {
            if (a == 1)
                boxOrientation = BOXORIENTATION.Front;
            else
                boxOrientation = BOXORIENTATION.Back;
        }
    }
}
