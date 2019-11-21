using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTest : MonoBehaviour
{
    public static string title;
    public static string totalDamage;
    public static string maxCombo;

    public int totalDamage_Nomber = 10;
    public int maxCombo_Nomber = 99;
    void Start()
    {
        title = "タイトル";
        totalDamage = "" + totalDamage_Nomber;
        maxCombo = "" + maxCombo_Nomber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
