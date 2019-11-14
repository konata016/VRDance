using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StepData : MonoBehaviour
{
    public string scoreName = "aaa";
    public string fileName = "Assets/SoundEditor2/Score/";

    public AudioSource source;     //サウンド

    public enum INPUT_TEXT { MusicScore, PlStep = 8 }
    public enum PL_STEP_TIMING
    {
        Nothing,
        Step,
    }

    public class Data
    {
        public PL_STEP_TIMING plStep;
        public float musicScore;
    }
    static List<Data> StepData_ = new List<Data>();
    static AudioSource Source_ = new AudioSource();

    //static StepData StepData_ = new StepData();

    // Start is called before the first frame update
    void Start()
    {
        int count = 0;
        StepData_.Clear();
        fileName += scoreName + ".txt";

        //Debug.Log(File.Exists(fileName));

        //テキストの読み込み
        foreach (string str in File.ReadLines(fileName))
        {
            string[] arr = str.Split(',');                           //（,）カンマで分ける
            StepData_.Add(new Data());

            StepData_[count].musicScore = float.Parse(arr[(int)INPUT_TEXT.MusicScore]);
            StepData_[count].plStep = (PL_STEP_TIMING)int.Parse(arr[(int)INPUT_TEXT.PlStep]);
            Debug.Log((PL_STEP_TIMING)int.Parse(arr[(int)INPUT_TEXT.PlStep]));
            count++;
        }

        Source_ = source;   //オーディオデータの代入
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static List<Data> GetStepData { get { return StepData_; } }
    public static float GetSoundPlayTime { get { return Source_.time; } }
}
