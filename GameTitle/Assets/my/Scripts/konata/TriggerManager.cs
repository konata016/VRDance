using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public static bool SetOnTriggerFoot { private get; set; }

    // Start is called before the first frame update
    void Start()
    {
        SetOnTriggerFoot = false;
    }

    // Update is called once per frame
    void Update()
    {

        SetOnTriggerFoot = OnDebugTriggerFoot();

        //足の判定用
        bool OnDebugTriggerFoot()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }

    //Updateの処置が終わったら処理が始まる
    void LateUpdate()
    {
        SetOnTriggerFoot = false;
    }

    public static bool GetOnTriggerFoot { get { return SetOnTriggerFoot; } }
}
