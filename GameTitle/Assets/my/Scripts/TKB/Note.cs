using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Note : MonoBehaviour
{
    public float generateTime;
    protected float reachTime;
    protected Color[] mColor;
    protected Vector3 position;
    protected Vector3 moveVector;
    protected GameObject note;

    protected GameObject[] noteObj;
    //protected int colNum = 3;

    public Note(float reachTime, Vector3 position, GameObject note)
    {
        this.generateTime = reachTime;
        this.position = position;
        this.note = note;
        this.note.GetComponent<GameObject>();
        mColor = new Color[40];
        for (int i = 0; i < 40; i++)
        {
            mColor[i] = new Color(59.0f, 199.0f, 229.0f, 0.0f);
        }

        //noteObj = new GameObject[colNum];
    }

    public abstract bool NoteMove(int pos);
    public abstract void NoteGenerate(GameObject obj, Vector3 pos);
}

public class WideWaveNote : Note
{
    private float widePos = 0;
    private const float animTime = 3.0f;
    private const float noteSpeed = 10.0f;
    float[] checkTime = new float[40];

    int colNum = 40;
    int wNum = 0;

    public WideWaveNote(float reachTime, Vector3 position, GameObject note) : base(reachTime, position, note)
    {
        generateTime = reachTime;
        moveVector = new Vector3(0, 0, 1);
        noteObj = new GameObject[colNum];
    }

    public override bool NoteMove(int pos)
    {
        for (int i = 0; i < colNum; i++)
        {
            if (noteObj[i] != null)
            {
                if (noteObj[i].transform.position.z < -2.0f)
                {
                    Destroy(noteObj[i]);
                }
                else
                {
                    checkTime[0] += Time.deltaTime;
                    noteObj[i].transform.position -= new Vector3(0, 0, Time.deltaTime * noteSpeed);
                }
            }
        }
        return true;
    }

    public override void NoteGenerate(GameObject colli, Vector3 pos)
    {
        if (noteObj[wNum] == null)
        {
            checkTime[0] = 0;
            noteObj[wNum] = Instantiate(colli, new Vector3(pos.x, StepDetermination.groundPosition.y, pos.z), Quaternion.identity);
            wNum++;
        }
        if (wNum >= colNum)
        {
            wNum = 0;
        }
    }
}

    public class VerticalWaveNote : Note
{
    //private float vertPos = 0;
    private const float animTime = 3.0f;
    private const float noteSpeed = 10.0f;
    float[] checkTime = new float[40];

    int colNum = 40;
    int vNum = 0;

    public VerticalWaveNote(float reachTime, Vector3 position, GameObject note) 
        : base(reachTime, position, note)
    {
        generateTime = reachTime;
        moveVector = new Vector3(0, 0, 1);
        noteObj = new GameObject[colNum];
    }

    public override bool NoteMove(int pos)
    {
        for (int i = 0; i < colNum; i++)
        {
            if (noteObj[i] != null)
            {
                if (noteObj[i].transform.position.z < -2.0f)
                {                 
                    Destroy(noteObj[i]);
                }
                else
                {
                    checkTime[0] += Time.deltaTime;
                    noteObj[i].transform.position -= new Vector3(0, 0, Time.deltaTime * noteSpeed);                
                }
            }
        }
        return true;
    }

    public override void NoteGenerate(GameObject colli, Vector3 pos)
    {
        if (noteObj[vNum] == null)
        {
            checkTime[0] = 0;
            noteObj[vNum] = Instantiate(colli, 
                                        new Vector3(pos.x, StepDetermination.groundPosition.y, pos.z),
                                        Quaternion.identity);
            vNum++;
        }
        if (vNum >= colNum)
        {
            vNum = 0;
        }
    }
}

public class PunchNote : Note
{
    private const float animTime = 2.0f;

    public PunchNote(float reachTime, Vector3 position, GameObject note) : base(reachTime, position, note)
    {
        generateTime = this.reachTime - animTime;
    }

    public override bool NoteMove(int pos)
    {
        return false;
    }

    public override void NoteGenerate(GameObject colli, Vector3 pos)
    {

    }
}

public class LaserNote : Note
{
    private const float animTime = 3.0f;

    GameObject LaserTraces; //レーザー痕プレハブ

    List<GameObject> Traces = new List<GameObject>(); //生成プレハブ保存所

    LineRenderer lineRenderer;
    Vector3 hitPos;　        //レーザーヒット座標
    Vector3 tmpPos;　        //ヒット座標記憶用
    Vector3 targetPos;       //レーザーの目標座標
    float atkPosx;      //攻撃座標X

    private Vector3 enemyHeadPos; //敵頭位置
    private Vector3 laserStartPos; //レーザーの初期照射位置

    private float lazerDistance = 20.0f;
    private float lazerStartPointDistance = 0.15f;

    private float laserSpeed;
    private float speedAcceleration;
    private Vector3 setVector;
    public float setSpeed = 10.0f;

    public float lineWidth = 1.0f;    //レーザーの太さ
    public float tracesSpace = 0.06f; //痕の間隔
    public float laserDelay = 1.0f;   //レーザーが動き始めるタイミング

    public LaserNote(float reachTime, Vector3 position, GameObject note, GameObject trace) 
        : base(reachTime, position, note)
    {
        generateTime = reachTime;
        LaserTraces = trace;
    }

    public override bool NoteMove(int pos)
    {
        OnRay();
        return false;
    }

