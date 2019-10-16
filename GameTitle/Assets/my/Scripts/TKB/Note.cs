using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Note : MonoBehaviour
{
    private NoteType.Type NoteType { get; set; }
    public float generateTime;
    protected float reachTime;
    protected Vector3 position;
    protected Vector3 moveVector;
    protected GameObject note;

    public Note(float reachTime, Vector3 position, GameObject note)
    {
        this.reachTime = reachTime;
        this.position = position;
        this.note = note;
        this.note.GetComponent<GameObject>();
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
    private const float animTime = 3.0f;
    private const float noteSpeed = 0.1f;

    public VerticalWaveNote(float reachTime, Vector3 position, GameObject note) : base(reachTime, position, note)
    {
        generateTime = this.reachTime - animTime;
        moveVector = note.transform.position.normalized;
    }

    public override bool NoteMove(int pos)
    {
        if(pos == 1 && note.transform.position.x < 0.6f)
        {
            note.transform.position += new Vector3(0.6f, 0f, 0f);
        }
        else if (pos == 2 && note.transform.position.x > -0.5f)
        {
            note.transform.position += new Vector3(-0.5f, 0f, 0f);
        }

        note.transform.position -= moveVector * noteSpeed;

        if (note.transform.position.z < -2.0f)
        {
            note.transform.position =new Vector3(0.0f, 0.0f,  24.0f);
            return false;
        }
        else
        {
            return true;
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