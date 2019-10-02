using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlAttackAction : MonoBehaviour
{

    float timer;
    public GameObject centerTarget;

    //攻撃入力時の受け渡し用
    public static int rollSwordCount { get; set; }


    //くるくると回ってからターゲットに向かって放たれる
    [System.Serializable]
    public class RollSwordParameter
    {
        public GameObject target;
        public GameObject swordObj;
        public float rollSpeed = 25;
        public float speed = 5;
        public float waitTime = 0.5f;
        public int swordCount = 4;
        public float radius = 50;

        [Header("使わないやつ")]
        public List<GameObject> swordList = new List<GameObject>();
        public bool onSword = true;
    }
    public RollSwordParameter RSP = new RollSwordParameter();


    //王の宝物庫
    [System.Serializable]
    public class GateOfBabylonParameter
    {
        public GameObject target;
        public GameObject gateObj;
        public GameObject swordObj;
        public int swordCount;
        public float speed;

        public List<GameObject> gate = new List<GameObject>();
        public List<GameObject> sword = new List<GameObject>();

        public bool onGate;
        public bool onSword;
    }
    public GateOfBabylonParameter GOBP = new GateOfBabylonParameter();

    // Start is called before the first frame update
    void Start()
    {
        //生成時に剣の生成数を決める
        RSP.swordCount = rollSwordCount;

        transform.position = JumpStart.groundPosition;
    }

    // Update is called once per frame
    void Update()
    {

        RollSword();

    }

    //途中
    void GateOfBabylon()
    {
        //遅延
        timer += 1.0f * Time.deltaTime;

        if (!GOBP.onGate)
        {
            for (int i = 0; i < RSP.swordCount; i++)
            {
                //距離の変数を用意すること
                Vector3 v3 = CirclePos(GOBP.swordCount, 50, i, Vector3.zero);
                GOBP.gate.Add(Instantiate(GOBP.swordObj, v3, new Quaternion()));
            }

            //無限生成防ぐやつ
            GOBP.onGate = true;
        }
    }

    //くるくると回ってからターゲットに向かって放たれる
    public void RollSword()
    {
        float wait = 0.5f;

        //遅延
        timer += 1.0f * Time.deltaTime;

        if (!RSP.onSword)
        {
            for (int i = 0; i < RSP.swordCount; i++)
            {
                //半円上に剣を生成する
                Vector3 v3 = CirclePos(RSP.swordCount - 1, RSP.radius, i, Vector3.zero);
                RSP.swordList.Add(Instantiate(RSP.swordObj, v3, new Quaternion()));
            }
            //次から生成しないようにする
            RSP.onSword = true;
        }

        //生成した数分だけ操作する
        foreach (GameObject sword in RSP.swordList)
        {
            wait += 0.2f;

            //回転して数秒立つとターゲットの方を見る
            if (timer > RSP.waitTime)
            {
                sword.transform.LookAt(RSP.target.transform);

                //ターゲットを見た後数秒後にターゲットに向かって剣が飛んでいく
                if (timer > RSP.waitTime + wait)
                {
                    sword.transform.position = Vector3.MoveTowards(sword.transform.position, RSP.target.transform.position, RSP.speed * RSP.speed * Time.deltaTime);
                }
            }
            else
            {
                //初めの回転演出
                sword.transform.Rotate(RSP.rollSpeed, 0, 0);
            }
        }
    }


    //配置用
    Vector3 CirclePos(int count, float radius, int swordNum, Vector3 pos)
    {
        if (count != 0)
        {
            //きれいに半円状にに出すやつ
            float r = (180 / count) * swordNum;

            float angle = r * Mathf.Deg2Rad;
            pos.x = radius * Mathf.Cos(angle);
            pos.y = radius * Mathf.Sin(angle);
        }
        else
        {
            pos.x = 0;
            pos.y = radius;
        }

        return pos;
    }

}
