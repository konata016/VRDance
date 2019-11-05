using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShufflePos : MonoBehaviour
{
    public GameObject[] pointPos = new GameObject[4];   //オブジェクトのセット
    public float correctionY = 1f;                      //Y座標の修正用
    public float speed = 5;                             //移動する速度
    public int randomSeed = 10;                         //シード値

    Vector3[] savePos = new Vector3[4];                 //
    Vector3[] newPos = new Vector3[4];                  //
    List<int> randomTable = new List<int>();            //被らない乱数を出すためのやつ

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(randomSeed);   //シード値をセット
        Vector3 v3 = Vector3.zero;
        int count = 0;

        for (int i = 0; i < pointPos.Length; i++)
        {
            randomTable.Add(i); //ラダンムテーブル作成

            //オブジェクトの中でY座標が最も高い座標を先頭に配列に格納
            if (pointPos[i].transform.position.y > v3.y)
            {
                savePos[0] = pointPos[i].transform.position;
                v3 = savePos[i];
            }
            else
            {
                count++;
                savePos[count] = pointPos[i].transform.position;
            }
        }

        //Y座標が最も高い座標と他の座標を入れ替える
        newPos[0] = new Vector3(savePos[0].x, savePos[1].y - correctionY, savePos[0].z);
        for (int i = 1; i < pointPos.Length; i++)
        {
            newPos[i] = new Vector3(savePos[i].x, savePos[0].y - correctionY, savePos[i].z);
        }

        //ポジションをランダムにする
        for (int i = 0; i < pointPos.Length; i++)
        {
            int randomNum = Random.Range(0, randomTable.Count);
            savePos[i] = newPos[randomTable[randomNum]];
            randomTable.RemoveAt(randomNum);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        //座標を動かす
        for (int i = 0; i < pointPos.Length; i++)
        {
            Vector3 pos = pointPos[i].transform.position;
            pos = Vector3.MoveTowards(pos, savePos[i], speed * Time.deltaTime);
            pointPos[i].transform.position = pos;
        }
    }
}
