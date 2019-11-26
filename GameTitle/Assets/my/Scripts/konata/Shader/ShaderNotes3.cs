using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//シェーダーノーツを動かすやつ
//完成形

public class ShaderNotes3 : MonoBehaviour
{
    public float speed = 10;        //ノーツの速度
    public GameObject startPosObj;  //ノーツが放たれる場所

    Material material;              //シェーダーを読み込む用
    Vector3 worldStartPos;          //ノーツが放たれる場所の変換
    Vector3 startPos;               //個々のオブジェクトのノーツの開始ポイント(個々のシェーダー処理用)
    Vector3 endPos;                 //個々のオブジェクトのノーツのエンドポイント(個々のシェーダー処理用)
    float interval;                 //ノーツが放たれてから自分のオブジェクトに達したかどうかの計算用

    class Notes
    {
        public bool onTimeStart;        //シェーダー内の「_Pos」に空きがあるかどうかのフラグ
        public float setShaderFloat;    //ノーツ移動の計算用

        public Notes(float startPos)    //初期化用
        {
            onTimeStart = false;
            setShaderFloat = startPos;
        }
    }
    Notes[] notesArr = new Notes[4];    //最大シェーダー上で4つのノーツしか動かすことができないので4

    //時間のリストを読む場合、いらない
    List<float> timeList = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().materials[1];                       //シェーダー参照用
        worldStartPos = startPosObj.transform.position;                     //ノーツが放たれる場所の変換
        startPos = transform.GetChild(0).gameObject.transform.position;     //子オブジェクトの頭にあるオブジェクトの位置をノーツの開始ポイントとする
        endPos = transform.GetChild(1).gameObject.transform.position * -1;  //子オブジェクトの頭の次にあるオブジェクトの位置をノーツのエンドポイントとする

        //ノーツが放たれる場所から自身がどれだけ離れているかの計算
        //もしかしたらstartPosにした方がいいかもしれない
        float dis = worldStartPos.z - (transform.position.z + transform.localScale.z / 2); 
        interval = dis / speed;

        for (int i = 0; i < notesArr.Length; i++)
        {
            notesArr[i] = new Notes(-startPos.z);           //クラスの配列のインスタンス化
            material.SetFloat("_Pos" + i, startPos.z * -1); //シェーダー「_Pos」の初期化
        }
    }

    // Update is called once per frame
    void Update()
    {
        //時間のリストを読む場合、いらない
        if (OnTrigger())
        {
            timeList.Add(0f);
        }

        //時間のリストが入る場合これがいらない
        for (int i = 0; i < timeList.Count; i++)
        {
            timeList[i] += Time.deltaTime;
        }

        if (timeList.Count != 0)        //時間のリストを読む場合、いらない
        {
            if (timeList[0] > interval) //時間のリストを読む場合ここに入れ替わりで入る
            {
                timeList.RemoveAt(0);   //時間のリストを読む場合、いらない

                //グラグが立っていないものを探す
                for (int f = 0; f < notesArr.Length; f++)
                {
                    if (!notesArr[f].onTimeStart)
                    {
                        notesArr[f].onTimeStart = true;
                        break;
                    }
                }
            }
        }
        
        for (int i = 0; i < notesArr.Length; i++)
        {
            //グラグが立っていいる場合処理をする
            if (notesArr[i].onTimeStart)
            {
                //移動の処理
                notesArr[i].setShaderFloat += speed * Time.deltaTime;
                material.SetFloat("_Pos" + i, notesArr[i].setShaderFloat);

                //既定のポイントを超えた場合初期化
                if (notesArr[i].setShaderFloat >= endPos.z)
                {
                    notesArr[i] = new Notes(-startPos.z);
                    material.SetFloat("_Pos" + i, -startPos.z);
                }
            }
        }

        
    }

    //トリガーの処理
    bool OnTrigger()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
