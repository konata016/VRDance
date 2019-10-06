using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Status status;
    public float damagePoint;
    public int comboCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //計算
        if (Music.IsPlaying && Music.IsJustChangedBar())
        {
            damagePoint = (plAttackControl2.plAct.attackStep * comboCount) + status.STR;
            status.HP += plAttackControl2.plAct.healing + status.INT;
        }

        //コンボ計算
        if (Music.IsPlaying && Music.IsJustChangedBeat())
        {
            if(HitPos.rankJudge== HitPos.RANK.Excellent|| HitPos.rankJudge == HitPos.RANK.Good)
            {
                comboCount++;
            }
            else
            {
                comboCount = 0;
            }
        }

        //デバッグ用
        Debug();
    }

    void Debug()
    {
        DebugPanel.text1 = "HP      ：" + status.HP;
        DebugPanel.text2 = "攻撃力  ：" + damagePoint;
        DebugPanel.text3 = "コンボ数：" + comboCount;
        DebugPanel.text4 = "判定    ：" + HitPos.rankJudge;
    }
}
