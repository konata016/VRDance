using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenerateStage : MonoBehaviour
{
    private enum NoteType
    {
        none,
        wideWave,
        verticalWave,
        punch,
        laser
    }

    private string VERTICAL_R = "1";
    private const string THROW = "2";
    private const string WIDE = "3";

    private string VERTICAL_L = "vl";
    private const string PUNCH = "p";
    private const string LASER = "l";
    

    private struct Data
    {
        public NoteType type;
        public float time;
    }

    [SerializeField] GameObject groundCube;

    private List<Note> noteTime = new List<Note>();
    private List<Data> noteDates = new List<Data>();
    private Data[] dates = new Data[256];

    private Vector3[] wavePositionPreset = new Vector3[3];
    private Vector3[,] punchPositionPreset = new Vector3[3, 3];
    private Vector3[] laserPositionPreset = new Vector3[3];

    private string[] notes;
    private string[] textLoad;    //１行毎
    private string[][] textNotes; //わけわけ用 0:時間　1:種類

    private int rowL; //行
    private int colL; //列

    private int n = 0;      //譜面の何番目？
    private float time = 0; //経過時間

    WideWaveNote wide;
    VerticalWaveNote vertical;
    PunchNote punch;
    LaserNote laser;

    GameObject nmo;
    NoteMover mover;

    //float musicTime;
    //float leftTime;

    GameObject musicObj;
    AudioClip clip;
    AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        LoadNotes();

        nmo = GameObject.Find("NoteMover");
        mover = nmo.GetComponent<NoteMover>();

        GenerateGround();

        musicObj = GameObject.Find("GameManager");
        music = musicObj.GetComponent<AudioSource>();
        //musicTime = music.clip.length;
        //leftTime = musicTime - musicObj.GetComponent<AudioSource>().time;

        //Debug.Log(musicTime);
    }

    void InitializePreset()
    {

    }

    void LoadNotes()
    {
        TextAsset text = new TextAsset();

        text = Resources.Load("Notes/aaa", typeof(TextAsset)) as TextAsset;

        string textAll = text.text;

        string[] del = { "\r\n" };

        textLoad = textAll.Split(del, StringSplitOptions.None);

        colL = textLoad[0].Split(',').Length;
        rowL = textLoad.Length;

        textNotes = new string[rowL][];

        for (int i = 0; i < rowL; i++)
        {
            string[] tempNote = textLoad[i].Split(',');
            colL = textLoad[i].Split(',').Length;
            textNotes[i] = new string[colL];

            for (int j = 0; j < colL; j++)
            {            
                
                textNotes[i][j] = tempNote[j];
                //Debug.Log(textNotes[i][0]);
            }
        }
    }

    void GenerateGround()
    {
        float dist = 0.4f;
        float maxX = 1.0f;
        float maxZ = 19.0f;

        float x = -1.0f, y = 0.0f, z = 1.4f;
        Vector3 pos;

        for (x = -1.0f; x <= maxX; x += dist)
        {
            for (z = -1.4f; z <= maxZ; z += dist)
            {
                pos = new Vector3(x, y, z);
                GameObject obj = Instantiate(groundCube, pos, Quaternion.identity) as GameObject;
                obj.transform.parent = transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (music.isPlaying)
        {
            time += Time.deltaTime;
        }

        float noteTime = float.Parse(textNotes[n][0]);
        string tn = textNotes[n][1];
        int type = 0;

        if (tn.Equals(WIDE))
        {
            type = 3;
        }
        else if (tn.Equals(VERTICAL_R))
        {
            type = 1;
        }
        else if (tn.Equals(VERTICAL_L))
        {
            type = 5;
        }
        else if (tn.Equals(PUNCH))
        {
            type = 5;
        }
        else if (tn.Equals(LASER))
        {
            type = 5;
        }
        else if (tn.Equals(THROW))
        {
            type = 2;
        }
        noteTime = mover.GetNoteAnimTime(noteTime, type);

        //Debug.Log(textNotes[n][1]);
        //Debug.Log(type);

        if (time >= noteTime)
        {
            switch (type)
            {
                case (int)NotesType.wideWave:
                    mover.NoteSet(NotesType.wideWave, textNotes[n]);
                    break;

                case (int)NotesType.verticalWaveRight:
                    mover.NoteSet(NotesType.verticalWaveRight, textNotes[n]);
                    break;

                case (int)NotesType.punch:
                    
                    break;

                case (int)NotesType.laser:
                    
                    break;

                case (int)NotesType.throwCube:
                    mover.NoteSet(NotesType.throwCube, textNotes[n]);
                    
                    break;
            }
            
        
            if(n >= rowL - 1)
            {
                n = 0;
                time = 0;
            }
            else
            {
                n++;
            }
        }
    }
}
