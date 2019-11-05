using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleGenerator : MonoBehaviour
{
    float timer;
    List<Vector3> divideSiz = new List<Vector3>();

    public GameObject obj;                                         //生成するオブジェクト
    public List<GameObject> objList = new List<GameObject>();      //生成したオブジェクトリスト
    public int count = 50;                                         //生成したオブジェクトの生成数
    public float range = 5;                                        //半径
    public float siz = 0.1f;                                       //生成したオブジェクトのサイズ

    [System.Serializable]
    class RangeSizParameter     //一定のオブジェクトの大きさをだんだんにする
    {
        public int divide;      //キーオブジェクトの数
        public float sizMin;    //だんだんにした時の一番小さいオブジェクトのサイズ
    }
    [SerializeField] RangeSizParameter rangeSiz = new RangeSizParameter();

    [System.Serializable]
    class MoveListParameter             //生成したオブジェクトを回転させる(リストの入れ替えをする)
    {
        public float interval = 0.1f;   //次回転するまでの時間
        public int dir = 1;             //回転する方向(1,-1)
    }
    [SerializeField] MoveListParameter moveList = new MoveListParameter();

    // Start is called before the first frame update
    void Start()
    {
        StartScript();
    }

    // Update is called once per frame
    void Update()
    {
        //回転させる式
        if (rangeSiz.sizMin != 0)
        {
            divideSiz = new List<Vector3>(MoveList(divideSiz, moveList.interval, moveList.dir));
            for (int i = 0; i < objList.Count; i++)
            {
                objList[i].transform.localScale = divideSiz[i];
            }
        }
    }

    void StartScript()
    {
        //半円上にオブジェクトを生成
        objList = new List<GameObject>(InstantCirclePos(count, obj, range));
        //最小値がゼロでないときオブジェトのサイズをだんだんにする
        if (rangeSiz.sizMin != 0) divideSiz = SizChange(count, rangeSiz.divide, new Vector2(siz, rangeSiz.sizMin));

        for (int i = 0; i < objList.Count; i++)
        {
            //子にする
            objList[i].transform.parent = transform;
            //最小値がゼロの時に、指定したサイズにする
            if (rangeSiz.sizMin == 0) objList[i].transform.localScale = new Vector3(siz, 1, siz);
            else objList[i].transform.localScale = divideSiz[i];
        }
    }

    //回転させる(リストの入れ替え)
    List<Vector3> MoveList(List<Vector3> sizList, float interval, int dir)
    {
        timer += Time.deltaTime;
        if (timer > interval)
        {
            if (dir == 1)
            {
                Vector3 siz = sizList[sizList.Count - 1];
                sizList.RemoveAt(sizList.Count - 1);
                sizList.Insert(0, siz);
                timer = 0;
            }
            else
            {
                Vector3 siz = sizList[0];
                sizList.RemoveAt(0);
                sizList.Add(siz);
                timer = 0;
            }
        }

        return sizList;
    }

    //オブジェクトのサイズをだんだんにする
    List<Vector3> SizChange(int listSiz, int divide, Vector2 sizMaxMin)
    {
        List<Vector3> siz = new List<Vector3>();
        int percentage = listSiz / divide;
        //指定した割合に従ってだんだんを生成
        for (int i = 0; i < divide; i++)
        {
            for (int f = 0; f < percentage; f++)
            {
                float oneSiz = (sizMaxMin.x - sizMaxMin.y) / percentage;
                siz.Add(new Vector3(oneSiz * f, 1, oneSiz * f));
            }
        }

        return siz;
    }

    //半円上にオブジェクトを生成する
    List<GameObject> InstantCirclePos(int count, GameObject obj, float radius)
    {
        List<GameObject> objList = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            //半円上に生成する
            Vector3 v3 = CirclePos(count, radius, i, Vector3.zero);
            Quaternion q = Quaternion.LookRotation(Vector3.up, transform.position - v3);
            objList.Add(Instantiate(obj, v3, q * Quaternion.AngleAxis(90, Vector3.right)));
        }
        return objList;

        //半円上のポジションを取得
        Vector3 CirclePos(int maxNum, float rad, int currentNum, Vector3 pos)
        {
            if (maxNum != 0)
            {
                //きれいに半円状にに出すやつ
                float r = (360 / maxNum) * currentNum;

                float angle = r * Mathf.Deg2Rad;
                pos.x = rad * Mathf.Cos(angle);
                pos.z = rad * Mathf.Sin(angle);
            }
            else
            {
                pos.x = 0;
                pos.z = rad;
            }

            return pos;
        }
    }
}
