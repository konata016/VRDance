using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleAttack : MonoBehaviour
{
    public GameObject[] pointPos = new GameObject[4];   //メッシュの頂点用
    public Material material;                           //作成したメッシュのマテリアル

    public GameObject beamObj;                          //メッシュ作成用頂点を結ぶオブジェクト
    public float speed = 3;                             //メッシュ作成用頂点を結ぶオブジェクトが伸びる速度

    public GameObject effectObj;
    bool onEffect;

    GameObject myMeshObj;                               //動的メッシュの器                                 
    float timer;

    class BeamData                                      //メッシュ作成用頂点を結ぶオブジェクトのデータ
    {
        public GameObject beamObjList;
        public float dis;
    }
    List<BeamData> beam = new List<BeamData>();

    public bool[] onStop;

    // Start is called before the first frame update
    void Start()
    {
        //動的メッシュ作成用オブジェクト準備
        myMeshObj = new GameObject("test");
        myMeshObj.AddComponent<MeshRenderer>();
        myMeshObj.AddComponent<MeshFilter>();
        myMeshObj.GetComponent<Renderer>().material = material;
        myMeshObj.transform.parent = transform;

        //特定のオブジェクト間を結ぶ棒状のオブジェクトの準備
        for (int i = 0; i < 6;i++) beam.Add(new BeamData());
        for (int i = 0; i < 3; i++)
        {
            beam[i].beamObjList = Instantiate(beamObj, pointPos[0].transform);
        }
        for (int i = 0; i < 2; i++)
        {
            beam[3 + i].beamObjList = Instantiate(beamObj, pointPos[pointPos.Length - 1].transform);
        }
        beam[5].beamObjList = Instantiate(beamObj, pointPos[1].transform);

        onStop = new bool[beam.Count];
    }

    // Update is called once per frame
    void Update()
    {
        Beam();
        BeamControl();
    }

    //特定のオブジェクトの方向を向き、そこまでの距離を求める
    void Beam()
    {
        for (int i = 0; i < 3; i++)
        {
            beam[i].beamObjList.transform.LookAt(pointPos[i + 1].transform);
            beam[i].dis = Vector3.Distance(pointPos[0].transform.position, pointPos[i + 1].transform.position);
        }
        for (int i = 0; i < 2; i++)
        {
            beam[3 + i].beamObjList.transform.LookAt(pointPos[i + 1].transform);
            beam[3 + i].dis = Vector3.Distance(pointPos[pointPos.Length - 1].transform.position, pointPos[i + 1].transform.position);
        }
        beam[5].beamObjList.transform.LookAt(pointPos[2].transform);
        beam[5].dis = Vector3.Distance(pointPos[1].transform.position, pointPos[2].transform.position);
    }

    void BeamControl()
    {
        //特定のオブジェクトまで準備した棒状のオブジェクトを伸ばす処理
        for (int i = 0; i < beam.Count; i++)
        {
            if (beam[i].beamObjList.transform.localScale.z < beam[i].dis * 2)
            {
                beam[i].beamObjList.transform.localScale = Siz(i, 1);
            }
            else
            {
                beam[i].beamObjList.transform.localScale = Siz(i, -1);  //行きすぎたら縮める
                onStop[i] = true;
            }
        }

        if (onStop[onStop.Length - 1])
        {
            //棒状のオブジェクトが特定のオブジェクトの位置まで達したら
            //特定のオブジェクト間を結ぶメッシュの作成
            Vector3[] posArray = new Vector3[pointPos.Length];
            for (int i = 0; i < pointPos.Length; i++) posArray[i] = pointPos[i].transform.position;
            myMeshObj.GetComponent<MeshFilter>().sharedMesh = MeshCreate(posArray);

            //ディゾルブマテリアルの操作
            if (timer < 1)
            {
                timer += Time.deltaTime;
                material.SetFloat("_Timer", timer);
            }
            else if (!onEffect)
            {
                GameObject obj = Instantiate(effectObj, TriangleCenterPos(posArray), new Quaternion());
                Destroy(obj, 1);
                onEffect = true;
            }
            else Destroy(transform.root.gameObject,0.1f);
        }

        //サイズの調整用
        Vector3 Siz(int count, int dir)
        {
            Vector3 siz = beam[count].beamObjList.transform.localScale;
            siz.z += speed * dir * Time.deltaTime;
            return siz;
        }

    }

    //三角錐の中心を出すやつ
    Vector3 TriangleCenterPos(Vector3[] pos)
    {
        Vector3[] v3 = new Vector3[4];
        v3[0] = (pos[0] + pos[1] + pos[2]) / 3;
        v3[1] = (pos[0] + pos[1] + pos[3]) / 3;
        v3[2] = (pos[0] + pos[2] + pos[3]) / 3;
        v3[3] = (pos[1] + pos[2] + pos[3]) / 3;

        return (v3[0] + v3[1] + v3[2] + v3[3]) / 4;
    }

    //動的メッシュの作成
    Mesh MeshCreate(Vector3[] pos)
    {
        var mesh = new Mesh();
        mesh.vertices = new Vector3[]   //メッシュの頂点座標
        {
            pos[0], pos[1], pos[2],     //今回は三角のパネル4面で構成されたメッシュが欲しかったので
            pos[0], pos[1], pos[3],     //3個区切りにしている
            pos[0], pos[2], pos[3],
            pos[1], pos[2], pos[3]
        };
        mesh.triangles = new int[]      //三角で構成されたメッシュ
        {
            0, 2, 1,                    //1と2が入れ替わっているのは外側に向けないとカメラに映らないため
            3, 4, 5,                    //個人的な解釈だとパネルの向き
            6, 8, 7,
            9, 10, 11,
        };
        mesh.uv = new Vector2[]        //UV展開、これをしないと一色のマテリアルは大丈夫だけど、複雑な模様だとちゃんと反映されない
        {                              //内部の数が大きければ多いほど模様が細かくなる
            new Vector2 (0.5f, 1f),new Vector2 (1f, 0), new Vector2 (0, 0), 
            new Vector2 (0.5f, 1f),new Vector2 (1f, 0), new Vector2 (0, 0),
            new Vector2 (0.5f, 1f),new Vector2 (1f, 0), new Vector2 (0, 0),
            new Vector2 (0.5f, 1f),new Vector2 (1f, 0), new Vector2 (0, 0),
        };

        mesh.RecalculateNormals();     //おまじない
        return mesh;
    }
}
