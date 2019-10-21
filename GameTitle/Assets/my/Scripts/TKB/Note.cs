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

    protected GameObject[] noteColli;
    protected int colNum = 3;

    public Note(float reachTime, Vector3 position, GameObject note)
    {
        this.reachTime = reachTime;
        this.position = position;
        this.note = note;
        this.note.GetComponent<GameObject>();

        noteColli = new GameObject[colNum];
    }

    public abstract bool NoteMove(int pos);
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
        //moveVector = note.transform.position.normalized;
        moveVector = new Vector3(0,0,1);
    }

    public override bool NoteMove(int pos)
    {
        //if(pos == 1 && note.transform.position.x < vertPos)
        //{
        //    note.transform.position += new Vector3(vertPos, 0f, 0f);
        //}
        //else if (pos == 2 && note.transform.position.x > -vertPos)
        //{
        //    note.transform.position += new Vector3(-vertPos, 0f, 0f);
        //}

        for (int i = 0; i < colNum; i++)
        {
            
            if (noteColli[i] != null)
            {            
                if (noteColli[i].transform.position.z < -2.0f)
                {
                    //noteColli[i].transform.position = new Vector3(0.0f, JumpStart.groundPosition.y, 24.0f);
                    Destroy(noteColli[i]);
                    //return true;
                }
                else
                {
                    noteColli[i].transform.position -= new Vector3(0, 0, 0.1f);
                    //return true;
                }
            }
        }

        return true;
        //if (note.transform.position.z < -2.0f)
        //{
        //    note.transform.position =new Vector3(0.0f, JumpStart.groundPosition.y,  24.0f);
        //    return false;
        //}

        //if (noteColli.transform.position.z < -2.0f)
        //{
        //    noteColli.transform.position = new Vector3(0.0f, JumpStart.groundPosition.y, 24.0f);
        //    return true;
        //}
        //else
        //{
        //    return true;
        //}
    }

    public void NoteGenerate(GameObject colli, int pos)
    {
        Vector3 p;

        if(pos == 1)
        {
            p = new Vector3(0.8f, StepDetermination.groundPosition.y, 24);
        }
        else
        {
            p = new Vector3(-0.8f, StepDetermination.groundPosition.y, 24);
        }

        if (noteColli[vNum] == null)
        {
            noteColli[vNum] = Instantiate(colli, p, Quaternion.identity);
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
}