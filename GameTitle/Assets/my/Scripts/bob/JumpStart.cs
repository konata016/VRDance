using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStart : MonoBehaviour
{
    public static Vector3 groundPosition;// 地面の位置
    private Vector3 lowFootPosition;     // 足の位置
    public bool onGroundL, onGroundR;    // 足が地面についているか否か
    private float timeCheck;             // 地面変更経過時間
    private float timeRegulary = 0.5f;   // 地面の位置をチェックする周期
    public int groundPosSaveCapacity = 2;// 地面の位置の保存数
    private Vector3 groundPosSave;       // 地面の位置(保存)
    private Vector3 groundPosSaveSave;   // 地面の位置(保存)
    private Vector3 footPosL, footPosR;  // コントローラの取得
    private bool only1Time;              // 一度だけ実行
    private int roundedDown = 100;       // 小数点以下切り捨て
    private bool jumpSwtich = false;     // ジャンプしたらスイッチ
    private bool landingSwtich = false;  // 着地したらスイッチ
    
    private AudioSource audioSource;// 音源
    public AudioClip leftFoot;      // 左足
    public AudioClip rightFoot;     // 右足
    public AudioClip landing;       // 着地

    // 受け渡す変数
    public enum ISGROUNDTOUCH { Landing, EndProcess, JumpWait }// 0:着地 1:判定後 2:ジャンプ中
    public static ISGROUNDTOUCH isGroundTouch_R { get; set; }
    public static ISGROUNDTOUCH isGroundTouch_L { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        footPosL = GameObject.Find("LeftHandAnchor").transform.position;
        footPosR = GameObject.Find("RightHandAnchor").transform.position;

        isGroundTouch_R = ISGROUNDTOUCH.EndProcess;
        isGroundTouch_L = ISGROUNDTOUCH.EndProcess;

        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(only1Time)
        {
            if (footPosL.y <= footPosR.y)// 地面の位置を取得（初期値）
                groundPosition.y = Mathf.Floor(footPosL.y * roundedDown) / roundedDown;
            else
                groundPosition.y = Mathf.Floor(footPosR.y * roundedDown) / roundedDown;

            groundPosSave.y = groundPosition.y;
            groundPosSaveSave.y = groundPosition.y;

            only1Time = false;
        }
        footPosL = GameObject.Find("LeftHandAnchor").transform.position;
        footPosR = GameObject.Find("RightHandAnchor").transform.position;

        timeCheck += Time.deltaTime;
        //Debug.Log("前回の更新からの時間：" + timeCheck);// 経過時間のログ

        if (timeCheck >= timeRegulary)// 地面の位置を取得（更新）
        {
            timeCheck = 0.0f;

            if (footPosL.y <= footPosR.y)
            {
                lowFootPosition.y = Mathf.Floor(footPosL.y * roundedDown) / roundedDown;
            }
            else
            {
                lowFootPosition.y = Mathf.Floor(footPosR.y * roundedDown) / roundedDown;
            }

            if (lowFootPosition.y == groundPosition.y)// 地面と足の位置が同じとき
            {
                    groundPosSave.y = lowFootPosition.y;
                    groundPosSaveSave.y = lowFootPosition.y;
            }
            else// 地面と足の位置が違うとき
            {
                //Debug.Log("lowFootPosition" + lowFootPosition);// 更新したかどうかのログ
                //Debug.Log("groundPosSave" + groundPosSave);// 更新したかどうかのログ
                //Debug.Log("groundPosSaveSave" + groundPosSaveSave);// 更新したかどうかのログ
                if (lowFootPosition.y == groundPosSave.y && lowFootPosition.y == groundPosSaveSave.y)
                {
                    groundPosition.y = lowFootPosition.y;
                    //Debug.Log("更新3");// 更新したかどうかのログ
                }
                else if (lowFootPosition.y == groundPosSave.y)
                {
                    groundPosSaveSave.y = lowFootPosition.y;
                    //Debug.Log("更新2");// 更新したかどうかのログ
                }
                else
                {
                    groundPosSave.y = lowFootPosition.y;
                    //Debug.Log("更新1");// 更新したかどうかのログ
                }
            }
        }
        //Debug.Log(footPosL);// 左足の位置のログ
        //Debug.Log(footPosR);// 右足の位置のログ
        //Debug.Log("groundPosition"+ Mathf.Floor(groundPosition.y * roundedDown) / roundedDown);// 地面の位置のログ

        if (groundPosition.y + 0.03f < Mathf.Floor(footPosL.y * roundedDown) / roundedDown)// 左足上げているか否か
        {
            audioSource.clip = leftFoot;
            audioSource.Play();

            onGroundL = false;
            //Debug.Log("左足");// 左足上げたのログ
            if (isGroundTouch_L == ISGROUNDTOUCH.EndProcess)
            {
                isGroundTouch_L = ISGROUNDTOUCH.JumpWait;
            }
        }
        else
        {
               onGroundL = true;
            if (isGroundTouch_L == ISGROUNDTOUCH.JumpWait)
            {
                isGroundTouch_L = ISGROUNDTOUCH.Landing;
            }
        }

        if (groundPosition.y + 0.03f < Mathf.Floor(footPosR.y * roundedDown) / roundedDown)// 右足上げているか否か
        {
            audioSource.clip = rightFoot;
            audioSource.Play();

            onGroundR = false;
            //Debug.Log("右足");// 右足上げたのログ
            if (isGroundTouch_R == ISGROUNDTOUCH.EndProcess)
            {
                isGroundTouch_R = ISGROUNDTOUCH.JumpWait;
            }
        }
        else
        {
            onGroundR = true;
            if (isGroundTouch_R == ISGROUNDTOUCH.JumpWait)
            {
                isGroundTouch_R = ISGROUNDTOUCH.Landing;
            }
        }

        if (onGroundL == false && onGroundR == false && jumpSwtich == false)
        {
            jumpSwtich = true;
            Debug.Log("ジャンプ");// ジャンプしたのログ

            landingSwtich = false;
        }
        if (onGroundL == true && onGroundR == true && jumpSwtich == true)
        {
            landingSwtich = true;
            Debug.Log("着地");// 着地したのログ

            audioSource.clip = landing;
            audioSource.Play();

            jumpSwtich = false;
        }

        //Debug.Log("onGroundL" + onGroundL);// 左足の位置のログ
        //Debug.Log("onGroundR" + onGroundR);// 右足の位置のログ
        //Debug.Log("jumpSwtich" + jumpSwtich);// ジャンプの位置のログ
        //Debug.Log("landingSwtich" + landingSwtich);// 着地の位置のログ

    }
}
