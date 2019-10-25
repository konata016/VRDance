using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCheck : MonoBehaviour
{
    [System.Serializable]
    public class Foot
    {
        public GameObject Right;
        public GameObject Left;
    }
    public Foot footCheck = new Foot();                             //足を監視するオブジェクト
    public Foot foot= new Foot();                                   //リアル足オブジェクト

    public int footCircleCutNum = 4;                                //割った数

    enum FOOT_RL { R, L, RL }                                       //左右を記す
    enum FOOT_POS { Down, Left, Up, Right }                         //足の間から見て片足の向きを記す
    public enum PAUSE_ACTION { Side, Vertical, Cross, Miss }        //攻撃方法を記す
    public static PAUSE_ACTION actionPause = new PAUSE_ACTION();    //攻撃データを渡すよう

    public bool onBothFeet;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //足の間にオブジェクトを置く
        transform.position = CenterPos(footCheck.Right.transform.position,
                                       footCheck.Left.transform.position,
                                       StepDetermination.groundPosition.z);
        //足の方向を監視させる
        GroundJudge();

        //どんなポーズがされたかを見る
        actionPause = PauseAction();
    }

    //対応する足の方向をずっと監視している
    void GroundJudge()
    {
        footCheck.Right.transform.rotation = Quaternion.LookRotation(Vector3.up, foot.Right.transform.position - footCheck.Right.transform.position);
        footCheck.Left.transform.rotation = Quaternion.LookRotation(Vector3.up, foot.Left.transform.position - footCheck.Left.transform.position);
    }

    //踏んだ時にどんなポーズをしているかを出す
    PAUSE_ACTION PauseAction()
    {
        FOOT_POS right = (FOOT_POS)AngFromCircleCutNum(foot.Right.transform.rotation.eulerAngles.y, footCircleCutNum);
        FOOT_POS left = (FOOT_POS)AngFromCircleCutNum(foot.Left.transform.rotation.eulerAngles.y, footCircleCutNum);

        if (OnTriggerArrayLR()[(int)FOOT_RL.R] || OnTriggerArrayLR()[(int)FOOT_RL.L])
        {
            if (right == FOOT_POS.Right && left == FOOT_POS.Left) return PAUSE_ACTION.Side;
            else if (right == FOOT_POS.Left && left == FOOT_POS.Right) return PAUSE_ACTION.Cross;
            else if (right == FOOT_POS.Up && left == FOOT_POS.Down || right == FOOT_POS.Down && left == FOOT_POS.Up) return PAUSE_ACTION.Vertical;
            else return PAUSE_ACTION.Miss;
        }
        else return PAUSE_ACTION.Miss;
    }

    //向きから対応した割った数の値を返す
    int AngFromCircleCutNum(float direction, int CircleCutNum)
    {
        float ang = 360 / CircleCutNum;
        float minAng = ang / 2;

        //中心から見た角度を出す
        if (direction <= minAng || direction > minAng + ang * (CircleCutNum - 1))
        {
            return 0;
        }
        else
        {
            int count = 0;
            for (count = 1; count < CircleCutNum; count++)
            {
                if (direction > minAng && direction <= minAng + ang) break;
                minAng += ang;
            }
            return count;
        }
    }

    //2つのオブジェクトの座標の中間をとる
    Vector3 CenterPos(Vector3 rightPos, Vector3 leftPos, float groundPos)
    {
        return new Vector3(Center(leftPos.x, rightPos.x), groundPos, Center(leftPos.z, rightPos.z));

        //2つの数値の中間の数値を出す
        float Center(float left, float right)
        {
            if (left > right) return ((right - left) / 2) + left;
            else return ((left - right) / 2) + right;
        }
    }

    //両足で着地した場合の式を書くこと
    bool OnBothFeet()
    {
        return false;
    }

    //トリガーの処理をここに入れる
    bool[] OnTriggerArrayLR()
    {
        bool bR = Input.GetKeyDown(KeyCode.RightArrow);
        bool bL = Input.GetKeyDown(KeyCode.LeftArrow);
        return new bool[(int)FOOT_RL.RL] { bR, bL };
    }
}
