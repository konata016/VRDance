using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMover : MonoBehaviour
{
    [SerializeField] GameObject wideNote;
    [SerializeField] GameObject verticalNote;
    [SerializeField] GameObject punchNote;
    [SerializeField] GameObject laserNote;

    WideWaveNote wide;
    VerticalWaveNote vertical;
    PunchNote punch;
    LaserNote laser;

    bool wideFlag = false;
    bool rightFlag = false;
    bool leftFlag = false;
    bool punchFlag = false;
    bool laserFlag = false;

    NoteType nt = new NoteType();

    // Start is called before the first frame update
    void Start()
    {
        wide = new WideWaveNote(3.0f, Vector3.zero, wideNote);
        vertical = new VerticalWaveNote(3.0f, Vector3.zero, verticalNote);
        punch = new PunchNote(3.0f, Vector3.zero, punchNote);
        laser = new LaserNote(3.0f, Vector3.zero, laserNote);
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
        if (leftFlag)
        {
            leftFlag = vertical.NoteMove(2);
        }
        if (punchFlag)
        {
            punchFlag = punch.NoteMove(0);
        }
        if (laserFlag)
        {
            laserFlag = laser.NoteMove(0);
        }
    }

    public void FlagSet(NotesType type)
    {   
        if(type == NotesType.wideWave)
        {
            wideFlag = true;
        }
        if (type == NotesType.verticalWaveRight)
        {
            rightFlag = true;
        }
        if (type == NotesType.verticalWaveLeft)
        {
            leftFlag = true;
        }
        if (type == NotesType.punch)
        {
            punchFlag = true;
        }
        if (type == NotesType.laser)
        {
            laserFlag = true;
        }
    }
}
