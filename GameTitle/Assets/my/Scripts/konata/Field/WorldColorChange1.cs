using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldColorChange1 : MonoBehaviour
{
    public float speed = 5;
    public Material badMaterial;
    public Material goodMaterial;
    public Material excellentMaterial;

    float alpha;

    // Start is called before the first frame update
    void Start()
    {
        alpha = excellentMaterial.color.a;

        Color color = GetComponent<Renderer>().material.color;
        color.a = 0;
        GetComponent<Renderer>().material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if (OnTrigger())
        {
            switch (NotesManager2.rank)
            {
                case NotesManager2.RANK.Bad: GetComponent<Renderer>().material = badMaterial; break;
                case NotesManager2.RANK.Good: GetComponent<Renderer>().material = goodMaterial; break;
                case NotesManager2.RANK.Excellent: GetComponent<Renderer>().material = excellentMaterial; break;
                default:break;
            }
            Color color= GetComponent<Renderer>().material.color;
            color.a = alpha;
            GetComponent<Renderer>().material.color = color;
        }


        if(0< GetComponent<Renderer>().material.color.a)
        {
            Color color = GetComponent<Renderer>().material.color;
            color.a -= speed * Time.deltaTime;
            GetComponent<Renderer>().material.color = color;
        }

    }

    bool OnTrigger()
    {
        bool on = Input.GetKeyDown(KeyCode.Alpha3);
        return on;
    }
}
