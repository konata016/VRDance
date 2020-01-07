using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの攻撃(ビーム)の処理
/// </summary>
public class PlAttackBeam : MonoBehaviour
{
    public GameObject targetObj;
    public GameObject beamObj;
    public float speed = 5;
    public float rad = 5;
    //public int spawnCount = 4;      //個々の数値を変えたら生成する数が変わる

    public List<GameObject> beamObjList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OnTrigger())
        {
            //ビームの生成
            InstantBeam();
        }

        //ビームの移動
        foreach (GameObject obj in beamObjList)
        {
            Vector3 pos = obj.transform.position;
            pos = Vector3.MoveTowards(pos, targetObj.transform.position, speed * Time.deltaTime);
            obj.transform.LookAt(targetObj.transform);
            obj.transform.position = pos;
        }

        //目的地点に到達した場合
        if (beamObjList.Count != 0)
        {
            float dis = Vector3.Distance(beamObjList[0].transform.position, targetObj.transform.position);
            if (0.3f > dis)
            {
                beamObjList.RemoveAt(0);
            }
        }
    }

    //ビームの生成
    void InstantBeam()
    {
        switch (NotesManager2.rank)
        {
            case NotesManager2.RANK.Bad:
                beamObjList.Add(InstantCirclePos(1, 1, beamObj, rad));
                break;

            case NotesManager2.RANK.Good:
                for (int i = 0; i < 2; i++)
                {
                    beamObjList.Add(InstantCirclePos(i, 2, beamObj, rad));
                }
                break;

            case NotesManager2.RANK.Excellent:
                for (int i = 0; i < 4; i++)
                {
                    beamObjList.Add(InstantCirclePos(i, 4, beamObj, rad));
                }
                break;

            default: break;
        }
    }

    //半円状にオブジェクトを生成
    GameObject InstantCirclePos(int count, int maxCount, GameObject obj, float radius)
    {
        GameObject gameObject;
        //半円上に生成する
        Vector3 v3 = CirclePos(maxCount - 1, radius, count, Vector3.zero);
        gameObject = Instantiate(obj, v3, new Quaternion());
        gameObject.transform.parent = transform;
        return gameObject;

        //半円上のポジションを取得
        Vector3 CirclePos(int maxNum, float rad, int currentNum, Vector3 pos)
        {
            if (maxNum != 0)
            {
                //きれいに半円状にに出すやつ
                float r = (180 / maxNum) * currentNum;

                float angle = r * Mathf.Deg2Rad;
                pos.x = rad * Mathf.Cos(angle);
                pos.y = rad * Mathf.Sin(angle);
            }
            else
            {
                pos.x = 0;
                pos.y = rad;
            }

            return pos;
        }
    }

    bool OnTrigger()
    {
        return TriggerManager.GetOnTriggerFoot;
    }
}
