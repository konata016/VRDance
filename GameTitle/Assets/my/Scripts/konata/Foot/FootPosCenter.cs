using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//両足の中間位ブロックを置き、足が地面に接触したときにブロックが、着地した足の方向を向く

public class FootPosCenter : MonoBehaviour
{
    public static int hitPosNum { get; set; }

    public GameObject leftFoot;
    public GameObject rightFoot;
    public Vector3 groundPos;

    //public int cutNum = 8;

    Quaternion foodQuaternion;
    float ang;


    // Start is called before the first frame update
    void Start()
    {
        ang = 360 / PlActionControl.FootCircleCutNum;
    }

    // Update is called once per frame
    void Update()
    {
        //両足の中間にポジションをとる
        transform.position = new Vector3(CenterPos(transform.position).x, StepDetermination.groundPosition.y, CenterPos(transform.position).z);
        
        //初めに地面についた足の方向を見る
        GroundJudge();

        //方向を記録する
        FootDirection();
    }

    //地面に接触したときに一番初めについた足の方向を見る
    void GroundJudge()
    {
        //片足が地面と接触したときに、両足の中央から見たときの方向をブロックに向かせる
        if (StepDetermination.isGroundTouch_R == StepDetermination.ISGROUNDTOUCH.Landing)
        {
                transform.rotation = Quaternion.LookRotation(Vector3.up, rightFoot.transform.position - transform.position);
        }
        if (StepDetermination.isGroundTouch_L == StepDetermination.ISGROUNDTOUCH.Landing)
        {
                transform.rotation = Quaternion.LookRotation(Vector3.up, leftFoot.transform.position - transform.position);
        }
    }

    //足の置いてある方向を決める
    void FootDirection()
    {
        foodQuaternion = transform.rotation;
        float FoodAng = foodQuaternion.eulerAngles.y;
        float minAng = ang / 2;

        //中心から見た足の角度を出す
        if (FoodAng <= minAng || FoodAng > minAng + ang * (PlActionControl.FootCircleCutNum - 1))
        {
            hitPosNum = 0;
        }
        else
        {
            for (int i = 1; i < PlActionControl.FootCircleCutNum; i++)
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
