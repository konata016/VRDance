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
                case NotesManager2.RANK.Bad:
                    GetComponent<Renderer>().material = badMaterial;
                    ResetAlpha();
                    break;

                case NotesManager2.RANK.Good:
                    GetComponent<Renderer>().material = goodMaterial;
                    ResetAlpha();
                    break;

                case NotesManager2.RANK.Excellent:
                    GetComponent<Renderer>().material = excellentMaterial;
                    ResetAlpha();
                    break;

                default:break;
            }
            
        }


        if(0< GetComponent<Renderer>().material.color.a)
        {
            Color color = GetComponent<Renderer>().material.color;
            color.a -= speed * Time.deltaTime;
            GetComponent<Renderer>().material.color = color;
        }

    }

    void ResetAlpha()
    {
        Color color = GetComponent<Renderer>().material.color;
        color.a = alpha;
        GetComponent<Renderer>().material.color = color;
    }

    bool OnTrigger()
    {
        if (StepDetermination.isGroundTouch_L == StepDetermination.ISGROUNDTOUCH.Landing ||
            StepDetermination.isGroundTouch_R == StepDetermination.ISGROUNDTOUCH.Landing ||
           OnDebugKey())
        {
            return true;
        }
        else return false;

        //ボタンの処理
        bool OnDebugKey()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0) ||
                Input.GetKeyDown(KeyCode.Alpha1) ||
                Input.GetKeyDown(KeyCode.Alpha2) ||
                Input.GetKeyDown(KeyCode.Alpha3) ||
                Input.GetKeyDown(KeyCode.Alpha4) ||
                Input.GetKeyDown(KeyCode.Alpha5) ||
                Input.GetKeyDown(KeyCode.Alpha6) ||
                Input.GetKeyDown(KeyCode.Alpha7))
            {
                return true;
            }
            else return false;
        }
    }
}
