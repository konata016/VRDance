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

    public abstract void NoteMove(int pos);
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
    
    public override void NoteMove(int pos)
    {
        note.transform.position -= moveVector * noteSpeed;
    }
}

public class VerticalWaveNote : Note
{
    private const float animTime = 3.0f;
    private const float noteSpeed = 3.0f;

    public VerticalWaveNote(float reachTime, Vector3 position, GameObject note) : base(reachTime, position, note)
    {
        generateTime = this.reachTime - animTime;
        moveVector = note.transform.position.normalized;
    }

    public override void NoteMove(int pos)
    {
        note.transform.position -= moveVector * noteSpeed;
    }
}

public class PunchNote : Note
{
    private const float animTime = 2.0f;

    public PunchNote(float reachTime, Vector3 position, GameObject note) : base(reachTime, position, note)
    {
        generateTime = this.reachTime - animTime;
    }

    public override void NoteMove(int pos)
    {
        Debug.Log("拳");
    }
}

public class LaserNote : Note
{
    private const float animTime = 3.0f;

    public LaserNote(float reachTime, Vector3 position, GameObject note) : base(reachTime, position, note)
    {
        generateTime = this.reachTime - animTime;
    }

    public override void NoteMove(int pos)
    {
        Debug.Log("レーザー");
    }
}