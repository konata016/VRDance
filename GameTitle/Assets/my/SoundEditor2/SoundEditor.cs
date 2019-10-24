using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using System;

//敵の攻撃パターンを作成するためのエディター

public class SoundEditor : MonoBehaviour
{
    public Slider slider;   //再生バー用
    public Text text;
    public int beat = 4;    //1小節に何拍打つか

    public enum ATTACKTYPE
    {
        WaveWide, WaveRight, WaveLeft,
        ThrowRight, ThrowLeft,
        Nothing
    }

    [Serializable]
    public class EnemyAttackTime
    {
        public ATTACKTYPE attackType;   //敵の攻撃の種類
        public float musicScore;        //攻撃を出す時間
    }
    public List<EnemyAttackTime> timeList = new List<EnemyAttackTime>();

    List<float> timeCheck = new List<float>();  //時間のチェック用に使う
   
    AudioSource source;     //サウンド再生環境
    AudioClip clip;         //サウンドデータ

    float a;
    bool onMusic;


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        clip = source.clip;
        source.Stop();

        //再生バーの終了位置セット
        slider.maxValue = clip.length;

        //テキストの列分だけ回す
        int count = 0;
        foreach (string str in File.ReadLines("aaa.txt"))
        {
            timeList.Add(new EnemyAttackTime());                //リスト作成
            string[] arr = str.Split(',');                      //（,）カンマで分ける
            timeList[count].musicScore = float.Parse(arr[0]);   //テキストに書かれている時間の格納

            //チェック用
            timeCheck.Add(float.Parse(arr[0]));

            //enumを格納するときに名称として格納されたためそれ用に割り振りなおしている
            switch (arr[1])
            {
                case "WaveWide": timeList[count].attackType = ATTACKTYPE.WaveWide; break;
                case "WaveRight": timeList[count].attackType = ATTACKTYPE.WaveRight; break;
                case "WaveLeft": timeList[count].attackType = ATTACKTYPE.WaveLeft; break;
                case "ThrowRight": timeList[count].attackType = ATTACKTYPE.ThrowRight; break;
                case "ThrowLeft": timeList[count].attackType = ATTACKTYPE.ThrowLeft; break;
                case "Nothing": timeList[count].attackType = ATTACKTYPE.Nothing; break;
                default:break;
            }
            count++;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        SoundControl();

        //初めて作成するときのみ
        if (Input.GetKeyDown(KeyCode.C))
        {
            //Beatの計算
            MusicBeatTime(beat);
        }

        Save();
        OutputBeatTime();
    }

    //操作系
    void SoundControl()
    {
        //BGMの再生時間と再生バーをリンク
        if (onMusic) slider.value = source.time;

        //マウスクリックをするとBGMが止まる
        if (Input.GetMouseButtonDown(0))
        {
            source.Stop();
            onMusic = false;
        }

        //スペースキーで再生
        if (Input.GetKeyDown(KeyCode.Space))
        {
            source.time = slider.value; //再生バーの位置とBGM再生位置をリンク
            onMusic = true;
            source.Play();              //BGM再生
        }
    }

    //リストのセーブ
    void Save()
    {
        //セーブ用
        if (Input.GetKeyDown(KeyCode.S))
        {
            //リスト内のデータを文字列に格納する
            List<string> strList = new List<string>();
            for (int i = 0; i < timeList.Count; i++)
            {
                strList.Add(timeList[i].musicScore + "," + timeList[i].attackType);
            }

            //テキストに書き出し
            File.WriteAllLines("aaa.txt", strList);
        }
    }

    //時間、リスト番号の表示
    void OutputBeatTime()
    {
        //目的の値に最も近い値を返す
        var min = timeCheck.Min(c => Math.Abs(c - slider.value));
        int num = timeCheck.IndexOf(timeCheck.First(c => Math.Abs(c - slider.value) == min));
        text.text = "リスト時間:" + timeCheck.First(c => Math.Abs(c - slider.value) == min) +
                    "\n    配列数   :" + num + "\nリアル時間:" + slider.value;
    }

    //リスト作成
    void MusicBeatTime(int beat)
    {
        //リスト初期化
        timeList.Clear();

        //一小節の時間の計算
        //60*拍子*小節数/テンポ
        float barTime = 60 * Music.MyInspectorList[0].UnitPerBeat * 1 / (float)Music.MyInspectorList[0].Tempo;

        //拍子にする
        barTime = barTime / beat;

        //拍子リストの作成
        int count = 0;
        while (true)
        {
            //チェック用
            timeCheck.Add(barTime * count);

            timeList.Add(new EnemyAttackTime());                    //リスト作成
            timeList[count].musicScore = barTime * count;           //1曲に何拍打つかの計算
            timeList[count].attackType = ATTACKTYPE.Nothing;
            if (timeList[count].musicScore >= clip.length) break;   //1曲の時間分すべて出すことができたらループから抜ける
            //if (count == 10) break;
            count++;
        }
    }

    //デバッグ用
    void DebugLog()
    {
        float barTime = 60 * Music.MyInspectorList[0].UnitPerBeat * 1 / (float)Music.MyInspectorList[0].Tempo;
        barTime = barTime / 4;

        float b = source.time - a;

        Debug.Log("引いた数" + b);

        a = source.time;

        Debug.Log("計算" + barTime);
        Debug.Log("リアル" + source.time);
    }
}
