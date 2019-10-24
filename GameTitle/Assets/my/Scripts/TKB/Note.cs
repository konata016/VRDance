using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Note : MonoBehaviour
{
    public float generateTime;
    protected float reachTime;
    protected Vector3 position;
    protected Vector3 moveVector;
    protected GameObject note;

    protected GameObject[] noteObj;
    protected int colNum = 3;

    public Note(float reachTime, Vector3 position, GameObject note)
    {
        this.reachTime = reachTime;
        this.position = position;
        this.note = note;
        this.note.GetComponent<GameObject>();

        noteObj = new GameObject[colNum];
    }

    public abstract bool NoteMove(int pos);
    public abstract void NoteGenerate(GameObject obj, int pos);
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

    public override void NoteGenerate(GameObject colli, int pos)
    {
        
    }
}

public class VerticalWaveNote : Note
{
    private float vertPos = 0;
    private const float animTime = 3.0f;
    private const float noteSpeed = 0.1f;

    int vNum = 0;

    public VerticalWaveNote(float reachTime, Vector3 position, GameObject note) : base(reachTime, position, note)
    {
        generateTime = this.reachTime - animTime;
        moveVector = new Vector3(0,0,1);
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
                    noteObj[i].transform.position -= new Vector3(0, 0, 0.1f);                    
                }
            }
        }

        return true;      
    }

    public override void NoteGenerate(GameObject colli, int pos)
    {
        Vector3 p;

        if(pos == 1)
        {
            p = new Vector3(0.8f, JumpStart.groundPosition.y, 24);
        }
        else
        {
            p = new Vector3(-0.8f, JumpStart.groundPosition.y, 24);
        }

        if (noteObj[vNum] == null)
        {
            noteObj[vNum] = Instantiate(colli, p, Quaternion.identity);
            vNum++;
        }
        if(vNum >= colNum)
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

    public override void NoteGenerate(GameObject colli, int pos)
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

    public override void NoteGenerate(GameObject colli, int pos)
    {

    }
}

public class ThrowCubeNote : Note
{
    private float cubePos = 0;
    private const float animTime = 3.0f;
    private const float noteSpeed = 0.1f;

    int cNum = 0;

    public ThrowCubeNote(float reachTime, Vector3 position, GameObject note) : base(reachTime, position, note)
    {
        generateTime = this.reachTime - animTime;
        moveVector = note.transform.position.normalized;
    }

    public override bool NoteMove(int pos)
    {
        
        return true;
    }

    public override void NoteGenerate(GameObject cube, int pos)
    {

    }
}