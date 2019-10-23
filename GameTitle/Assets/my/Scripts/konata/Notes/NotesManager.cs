using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//両サイド作ってから判定の処理を書くこと

public class NotesManager : MonoBehaviour
{
    public enum RANK { Bad, Good, Excellent }           //ランクのenum
    public static RANK rank { get; set; }               //ランクを他のスクリプトに渡すよう

    public GameObject headDisplay;
    public GameObject notesObj;                         //ノーツオブジェクト
    public float longNotesSiz = 1.5f;                   //1小節ごとに発生するノーツの長さの初期値
    public float speed = 30;                            //ノーツの速度

    public GameObject butObj, goodObj, excellentObj;    //評価を出すためのオブジェクト
    Vector3 butPos, gootPos, excellentPos;              //評価のポジションだけを取得するときに使う

    List<GameObject> notesRightList=new List<GameObject>(); //右のノーツを管理するよう
    List<GameObject> notesLeftList = new List<GameObject>();//左のノーツを管理するよう

    // Start is called before the first frame update
    void Start()
    {
        //評価の基準値を代入する
        butPos = butObj.transform.rotation.eulerAngles;
        gootPos = goodObj.transform.rotation.eulerAngles;
        excellentPos = excellentObj.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnNotes();
        NotesMove();
        RankJudge();
        overNotes();
    }

    //ノーツ生成処理
    void SpawnNotes()
    {
        if (Music.IsPlaying)
        {
            if (Music.IsJustChangedBar())
            {
                //左右のノーツ生成
                notesRightList.Add(Spawn(new Vector3(0, 90, 0)));
                notesLeftList.Add(Spawn(new Vector3(0, -90, 0)));

                int maxRight = notesRightList.Count - 1;
                int maxLeft = notesLeftList.Count - 1;

                //左右のノーツを大きくする
                notesRightList[maxRight] = SpawnLongNotes(notesRightList[maxRight]);
                notesLeftList[maxLeft] = SpawnLongNotes(notesLeftList[maxLeft]);
            }
            else if (Music.IsJustChangedBeat())
            {
                //左右のノーツ生成
                notesRightList.Add(Spawn(new Vector3(0, 90, 0)));
                notesLeftList.Add(Spawn(new Vector3(0, -90, 0)));
            }
        }

        //ノーツの生成
        GameObject Spawn(Vector3 roll)
        {
            GameObject obj = Instantiate(notesObj, transform.position, Quaternion.Euler(roll)) as GameObject;
            obj.transform.transform.parent = transform;
            return obj;
        }

        //1小節ごとに生成されるオブジェクトのサイズを変えるやつ
        GameObject SpawnLongNotes(GameObject obj)
        {
            Vector3 v3 = obj.transform.localScale;
            v3.y += longNotesSiz;
            obj.transform.localScale = v3;

            return obj;
        }
    }

    //ノーツの移動処理
    void NotesMove()
    {
        //左右の移動式
        for (int i = 0; i < notesRightList.Count; i++)
        {
            notesRightList[i].transform.rotation = Move(notesRightList[i], -1);
            notesLeftList[i].transform.rotation = Move(notesLeftList[i], 1);
        }

        //動かす式
        Quaternion Move(GameObject obj, int direction)
        {
            Vector3 v3 = obj.transform.rotation.eulerAngles;

            v3.y += direction * speed * Time.deltaTime;
            return Quaternion.Euler(v3);
        }
    }

    //ノーツがオーバーしていればノーツを消す
    void overNotes()
    {
        if (over(notesLeftList))
        {
            DestroyNotes(notesRightList);
            DestroyNotes(notesLeftList);
        }

        //ノーツがオーバーしているかどうかの判定
        bool over(List<GameObject> objList)
        {
            //リストがないときには実行しないようにする
            if (objList.Count != 0)
            {
                Vector3 roll = objList[0].transform.rotation.eulerAngles;

                //回転したときに360以上になると0になってしまうため消す範囲を制限する必要があった
                if (roll.y < 10 && roll.y > 3) return true;
                else return false;
            }
            else return false;
        }
    }

    //タイミングの評価の処理
    void RankJudge()
    {
        //リストがないときには実行しないようにする
        if (notesLeftList.Count != 0)
        {
            Vector3 roll = notesLeftList[0].transform.rotation.eulerAngles;

            //トリガーがオンになったら実行
            if (onTrigger())
            {
                //ランクの判定
                if (roll.y > butPos.y && roll.y <= gootPos.y)
                {
                    //Debug.Log("Bad!!");
                    rank = RANK.Bad;
                }
                else if (roll.y > gootPos.y && roll.y <= excellentPos.y)
                {
                    //Debug.Log("Good!!");
                    rank = RANK.Good;
                }
                else if (roll.y > excellentPos.y || roll.y <= 2)
                {
                    //Debug.Log("Excellent!!");
                    rank = RANK.Excellent;
                }

                //バッドからエクセレント内にある場合ノーツを消去する
                if (roll.y > butPos.y || roll.y <= 2)
                {
                    DestroyNotes(notesRightList);
                    DestroyNotes(notesLeftList);
                }
            }
        }
    }

    //トリガーの処理をここに入れる
    bool onTrigger()
    {
        if (StepDetermination.isGroundTouch_L == StepDetermination.ISGROUNDTOUCH.Landing ||
            StepDetermination.isGroundTouch_R == StepDetermination.ISGROUNDTOUCH.Landing ||
           OnDebugKey())
        {
            return true;
        }
        else return false;     
    }

    //ノーツを消す処理
    void DestroyNotes(List<GameObject> objList)
    {
        GameObject obj = objList[0];
        objList.RemoveAt(0);
        Destroy(obj);
    }

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
