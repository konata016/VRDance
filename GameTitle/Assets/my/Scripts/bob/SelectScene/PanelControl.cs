using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelControl : MonoBehaviour
{
    private int jumpTimePlus = 0;
    private int jumpTime = 0;
    private enum FOOTMOVEMENT { Up, Down }// 0:上げる 1:下げる
    private FOOTMOVEMENT footMovement_R;
    private FOOTMOVEMENT footMovement_L;
    void Start()
    {
        footMovement_R = FOOTMOVEMENT.Down;
        footMovement_L = FOOTMOVEMENT.Down;
    }
    
    void Update()
    {
        /* 右足 */
        if (footMovement_R == FOOTMOVEMENT.Down)// 地上
        {

        }
        else if (footMovement_R == FOOTMOVEMENT.Up)// 空中
        {

        }

        /* 左足 */
        if (footMovement_L == FOOTMOVEMENT.Down)// 地上
        {

        }
        else if (footMovement_L == FOOTMOVEMENT.Up)// 空中
        {

        }
    }
}
