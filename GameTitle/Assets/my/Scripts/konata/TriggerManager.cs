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
        SetOnTriggerFoot = false;
        SetOnTriggerFootL = false;
        SetOnTriggerFootR = false;
        SetOnTriggerJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        //処理の一番最後に初期化
        SetOnTriggerFoot = false;
        SetOnTriggerFootL = false;
        SetOnTriggerFootR = false;
        SetOnTriggerJump = false;

        //キーボード入力用
        SetOnTriggerFoot = OnDebugTriggerFoot();
        SetOnTriggerFootL = OnDebugTriggerFootL();
        SetOnTriggerFootR = OnDebugTriggerFootR();
        SetOnTriggerJump = OnDebugTriggerJump();

        //足の判定用
        bool OnDebugTriggerFoot() { return Input.GetKeyDown(KeyCode.Space); }
        //左足の判定用
        bool OnDebugTriggerFootL() { return Input.GetKeyDown(KeyCode.LeftArrow); }
        //右足の判定用
        bool OnDebugTriggerFootR() { return Input.GetKeyDown(KeyCode.RightArrow); }
        //両足着地判定用
        bool OnDebugTriggerJump() { return Input.GetKeyDown(KeyCode.DownArrow); }
    }

    //Updateの処置が終わったら処理が始まる
    void LateUpdate()
    {
        ////処理の一番最後に初期化
        //SetOnTriggerFoot = false;
        //SetOnTriggerFootL = false;
        //SetOnTriggerFootR = false;
        //SetOnTriggerJump = false;
    }

    /// <summary>
    /// どりらか片方の足が地面についたときの判定取得
    /// </summary>
    public static bool GetOnTriggerFoot { get { return SetOnTriggerFoot; } }

    /// <summary>
    /// 左足が地面についたときの判定取得
    /// </summary>
    public static bool GetOnTriggerFootL { get { return SetOnTriggerFootL; } }

    /// <summary>
    /// 右足が地面についたときの判定取得
    /// </summary>
    public static bool GetOnTriggerFootR { get { return SetOnTriggerFootR; } }

    /// <summary>
    /// ジャンプして着地したときの判定取得
    /// </summary>
    public static bool GetOnTriggerJump { get { return SetOnTriggerJump; } }
}
