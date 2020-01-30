using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ページ切り替え表示プログラム
/// ページ番号アイコン自動生成付き
/// </summary>
public class PageInstant : MonoBehaviour
{
    [Header("ページアイコンの設定")]
    public GameObject pageNumObj;
    public float siz = 0.05f;
    public float space = 0.08f;
    //public int instantCount = 7;  //最大ページ数

    [Header("ページアイコンの色")]
    public Material on;
    public Material off;

    [Header("表示するもの")]
    public GameObject[] panelArr;

    [HideInInspector] public List<GameObject> pageNumObjList = new List<GameObject>();
    [HideInInspector] public int num;

    public static bool OnTriggerFootR { get; set; }
    public static bool OnTriggerFootL { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        //生成する初期位置の計算
        float f = (panelArr.Length - 1) / 2f * space * -1;
        Vector3 tmpPos = transform.position;

        //ページのアイコンのサイズ変更
        pageNumObj.transform.localScale = Vector3.one * siz;

        //ページアイコン生成
        for (int i = 0; i < panelArr.Length; i++)
        {
            Vector3 pos = new Vector3(tmpPos.x + (f + (space * i)), tmpPos.y, tmpPos.z);
            pageNumObjList.Add(Instantiate(pageNumObj, pos, new Quaternion()));
            pageNumObjList[pageNumObjList.Count - 1].GetComponent<Renderer>().material = off;
            pageNumObjList[pageNumObjList.Count - 1].transform.parent = transform;
        }
        pageNumObjList[0].GetComponent<Renderer>().material = on;
    }

    // Update is called once per frame
    void Update()
    {
        //切り替え
        if (OnTriggerNext() && num < pageNumObjList.Count - 1)
        {
            SE_Manager.SePlay(SE_Manager.SE_NAME.Step);
            num++;
        }
        if (OnTriggerBack() && num > 0)
        {
            SE_Manager.SePlay(SE_Manager.SE_NAME.Step);
            num--;
        }

        //トリガーがひかれた場合、マテリアルの差し替え
        if (OnTriggerNext() || OnTriggerBack())
        {
            for (int i = 0; i < pageNumObjList.Count; i++)
            {
                pageNumObjList[i].GetComponent<Renderer>().material = off;

                panelArr[i].SetActive(false);   //パネルを非表示にする
            }
            pageNumObjList[num].GetComponent<Renderer>().material = on;

            panelArr[num].SetActive(true);      //ページ番号と同じ場所のパネルを表示

            OnTriggerFootR = false;
            OnTriggerFootL = false;
        }

    }

    bool OnTriggerNext()
    {
        return OnTriggerFootR || Input.GetKeyDown(KeyCode.RightArrow);
    }

    bool OnTriggerBack()
    {
        return OnTriggerFootL|| Input.GetKeyDown(KeyCode.LeftArrow);
    }
}
