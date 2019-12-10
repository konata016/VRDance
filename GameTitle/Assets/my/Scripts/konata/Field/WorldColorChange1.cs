using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //マテリアルを入れ替える
    void ResetAlpha()
    {
        color = GetComponent<Renderer>().material.color;
        color.a = alpha;
        GetComponent<Renderer>().material.color = color;

        //判定が終わったら判定を行っているスクリプトに判定が終わったことを伝える
        NotesManager2.rank = NotesManager2.RANK.Wait;
    }

    //トリガーの処理をここに入れる
    bool OnTrigger()
    {
        if (StepDetermination.isGroundTouch_L == StepDetermination.ISGROUNDTOUCH.Landing ||
            StepDetermination.isGroundTouch_R == StepDetermination.ISGROUNDTOUCH.Landing ||
           OnDebugKey() || PauseCheck.GetOnStep)
        {
            return true;
        }
        else return false;

        //ボタンの処理
        bool OnDebugKey()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0) ||
                Input.GetKeyDown(KeyCode.Alpha1) ||
                Input.GetKeyDown(KeyCode.Alpha2) ||
                Input.GetKeyDown(KeyCode.Alpha3) ||
                Input.GetKeyDown(KeyCode.Alpha4) ||
                Input.GetKeyDown(KeyCode.Alpha5) ||
                Input.GetKeyDown(KeyCode.Alpha6) ||
                Input.GetKeyDown(KeyCode.Alpha7))
            {
                return true;
            }
            else return false;
        }
    }
}
