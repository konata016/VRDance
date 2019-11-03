using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlActionControl2 : MonoBehaviour
{
    public Status status;
    public List<PauseCheck.PAUSE_ACTION> melodyList;

    float damage;
    int comboCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Music.IsPlaying && Music.IsJustChangedBar())
        {
            float tmpDamage = 0;
            Combo();
            for (int i = 0; i < melodyList.Count; i++)
            {
                //ダメージ計算
                switch (melodyList[i])
                {
                    case PauseCheck.PAUSE_ACTION.Side:
                        tmpDamage += 2 + comboCount;
                        break;
                    case PauseCheck.PAUSE_ACTION.Vertical:
                        tmpDamage += 2 + comboCount;
                        break;
                    case PauseCheck.PAUSE_ACTION.Cross:
                        tmpDamage *= 2 + comboCount;
                        break;
                    case PauseCheck.PAUSE_ACTION.Miss:
                        break;
                    default: break;
                }
            }
            damage = tmpDamage + status.STR;

            //攻撃力によって繰り出される技の処理を書くこと
            //技のバリエーションを管理するclassを作った方が良い？


            melodyList.Clear();
        }

        //入力されたデータを格納する
        if (Music.IsPlaying && Music.IsJustChangedBeat())
        {
            melodyList.Add(PauseCheck.actionPause);
        }
    }

    //コンボの判定
    void Combo()
    {
        if (NotesManager.rank == NotesManager.RANK.Excellent ||
                NotesManager.rank == NotesManager.RANK.Good)
        {
            comboCount++;
        }
        else
        {
            if (NotesManager.rank != NotesManager.RANK.Bad) comboCount = 0;
        }
    }
}
