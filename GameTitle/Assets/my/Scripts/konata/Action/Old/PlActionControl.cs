using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//攻撃を発動させる処理
//ゲームが重いのであれば改善の余地あり

public class PlActionControl : MonoBehaviour
{
    //アクションの種類
    public enum ACTIONTYPE { Attack, Healing, Support, Through }

    //マネージャーを生成してスキルを出すためのオブジェクトを入れる
    public GameObject plAttacManagerObj;

    //足で取る判定、円の分割数
    public int footCircleCutNum = 8;

    //分割した場所のアクション内容を取得するクラス
    [System.Serializable]
    public class FootActionNum {
        public List<int> attackNum = new List<int>() { 3, 4, 5 };
        public List<int> healNum = new List<int>() { 0, 1, 7 };
        public List<int> supportNum = new List<int>() { 2, 6 };
        public List<int> throughNum = new List<int>() { 8 };
    }
    public FootActionNum footActionNum = new FootActionNum();

    //足の位置
    int footPosNum;

    //SE読み込みオブジェクト
    private GameObject seObj;

    //[System.Serializable]
    public struct PlayerAction      //スキルカウント用
    {
        public List<ACTIONTYPE> melodyList;
        public int attackStep;
        public int healingStep;
        public int supportStep;

        public void Refresh()
        {
            attackStep = 0;
            healingStep = 0;
            supportStep = 0;
        }
    }
    public PlayerAction plAct = new PlayerAction();

    //一時保存
    public static List<ACTIONTYPE> melodySaveList = new List<ACTIONTYPE>();

    static PlActionControl PlActionControl_ = new PlActionControl();

    void Awake()
    {
        PlActionControl_.footCircleCutNum = footCircleCutNum;
    }

    // Start is called before the first frame update
    void Start()
    {
        seObj = GameObject.Find("SE");

        plAct.melodyList = new List<ACTIONTYPE>();

        //リスト初期化
        for (int i = 0; i < 4; i++)
        {
            melodySaveList.Add(ACTIONTYPE.Through);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //入力
        footPosNum = FootPosNum();

        // 保存した番号に何のスキルが割り振られているか一小節ごとに確認する
        if (Music.IsPlaying && Music.IsJustChangedBar())
        {
            //初期化
            plAct.Refresh();

            //1小節の中のデータを分ける
            for (int i = 0; i < plAct.melodyList.Count; i++)
            {
                switch (plAct.melodyList[i])
                {
                    case ACTIONTYPE.Attack: plAct.attackStep++; break;      //攻撃
                    case ACTIONTYPE.Healing: plAct.healingStep++; break;    //回復
                    case ACTIONTYPE.Support: plAct.supportStep++; break;    //サポート
                    case ACTIONTYPE.Through: break;
                    default: break;
                }
            }
            //おかしくなったら変える
            PlActionControl_.plAct = plAct;

            //処理を行うプレハブを生成
            SpawnPrefab();

            //メロディーを一端保存する
            Save();

            //すべて消す
            plAct.melodyList.Clear();

        }

        //入力した番号を保存
        if (Music.IsPlaying && Music.IsJustChangedBeat())
        {
            //1小節分の入力データの保存
            plAct.melodyList.Add(GetActionType(footPosNum));
            //初期化
            footPosNum = footActionNum.throughNum[0];

            //キーボード入力（デバッグ用）
            //Debug.Log(HitPos.footPosNum);
        }

    }

    //処理を行うプレハブを生成
    void SpawnPrefab()
    {
        //攻撃
        if (plAct.attackStep != 0)
        {
            Instantiate(plAttacManagerObj, transform);
            PlAttackAction.rollSwordCount = plAct.attackStep;
        }
        //回復
        if (plAct.healingStep != 0)
        {
            Instantiate(plAttacManagerObj, transform);
            //PlAttackAction.rollSwordCount = plAct.healingStep;
        }
        //サポート
        if (plAct.supportStep != 0)
        {
            Instantiate(plAttacManagerObj, transform);
        }
    }

    //1時的に1小節の"攻撃"回復"サポート"のデータを保存
    void Save()
    {
        melodySaveList.Clear();
        if (plAct.melodyList.Count > 3)
        {
            melodySaveList = new List<ACTIONTYPE>(plAct.melodyList);
        }
    }

    //踏んだ位置の入力
    int FootPosNum()
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
        if (StepDetermination.isGroundTouch_L == StepDetermination.ISGROUNDTOUCH.Landing ||
            StepDetermination.isGroundTouch_R == StepDetermination.ISGROUNDTOUCH.Landing)
        {
            num = FootPosCenter.hitPosNum;
        }
        return num;
    }

    //入力された数値を"攻撃"回復"サポート"に分ける
    ACTIONTYPE GetActionType(int num)
    {
        ACTIONTYPE actionType = new ACTIONTYPE();

        if (footActionNum.attackNum.Contains(num)) actionType = ACTIONTYPE.Attack;
        else if (footActionNum.healNum.Contains(num)) actionType = ACTIONTYPE.Healing;
        else if (footActionNum.supportNum.Contains(num)) actionType = ACTIONTYPE.Support;
        else if (footActionNum.throughNum.Contains(num)) actionType = ACTIONTYPE.Through;

        return actionType;
    }

    //判定用の円の分割数
    public static int GetFootCircleCutNum { get { return PlActionControl_.footCircleCutNum; } }
    public static PlayerAction GetPlAct { get { return PlActionControl_.plAct; } }
}