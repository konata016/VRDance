using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageNum : MonoBehaviour
{
    public Material on;
    public Material off;

    GameObject[] numArr;
    int num;

    // Start is called before the first frame update
    void Start()
    {
        //子オブジェクトを検出し、ページオブジェクトとして扱う
        numArr = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            numArr[i] = transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //切り替え
        if (OnTriggerNext() && num < numArr.Length - 1) num++;
        if (OnTriggerBack() && num > 0) num--;

        //トリガーがひかれた場合、マテリアルの差し替え
        if (OnTriggerNext() || OnTriggerBack())
        {
            for (int i = 0; i < numArr.Length; i++)
            {
                numArr[i].GetComponent<Renderer>().material = off;
            }
            numArr[num].GetComponent<Renderer>().material = on;
        }
    }

    bool OnTriggerNext()
    {
        return Input.GetKeyDown(KeyCode.RightArrow);
    }

    bool OnTriggerBack()
    {
        return Input.GetKeyDown(KeyCode.LeftArrow);
    }
}
