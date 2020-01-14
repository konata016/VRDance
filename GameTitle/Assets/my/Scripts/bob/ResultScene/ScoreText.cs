using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ScoreText : MonoBehaviour
{
    public TextMeshProUGUI title;       // 入れ物＿タイトル
    public TextMeshProUGUI totalDamage; // 入れ物＿合計ダメージ
    public TextMeshProUGUI maxCombo;    // 入れ物＿最大コンボ
    public TextMeshProUGUI scoreName;   // 入れ物＿スコア
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

    private int animationSequence = 0;
    private bool animationNext = true;
    private bool scoreView = false;

    void Start()
    {
        scoreTest = GetComponent<ScoreTest>();
        number_TD = scoreTest.totalDamage_Nomber;
        number_MC = scoreTest.maxCombo_Nomber;
        onlyOne = true;

        totalDamage.alpha = 0.0f;   // 評価を初めは透明にする
        maxCombo.alpha = 0.0f;
        scoreName.alpha = 0.0f;
        score.alpha = 0.0f;
        totalDamage.transform.localScale = Vector3.one * 3.0f;// サイズを調整
        maxCombo.transform.localScale = Vector3.one * 3.0f;
        scoreName.transform.localScale = Vector3.one * 2.0f;
        score.transform.localScale = Vector3.one * 3.0f;

        animationSequence = 0;
        animationNext = true;
        scoreView = false;
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
        if(animationNext)
        {
            switch (animationSequence)
            {
                case 0:
                    Sequence sequence1 = DOTween.Sequence()
                    .OnStart(() =>
                    {
                        animationNext = false;
                    })
                    .Append(totalDamage.DOFade(1.0f, 1.0f).SetEase(Ease.OutCubic))
                    .Join(totalDamage.transform.DOScale(2.0f, 0.5f).SetEase(Ease.InBack))
                    .OnComplete(() =>
                    {
                        animationSequence = 1;
                        animationNext = true;
                    });
                    break;
                case 1:
                    Sequence sequence2 = DOTween.Sequence()
                    .OnStart(() =>
                    {
                        animationNext = false;
                    })
                    .Append(maxCombo.DOFade(1.0f, 1.0f).SetEase(Ease.OutCubic))
                    .Join(maxCombo.transform.DOScale(2.0f, 0.5f).SetEase(Ease.InBack))
                    .OnComplete(() =>
                    {
                        animationSequence = 2;
                        animationNext = true;
                    });
                    break;
                case 2:
                    Sequence sequence3 = DOTween.Sequence()
                    .OnStart(() =>
                    {
                        animationNext = false;
                    })
                    .Append(scoreName.DOFade(1.0f, 1.0f).SetEase(Ease.OutCubic))
                    .Join(scoreName.transform.DOScale(1.5f, 0.5f).SetEase(Ease.InBack))
                    .OnComplete(() =>
                    {
                        animationSequence = 3;
                        animationNext = true;
                    });
                    break;
                case 3:
                    Sequence sequence4 = DOTween.Sequence()
                    .OnStart(() =>
                    {
                        scoreView = true;
                        animationNext = false;
                    })
                    .Append(score.DOFade(1.0f, 1.0f).SetEase(Ease.OutCubic))
                    .Join(score.transform.DOScale(3.0f, 1.5f).SetEase(Ease.InBack))
                    .OnComplete(() =>
                    {
                        animationSequence = 4;
                        animationNext = true;
                        ScoreController.moveSwitch = true;
                    });
                    break;
                default:
                    break;
            }
        }

        //////////////////////////////////
        /* 表示部分 */
        title.text = ScoreTest.title;
        totalDamage.text = "" + number_TD;
        maxCombo.text = "" + number_MC;
        score.text = "" + scorePlus;
        //////////////////////////////////

        if(scoreView)
        {
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
}
