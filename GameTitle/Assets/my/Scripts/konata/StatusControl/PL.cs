using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ダメージ&コンボ数の計算
/// </summary>
public class PL : MonoBehaviour
{
    public Status status;

    /// <summary>
    /// コンボ数を取得
    /// </summary>
    public static int GetComboCount { get; private set; }

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
        GetComboCount = 0;
        GetDamagePoint = 0;
        GetTotalDamage = 0;
        GetMaxComboCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (OnTrigger())
        {
            switch (NotesManager2.rank)
            {
                case NotesManager2.RANK.Bad: GetComboCount = 0; break;
                case NotesManager2.RANK.Good: GetComboCount++; break;
                case NotesManager2.RANK.Excellent: GetComboCount++; break;
                default: break;
            }

            //敵に与えるダメージ計算
            GetDamagePoint = ((int)NotesManager2.rank + (int)status.STR) * (GetComboCount / 5);
            GetTotalDamage += GetDamagePoint;

            //最大コンボ数更新
            if (GetMaxComboCount < GetComboCount)
            {
                GetMaxComboCount = GetComboCount;
            }
        }
    }

    bool OnTrigger()
    {
        return TriggerManager.GetOnTriggerFoot;
    }
}
