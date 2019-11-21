using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepDetermination : MonoBehaviour
{
    /* 判定関係 */
    public static Vector3 groundPosition;// 地面の位置
    private Vector3 lowFootPosition;     // 低い足の位置
    private Vector3 footPosL, footPosR;  // コントローラの取得
    private Vector3 groundPosSave;       // 地面の位置(保存)
    private Vector3 groundPosSaveSave;   // 地面の位置(保存)
    private float timeCheck;             // 地面変更経過時間
    private float timeRegulary = 0.5f;   // 地面の位置をチェックする周期
    private int roundedDown = 100;       // 小数点以下切り捨て
    private bool only1Time;              // 一度だけ実行
    /* オブジェクト */
    public GameObject ripplesObj;
    /* サウンド関係 */
    private AudioSource audioSource;// 音源
    public AudioClip leftFoot;      // 左足
    public AudioClip rightFoot;     // 右足
    public AudioClip landing;       // 着地
    /* 受け渡す変数 */
    public enum ISGROUNDTOUCH { Wait, Jump, Landing, EndProcess }// 0:待ち 1:ジャンプ 2:着地 3:判定取った後
    public static ISGROUNDTOUCH isGroundTouch_L { get; set; }
    public static ISGROUNDTOUCH isGroundTouch_R { get; set; }

    void Start()
    {
        footPosL = GameObject.Find("LeftHandAnchor").transform.position;
        footPosR = GameObject.Find("RightHandAnchor").transform.position;

        isGroundTouch_L = ISGROUNDTOUCH.Wait;
        isGroundTouch_R = ISGROUNDTOUCH.Wait;

        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (only1Time)
        {
            if (footPosL.y <= footPosR.y)// 地面の位置を取得（初期値）
                groundPosition.y = Mathf.Floor(footPosL.y * roundedDown) / roundedDown;
            else
                groundPosition.y = Mathf.Floor(footPosR.y * roundedDown) / roundedDown;

            groundPosSave = groundPosition;
            groundPosSaveSave = groundPosition;

            only1Time = false;
        }
        footPosL = GameObject.Find("LeftHandAnchor").transform.position;
        footPosR = GameObject.Find("RightHandAnchor").transform.position;

        timeCheck += Time.deltaTime;
        if (timeCheck >= timeRegulary)// 地面の位置を取得（更新）
        {
            timeCheck = 0.0f;

            if (footPosL.y <= footPosR.y)
                lowFootPosition.y = Mathf.Floor(footPosL.y * roundedDown) / roundedDown;
            else
                lowFootPosition.y = Mathf.Floor(footPosR.y * roundedDown) / roundedDown;

            if (lowFootPosition.y == groundPosition.y)// 地面と足の位置が同じとき
            {
                groundPosSave.y = lowFootPosition.y;
                groundPosSaveSave.y = lowFootPosition.y;
            }
            else// 地面と足の位置が違うとき
            {
                if (lowFootPosition.y == groundPosSave.y && lowFootPosition.y == groundPosSaveSave.y)
                    groundPosition.y = lowFootPosition.y;
                else if (lowFootPosition.y == groundPosSave.y)
                    groundPosSaveSave.y = lowFootPosition.y;
                else
                    groundPosSave.y = lowFootPosition.y;
            }
        }
        /* 左足の判定 */
        if (isGroundTouch_L == ISGROUNDTOUCH.Wait)// 待ち
        {
            //Debug.Log("待ち");
            if (groundPosition.y + 0.03f < Mathf.Floor(footPosL.y * roundedDown) / roundedDown)// 足を上げた
                isGroundTouch_L = ISGROUNDTOUCH.Jump;
        }
        else if (isGroundTouch_L == ISGROUNDTOUCH.Jump)// ジャンプ
        {
            //Debug.Log("ジャンプ");
            if (groundPosition.y + 0.03f >= Mathf.Floor(footPosL.y * roundedDown) / roundedDown)// 足を地面につけた
            {
                isGroundTouch_L = ISGROUNDTOUCH.Landing;
                audioSource.clip = leftFoot;
                audioSource.Play();
            }
        }
        else if (isGroundTouch_L == ISGROUNDTOUCH.Landing)// 着地
        {
            //Debug.Log("着地");
        }
        else if (isGroundTouch_L == ISGROUNDTOUCH.EndProcess)// 判定後
        {
            //Debug.Log("判定後");
            Instantiate(ripplesObj, footPosL, Quaternion.identity);// 波紋の生成
            isGroundTouch_L = ISGROUNDTOUCH.Wait;
        }
        /* 右足の判定 */
        if (isGroundTouch_R == ISGROUNDTOUCH.Wait)// 待ち
        {
            //Debug.Log("待ち");
            if (groundPosition.y + 0.03f < Mathf.Floor(footPosR.y * roundedDown) / roundedDown)// 足を上げた
                isGroundTouch_R = ISGROUNDTOUCH.Jump;
        }
        else if (isGroundTouch_R == ISGROUNDTOUCH.Jump)// ジャンプ
        {
            //Debug.Log("ジャンプ");
            if (groundPosition.y + 0.03f >= Mathf.Floor(footPosR.y * roundedDown) / roundedDown)// 足を地面につけた
            {
                isGroundTouch_R = ISGROUNDTOUCH.Landing;
                audioSource.clip = rightFoot;
                audioSource.Play();
            }
        }
        else if (isGroundTouch_R == ISGROUNDTOUCH.Landing)// 着地
        {
            //Debug.Log("着地");
        }
        else if (isGroundTouch_R == ISGROUNDTOUCH.EndProcess)// 判定後
        {
            //Debug.Log("判定後");
            Instantiate(ripplesObj, footPosR, Quaternion.identity);// 波紋の生成
            Debug.Log("波紋！");
            isGroundTouch_R = ISGROUNDTOUCH.Wait;
        }
    }
}
