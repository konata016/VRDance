using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BpmMove : MonoBehaviour
{
    public int bpm = 120;
    private int bpmOld;
    private int bpmTiming = 0;
    private bool moveSwitch = false;
    public enum BOXORIENTATION { Front, Right, Back, Left, Top, Bottom, Other }// 0:正面 1:右面 2:背後 3:左面 4:上面 5:底面 6:その他
    private BOXORIENTATION boxOrientation { get; set; }

    void Start()
    {
        boxOrientation = BOXORIENTATION.Front;
        bpmOld = bpm;
    }
    
    void Update()
    {
        // セレクトボックス回転
        if(boxOrientation != BOXORIENTATION.Top)
        {
            if (Input.GetKeyDown(KeyCode.A))// 上から見て時計回転
            {
                DOTween
                    .To(value => Y_AxisRotate(value, 1), 0, 1, 0.5f)
                    .SetEase(Ease.OutCubic)
                    .OnComplete(() => Y_AxisRotate_Number(1));
            }
            if (Input.GetKeyDown(KeyCode.D))// 上から見て反時計回転
            {
                DOTween
                    .To(value => Y_AxisRotate(value, -1), 0, 1, 0.5f)
                    .SetEase(Ease.OutCubic)
                    .OnComplete(() => Y_AxisRotate_Number(-1));
            }
            //if (Input.GetKeyDown(KeyCode.W))// 曲詳細へ
            //{
            //    DOTween
            //        .To(value => X_AxisRotate(value, 1), 0, 1, 0.5f)
            //        .SetEase(Ease.OutCubic)
            //        .OnComplete(() => boxOrientation = BOXORIENTATION.Top);
            //}
        }
        //else
        //{
        //    if (Input.GetKeyDown(KeyCode.S))// 戻る
        //    {
        //        DOTween
        //            .To(value => X_AxisRotate(value, -1), 0, 1, 0.5f)
        //            .SetEase(Ease.OutCubic)
        //            .OnComplete(() => X_AxisRotate_Number());
        //    }
        //}
        Debug.Log("boxOrientation :" + boxOrientation);
    }
    
    private void Y_AxisRotate(float value, int a)
    {
        var rot = transform.localEulerAngles;
        rot.y = Mathf.Lerp((int)boxOrientation * 90, (int)boxOrientation * 90 + 90 * a, value);
        transform.localEulerAngles = rot;
    }
    private void X_AxisRotate(float value, int a)
    {
        var rot = transform.localEulerAngles;
        if(a == 1)
        {
            rot.x = Mathf.Lerp(0, 270, value);
        }
        else
        {
            rot.x = Mathf.Lerp(270, 0, value);
        }
        transform.localEulerAngles = rot;
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

    private void X_AxisRotate_Number()
    {
        if (transform.localEulerAngles.y == (int)BOXORIENTATION.Front * 90)
            boxOrientation = BOXORIENTATION.Front;
        else if (transform.localEulerAngles.y == (int)BOXORIENTATION.Right * 90)
            boxOrientation = BOXORIENTATION.Right;
        else if (transform.localEulerAngles.y == (int)BOXORIENTATION.Back * 90)
            boxOrientation = BOXORIENTATION.Back;
        else if (transform.localEulerAngles.y == (int)BOXORIENTATION.Left * 90)
            boxOrientation = BOXORIENTATION.Left;
    }
}
