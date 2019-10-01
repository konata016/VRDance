using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPos : MonoBehaviour
{
    //判定を渡すよう
    public enum RANK { Bad, Good, Excellent }

    float notesLeftPos;
    float notesRightPos;

    GameObject obj;
    GameObject obj1;

    public static int footPosNum { get; set; }
    public static RANK rankJudge { get; set; }

    KeyCode NotesKeyName;


    // Start is called before the first frame update
    void Start()
    {
        //ボタン
        NotesKeyName = KeyCode.Space;
        footPosNum = 0;

        rankJudge = RANK.Bad;
    }

    // Update is called once per frame
    void Update()
    {

        if (BeatUi.isNotesPopUp)
        {
            //listObj = BeatUi.notesLefts;
            notesLeftPos = BeatUi.notesLefts[0].GetComponent<RectTransform>().localPosition.x;
            notesRightPos = BeatUi.notesRights[0].GetComponent<RectTransform>().localPosition.x;

            //ボタン判定
            footPosNum = FootPosNumDebug();

            //左のノーツの処理
            if (notesLeftPos >= -150f && notesLeftPos < 100f)
            {
                obj = BeatUi.notesLefts[0];

                if (Input.GetKeyDown(NotesKeyName) || Input.anyKeyDown)
                {
                    if (notesLeftPos <= 50f && notesLeftPos >= -30f)
                    {
                        Debug.Log("Excellent!!");
                        rankJudge = RANK.Excellent;

                    }
                    if (notesLeftPos < -30f && notesLeftPos >= -60f)
                    {
                        Debug.Log("Good!!");
                        rankJudge = RANK.Good;
                    }
                    if (notesLeftPos < -60f && notesLeftPos >= -150f)
                    {
                        Debug.Log("Bad!!");
                        rankJudge = RANK.Bad;
                    }

                    BeatUi.notesLefts.RemoveAt(0);
                    Destroy(obj);

                }
            }


            //消す
            if (notesLeftPos > 2f)
            {
                obj.GetComponent<Image>().color = Color.clear;
            }
            if (notesLeftPos > 100f)
            {
                BeatUi.notesLefts.RemoveAt(0);
                Destroy(obj);
            }


            //右のノーツの処理
            if (notesRightPos <= 150f && notesRightPos > -100f)
            {
                obj1 = BeatUi.notesRights[0];

                if (Input.GetKeyDown(NotesKeyName) || Input.anyKeyDown)
                {
                    BeatUi.notesRights.RemoveAt(0);
                    Destroy(obj1);
                }
            }
            if (!Input.GetKeyDown(NotesKeyName) || !Input.anyKeyDown)
            {
                if (notesRightPos < -2f)
                {
                    obj1.GetComponent<Image>().color = Color.clear;
                }
                if (notesRightPos < -100f)
                {
                    BeatUi.notesRights.RemoveAt(0);
                    Destroy(obj1);
                }
            }
        }
    }


    //デバッグ用ボタン判定
    int FootPosNumDebug()
    {
        int Num = footPosNum;
        if (Input.GetKeyDown(KeyCode.Alpha0)) Num = 0;
        if (Input.GetKeyDown(KeyCode.Alpha1)) Num = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) Num = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) Num = 3;
        if (Input.GetKeyDown(KeyCode.Alpha4)) Num = 4;
        if (Input.GetKeyDown(KeyCode.Alpha5)) Num = 5;
        if (Input.GetKeyDown(KeyCode.Alpha6)) Num = 6;
        if (Input.GetKeyDown(KeyCode.Alpha7)) Num = 7;        
        return Num;
    }

    //足の角度判定用
    int FootPosNum()
    {
        int num = footPosNum;

        if (""=="")
        {
            num = FootPosCenter.hitPosNum;
        }

        return num;
    }
}
