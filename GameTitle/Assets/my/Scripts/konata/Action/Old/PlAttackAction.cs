using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//バグあるかも？デバッグ確認順位高い
//1テンポごとに発動させる処理の関数を作った方が良い！

public class PlAttackAction : MonoBehaviour
{
    //SE読み込みオブジェクト
    private GameObject seObj;

    float timer;
    public string targetName;

    //攻撃入力時の受け渡し用
    public static int rollSwordCount { get; set; }

    //攻撃の種類
    //enum ACTIONTYPE { Attack, Healing, Support, Through }
    List<PlActionControl.ACTIONTYPE> actionTypeList = new List<PlActionControl.ACTIONTYPE>();

    #region クラス(くるくると回ってからターゲットに向かって放たれる)
    [System.Serializable]
    public class RollSwordParameter
    {
        public GameObject swordObj;
        public float rollSpeed = 25;
        public float speed = 5;
        public float waitTime = 0.5f;
        public int swordCount = 4;
        public float radius = 50;

        [HideInInspector] public Vector3 target;
        [HideInInspector] public List<GameObject> swordList = new List<GameObject>();
        [HideInInspector] public bool onSword = true;


        //タイミングを合わせるほうに使う
        [HideInInspector] public bool isStart;
        [HideInInspector] public int timingCount = 0;
    }
    #endregion
    public RollSwordParameter RSP = new RollSwordParameter();

    #region クラス途中(王の宝物庫)
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
    #endregion
    public GateOfBabylonParameter GOBP = new GateOfBabylonParameter();

    #region クラス(回復のやつ)
    [System.Serializable]
    public class HealEffect
    {
        public GameObject materialObj;

        [HideInInspector] public int count;
        [HideInInspector] public List<GameObject> healList = new List<GameObject>();
    }
    #endregion
    public HealEffect healEffect = new HealEffect();

    #region クラス(サポートのやつ)
    [System.Serializable]
    public class SupportEffect
    {
        public GameObject materialObj;

        [HideInInspector] public int count;
        [HideInInspector] public List<GameObject> supportList = new List<GameObject>();
    }
    #endregion
    public SupportEffect supportEffect = new SupportEffect();

    // Start is called before the first frame update
    void Start()
    {
        seObj = GameObject.Find("SE");

        //Debug.Log(PlActionControl.melodySaveList.Count);

        //メロデイーリストからフラグを作成
        actionTypeList = new List<PlActionControl.ACTIONTYPE>(PlActionControl.melodySaveList);

        //生成時に剣の生成数を決める
        RSP.swordCount = rollSwordCount;

        //地面の位置から計算
        transform.position = FootJudgment_Right.groundPosition;

        //Debug.Log(targetName);

        //ターゲットを決める
        RSP.target = GameObject.Find("EnemyPoint").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Heal();
        Support();
        RollSword2();
    }

    //RollSwordの2～3の音に合わせて飛ぶ剣の"生成""回転""カウントの進め"までのテンプレ
    void RollSwordTemplate()
    {
        if (!RSP.onSword)
        {
            //オブジェクトの生成
            RSP.swordList = new List<GameObject>(InstantCirclePos(RSP.swordCount, RSP.swordObj, RSP.radius));
            //次から生成しないようにする
            RSP.onSword = true;
        }

        //回した後に敵の方を向く
        for (int i = 0; i < RSP.swordList.Count; i++)
        {
            if (!RSP.isStart) RSP.swordList[i].transform.Rotate(RSP.rollSpeed, 0, 0);//回す処理
            else RSP.swordList[i].transform.LookAt(RSP.target);                     //敵の方向を向く
        }

        //１拍後にカウントを進める
        if (Music.IsPlaying && Music.IsJustChangedBeat())
        {
            if (RSP.timingCount < actionTypeList.Count)
            {
                SE(RSP.timingCount);
                RSP.timingCount++;
            }
            RSP.isStart = true;
        }
    }

