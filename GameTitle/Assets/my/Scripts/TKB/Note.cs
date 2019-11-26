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
            mColor[i] = new Color(59.0f, 199.0f, 229.0f, 1);
        }

        //noteObj = new GameObject[colNum];
    }

    public abstract bool NoteMove(int pos);
    public abstract void NoteGenerate(GameObject obj, Vector3 pos);
}

public class WideWaveNote : Note
{
    private const float animTime = 3.0f;
    private const float noteSpeed = 3.0f;

    public WideWaveNote(float reachTime, Vector3 position, GameObject note) : base(reachTime, position, note)
    {
        generateTime = this.reachTime - animTime;
        moveVector = note.transform.position.normalized;
    }
    
    public override bool NoteMove(int pos)
    {
        note.transform.position -= moveVector * noteSpeed;
        return false;
    }

    public override void NoteGenerate(GameObject colli, Vector3 pos)
    {
        
    }
}

public class VerticalWaveNote : Note
{
    private float vertPos = 0;
    private const float animTime = 3.0f;
    private const float noteSpeed = 10.0f;
    float[] checkTime = new float[40];

    int colNum = 40;
    int vNum = 0;

    public VerticalWaveNote(float reachTime, Vector3 position, GameObject note) : base(reachTime, position, note)
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
            noteObj[vNum] = Instantiate(colli, new Vector3(pos.x, StepDetermination.groundPosition.y, pos.z), Quaternion.identity);
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

    public LaserNote(float reachTime, Vector3 position, GameObject note) : base(reachTime, position, note)
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
    float[] target = new float[4]; //飛んでくる場所へ補正

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
                                 - new Vector3(target[i], JumpStart.groundPosition.y +0.5f, 0);
                    moveVec[i] = moveVec[i].normalized;

                    mColor[i].r += 10.1f;
                    mColor[i].g -= 10.1f;
                    mColor[i].b -= 10.1f;

                    noteObj[i].GetComponent<Renderer>().material.color = mColor[i];
                    noteObj[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", mColor[i]);
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