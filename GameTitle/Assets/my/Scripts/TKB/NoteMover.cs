using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMover : MonoBehaviour
{
    [SerializeField] GameObject wideNote;
    [SerializeField] GameObject verticalNoteR;
    [SerializeField] GameObject verticalNoteL;
    [SerializeField] GameObject punchNote;
    [SerializeField] GameObject laserNote;
    [SerializeField] GameObject throwNote;

    private WideWaveNote wide;
    private VerticalWaveNote vertical;
    private PunchNote punch;
    private LaserNote laser;
    private ThrowCubeNote throwCube;

    private bool wideFlag = false;
    private bool rightFlag = false;
    private bool leftFlag = false;
    private bool punchFlag = false;
    private bool laserFlag = false;
    private bool throwFlag = false;

    private Vector3[] notePos = new Vector3[6];

    // Start is called before the first frame update
    void Start()
    {
        wide = new WideWaveNote(0.0f, Vector3.zero, wideNote);
        vertical = new VerticalWaveNote(0.0f, Vector3.zero, verticalNoteR);
        punch = new PunchNote(0.0f, Vector3.zero, punchNote);
        laser = new LaserNote(0.0f, Vector3.zero, laserNote);
        throwCube = new ThrowCubeNote(0.0f, Vector3.zero, throwNote);

        float x = -1.0f;
        for (int i = 0; i < 6; i++)
        {
            notePos[i] = new Vector3(x, JumpStart.groundPosition.y, 22);
            x += 0.4f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (wideFlag)
        {
            wideFlag = wide.NoteMove(0);
        }
        if (rightFlag)
        {
            rightFlag = vertical.NoteMove(1);
        }
        //if (leftFlag)
        //{
        //    leftFlag = vertical.NoteMove(2);
        //}
        if (punchFlag)
        {
            punchFlag = punch.NoteMove(0);
        }
        if (laserFlag)
        {
            laserFlag = laser.NoteMove(0);
        }
        if (throwFlag)
        {
            throwFlag = throwCube.NoteMove(0);
        }
    }

    public void NoteSet(NotesType type, string[] posBool)
    {   
        switch (type)
        {
            case NotesType.wideWave:
                wideFlag = true;
                break;

            case NotesType.verticalWaveRight:
                rightFlag = true;
                //vertical.NoteGenerate(verticalNoteR, 1);
                for (int i = 2; i < posBool.Length; i++)
                {
                    if (posBool[i] == "true")
                    {
                        vertical.NoteGenerate(verticalNoteR, notePos[i]);
                    }
                }
                break;

            case NotesType.verticalWaveLeft:
                leftFlag = true;
                //vertical.NoteGenerate(verticalNoteL, 2);
                break;

            case NotesType.punch:
                punchFlag = true;
                break;

            case NotesType.laser:
                laserFlag = true;
                break;

            case NotesType.throwCube:
                throwFlag = true;
                //int p = Random.Range(0, 2);
                //throwCube.NoteGenerate(throwNote, p);
                break;
        }
    }

    public void PositionSet(string[] posBool)
    {
        for(int i = 2; i < posBool.Length; i++)
        if (posBool[i] == "true")
        {
            vertical.NoteGenerate(verticalNoteR, notePos[i]);
        }      
    }
}
