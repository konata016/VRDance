using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicManagement : MonoBehaviour
{
    [System.Serializable]
    public class SoundBox
    {
        public TextMeshProUGUI soundName;       // 曲名
        public TextMeshProUGUI difficultyLevel; // 難易度
        public Image jacketImage;               // ジェケットの画像
        public AudioSource audioSource;         // サンプルサウンド
    }
    public List<SoundBox> soundBoxList = new List<SoundBox>();

    [System.Serializable]
    public class MusicInfo
    {
        public string soundName;        // 曲名
        public string difficultyLevel;  // 難易度
        public Sprite jacketImage;      // ジェケットの画像
        public AudioClip sampleSound;   // サンプルサウンド
        public float soundBPM;          // 曲のBPM（テンポ）
        public string soundScore;       // 譜面の名前
    }
    public List<MusicInfo> musicInfoList = new List<MusicInfo>();
    public int nomberOfMusic; // 見えている曲
    private int nomberOfMusic_Old;
    private bool onlyOneTime = true;
    private Example example;
    public static string GetSoundScore { get; private set; }


    void Start()
    {
        nomberOfMusic = 0;// 曲数
        nomberOfMusic_Old = nomberOfMusic;
        // ボックスの背面
        soundBoxList[2].soundName.text = musicInfoList[2].soundName;
        soundBoxList[2].difficultyLevel.text = musicInfoList[2].difficultyLevel;
        soundBoxList[2].jacketImage.sprite = musicInfoList[2].jacketImage;
        GameObject example_date = GameObject.Find("DokudoCube");
        example = example_date.GetComponent<Example>();
    }
    
    void Update()
    {
        //Debug.Log("song : " + (nomberOfMusic + 1));
        if (onlyOneTime)
        {
            MusicInformationSet();
            onlyOneTime = false;
        }
        if (BpmMove_Cube.boxOrientation >= BpmMove_Cube.BOXORIENTATION.soundBox_1 && BpmMove_Cube.boxOrientation <= BpmMove_Cube.BOXORIENTATION.soundBox_4)
            if (soundBoxList[(int)BpmMove_Cube.boxOrientation].audioSource.time == 0.0f)
                example.timePuls = 0.0f;
    }

    public void MusicInformationSet()// 回転に合わせて曲の情報をセット
    {
        if (BpmMove_Cube.boxOrientation >= BpmMove_Cube.BOXORIENTATION.soundBox_1 && BpmMove_Cube.boxOrientation <= BpmMove_Cube.BOXORIENTATION.soundBox_4)
        {
            for (int i = 0; i < soundBoxList.Count; i++)
            {
                if (i == (int)BpmMove_Cube.boxOrientation - 1 || i == (int)BpmMove_Cube.boxOrientation + 3)// 正面の時だけ別例
                {
                    if (nomberOfMusic == 0)// 一曲目の時だけ例外
                    {
                        soundBoxList[i].soundName.text = musicInfoList[musicInfoList.Count - 1].soundName;
                        soundBoxList[i].difficultyLevel.text = musicInfoList[musicInfoList.Count - 1].difficultyLevel;
                        soundBoxList[i].jacketImage.sprite = musicInfoList[musicInfoList.Count - 1].jacketImage;
                        soundBoxList[i].audioSource.clip = musicInfoList[musicInfoList.Count - 1].sampleSound;
                    }
                    else
                    {
                        soundBoxList[i].soundName.text = musicInfoList[nomberOfMusic - 1].soundName;
                        soundBoxList[i].difficultyLevel.text = musicInfoList[nomberOfMusic - 1].difficultyLevel;
                        soundBoxList[i].jacketImage.sprite = musicInfoList[nomberOfMusic - 1].jacketImage;
                        soundBoxList[i].audioSource.clip = musicInfoList[nomberOfMusic - 1].sampleSound;
                    }
                    soundBoxList[i].audioSource.Stop();// サンプルサウンド止める
                }
                else if (i == (int)BpmMove_Cube.boxOrientation)
                {
                    soundBoxList[i].soundName.text = musicInfoList[nomberOfMusic].soundName;
                    soundBoxList[i].difficultyLevel.text = musicInfoList[nomberOfMusic].difficultyLevel;
                    soundBoxList[i].jacketImage.sprite = musicInfoList[nomberOfMusic].jacketImage;
                    soundBoxList[i].audioSource.clip = musicInfoList[nomberOfMusic].sampleSound;
                    soundBoxList[i].audioSource.time = 0.0f;
                    soundBoxList[i].audioSource.Play();// サンプルサウンド再生
                    ScoreText.soundTitle= musicInfoList[nomberOfMusic].soundName;// Resultにタイトルを教える
                    Example.nowBPM = musicInfoList[nomberOfMusic].soundBPM;// ドキドキューブにBPMを教える
                    GetSoundScore = musicInfoList[nomberOfMusic].soundScore;// 選択されてる曲の譜面
                    example.timePuls = 0.0f;

                }
                else if (i == (int)BpmMove_Cube.boxOrientation + 1 || i == (int)BpmMove_Cube.boxOrientation - 3)// 左面の時だけ別例
                {
                    if (nomberOfMusic == musicInfoList.Count - 1)// 最後の曲の時だけ例外
                    {
                        soundBoxList[i].soundName.text = musicInfoList[0].soundName;
                        soundBoxList[i].difficultyLevel.text = musicInfoList[0].difficultyLevel;
                        soundBoxList[i].jacketImage.sprite = musicInfoList[0].jacketImage;
                        soundBoxList[i].audioSource.clip = musicInfoList[0].sampleSound;
                    }
                    else
                    {
                        soundBoxList[i].soundName.text = musicInfoList[nomberOfMusic + 1].soundName;
                        soundBoxList[i].difficultyLevel.text = musicInfoList[nomberOfMusic + 1].difficultyLevel;
                        soundBoxList[i].jacketImage.sprite = musicInfoList[nomberOfMusic + 1].jacketImage;
                        soundBoxList[i].audioSource.clip = musicInfoList[nomberOfMusic + 1].sampleSound;
                    }
                    soundBoxList[i].audioSource.Stop();// サンプルサウンド止める
                }
            }
        }
    }
}
