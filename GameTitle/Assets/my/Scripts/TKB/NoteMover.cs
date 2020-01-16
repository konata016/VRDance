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
    [SerializeField] GameObject laserTrace;
    [SerializeField] GameObject throwNote;

    [SerializeField] float verticalAnimTime = 2.15f;
    [SerializeField] float throwAnimTime = 1.85f;
    [SerializeField] float wideAnimTime = 2.15f;

    GameObject musicObj;
    AudioSource music;

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

    private const string TRUE = "True";
    private const string FALSE = "False";

    private Vector3[] notePos = new Vector3[6];
    private float lastMusicTime;

    // Start is called before the first frame update
    void Start()
    {
        wide = new WideWaveNote(wideAnimTime, Vector3.zero, wideNote);
        vertical = new VerticalWaveNote(verticalAnimTime, Vector3.zero, verticalNoteR);
        punch = new PunchNote(0.0f, Vector3.zero, punchNote);
        laser = new LaserNote(0.0f, Vector3.zero, laserNote, laserTrace);
        throwCube = new ThrowCubeNote(throwAnimTime, Vector3.zero, throwNote);

        musicObj = GameObject.Find("GameManager");
        music = musicObj.GetComponent<AudioSource>();

        lastMusicTime = -1.0f;

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
        if (-1 < PlDamageStage.Life)
        {
            if (wideFlag)
            {
                wideFlag = wide.NoteMove(0);
            }
            if (rightFlag)
            {
                rightFlag = vertical.NoteMove(1);
            }
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
    }

    public void NoteSet(NotesType type, string[] posBool)
    {
        switch (type)
        {
            case NotesType.wideWave:
                wideFlag = true;
                for (int i = 2; i < 8; i++)
                {
                    if (posBool[i] == TRUE)
                    {
                        wide.NoteGenerate(wideNote, notePos[i - 2]);
                        //Debug.Log(posBool[i]);
                    }
                }
                break;

            case NotesType.verticalWaveRight:
                rightFlag = true;
                //Debug.Log(posBool[3]);
                //vertical.NoteGenerate(verticalNoteR, 1);
                for (int i = 2; i < 8; i++)
                {
                    if (posBool[i] == TRUE)
                    {
                        vertical.NoteGenerate(verticalNoteR, notePos[i-2]);
                        //Debug.Log(posBool[i]);
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
                bool R = false;
                bool L = false;
                for (int i = 2; i < 8; i++)
                {
                    if (posBool[i] == TRUE)
                    {
                        if (i-2 <= 2) R = true;
                        if (i-2 >= 3) L = true;                       
                        //Debug.Log(posBool[i]);
                    }
                }
                if (R == true) throwCube.NoteGenerate(throwNote, new Vector3(0, 0, 0));
                if (L == true) throwCube.NoteGenerate(throwNote, new Vector3(1, 0, 0));
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

    public float GetNoteAnimTime(float time, int type)
    {
        float gTime = 0;

        switch (type)
        {
            case (int)NotesType.wideWave:
                gTime = time - vertical.generateTime;
                break;

            case (int)NotesType.verticalWaveRight:
                gTime = time - vertical.generateTime;
                break;

            case (int)NotesType.punch:
                
                break;

            case (int)NotesType.laser:
                
                break;

            case (int)NotesType.throwCube:
                gTime = time - throwCube.generateTime;
                break;
        }

        return gTime;
    }
}