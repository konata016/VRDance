using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// プレイヤーがダメージを受けた場合、ステージを落とす
/// </summary>
public class PlDamageStage : MonoBehaviour
{
    public Material changeColor;
    public GameObject[] stageObjArr;
    public float lostPoint = -10;
    public float fallTime = 3;

    public static int Life { get;private set; }
    public static bool OnDamageTrigger { private get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Life = stageObjArr.Length;
    }

    // Update is called once per frame
    void Update()
    {
        DebugTrigger();             //キーボード入力用

        //敵の攻撃に当たった場合Lifeをマイナスする
        if (OnDamageTrigger)
        {
            Life--;
            if (Life != -1) FallMove(stageObjArr[Life]);
            OnDamageTrigger = false;
        }

        StageHidden();
    }

    //ステージが一定の距離まで動いたら非表示にする
    void StageHidden()
    {
        for (int i = 0; i < stageObjArr.Length; i++)
        {
            var obj = stageObjArr[i];
            var pos = obj.transform.position;
            if (lostPoint > pos.y -1f)
            {
                obj.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    //ステージが落ちるモーション
    void FallMove(GameObject obj)
    {
        obj.GetComponent<Renderer>().material = changeColor;

        DOTween
               .To(value => Move(value), 0, 1, fallTime)
               .SetEase(Ease.InQuart);

        void Move(float value)
        {
            var pos = obj.transform.position;
            pos.y = Mathf.Lerp(pos.y, lostPoint, value);
            obj.transform.position = pos;
        }
    }

    //デバッグ用
    void DebugTrigger()
    {
        if (Input.GetKeyDown(KeyCode.D)) Life--;
    }
}
