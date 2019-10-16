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
        public List<int> throughNUm = new List<int>() { 8 };
    }
    public FootActionNum footActionNum = new FootActionNum();

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
    public static PlayerAction plAct = new PlayerAction();

    //一時保存
    public static List<ACTIONTYPE> melodySaveList = new List<ACTIONTYPE>();

    static PlActionControl PlActionControl_ = new PlActionControl();

    // Start is called before the first frame update
    void Start()
    {
        seObj = GameObject.Find("SE");

        plAct.melodyList = new List<ACTIONTYPE>();

        for (int i = 0; i < 4; i++)
        {
            melodySaveList.Add(ACTIONTYPE.Through);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 保存した番号に何のスキルが割り振られているか一小節ごとに確認する
        if (Music.IsPlaying && Music.IsJustChangedBar())
        {
            //初期化
            plAct.Refresh();

            for (int i = 0; i < plAct.melodyList.Count; i++)
            {
                switch (plAct.melodyList[i])
                {
                    case ACTIONTYPE.Attack: plAct.attackStep++; break;      //攻撃
                    case ACTIONTYPE.Healing: plAct.healingStep++; break;    //回復
                    case ACTIONTYPE.Support: plAct.supportStep++; break;    //サポート
                    default: break;
                }
            }

            //処理を行うプレハブを生成//
                //攻撃
            if (plAct.attackStep > 0)
            {
                Instantiate(plAttacManagerObj, transform);
                PlAttackAction.rollSwordCount = plAct.attackStep;
            }
                //回復
            if (plAct.healingStep > 0)
            {
                Instantiate(plAttacManagerObj, transform);
                //PlAttackAction.rollSwordCount = plAct.healingStep;
            }

            //メロディーを一端保存する
            Save();

            //すべて消す
            plAct.melodyList.Clear();

        }

        //入力した番号を保存
        if (Music.IsPlaying && Music.IsJustChangedBeat())
        {
            plAct.melodyList.Add(GetActionType(HitPos.footPosNum));


            HitPos.footPosNum = 8;
            //キーボード入力（デバッグ用）
            //Debug.Log(HitPos.footPosNum);
        }

    }

    ACTIONTYPE GetActionType(int num)
    {
        ACTIONTYPE actionType = new ACTIONTYPE();

        if (footActionNum.attackNum.Contains(num)) actionType = ACTIONTYPE.Attack;
        else if (footActionNum.healNum.Contains(num)) actionType = ACTIONTYPE.Healing;
        else if(footActionNum.supportNum.Contains(num)) actionType = ACTIONTYPE.Support;
        else if(footActionNum.throughNUm.Contains(num)) actionType = ACTIONTYPE.Through;

        return actionType;
    }

    void Save()
    {
        if (plAct.melodyList.Count > 3)
        {
            melodySaveList = new List<ACTIONTYPE>(plAct.melodyList);
        }
        else
        {
            //melodySaveList
        }

    }

    //判定用の円の分割数
    public static int FootCircleCutNum { get { return PlActionControl_.footCircleCutNum; } }
}