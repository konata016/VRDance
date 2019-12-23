using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlGroundNotes : MonoBehaviour
{
    public GameObject notesObj;
    public float speed = 10;
    public Vector3 startPos;
    Vector3 endPos;


    public List<GameObject> objList = new List<GameObject>();

    int stepDataCount;
    public float fixTime;                  //音に合うタイミングにする用

    // Start is called before the first frame update
    void Start()
    {
        //地面の調整
        transform.position = GameDirector.GetGroundPos;

        //短縮用
        startPos = transform.GetChild(0).gameObject.transform.localPosition;
        endPos = transform.GetChild(1).gameObject.transform.localPosition;

        //差分をなくすよう
        fixTime = (startPos.z - GameDirector.GetGroundPos.z) / speed;
    }

    // Update is called once per frame
    void Update()
    {
        //時間になったらノーツを生成する
        if (StepData.GetSoundPlayTime >= StepData.GetStepData[stepDataCount].musicScore - fixTime)
        {
            if (StepData.GetStepData[stepDataCount].plStep != StepData.PL_STEP_TIMING.Nothing)
            {
                objList.Add(Instantiate(notesObj, startPos, new Quaternion()));
            }
            stepDataCount++;
        }

        //移動
        for(int i = 0; i < objList.Count; i++)
        {
            Vector3 pos = objList[i].transform.position;
            pos.z -= speed * Time.deltaTime;
            objList[i].transform.position = pos;
        }

        //オブジェクトの消去
        if (objList.Count != 0)
        {
            if (objList[0].transform.position.z <= endPos.z)
            {
                Destroy(objList[0]);
                objList.RemoveAt(0);
            }
        }
    }
}
