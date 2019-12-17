using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plAttackControl2 : MonoBehaviour
{
    //マネージャーを生成してスキルを出すためのオブジェクトを入れる
    public GameObject plAttacManagerObj;

    //[System.Serializable]
    public struct PlayerAction      //スキルカウント用
    {
        public List<int> melodyList;
        public int attackStep;
        public int defenseStep;
        public int supportStep;

        public void Refresh()
        {
            attackStep = 0;
            defenseStep = 0;
            supportStep = 0;
        }
    }
    public PlayerAction plAct = new PlayerAction();

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

                    //防御
                    case 0:
                    case 1:
                    case 7:
                        plAct.defenseStep++;
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
            //すべて消す
            plAct.melodyList.Clear();

        }

        //入力した番号を保存
        if (Music.IsPlaying && Music.IsJustChangedBeat())
        {
            plAct.melodyList.Add(HitPos.footPosNum);

            //キーボード入力（デバッグ用）
            Debug.Log(HitPos.footPosNum);
            HitPos.footPosNum = 8;
        }
    }
}
