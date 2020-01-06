using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleInstant : MonoBehaviour
{
    public int count = 10;
    public float r = 5;
    public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject gameObject = InstantCirclePos(i, count, obj, r);
            gameObject.transform.LookAt(transform);
            gameObject.transform.GetChild(0).gameObject.GetComponent<ScreenShot>().num = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //半円状にオブジェクトを生成
    GameObject InstantCirclePos(int count, int maxCount, GameObject obj, float radius)
    {
        GameObject gameObject;
        //半円上に生成する
        Vector3 v3 = CirclePos(maxCount - 1, radius, count,transform.position);
        gameObject = Instantiate(obj, v3, new Quaternion());
        gameObject.transform.parent = transform;
        return gameObject;

        //半円上のポジションを取得
        Vector3 CirclePos(int maxNum, float rad, int currentNum, Vector3 pos)
        {
            if (maxNum != 0)
            {
                //きれいに半円状にに出すやつ
                float r = (360 / maxNum) * currentNum;

                float angle = r * Mathf.Deg2Rad;
                pos.x += rad * Mathf.Cos(angle);
                pos.z += rad * Mathf.Sin(angle);
            }
            else
            {
                pos.x += 0;
                pos.z += rad;
            }

            return pos;
        }
    }
}
