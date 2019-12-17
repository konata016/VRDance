using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlActionControl2 : MonoBehaviour
{
    public Status status;
    public GameObject instantAttackManager;
    public List<PauseCheck.PAUSE_ACTION> melodyList = new List<PauseCheck.PAUSE_ACTION>();

    static float damage;
    int comboCount;

    // Start is called before the first frame update
    void Start()
    {
        damage = 0;
        for (int i = 0; i < 8; i++) melodyList.Add(PauseCheck.PAUSE_ACTION.Through);
    }

    // Update is called once per frame
    void Update()
    {
        if (Music.IsPlaying && Music.IsJustChangedBar())
        {
            float tmpDamage = 0;
            int throughCount = 0;
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
                    case PauseCheck.PAUSE_ACTION.Through:
                        throughCount++;
                        break;
                    default: break;
                }
            }
            damage = tmpDamage + status.STR;

            //攻撃用のマネージャーを生成
            if (throughCount != melodyList.Count)
            {
                GameObject obj = Instantiate(instantAttackManager, transform);
                Destroy(obj, 5);
            }


            melodyList.Clear();
        }

        //入力されたデータを格納する
        if (Music.IsPlaying && Music.IsJustChangedBeat())
        {
            melodyList.Add(PauseCheck.actionPause);
            PauseCheck.actionPause = PauseCheck.PAUSE_ACTION.Through;
        }
    }

    //コンボの判定
    void Combo()
    {
        if (NotesManager.GetRank == NotesManager.RANK.Excellent ||
                NotesManager.GetRank == NotesManager.RANK.Good)
        {
            comboCount++;
        }
        else
        {
            if (NotesManager.GetRank != NotesManager.RANK.Bad) comboCount = 0;
        }
    }

    public static float GetDamage { get{ return damage; } }
}
