using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// トリガーアクション取得用
/// </summary>
public class TriggerManager : MonoBehaviour
{

    public GameObject footL;
    public GameObject footR;

    bool tmpL,tmpR;
    int frameCount;

    // Start is called before the first frame update
    void Start()
    {
        //初期化
        GetOnTriggerFoot = false;
        GetOnTriggerFootL = false;
        GetOnTriggerFootR = false;
        GetOnTriggerJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        FootJudge2();


        //デバッグ用
        if (Input.GetKeyDown(KeyCode.Space)) GetOnTriggerFoot = true;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) GetOnTriggerFootL = true;
        if (Input.GetKeyDown(KeyCode.RightArrow)) GetOnTriggerFootR = true;
        if (Input.GetKeyDown(KeyCode.UpArrow)) GetOnTriggerJump = true;

        void FootJudge2()
        {
            //どの足でも反応
            GetOnTriggerFoot = false;
            GetOnTriggerFoot = Event(footL, GetOnTriggerFoot);
            GetOnTriggerFoot = Event(footR, GetOnTriggerFoot);

            //右足に反応
            GetOnTriggerFootR = false;
            GetOnTriggerFootR = Event(footR, GetOnTriggerFootR);
            if (!tmpR) tmpR = GetOnTriggerFootR;

            //左足に反応
            GetOnTriggerFootL = false;
            GetOnTriggerFootL = Event(footL, GetOnTriggerFootL);
            if (!tmpL) tmpL = GetOnTriggerFootL;

            //ジャンプに反応
            if (GetOnTriggerJump)
            {
                GetOnTriggerJump = false;
                frameCount = 0;
            }
            GetOnTriggerJump = Clearflag(3);

            //〇フレーム待つ
            bool Clearflag(int waitFrame)
            {
                if (frameCount == waitFrame)
                {
                    tmpR = false;
                    tmpL = false;
                    frameCount = 0;
                }
                frameCount++;
                return tmpR && tmpL;
            }

            //イベントの処理
            bool Event(GameObject obj, bool on)
            {
                if (obj.GetComponent<Foot>().Event == GroundManager.EVENT.Down)
                {
                    obj.GetComponent<Foot>().Event = GroundManager.EVENT.End;
                    return true;
                }
                else return on;
            }
        }
    }

    /// <summary>
    /// どちらか片方の足が地面についたときの判定取得
    /// </summary>
    public static bool GetOnTriggerFoot { get; private set; }

    /// <summary>
    /// 左足が地面についたときの判定取得
    /// </summary>
    public static bool GetOnTriggerFootL { get; private set; }

    /// <summary>
    /// 右足が地面についたときの判定取得
    /// </summary>
    public static bool GetOnTriggerFootR { get; private set; }

    /// <summary>
    /// ジャンプして着地したときの判定取得
    /// </summary>
    public static bool GetOnTriggerJump { get; private set; }

}
