using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// トリガーアクション取得用
/// </summary>
public class TriggerManager : MonoBehaviour
{
    public static bool SetOnTriggerFoot { private get; set; }
    public static bool SetOnTriggerFootL { private get; set; }
    public static bool SetOnTriggerFootR { private get; set; }
    public static bool SetOnTriggerJump { private get; set; }

    // Start is called before the first frame update
    void Start()
    {
        //初期化
        SetOnTriggerFoot = false;
        SetOnTriggerFootL = false;
        SetOnTriggerFootR = false;
        SetOnTriggerJump = false;

        GetOnTriggerFoot = false;
        GetOnTriggerFootL = false;
        GetOnTriggerFootR = false;
        GetOnTriggerJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        //足の着地判定用
        if (SetOnTriggerFoot)
        {
            GetOnTriggerFoot = SetOnTriggerFoot;
            SetOnTriggerFoot = false;
        }
        else GetOnTriggerFoot = false;

        //左足の着地判定用
        if (SetOnTriggerFootL)
        {
            GetOnTriggerFootL = SetOnTriggerFootL;
            SetOnTriggerFootL = false;
        }
        else GetOnTriggerFootL = false;

        //右足の着地判定用
        if (SetOnTriggerFootR)
        {
            GetOnTriggerFootR = SetOnTriggerFootR;
            SetOnTriggerFootR = false;
        }
        else GetOnTriggerFootR = false;

        //ジャンプした時の判定用
        if (SetOnTriggerJump)
        {
            GetOnTriggerJump = SetOnTriggerJump;
            SetOnTriggerJump = false;
        }
        else GetOnTriggerJump = false;


        //デバッグ用
        if (Input.GetKeyDown(KeyCode.Space)) GetOnTriggerFoot = true;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) GetOnTriggerFootL = true;
        if (Input.GetKeyDown(KeyCode.RightArrow)) GetOnTriggerFootR = true;
        if (Input.GetKeyDown(KeyCode.UpArrow)) GetOnTriggerJump = true;
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
