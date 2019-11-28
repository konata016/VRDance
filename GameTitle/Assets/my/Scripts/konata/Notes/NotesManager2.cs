using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesManager2 : MonoBehaviour
{
    public enum RANK { Bad, Good, Excellent }           //ランクのenum
    public static RANK rank { get; set; }               //ランクを他のスクリプトに渡すよう

    public GameObject notesObj;                         //ノーツオブジェクト
    public float longNotesSiz = 1.5f;                   //1小節ごとに発生するノーツの長さの初期値
    public float speed = 30;                            //ノーツの速度
    public float angle = 45;

    public GameObject butObj, goodObj, excellentObj;    //評価を出すためのオブジェクト
    Vector3 butPos, gootPos, excellentPos;              //評価のポジションだけを取得するときに使う

    List<GameObject> notesRightList = new List<GameObject>(); //右のノーツを管理するよう
    List<GameObject> notesLeftList = new List<GameObject>();//左のノーツを管理するよう

    int stepDataCount;      //リストのカウント
    float fixTime;          //音に合うタイミングにする用

    bool onStartBgm;

    // Start is called before the first frame update
    void Start()
    {
        //評価の基準値を代入する
        butPos = butObj.transform.rotation.eulerAngles;
        gootPos = goodObj.transform.rotation.eulerAngles;
        excellentPos = excellentObj.transform.rotation.eulerAngles;

        //生成のタイミングをずらす
        fixTime = angle / speed;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnNotes();
        NotesMove();
        RankJudge();
        OverNotes();

        //Debug.Log(StepData.GetSoundPlayTime);
    }

    //ノーツ生成処理
    void SpawnNotes()
    {
        //テキストに登録されている時間が来たらノーツを生成する
        if (StepData.GetSoundPlayTime >= StepData.GetStepData[stepDataCount].musicScore - fixTime)
        {
            float a = StepData.GetStepData[stepDataCount].musicScore - fixTime;
            //Debug.Log("Sound" + StepData.GetSoundPlayTime + "テキスト" + StepData.GetStepData[stepDataCount].musicScore + "リスト" + stepDataCount);

            //ステップの指示がされている場合ノーツを生成
            if (StepData.GetStepData[stepDataCount].plStep != StepData.PL_STEP_TIMING.Nothing)
            {
                notesRightList.Add(Spawn(angle));
                notesLeftList.Add(Spawn(-angle));
            }

            //再生時間よりテキストデータの時間が遅い場合リストのカウントを進める
            stepDataCount++;
        }


        GameObject Spawn(float roll)
        {
            //Y軸上にroll分だけ回転させる
            Quaternion rot = Quaternion.AngleAxis(roll, Vector3.up);
            //自信の回転取得
            Quaternion q = transform.rotation;

            //初期設定の回転と自信の回転を掛け合わせたら、うまいこと合成できたぜ！
            GameObject obj = Instantiate(notesObj, transform.position, q * rot) as GameObject;

            //自信の子オブジェクトとして登録する
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
            //動きの計算
            float move = direction * speed * Time.deltaTime;
            //Y軸を基準としてmove分だけ回転する
            Quaternion rot = Quaternion.AngleAxis(move, Vector3.up);
            //生成したオブジェクトの回転データ
            Quaternion q = obj.transform.rotation;

            //掛け合わせることによって、生成オブジェクト独自の動きと
            //親オブジェクトの回転を合成することができる
            return q * rot;
        }
    }

    //ノーツがオーバーしていればノーツを消す
    void OverNotes()
    {
        if (Over(notesLeftList))
        {
            DestroyNotes(notesRightList);
            DestroyNotes(notesLeftList);
        }

        //ノーツがオーバーしているかどうかの判定
        bool Over(List<GameObject> objList)
        {
            //リストがないときには実行しないようにする
            if (objList.Count != 0)
            {
                //生成したオブジェクトのローカル回転データ格納
                Vector3 roll = objList[0].transform.localRotation.eulerAngles;

                //回転したときに360以上になると0になってしまうため消す範囲を制限する必要があった
                if (roll.y < 10 && roll.y > 0) return true;
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
            //生成したオブジェクトのローカル回転データ格納
            Vector3 roll = notesLeftList[0].transform.localRotation.eulerAngles;

            //トリガーがオンになったら実行
            if (OnTrigger())
            {
                //ランクの判定
                if (roll.y > butPos.y && roll.y <= gootPos.y)
                {
                    Debug.Log("Bad!!");
                    rank = RANK.Bad;
                }
                else if (roll.y > gootPos.y && roll.y <= excellentPos.y)
                {
                    Debug.Log("Good!!");
                    rank = RANK.Good;
                }
                else if (roll.y > excellentPos.y || roll.y <= 2)
                {
                    Debug.Log("Excellent!!");
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

    //ノーツを消す処理
    void DestroyNotes(List<GameObject> objList)
    {
        //GameObject obj = objList[0];
        Destroy(objList[0]);
        objList.RemoveAt(0);
    }

}