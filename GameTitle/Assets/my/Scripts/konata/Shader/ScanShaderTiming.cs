using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リズムに合わせて光る棒を出す処理
/// (今は使っていない)
/// </summary>
public class ScanShaderTiming : MonoBehaviour
{

    public Material material;
    float barTime;
    bool onMusicStart;

    // Start is called before the first frame update
    void Start()
    {
        //一小節の時間の計算
        //60*拍子*小節数/テンポ
        barTime = 60 * Music.MyInspectorList[0].UnitPerBeat * 1 / (float)Music.MyInspectorList[0].Tempo;
    }

    // Update is called once per frame
    void Update()
    {
        if (!onMusicStart)
        {
            if (Music.IsPlaying && Music.IsJustChangedBar())
            {
                //曲が始まったら光の棒が生成される
                material.SetFloat("Vector1_156873A5", 1f);

                //光の棒が出るまでの時間を調整したい（1小節ごとに）
                material.SetFloat("Vector1_C34C010D", barTime);
                onMusicStart = true;
            }
        }
    }
}
