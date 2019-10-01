using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPosCenter : MonoBehaviour
{
    public static int hitPosNum { get; set; }

    public GameObject leftFoot;
    public GameObject rightFoot;
    public Vector3 groundPos;

    public int cutNum = 8;

    Quaternion foodQuaternion;
    float numUP = 100;
    float ang;

    public struct FootPosParameter
    {
        public bool isFootUpR;
        public bool isFootUpL;

        public bool isFootDownR;
        public bool isFootDownL;

        public bool isGroundR;
        public bool isGroundL;

        public void refresh()
        {
            isFootUpR = false;
            isFootUpL = false;
            isFootDownR = false;
            isFootDownL = false;
        }
    }
    public FootPosParameter footPos = new FootPosParameter();


    // Start is called before the first frame update
    void Start()
    {
        ang = 360 / cutNum;
    }

    // Update is called once per frame
    void Update()
    {
        //両足の中間にポジションをとる
        transform.position = CenterPos(transform.position);

        //初めに地面についた足の方向を見る
        GroundJudge();

        //方向を記録する
        FootDirection();

        //フラグの初期化
        footPos.refresh();
    }

    //地面に接触したときに一番初めについた足の方向を見る
    void GroundJudge()
    {
        //どっちも判定をとってしまう

        float yR, yL;

        yR = Mathf.Floor(rightFoot.transform.position.y * numUP) / numUP;
        yL = Mathf.Floor(leftFoot.transform.position.y * numUP) / numUP;

        //足を上げたとき
        if (groundPos.y < yR)
        {
            footPos.isFootUpR = true;
            footPos.isGroundR = false;
        }
        if (groundPos.y < yL)
        {
            footPos.isFootUpL = true;
            footPos.isGroundL = false;
        }

        //地面と接触したとき
        if (groundPos.y >= yR)
        {
            footPos.isFootDownR = true;
            footPos.isGroundR = true;
        }
        if (groundPos.y >= yL)
        {
            footPos.isFootDownL = true;
            footPos.isGroundL = true;
        }

        //方向を向かせる
        if (footPos.isFootDownR && !footPos.isFootDownL)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.up, rightFoot.transform.position - transform.position);
        }
        if (footPos.isFootDownL && !footPos.isFootDownR)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.up, leftFoot.transform.position - transform.position);
        }
        Debug.Log(hitPosNum);
    }

    //足の置いてある方向を決める
    void FootDirection()
    {
        foodQuaternion = transform.rotation;
        float FoodAng = foodQuaternion.eulerAngles.y;
        float minAng = ang / 2;

        //中心から見た足の角度を出す
        if (FoodAng <= minAng || FoodAng > minAng + ang * (cutNum - 1))
        {
            hitPosNum = 0;
        }
        else
        {
            for (int i = 1; i < cutNum; i++)
            {
                if (FoodAng > minAng && FoodAng <= minAng + ang)
                {
                    hitPosNum = i;
                    break;
                }
                minAng += ang;
            }

        }

    }

    //中間にする
    Vector3 CenterPos(Vector3 pos)
    {

        //二つのオブジェクトのX座標の中間をとる
        if (leftFoot.transform.position.x > rightFoot.transform.position.x)
        {
            pos.x = ((rightFoot.transform.position.x - leftFoot.transform.position.x) / 2) + leftFoot.transform.position.x;
        }
        else
        {
            pos.x = ((leftFoot.transform.position.x - rightFoot.transform.position.x) / 2) + rightFoot.transform.position.x;
        }

        //二つのオブジェクトのY座標の中間をとる
        if (leftFoot.transform.position.z > rightFoot.transform.position.z)
        {
            pos.z = ((rightFoot.transform.position.z - leftFoot.transform.position.z) / 2) + leftFoot.transform.position.z;
        }
        else
        {
            pos.z = ((leftFoot.transform.position.z - rightFoot.transform.position.z) / 2) + rightFoot.transform.position.z;
        }

        return pos;
    }
}
