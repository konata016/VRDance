using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///　足の判定
/// </summary>
public class GroundManager : MonoBehaviour
{
    /// <summary>
    /// 処理が終わったことを別のスクリプトで宣言してあげること
    /// </summary>
    public enum EVENT { End, Vs, Up, Down }


    public float fixUp;                         //地面の高さ調整用
    public float groundPosCheckStartTime = 3;   //地面の位置を変更する処理が始める時間
    public float groundPosCheckUpdateTime = 5;  //〇秒ごとに地面の位置を調整

    public GameObject footL;
    public GameObject footR;

    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        //〇秒後に呼び出され、〇秒ごとに呼び出される
        InvokeRepeating("GroundPosUpdate", groundPosCheckStartTime, groundPosCheckUpdateTime);
    }

    // Update is called once per frame
    void Update()
    {
        //省略用
        pos = transform.position;

        //イベント処理
        EventProcessing(footL);
        EventProcessing(footR);
    }

    //イベント処理
    void EventProcessing(GameObject obj)
    {
        if (obj.transform.position.y > pos.y)
        {
            //地面より足が高い場合Upのイベント発生
            if (obj.GetComponent<Foot>().Event == EVENT.End)
            {
                obj.GetComponent<Foot>().Event = EVENT.Up;
            }
        }
        else
        {
            //足同士の位置が大体同じ名場合Downイベント発生
            if (obj.GetComponent<Foot>().Event == EVENT.Vs)
            {
                obj.GetComponent<Foot>().Event = EVENT.Down;
            }
        }
    }

    //地面の位置の更新
    void GroundPosUpdate()
    {
        //省略用
        float posL = footL.transform.position.y;
        float posR = footR.transform.position.y;

        //足が上がっていない場合,高い方に合わせる
        if (footL.GetComponent<Foot>().Event == EVENT.End &&
            footR.GetComponent<Foot>().Event == EVENT.End)
        {
            if (posL > posR)
            {
                pos.y = posL + fixUp;
            }
            else
            {
                pos.y = posR + fixUp;
            }
            transform.position = pos;
        }
    }
}
