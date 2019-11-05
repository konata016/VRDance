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
        public GameObject meteorShowerObj;

        public float speed = 10;
        public float interval = 0.1f;
        public int spawnCount = 50;

        [HideInInspector] public Vector3 targetPos;
    }
    public MeteorShowerParameter meteorShower = new MeteorShowerParameter();

    [System.Serializable]
    public class TriangleParameter
    {
        public GameObject TriangleObj;

        public float beaconSpeed = 30;

        public Material myMeshMaterial;
        public GameObject beamObj;
        public float beamSpeed = 35;
        public GameObject explosionParticle;

        public float changefixPosY = 1;
        public float posChangeSpeed = 15;
        public int randomSeed = 13;
    }
    public TriangleParameter triangle = new TriangleParameter();

    static PlAttackManager PlAttackManager_ = new PlAttackManager();

    //Startより前に呼び出される
    void Awake()
    {
        PlAttackManager_.triangle = triangle;
        PlAttackManager_.meteorShower = meteorShower;
    }

    // Start is called before the first frame update
    void Start()
    {
        //剣を生成
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
            if (Music.IsPlaying && Music.IsJustChangedBeat())
            {
                Triangle();
                //MeteorShower();
                isAttack = true;
            }
        }

    }

    void RollSword()
    {
        for (int i = 0; i < rollSword.swordList.Count; i++)
        {
            if (isAttack)
            {
                //回す処理
                rollSword.swordList[i].transform.Rotate(rollSword.rollSpeed, 0, 0);
            }
            else
            {
                //飛んでいく処理
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
            GameObject obj = Instantiate(meteorShower.meteorShowerObj, meteorShower.targetPos, new Quaternion());
        }
    }

    void Triangle()
    {
        GameObject obj = Instantiate(triangle.TriangleObj, transform);
        if (Music.IsPlaying && Music.IsJustChangedBeat())
        {
            //GameObject obj = Instantiate(triangle.TriangleObj, transform);
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

    public static TriangleParameter GetTriangle
    {
        get { return PlAttackManager_.triangle; }
    }
}