    //現在は使っていない
    #region くるくると回ってからターゲットに向かって放たれる
    public void RollSword()
    {
        float wait = 0.5f;

        //遅延
        timer += 1.0f * Time.deltaTime;

        if (!RSP.onSword)
        {
            //オブジェクトの生成
            RSP.swordList = new List<GameObject>(InstantCirclePos(RSP.swordCount, RSP.swordObj, RSP.radius));
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
                sword.transform.LookAt(RSP.target);

                //ターゲットを見た後数秒後にターゲットに向かって剣が飛んでいく
                if (timer > RSP.waitTime + wait)
                {
                    sword.transform.position = Vector3.MoveTowards(sword.transform.position, RSP.target, RSP.speed * RSP.speed * Time.deltaTime);
                }
            }
            else
            {
                //初めの回転演出
                sword.transform.Rotate(RSP.rollSpeed, 0, 0);
            }
        }
    }
    #endregion

    //くるくると回ってからターゲットに向かって放たれる
    #region テンポに合わせて剣が飛んでいくバージョン
    public void RollSword2()
    {
        //遅延
        timer += 1.0f * Time.deltaTime;

        RollSwordTemplate();

        //1拍のタイミングで動き始める
        if (RSP.isStart)
        {
            //swordListの配列用
            int count = 0;

            for (int i = 0; i < RSP.timingCount; i++)
            {
                //1小節の中の攻撃を調べる
                if (actionTypeList[i] == PlActionControl.ACTIONTYPE.Attack)
                {

                    //敵に向かって剣が飛んでいく
                    RSP.swordList[count].transform.position =
                        Vector3.MoveTowards(RSP.swordList[count].transform.position, RSP.target, RSP.speed * RSP.speed * Time.deltaTime);

                    count++;
                }
            }
        }
    }
    #endregion

    public void RollSword3()
    {
        //遅延
        timer += 1.0f * Time.deltaTime;

        RollSwordTemplate();

        if (RSP.isStart)
        {

        }
    }

    #region 回復
    public void Heal()
    {

        if (healEffect.count < actionTypeList.Count)
        {
            //テンポごとの判定
            if (Music.IsPlaying && Music.IsJustChangedBeat())
            {
                //1小節の中の回復を調べる
                if (actionTypeList[healEffect.count] == PlActionControl.ACTIONTYPE.Healing)
                {
                    //回復エフェクトのリストの作成
                    healEffect.healList.Add(Instantiate(healEffect.materialObj, transform));
                }
                healEffect.count++;
            }
        }

    }
    #endregion

    void Support()
    {
        if (supportEffect.count < actionTypeList.Count)
        {
            //テンポごとの判定
            if (Music.IsPlaying && Music.IsJustChangedBeat())
            {
                //1小節の中の回復を調べる
                if (actionTypeList[supportEffect.count] == PlActionControl.ACTIONTYPE.Support)
                {
                    //回復エフェクトのリストの作成
                    supportEffect.supportList.Add(Instantiate(supportEffect.materialObj, transform));
                }
                supportEffect.count++;
            }
        }
    }

    //半円上にオブジェクトを生成する
    List<GameObject> InstantCirclePos(int count, GameObject obj, float radius)
    {
        List<GameObject> objList = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            //半円上に生成する
            Vector3 v3 = CirclePos(count - 1,radius, i, Vector3.zero);
            objList.Add(Instantiate(obj, v3, new Quaternion()));
        }
        return objList;

        //半円上のポジションを取得
        Vector3 CirclePos(int maxNum,float rad, int currentNum, Vector3 pos)
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

    #region 効果音(剣の処理)
    void SE(int count)
    {
        //インデックス外になる
        if (count < actionTypeList.Count)
            if (actionTypeList[count] == PlActionControl.ACTIONTYPE.Attack)
            {
                MainGame_SE mainGame_SE = seObj.GetComponent<MainGame_SE>();
                mainGame_SE.SwordSound();// 効果音＿剣
            }
    }
    #endregion
}
