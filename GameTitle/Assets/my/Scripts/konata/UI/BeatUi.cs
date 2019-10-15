using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatUi : MonoBehaviour
{

    public GameObject notesLeft;
    public GameObject notesRight;
    public GameObject canvas;

    public static List<GameObject> notesLefts = new List<GameObject>();
    public static List<GameObject> notesRights = new List<GameObject>();

    public static bool isNotesPopUp { get; set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //何たら拍子のタイミングで、ノーツを生成する
        if (Music.IsPlaying && Music.IsJustChangedBeat())
        {
            GameObject prefab = Instantiate(notesLeft) as GameObject;
            prefab.transform.SetParent(canvas.transform, false);
            notesLefts.Add(prefab);

            GameObject prefab1 = Instantiate(notesRight) as GameObject;
            prefab1.transform.SetParent(canvas.transform, false);
            notesRights.Add(prefab1);

            //インデックス外を防ぐやつ
            isNotesPopUp = true;
        }
    }
}
