using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 目的地までの移動と元の場所に戻るプログラム
/// </summary>
public class PopUp : MonoBehaviour
{
    public GameObject headDisplay;
    public float fallTime = 3;
    Vector3 tmpPos;
    bool change;

    // Start is called before the first frame update
    void Start()
    {
        tmpPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (OnTrigger())
        {
            if (!change)
            {
                //目的地まで移動する
                Move(gameObject, headDisplay.transform.position);
                change = true;
            }
            else
            {
                //元の位置に戻る
                Move(gameObject, tmpPos);
                change = false;
            }
        }
    }

    //移動
    void Move(GameObject obj,Vector3 Destination)
    {
        DOTween
       .To(value => Move(value), 0, 1, fallTime)
       .SetEase(Ease.InQuart);

        void Move(float value)
        {
            var pos = obj.transform.position;
            pos.y = Mathf.Lerp(pos.y, Destination.y, value);
            obj.transform.position = pos;
        }
    }

    bool OnTrigger()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
