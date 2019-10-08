using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//攻撃を発動させる処理
//ゲームが重いのであれば改善の余地あり

public class PlActionControl : MonoBehaviour
{
    //マネージャーを生成してスキルを出すためのオブジェクトを入れる
    public GameObject plAttacManagerObj;

    //[System.Serializable]
    public struct PlayerAction      //スキルカウント用
    {
        public List<int> melodyList;
        public int attackStep;
        public int healing;
        public int supportStep;

        public void Refresh()
        {
            attackStep = 0;
            healing = 0;
            supportStep = 0;
        }
    }
    public static PlayerAction plAct = new PlayerAction();

    //デバッグ用リスト
    public static List<int> melodySaveList = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        plAct.melodyList = new List<int>();
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
                    //攻撃
                    case 3:
                    case 4:
                    case 5:
                        plAct.attackStep++;
                        break;

                    //ヒール
                    case 0:
                    case 1:
                    case 7:
                        plAct.healing++;
                        break;

                    //サポート
                    case 2:
                    case 6:
                        plAct.supportStep++;
                        break;

                    default: break;
                }
            }

            //攻撃
            if (plAct.attackStep > 0)
            {
                Instantiate(plAttacManagerObj, transform);
                PlAttackAction.rollSwordCount = plAct.attackStep;
            }

            //メロディーを一端保存する
            Save();

            //すべて消す
            plAct.melodyList.Clear();

        }

        //入力した番号を保存
        if (Music.IsPlaying && Music.IsJustChangedBeat())
        {
            plAct.melodyList.Add(HitPos.footPosNum);

            //キーボード入力（デバッグ用）
            //Debug.Log(HitPos.footPosNum);
            HitPos.footPosNum = 8;
        }
    }

    void Save()
    {
        melodySaveList = new List<int>(plAct.melodyList);
    }
}