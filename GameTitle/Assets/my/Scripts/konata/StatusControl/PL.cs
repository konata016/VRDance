using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ダメージ&コンボ数の計算
/// </summary>
public class PL : MonoBehaviour
{
    public Status status;

    int comboCount;

    /// <summary>
    /// 敵に与えるダメージを取得
    /// </summary>
    public static int GetDamagePoint { get; private set; }

    /// <summary>
    /// 敵に与えたトータルダメージ数を取得
    /// </summary>
    public static int GetTotalDamage { get; private set; }

    /// <summary>
    /// 最大コンボ数を取得
    /// </summary>
    public static int GetMaxComboCount { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OnTrigger())
        {
            switch (NotesManager2.rank)
            {
                case NotesManager2.RANK.Bad: comboCount = 0; break;
                case NotesManager2.RANK.Good: comboCount++; break;
                case NotesManager2.RANK.Excellent: comboCount++; break;
                default: break;
            }

            //敵に与えるダメージ計算
            GetDamagePoint = ((int)NotesManager2.rank + (int)status.STR) * (comboCount / 5);
            GetTotalDamage += GetDamagePoint;

            //最大コンボ数更新
            if (GetMaxComboCount < comboCount)
            {
                GetMaxComboCount = comboCount;
            }
        }
    }

    bool OnTrigger()
    {
        return TriggerManager.GetOnTriggerFoot;
    }
}
