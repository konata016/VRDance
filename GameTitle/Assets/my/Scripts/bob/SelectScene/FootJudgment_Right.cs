using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FootJudgment_Right : MonoBehaviour
{
    public static Vector3 groundPosition;       // 地面の位置

    public GameObject rightHandAnchor;
    public GameObject leftHandAnchor;

    private Vector3 myLowFootPos;               // 低い足の位置
    private bool onGround_R, onGround_L;        // 足が地面についているか否か
    private bool onGround_R_Old, onGround_L_Old;// 足が地面についているか否か
    private float timeCheck;                    // 地面変更経過時間
    private float timeRegulary = 4.0f;          // 地面の位置をチェックする周期
    private Vector3 groundPosSave;              // 地面の位置(保存)
    private Vector3 groundPosSaveSave;          // 地面の位置(保存)
    private Vector3 footConPos_R, footConPos_L; // コントローラの取得
    private int roundedDown = 100;              // 小数点以下切り捨て
    public float tweak = 0.03f;                 // 微調整
    private bool onlyOneTime = true;            // 一度だけ読み込む
    private bool judgment;                      // 足の位置を更新時にステップを踏まなくする
    // 受け渡す変数
    public enum ISGROUNDTOUCH { Landing, Stoping, Jumping }// 0:着地後 1:静止中 2:ジャンプ中
    public static ISGROUNDTOUCH isGroundTouch_R { get; set; }
    public static ISGROUNDTOUCH isGroundTouch_R_Old { get; set; }
    public static ISGROUNDTOUCH isGroundTouch_L { get; set; }
    public static ISGROUNDTOUCH isGroundTouch_L_Old { get; set; }
    [SerializeField] float landingFlameMax = 0.05f;// ジャンプの判定秒数
    private float landingFlame = 0;   // ジャンプの判定フレーム数計算用
    private BpmMove_Cube bpmMove_Cube;
    private BpmMove_DokudoCube bpmMove_DokudoCube;

    void Start()
    {
        footConPos_R = rightHandAnchor.transform.position;
        footConPos_L = leftHandAnchor.transform.position;
        onlyOneTime = true;
        judgment = true;
        landingFlame = 0;
        bpmMove_Cube = GetComponent<BpmMove_Cube>();
        bpmMove_DokudoCube = GetComponent<BpmMove_DokudoCube>();
        isGroundTouch_R = ISGROUNDTOUCH.Stoping;
        isGroundTouch_L = ISGROUNDTOUCH.Stoping;
        isGroundTouch_R_Old = isGroundTouch_R;
        isGroundTouch_L_Old = isGroundTouch_L;
    }

    void Update()
    {
        //if (onlyOneTime)
        //{
        //    if (footConPos_R.y <= footConPos_L.y)// 地面の位置を取得（初期値）
        //        groundPosition.y = Mathf.Floor(footConPos_R.y * roundedDown) / roundedDown;
        //    else
        //        groundPosition.y = Mathf.Floor(footConPos_L.y * roundedDown) / roundedDown;

        //    onlyOneTime = false;
        //    landingFlame = 0;
        //}
        footConPos_R = rightHandAnchor.transform.position;
        footConPos_L = leftHandAnchor.transform.position;

        groundPosition.y = Mathf.Floor(GroundManager.GetGroundPos.y * roundedDown) / roundedDown;

        Debug.Log("groundPosition : " + groundPosition);

        timeCheck += Time.deltaTime;

        //if (timeCheck >= timeRegulary)// 地面の位置を取得（更新）
        //{
        //    timeCheck = 0.0f;

        //    if (footConPos_R.y <= footConPos_L.y)
        //    {
        //        myLowFootPos.y = Mathf.Floor(footConPos_R.y * roundedDown) / roundedDown;
        //    }
        //    else
        //    {
        //        myLowFootPos.y = Mathf.Floor(footConPos_L.y * roundedDown) / roundedDown;
        //    }

        //    if (myLowFootPos.y == groundPosition.y)// 地面と足の位置が同じとき
        //    {
        //        groundPosSave.y = myLowFootPos.y;
        //        groundPosSaveSave.y = myLowFootPos.y;
        //    }
        //    else// 地面と足の位置が違うとき
        //    {
        //        if (myLowFootPos.y == groundPosSave.y && myLowFootPos.y == groundPosSaveSave.y)
        //        {
        //            judgment = false;
        //            groundPosition.y = myLowFootPos.y;
        //            judgment = true;
        //            isGroundTouch_R = ISGROUNDTOUCH.Stoping;
        //            isGroundTouch_L = ISGROUNDTOUCH.Stoping;
        //        }
        //        else if (myLowFootPos.y == groundPosSave.y)
        //            groundPosSaveSave.y = myLowFootPos.y;
        //        else
        //            groundPosSave.y = myLowFootPos.y;
        //    }
        //}

        onGround_R_Old = onGround_R;
        if (groundPosition.y + tweak < Mathf.Floor(footConPos_R.y * roundedDown) / roundedDown)// 地面の高さ+α < 自分の足の位置　// 右足を上げている
        {
            onGround_R = false;
            if (isGroundTouch_R == ISGROUNDTOUCH.Landing)
            {
                if(judgment)
                {
                    RightJudgment();
                    //Debug.Log("はなしたな！");
                }

                isGroundTouch_R = ISGROUNDTOUCH.Jumping;
            }
            else if (isGroundTouch_R == ISGROUNDTOUCH.Stoping)
            {
                isGroundTouch_R = ISGROUNDTOUCH.Jumping;
            }
        }
        else// 右足を下げている
        {
            onGround_R = true;
            if (isGroundTouch_R == ISGROUNDTOUCH.Jumping)
            {
                if (isGroundTouch_L == ISGROUNDTOUCH.Landing)
                {
                    if (judgment)
                        JumpJudgment();

                    isGroundTouch_R = ISGROUNDTOUCH.Stoping;
                    isGroundTouch_L = ISGROUNDTOUCH.Stoping;
                }
                else
                {
                    landingFlame = 0;
                    isGroundTouch_R = ISGROUNDTOUCH.Landing;
                }
            }
            else if (isGroundTouch_R == ISGROUNDTOUCH.Landing)
            {
                landingFlame += Time.deltaTime;
                if (landingFlame >= landingFlameMax)
                {
                    if (judgment)
                    {
                        RightJudgment();
                        //Debug.Log("ついたとき！");
                    }

                    isGroundTouch_R = ISGROUNDTOUCH.Stoping;
                }
            }
        }

        onGround_L_Old = onGround_L;
        if (groundPosition.y + tweak < Mathf.Floor(footConPos_L.y * roundedDown) / roundedDown)// 地面の高さ+α < 自分の足の位置　// 左足を上げている
        {
            onGround_L = false;
            if (isGroundTouch_L == ISGROUNDTOUCH.Landing)
            {
                if (judgment)
                    LeftJudgment();

                isGroundTouch_L = ISGROUNDTOUCH.Jumping;
            }
            else if (isGroundTouch_L == ISGROUNDTOUCH.Stoping)
            {
                isGroundTouch_L = ISGROUNDTOUCH.Jumping;
            }
        }
        else// 左足を下げている
        {
            onGround_L = true;
            if (isGroundTouch_L == ISGROUNDTOUCH.Jumping)
            {
                if (isGroundTouch_R == ISGROUNDTOUCH.Landing)
                {
                    if (judgment)
                        JumpJudgment();

                    isGroundTouch_R = ISGROUNDTOUCH.Stoping;
                    isGroundTouch_L = ISGROUNDTOUCH.Stoping;
                }
                else
                {
                    landingFlame = 0;
                    isGroundTouch_L = ISGROUNDTOUCH.Landing;
                }
            }
            else if (isGroundTouch_L == ISGROUNDTOUCH.Landing)
            {
                landingFlame += Time.deltaTime;
                if (landingFlame >= landingFlameMax)
                {
                    if (judgment)
                        LeftJudgment();

                    isGroundTouch_L = ISGROUNDTOUCH.Stoping;
                }
            }
        }

        //if (isGroundTouch_R_Old != isGroundTouch_R)
        //{
        //    Debug.Log("isGroundTouch_R : " + isGroundTouch_R);
        //}
        //if (isGroundTouch_L_Old != isGroundTouch_L)
        //{
        //    Debug.Log("isGroundTouch_L : " + isGroundTouch_L);
        //}
        //Debug.Log("groundPosition : " + groundPosition);

    }

    private void JumpJudgment()
    {

        switch (SceneManager.GetActiveScene().name){
            case "SelectScene":
                BpmMove_Cube.Set_JumpJudgment = true;
                BpmMove_DokudoCube.Set_JumpJudgment = true;
                Debug.Log("来てます");
                break;

            case "ManualScene":
                PopUp.OnTriggerJump = true;
                break;

            case "GameScore":
                ScoreController.Set_JumpJudgment = true;
                break;

            default:
                Debug.Log("来てます"); break;
        }

    }
    private void RightJudgment()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "SelectScene":
                BpmMove_Cube.Set_RightJudgment = true;
                BpmMove_DokudoCube.Set_RightJudgment = true;
                Debug.Log("来てます");
                break;

            case "ManualScene":
                PageInstant.OnTriggerFootR = true;
                break;

            default:
                Debug.Log("来てます"); break;
        }
    }
    private void LeftJudgment()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "SelectScene":
                BpmMove_Cube.Set_LeftJudgment = true;
                BpmMove_DokudoCube.Set_LeftJudgment = true;
                Debug.Log("来てます");
                break;

            case "ManualScene":
                PageInstant.OnTriggerFootL = true;
                break;

            default:
                Debug.Log("来てます"); break;
        }
    }
}
