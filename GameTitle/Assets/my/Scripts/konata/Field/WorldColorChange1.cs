using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのステップの評価に対して世界の色を変える
/// </summary>
public class WorldColorChange1 : MonoBehaviour
{
    public float speed = 5;
    public Material badMaterial;
    public Material goodMaterial;
    public Material excellentMaterial;

    float alpha;
    Color color;

    // Start is called before the first frame update
    void Start()
    {
        //アルファ値の初期化用
        alpha = excellentMaterial.color.a;

        //はじめは透明にする
        color = GetComponent<Renderer>().material.color;
        color.a = 0;
        GetComponent<Renderer>().material.color = color;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (OnTrigger())
        {
            //評価によってワールド色を変える
            switch (NotesManager2.rank)
            {
                case NotesManager2.RANK.Bad:
                    GetComponent<Renderer>().material = badMaterial;
                    ResetAlpha();
                    break;

                case NotesManager2.RANK.Good:
                    GetComponent<Renderer>().material = goodMaterial;
                    ResetAlpha();
                    break;

                case NotesManager2.RANK.Excellent:
                    GetComponent<Renderer>().material = excellentMaterial;
                    ResetAlpha();
                    break;

                case NotesManager2.RANK.Miss:
                    NotesManager2.rank = NotesManager2.RANK.Wait;
                    break;

                default: break;
            }
        }

        //アルファ値を下げる
        if (0 <= GetComponent<Renderer>().material.color.a)
        {
            color.a -= speed * Time.deltaTime;
            GetComponent<Renderer>().material.color = color;
        }
    }

    private void LateUpdate()
    {
        if (OnTrigger())
        {
            //判定が終わったら判定を行っているスクリプトに判定が終わったことを伝える
            NotesManager2.rank = NotesManager2.RANK.Wait;
        }
    }

    //マテリアルを入れ替える
    void ResetAlpha()
    {
        color = GetComponent<Renderer>().material.color;
        color.a = alpha;
        GetComponent<Renderer>().material.color = color;
    }

    //トリガーの処理をここに入れる
    bool OnTrigger()
    {
        return TriggerManager.GetOnTriggerFoot;
    }
}
