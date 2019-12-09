using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextOutput : MonoBehaviour
{
    public GameObject instantObjGroup;
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
        if (Input.GetKeyDown(KeyCode.S))
        {
            List<string> strList = new List<string>();
            int count = 0;

            //一小節の時間の計算
            //60*拍子*小節数/テンポ
            barTime = 60 * (float)bar * 1 / (float)tempo;
            barTime = barTime / beat;

            while (true)
            {
                //宣言
                bool[] fgr = new bool[StepData.GetStepData[0].enemyAttackPos.Length];
                int enemyAtt = 0, pl = 0;
                float time = 0;

                //時間の計算
                time = barTime * count;

                //オブジェクトの数が足りない場合処理をスキップする
                if (instantObjGroup.transform.childCount > count)
                {
                    //短縮用
                    GameObject child = instantObjGroup.transform.GetChild(count).gameObject;
                    GameObject objTrue = child.transform.GetChild((int)ObjGenerator.PARENT_OBJ.FrgTrue).gameObject;
                    GameObject objEnemyAtt = child.transform.GetChild((int)ObjGenerator.PARENT_OBJ.EnemyAttackType).gameObject;
                    GameObject objPl = child.transform.GetChild((int)ObjGenerator.PARENT_OBJ.PlStep).gameObject;

                    //敵の攻撃座標
                    for (int f = 0; f < objTrue.transform.childCount; f++)
                    {
                        if (objTrue.transform.GetChild(f).gameObject.activeSelf)
                        {
                            fgr[f] = true;
                        }
                    }

                    //敵の攻撃種類
                    for (int f = 0; f < objEnemyAtt.transform.childCount; f++)
                    {
                        if (objEnemyAtt.transform.GetChild(f).gameObject.activeSelf)
                        {
                            enemyAtt = f;
                            break;
                        }
                    }

                    //プレイヤーのステップタイミング
                    for (int f = 0; f < objPl.transform.childCount; f++)
                    {
                        if (objPl.transform.GetChild(f).gameObject.activeSelf)
                        {
                            pl = f;
                            break;
                        }
                    }
                }

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
