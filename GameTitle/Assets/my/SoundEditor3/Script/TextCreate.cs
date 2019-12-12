using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextCreate : MonoBehaviour
{
    public int bar = 4;         //拍子
    public int beat = 16;       //1小節の刻む数
    public int tempo = 120;     //BMP

    float barTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            int num = (int)StepData.INPUT_TEXT.EnemyAttackLane5 - (int)StepData.INPUT_TEXT.EnemyAttackLane0+1;
            List<string> strList = new List<string>();
            bool[] fgr = new bool[num];
            int count = 0;
            float time = 0;
            barTime = 60 * (float)bar * 1 / (float)tempo;
            barTime = barTime / beat;

            int enemyAtt = 0, pl = 0;

            while (true)
            {
                //時間の計算
                time = barTime * count;

                //リストに格納
                strList.Add(time + "," + enemyAtt +
                        "," + fgr[0] + "," + fgr[1] + "," + fgr[2] +
                        "," + fgr[3] + "," + fgr[4] + "," + fgr[5] +
                        "," + pl);
                if (time >= StepData.GetSoundMaxTime) break;
                count++;
            }

            //テキストに書き出し
            File.WriteAllLines(StepData.GetScoreLink, strList);
        }
    }
}
