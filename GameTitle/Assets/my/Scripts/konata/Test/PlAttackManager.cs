using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlAttackManager : MonoBehaviour
{
    bool isAttack;

    [System.Serializable]
    public class RollSwordParameter
    {
        public GameObject swordObj;
        public float rollSpeed = 25;
        public float speed = 5;
        public int swordCount = 4;
        public float radius = 50;

        [HideInInspector] public Vector3 targetPos;
        [HideInInspector] public List<GameObject> swordList = new List<GameObject>();
    }
    public RollSwordParameter rollSword = new RollSwordParameter();

    [System.Serializable]
    public class MeteorShowerParameter
    {
        public GameObject showerObj;
        public float speed = 10;
        public float interval = 0.1f;
        public int spawnCount = 50;

        [HideInInspector] public Vector3 targetPos;
    }
    public MeteorShowerParameter meteorShower = new MeteorShowerParameter();

    static PlAttackManager PlAttackManager_ = new PlAttackManager();

    // Start is called before the first frame update
    void Start()
    {
        //rollSword.swordList = new List<GameObject>
        //    (
        //    InstantCirclePos(rollSword.swordCount, rollSword.swordObj, rollSword.radius, false)
        //    );

    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttack)
        {
            MeteorShower();

            if (Music.IsPlaying && Music.IsJustChangedBeat()) isAttack = false;
        }

    }

    void RollSword()
    {
        for (int i = 0; i < rollSword.swordList.Count; i++)
        {
            if (isAttack)
            {
                rollSword.swordList[i].transform.Rotate(rollSword.rollSpeed, 0, 0);
            }
            else
            {
                rollSword.swordList[i].transform.position = Vector3.MoveTowards
                    (
                    rollSword.swordList[i].transform.position, rollSword.targetPos, rollSword.speed * Time.deltaTime
                    );
            }
        }
    }

    void MeteorShower()
    {
        if (Music.IsPlaying && Music.IsJustChangedBeat())
        {
            GameObject obj = Instantiate(meteorShower.showerObj, meteorShower.targetPos, new Quaternion());
        }
    }





    //半円上にオブジェクトを生成する
    List<GameObject> InstantCirclePos(int count, GameObject obj, float radius, bool onAxisZ)
    {
        List<GameObject> objList = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            //半円上に生成する
            Vector3 v3 = CirclePos(count - 1, radius, i, Vector3.zero);
            objList.Add(Instantiate(obj, v3, new Quaternion()));
        }
        return objList;

        //半円上のポジションを取得
        Vector3 CirclePos(int maxNum, float rad, int currentNum, Vector3 pos)
        {
            if (maxNum != 0)
            {
                //きれいに半円状にに出すやつ
                float r = (180 / maxNum) * currentNum;

                float angle = r * Mathf.Deg2Rad;
                pos.x = rad * Mathf.Cos(angle);
                if(!onAxisZ) pos.y = rad * Mathf.Sin(angle);
                else pos.z = rad * Mathf.Sin(angle);
            }
            else
            {
                pos.x = 0;
                if (!onAxisZ) pos.y = rad;
                else pos.z = rad;
            }

            return pos;
        }
    }

    public static MeteorShowerParameter GetMeteorShower
    {
        get { return PlAttackManager_.meteorShower; }
    }
}
