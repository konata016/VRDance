using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    public TextMeshProUGUI title;       // 入れ物＿タイトル
    public TextMeshProUGUI totalDamage; // 入れ物＿合計ダメージ
    public TextMeshProUGUI maxCombo;    // 入れ物＿最大コンボ
    public TextMeshProUGUI score;       // 入れ物＿スコア

    private int number_TD;      // 合計ダメージ
    private int number_MC;      // 最大コンボ
    private int number_SCORE;   // 最終スコア
    private int scorePlus = 0;  // 表示するスコア

    public int second = 4;      // 秒数
    private int scoreCount;     // フレーム数
    private int timeLapse = 0;  // 時間の流れ

    private bool onlyOne = true;// 一度だけ読み込む

    private ScoreTest scoreTest;

    void Start()
    {
        scoreTest = GetComponent<ScoreTest>();
        number_TD = scoreTest.totalDamage_Nomber;
        number_MC = scoreTest.maxCombo_Nomber;
        onlyOne = true;
    }
    
    void Update()
    {
        if(onlyOne == true)// 一度だけ読み込む
        {
            number_SCORE = number_TD * number_MC;// 最終スコア = 合計ダメージ * 最大コンボ

            Debug.Log("number_TD : " + number_TD);
            Debug.Log("number_MC : " + number_MC);
            Debug.Log("number_SCORE : " + number_SCORE);

            scoreCount = 60 * second;// フレーム数

            onlyOne = false;
            scorePlus = 0;
            timeLapse = 0;
        }

        //////////////////////////////////
        /* 表示部分 */
        title.text = ScoreTest.title;
        totalDamage.text = "" + number_TD;
        maxCombo.text = "" + number_MC;
        score.text = "" + scorePlus;
        //////////////////////////////////

        if (scorePlus < number_SCORE)
        {
            scorePlus = (number_SCORE / scoreCount) * timeLapse;

            timeLapse++;
        }
        else if (scorePlus >= number_SCORE)// 最終スコアをオーバーした場合最終スコアに合わせる
        {
            scorePlus = number_SCORE;
        }
    }
}
