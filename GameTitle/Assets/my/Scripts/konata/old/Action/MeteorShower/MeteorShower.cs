using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShower : MonoBehaviour
{
    float timer;

    [System.Serializable]
    public class BeamRain
    {
        public GameObject effectObj;
        public GameObject rainSpawnPos;
        public GameObject breakPos;
        public float speed = 3;
        public float Interval = 1;
        public int spawnCount = 5;

        public int count;
        public bool onTriggerBreakObj;
        public Vector3[] randomPos;
        public List<GameObject> effectObjList = new List<GameObject>();
    }
    public BeamRain beamRain = new BeamRain();

    // Start is called before the first frame update
    void Start()
    {
        beamRain.speed = PlAttackManager.GetMeteorShower.speed;
        beamRain.Interval = PlAttackManager.GetMeteorShower.interval;
        beamRain.spawnCount = PlAttackManager.GetMeteorShower.spawnCount;


        StartAreaSpawnObj();
    }

    // Update is called once per frame
    void Update()
    {
        Attack1();
    }

    void StartAreaSpawnObj()
    {
        GameObject groupObj = new GameObject("Group");  //子オブジェクト回転用

        //リストにセット
        beamRain.randomPos = new Vector3[beamRain.spawnCount];
        beamRain.randomPos = RandomV3(beamRain.rainSpawnPos.transform.localScale, beamRain.spawnCount);

        for (int i = 0; i < beamRain.spawnCount; i++)
        {
            //サイズ分だけずらすやつ
            Vector3 v3 = (beamRain.rainSpawnPos.transform.lossyScale / 2) - beamRain.effectObj.transform.lossyScale;
            //枠の中に生成する
            Vector3 pos = beamRain.rainSpawnPos.transform.position - beamRain.randomPos[i] + v3;
            beamRain.effectObjList.Add(Instantiate(beamRain.effectObj, pos, new Quaternion()));
            //空のオブジェクトの子にする
            beamRain.effectObjList[i].transform.parent = groupObj.transform;
        }
        //空のオブジェクトをターゲットオブジェクトと同じ向きにする(回転)
        groupObj.transform.rotation = beamRain.rainSpawnPos.transform.rotation;
        //生成したオブジェクトをターゲットオブジェクトにし、空のオブジェクトを消す
        for (int i = 0; i < beamRain.spawnCount; i++)
        {
            beamRain.effectObjList[i].transform.parent = beamRain.rainSpawnPos.transform;
            beamRain.effectObjList[i].SetActive(false);
        }
        Destroy(groupObj);
    }

    void Attack1()
    {
        timer += Time.deltaTime;

        //移動開始時間のインターバルセット用
        if (!beamRain.onTriggerBreakObj)
        {
            if (timer > beamRain.Interval && beamRain.count < beamRain.spawnCount)
            {
                beamRain.count++;
                timer = 0;
            }
        }


        for(int i = 0; i < beamRain.count; i++)
        {
            //移動
            Vector3 pos = beamRain.effectObjList[i].transform.localPosition;
            pos.y += -(beamRain.speed * Time.deltaTime);
            beamRain.effectObjList[i].transform.localPosition = pos;

            //オブジェクトが一定位置に達したときにオブジェクトを破棄する
            if (beamRain.effectObjList[i].transform.position.y < beamRain.breakPos.transform.position.y)
            {
                GameObject obj = beamRain.effectObjList[i];
                beamRain.effectObjList.RemoveAt(i);
                Destroy(obj);
                beamRain.onTriggerBreakObj = true;
                if (beamRain.count > beamRain.effectObjList.Count) beamRain.count--;
            }
            else beamRain.effectObjList[i].SetActive(true);
        }
    }

    //オブジェクトの中にランダムでポジションを出す
    Vector3[] RandomV3(Vector3 pos, int randomNum)
    {
        Vector3[] randomTable = new Vector3[randomNum];
        List<Vector3> xyz = new List<Vector3>();

        //テーブル作成
        for (int x = 0; x < pos.x; x++)
        {
            for (int y = 0; y < pos.y; y++)
            {
                for (int z = 0; z < pos.z; z++)
                {
                    xyz.Add(new Vector3(x, y, z));
                }
            }
        }

        //作成したテーブルの中からランダムでピックアップ
        for(int i = 0; i < randomNum; i++)
        {
            int posNum = Random.Range(0, xyz.Count);

            randomTable[i] = xyz[posNum];
            xyz.RemoveAt(posNum);
        }

        return randomTable;
    }


}
