using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelControl : MonoBehaviour
{
    public static Vector3 groundPosition;// 地面の位置
    private Vector3 lowFootPosition;     // 足の位置
    private bool jumping = false;   // ジャンプしてるか否か
    private int jumpFrame = 0;      // ジャンプ中の判定 
    public int jumpFrameTime = 10;  // ジャンプ中の判定時間
    private enum FOOTMOVEMENT { Up, Down }// 0:上げる 1:下げる
    private FOOTMOVEMENT footMovement_R;
    private FOOTMOVEMENT footMovement_R_Old;
    private FOOTMOVEMENT footMovement_L;
    private FOOTMOVEMENT footMovement_L_Old;
    void Start()
    {
        footMovement_R = FOOTMOVEMENT.Down;
        footMovement_R_Old = footMovement_R;
        footMovement_L = FOOTMOVEMENT.Down;
        footMovement_L_Old = footMovement_L;
    }
    
    void Update()
    {
        /* 足の位置判定 */
        /* ステップ判定 */
        if (jumping)// ジャンプ
        {

            if (footMovement_R == FOOTMOVEMENT.Down || footMovement_L == FOOTMOVEMENT.Down)// 両足着地
            {
                // ジャンプ判定
            }
            else if (footMovement_R == FOOTMOVEMENT.Down)// 右足着地
            {
                jumpFrame++;
                if (jumpFrame == jumpFrameTime)
                {
                    jumpFrame = 0;
                    jumping = false;

                    // 右足判定
                }
            }
            else if (footMovement_L == FOOTMOVEMENT.Down)// 左足着地
            {
                jumpFrame++;
                if (jumpFrame == jumpFrameTime)
                {
                    jumpFrame = 0;
                    jumping = false;

                    // 左足判定
                }
            }
        }
        else
        {
            /* 右足 */
            if (footMovement_R == FOOTMOVEMENT.Down || jumping == false)// 地上
            {
                if(footMovement_R_Old != footMovement_R)
                {
                    // 右足判定
                }
            }
            else if (footMovement_R == FOOTMOVEMENT.Up)// 空中
            {
                if (footMovement_L == FOOTMOVEMENT.Up)// 空中
                {
                    jumping = true;
                }
            }
            /* 左足 */
            if (footMovement_L == FOOTMOVEMENT.Down || jumping == false)// 地上
            {
                if (footMovement_L_Old != footMovement_L)
                {
                    // 左足判定
                }
            }
            else if (footMovement_L == FOOTMOVEMENT.Up)// 空中
            {
                if (footMovement_R == FOOTMOVEMENT.Up)// 空中
                {
                    jumping = true;
                }
            }
        }

        footMovement_R_Old = footMovement_R;
        footMovement_L_Old = footMovement_L;
    }
}
