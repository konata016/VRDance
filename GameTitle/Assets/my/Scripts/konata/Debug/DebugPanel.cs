using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugPanel : MonoBehaviour
{
    public TextMeshProUGUI txt1;
    public TextMeshProUGUI txt2;
    public TextMeshProUGUI txt3;
    public TextMeshProUGUI txt4;

    public static string text1;
    public static string text2;
    public static string text3;
    public static string text4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        txt1.text = text1;
        txt2.text = text2;
        txt3.text = text3;
        txt4.text = text4;
    }
}
