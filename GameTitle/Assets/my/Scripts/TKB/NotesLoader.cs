using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesLoader : MonoBehaviour
{
    private string[] textLoad;   //１行毎
    private string[,] textNotes; //わけわけ用

    private int rowL; //行
    private int colL; //列

    // Start is called before the first frame update
    void Start()
    {
        TextAsset text = new TextAsset();

        text = Resources.Load("Notes/aaa", typeof(TextAsset)) as TextAsset;

        string textAll = text.text;

        textLoad = textAll.Split('\n');

        colL = textLoad[0].Split('\t').Length;
        rowL = textLoad.Length;

        textNotes = new string[rowL, colL];

        for(int i = 0; i < rowL; i++)
        {
            string[] tempNote = textLoad[i].Split('\t');

            for(int j = 0; j < colL; j++)
            {
                textNotes[i, j] = tempNote[j];
            }
        }
    }

    public string GetNotes(int r, int c)
    {
        return textNotes[r, c];
    }

    public int GetRowLength()
    {
        return rowL;
    }
    public int GetColLength()
    {
        return colL;
    }
}
