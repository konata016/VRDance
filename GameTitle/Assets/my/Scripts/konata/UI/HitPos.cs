using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ノーツの処理と着地のタイミングから導き出されるランクの判定

public class HitPos : MonoBehaviour
{
    //判定を渡すよう
    public enum RANK { Bad, Good, Excellent,Through }

    float notesLeftPos;
    float notesRightPos;

    GameObject obj;
    GameObject obj1;
    private GameObject seObj;//SE読み込みオブジェクト

    public static int footPosNum { get; set; }
    public static RANK rankJudge { get; set; }

    KeyCode NotesKeyName;


    // Start is called before the first frame update
    void Start()
    {
        seObj = GameObject.Find("SE");

        NotesKeyName = KeyCode.Space;   //判定するボタン
        footPosNum = 0;                 //足の位置の初期向き

        rankJudge = RANK.Through;       //ランクの初期化
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
            //Debug.Log(footPosNum);
            //footPosNum = FootPosNum();

            //左のノーツの処理
            if (notesLeftPos >= -150f && notesLeftPos < 100f)
            {
                obj = BeatUi.notesLefts[0];

                //足が地面に接触したときまたはキーボードから入力されたときに処理する
                if (Input.GetKeyDown(NotesKeyName))
                {
                    
                }
                if (StepDetermination.isGroundTouch_R == StepDetermination.ISGROUNDTOUCH.Landing) NotesProcess();
                if (StepDetermination.isGroundTouch_L == StepDetermination.ISGROUNDTOUCH.Landing) NotesProcess();
            }


            //ノーツを消す
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

                if (Input.GetKeyDown(NotesKeyName))
                {
                }
                if (StepDetermination.isGroundTouch_R == StepDetermination.ISGROUNDTOUCH.Landing)
                {
                    //処理を返す(地面に接触したときにフラグを返す)
                    StepDetermination.isGroundTouch_R = StepDetermination.ISGROUNDTOUCH.EndProcess;
                    BeatUi.notesRights.RemoveAt(0);
                    Destroy(obj1);
                }
                else if (StepDetermination.isGroundTouch_L == StepDetermination.ISGROUNDTOUCH.Landing)
                {
                    //処理を返す(地面に接触したときにフラグを返す)
                    StepDetermination.isGroundTouch_L = StepDetermination.ISGROUNDTOUCH.EndProcess;
                    BeatUi.notesRights.RemoveAt(0);
                    Destroy(obj1);
                }
                //BeatUi.notesRights.RemoveAt(0);
                //Destroy(obj1);
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

                    //スルーした時のランク
                    rankJudge = RANK.Through;
                }
            }
        }
    }


    void NotesProcess()
    {
        //タイミングのランクを登録する
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

        MainGame_SE mainGame_SE = seObj.GetComponent<MainGame_SE>();
        mainGame_SE.StepSound();//足音

        BeatUi.notesLefts.RemoveAt(0);
        Destroy(obj);
    }

    //デバッグ用ボタン判定
    int FootPosNumDebug()
    {
        int num = footPosNum;

        //キーボード入力
        if (Input.GetKeyDown(KeyCode.Alpha0)) num = 0;
        if (Input.GetKeyDown(KeyCode.Alpha1)) num = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) num = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) num = 3;
        if (Input.GetKeyDown(KeyCode.Alpha4)) num = 4;
        if (Input.GetKeyDown(KeyCode.Alpha5)) num = 5;
        if (Input.GetKeyDown(KeyCode.Alpha6)) num = 6;
        if (Input.GetKeyDown(KeyCode.Alpha7)) num = 7;

        //足の入力
        if (StepDetermination.isGroundTouch_R == StepDetermination.ISGROUNDTOUCH.Landing) num = FootPosCenter.hitPosNum;
        if (StepDetermination.isGroundTouch_L == StepDetermination.ISGROUNDTOUCH.Landing) num = FootPosCenter.hitPosNum;

        return num;
    }

    //足の角度判定用
    //int FootPosNum()
    //{
    //    int num = footPosNum;

    //    //踏んだ瞬間を取得
    //    num = FootPosCenter.hitPosNum;

    //    return num;
    //}
}
