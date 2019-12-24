using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

/// <summary>
/// テキストデータから敵の攻撃とプレイヤーのノーツ情報を返す
/// </summary>
public class StepData : MonoBehaviour
{
    public string scoreName = "aaa";
    public string fileName = "Assets/my/Scripts/konata/Notes/new/Score/";

    public AudioSource source;          //サウンド
    public enum INPUT_TEXT              //テキストデータの種類
    {
        MusicScore,

        EnemyAttackType,
        EnemyAttackLane0, EnemyAttackLane1, EnemyAttackLane2,
        EnemyAttackLane3, EnemyAttackLane4, EnemyAttackLane5,

        PlStep
    }

    public enum PL_STEP_TIMING      //プレイヤーのノーツの種類
    {
        Nothing,
        Step,
    }
    public enum ENEMY_ATTACK_TYPE   //敵の攻撃の種類
    {
        Nothing,
        WaveWide,
        Throw,
        WaveUnder
    }

    [System.Serializable]
    public class Data
    {
        public PL_STEP_TIMING plStep;               //プレイヤーノーツ

        public ENEMY_ATTACK_TYPE ememyAttackType;   //敵の攻撃タイプ
        public bool[] enemyAttackPos = new bool[6]; //敵の攻撃位置

        public float musicScore;                    //時間
    }
    public List<Data> stepData = new List<Data>();
    List<float> textTime = new List<float>();

    static StepData StepData_;  //自身を参照用

    // Start is called before the first frame update
    private void Awake()
    {
        int count = 0;
        stepData.Clear();
        fileName += scoreName + ".txt";

        //Debug.Log(File.Exists(fileName));

        //テキストの読み込み
        if (File.Exists(fileName))
        {
            foreach (string str in File.ReadLines(fileName))
            {
                string[] arr = str.Split(',');                           //（,）カンマで分ける
                stepData.Add(new Data());

                textTime.Add(float.Parse(arr[(int)INPUT_TEXT.MusicScore]));

                stepData[count].musicScore = float.Parse(arr[(int)INPUT_TEXT.MusicScore]);
                stepData[count].ememyAttackType = (ENEMY_ATTACK_TYPE)int.Parse(arr[(int)INPUT_TEXT.EnemyAttackType]);
                stepData[count].plStep = (PL_STEP_TIMING)int.Parse(arr[(int)INPUT_TEXT.PlStep]);

                for (int i = (int)INPUT_TEXT.EnemyAttackLane0; i <= (int)INPUT_TEXT.EnemyAttackLane5; i++)
                {
                    stepData[count].enemyAttackPos[i - (int)INPUT_TEXT.EnemyAttackLane0] = bool.Parse(arr[i]);
                }

                count++;
            }
        }

        StepData_ = this;   //初期化と数値の代入(thisしないとバグる)
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// timeに一番近いテキスト内サウンド時間の配列番号を返す
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    static public int GetTimeNearBeatTime(float time)
    {
        int num = 0;
        //目的の値に最も近い値を返す
        if (File.Exists(StepData_.fileName))
        {
            var min = StepData_.textTime.Min(c => Math.Abs(c - time));
            num = StepData_.textTime.IndexOf(StepData_.textTime.First(c => Math.Abs(c - time) == min));
        }
        return num;
    }

    /// <summary>
    /// テキスト内にあるデータを持ってくることができる。enumのINPUT_TEXTで配列内の各データの種類がわかる。
    /// </summary>
    public static List<Data> GetStepData { get { return StepData_.stepData; } }

    /// <summary>
    /// 曲の現在の再生時間を取得できる
    /// </summary>
    public static float GetSoundPlayTime { get { return StepData_.source.time; } }

    /// <summary>
    /// 曲の長さ(時間)
    /// </summary>
    public static float GetSoundMaxTime { get { return StepData_.source.clip.length; } }

    /// <summary>
    /// テキストファイルの場所
    /// </summary>
    public static string GetScoreLink { get { return StepData_.fileName; } }
}