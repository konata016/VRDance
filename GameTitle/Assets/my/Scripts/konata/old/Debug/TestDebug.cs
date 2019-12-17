using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TestDebug : MonoBehaviour
{

    public TextMeshProUGUI txt;
    public Image image;
    ///StepClass.Step step;



    public Sprite[] leftFoodImg;

    public static string debugText;

    // Start is called before the first frame update
    void Start()
    {
        //txt = GetComponent<TextMeshProUGUI>();
        //renderer= gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        


        image.sprite = leftFoodImg[FootPosCenter.hitPosNum];

    }
}
