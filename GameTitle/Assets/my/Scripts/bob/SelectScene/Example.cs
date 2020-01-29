using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Example : MonoBehaviour
{
    public static float nowBPM {private get; set; } // 取得するBPM
    [SerializeField] float nowBPM_Old = 120.0f;   // 取得するBPM
    public float timePuls = 0.0f;       // 経過時間
    private bool onlyOneTime = true;    // 一度だけ読み込み
    private float dondonTime;           // ４分音符の長さ
    private float memoCount_Now = 0.0f; // 記録
    private float memoCount_Old = 60.0f;// 記録

    private void Start()
    {
        //nowBPM = 120.0f;
        nowBPM_Old = nowBPM;
        timePuls = 0.0f;
        onlyOneTime = true;
        dondonTime = 60 * 1 / (float)nowBPM;
        memoCount_Now = 0.0f;
        memoCount_Old = 60.0f;
        if (SceneManager.GetActiveScene().name == "GameScore")
            nowBPM = 160.0f;
    }
    private void Update()
    {
        if (nowBPM_Old != nowBPM)
        {
            dondonTime = 60 * 1 / (float)nowBPM;
            nowBPM_Old = nowBPM;
        }
        timePuls += Time.deltaTime;
        memoCount_Now = timePuls % dondonTime;
        if (memoCount_Now < memoCount_Old)
        {
            DOTween
                .To(value => OnScale(value), 0, 1, 0.1f)
                .SetEase(Ease.InQuad)
                .SetLoops(2, LoopType.Yoyo);
        }
        memoCount_Old = memoCount_Now;
        if (timePuls >= 60.0f)// オーバーフロー防止
            timePuls = 0.0f;
    }

    private void OnScale(float value)
    {
        var scale = Mathf.Lerp(2, 2.1f, value);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}