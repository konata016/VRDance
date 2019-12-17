using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlDamageEffect : MonoBehaviour
{
    public Material material;       //ディゾルブマテリアル
    public int maxHp = 5;           //最大HP
    public float speed = 0.1f;      //消えてく速度
    public bool isDamage;           //ダメージが入った場合
    float hp;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        //ディゾルブの初期化　これを書かないと消えっぱなしになる
        material.SetFloat("_Gauge", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        float tmpHp = material.GetFloat("_Gauge");

        //ダメージを受けるとHPが減る
        if (isDamage)
        {
            count++;
            hp = 1f / (maxHp + 1) * count;
            isDamage = false;
        }

        //ゆっくりとHPが減る処理
        if (tmpHp < hp)
        {
            //Hpをマイナスする
            material.SetFloat("_Gauge", tmpHp += Time.deltaTime* speed);
        }
    }


}
