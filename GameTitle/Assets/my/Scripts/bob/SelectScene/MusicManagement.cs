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
        public TextMeshProUGUI soundName;        // 曲名
        public TextMeshProUGUI difficultyLevel;  // 難易度
    }
    public List<SoundBox> soundBoxList = new List<SoundBox>();

    [System.Serializable]
    public class MusicInfo
    {
        public string soundName;        // 曲名
        public string difficultyLevel;  // 難易度
        //public Image jacketImage;       // ジェケットの画像
    }
    public List<MusicInfo> musicInfoList = new List<MusicInfo>();
    public int nomberOfMusic; // 見えている曲
    private int nomberOfMusic_Old;


    void Start()
    {
        nomberOfMusic = 0;// 曲数
        nomberOfMusic_Old = nomberOfMusic;
        MusicInformationSet();
    }
    
    void Update()
    {
        //Debug.Log("song : " + (nomberOfMusic + 1));
    }

    public void MusicInformationSet()
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
                    }
                    else
                    {
                        soundBoxList[i].soundName.text = musicInfoList[nomberOfMusic - 1].soundName;
                        soundBoxList[i].difficultyLevel.text = musicInfoList[nomberOfMusic - 1].difficultyLevel;
                    }
                }
                else if (i == (int)BpmMove_Cube.boxOrientation)
                {
                    soundBoxList[i].soundName.text = musicInfoList[nomberOfMusic].soundName;
                    soundBoxList[i].difficultyLevel.text = musicInfoList[nomberOfMusic].difficultyLevel;
                }
                else if (i == (int)BpmMove_Cube.boxOrientation + 1 || i == (int)BpmMove_Cube.boxOrientation - 3)// 左面の時だけ別例
                {
                    if (nomberOfMusic == musicInfoList.Count - 1)// 最後の曲の時だけ例外
                    {
                        soundBoxList[i].soundName.text = musicInfoList[0].soundName;
                        soundBoxList[i].difficultyLevel.text = musicInfoList[0].difficultyLevel;
                    }
                    else
                    {
                        soundBoxList[i].soundName.text = musicInfoList[nomberOfMusic + 1].soundName;
                        soundBoxList[i].difficultyLevel.text = musicInfoList[nomberOfMusic + 1].difficultyLevel;
                    }
                }
            }
        }
    }
}