    public override void NoteGenerate(GameObject laser, Vector3 pos)
    {
        Instantiate(laser, new Vector3(0, 0, 0), Quaternion.identity);

        laserSpeed = 0f;
        speedAcceleration = 0f;
        enemyHeadPos = new Vector3(0.0f, 4.0f + StepDetermination.groundPosition.y, 20.0f);
        laserStartPos = new Vector3(10.0f, 0.5f + StepDetermination.groundPosition.y, 0.5f);
        atkPosx = laserStartPos.x;

        lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        targetPos = enemyHeadPos;
        tmpPos = targetPos;

        setVector = laserStartPos - enemyHeadPos;
        Vector3.Normalize(setVector);
    }

    float RowCutAccelerationCal(float x)
    {
        float y = x * x - laserDelay;

        if (y < 0) return 0;

        return y;
    }

    void OnRay()
    {
        Vector3 rayStartPosition = enemyHeadPos;
        RaycastHit hit;

        //if (Input.GetKey(KeyCode.A)) atkPosx -= 0.2f;       
        //if (Input.GetKey(KeyCode.D)) atkPosx += 0.2f;

        if (targetPos.x < laserStartPos.x)
        {
            targetPos += setVector * Time.deltaTime * setSpeed;
        }
        else if(atkPosx >= -laserStartPos.x)
        {
            speedAcceleration += Time.deltaTime;
            laserSpeed = RowCutAccelerationCal(speedAcceleration);
            atkPosx -= laserSpeed;

            targetPos = new Vector3(atkPosx, 0.5f + StepDetermination.groundPosition.y, 0.5f);
        }
        else
        {
            targetPos -= new Vector3(-setVector.x, setVector.y, setVector.z) * Time.deltaTime * setSpeed;
        }

        Ray ray = new Ray(rayStartPosition, -(enemyHeadPos - targetPos));

        lineRenderer.SetPosition(0, rayStartPosition);

        if (Physics.Raycast(ray, out hit, lazerDistance))
        {
            hitPos = hit.point;
            lineRenderer.SetPosition(1, hitPos);

            Vector3 pos = hitPos + (hit.normal * 0.01f);
            if (tmpPos.x - tracesSpace > hitPos.x)
            {
                tmpPos = pos;
                //ヒットしたオブジェクトの法線方向に回転
                Quaternion rot = Quaternion.FromToRotation(-1 * LaserTraces.transform.forward, hit.normal);

                Traces.Add(Instantiate(LaserTraces, pos, rot));
            }
        }
        else
        {
            lineRenderer.SetPosition(1, targetPos - (enemyHeadPos - targetPos));
        }

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 0.1f);
    }
}

public class ThrowCubeNote : Note
{
    private float cubePos = 0;
    private const float animTime = 3.0f;
    private const float noteSpeed = 12.0f;
    private float[] upSpeed;
    private Vector3[] moveVec;

    float ct = 0;

    int cNum = 0;
    int cubeNum = 40;
    float[] target = new float[40]; //飛んでくる場所へ補正

    public ThrowCubeNote(float reachTime, Vector3 position, GameObject note) : base(reachTime, position, note)
    {
        generateTime = reachTime;
        moveVector = note.transform.position.normalized;
        noteObj = new GameObject[cubeNum];
        moveVec = new Vector3[cubeNum];
        upSpeed = new float[cubeNum];
    }

    public override bool NoteMove(int pos)
    {
        for(int i = 0; i < cubeNum; i++)
        {
            if (noteObj[i] != null)
            {
                if (upSpeed[i] >= 0.0001f)
                {
                    noteObj[i].transform.position += new Vector3(0, upSpeed[i], 0);
                    upSpeed[i] *= 0.9f;
                    moveVec[i] = noteObj[i].transform.position 
                                 - new Vector3(target[i], JumpStart.groundPosition.y + 0.5f, 0);
                    moveVec[i] = moveVec[i].normalized;

                    if (upSpeed[i] <= 0.01f)
                    {
                        mColor[i].r += 3.0f;
                        mColor[i].g -= 6.0f;
                        mColor[i].b -= 6.0f;
                    }

                    //noteObj[i].GetComponent<Renderer>().material.color = mColor[i];

                    noteObj[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", mColor[i] * 0.005f);
                }
                else
                {
                    if (noteObj[i].transform.position.z <= -1.0f)
                    {
                        Destroy(noteObj[i]);
                    }
                    else
                    {
                        noteObj[i].transform.position -= moveVec[i] * noteSpeed * Time.deltaTime;
                        noteObj[i].transform.Rotate(new Vector3(180f, 60f,180f) * Time.deltaTime);
                        ct += Time.deltaTime;                       
                    }
                }
            }
        }
        return true;
    }

    public override void NoteGenerate(GameObject cube, Vector3 pos)
    {
        Vector3 p; 

        if(pos.x == 0)
        {
            float x = Random.value;
            p = new Vector3(5.0f + x, JumpStart.groundPosition.y-0.2f, 22.0f);
            target[cNum] = 0.8f;
        }
        else
        {
            float x = Random.value;
            p = new Vector3(-5.0f - x, JumpStart.groundPosition.y - 0.2f, 22.0f);
            target[cNum] = -0.8f;
        }

        if (noteObj[cNum] == null)
        {
            noteObj[cNum] = Instantiate(cube, p, Quaternion.identity);
            //moveVec[cNum] = noteObj[cNum].transform.position - new Vector3(target[cNum], 0, 0);
            //moveVec[cNum] = moveVec[cNum].normalized;
       
            upSpeed[cNum] = 0.5f;

            cNum++;
        }
        if (cNum >= cubeNum)
        {
            cNum = 0;
        }
    }
}