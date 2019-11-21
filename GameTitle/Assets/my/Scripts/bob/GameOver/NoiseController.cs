using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseController : MonoBehaviour
{
    // 描画関係
    private RectTransform pos;
    private int randomNoise = 0;// ノイズ
    private float randomNoiseGap = 0;// ノイズの揺れ加減
    private int randomGap = 0;// 画像の揺れ
    public int percentage = 5;// 全体のパーセント
    public bool noiseOnOff = true;
    // サウンド関係
    public AudioSource noiseLong;
    public AudioSource noise_1;
    public AudioSource noise_2;

    IEnumerator GeneratePulseNoise()
    {
        randomNoiseGap = Random.Range(0.1f, 1.0f);// ノイズの揺れ加減
        GetComponent<Image>().material.SetFloat("_Random", randomNoiseGap);
        for (int i = 0; i <= 180; i += 30)
        {
            GetComponent<Image>().material.SetFloat("_Amount", 0.2f * Mathf.Sin(i * Mathf.Deg2Rad));// ノイズ発生
            yield return null;
        }
    }
    void Start()
    {
        pos = GameObject.Find("GameOverUI").GetComponent<RectTransform>();
    }

    void Update()
    {
        if(noiseOnOff)
        {
            noiseLong.Play();
            randomNoise = Random.Range(0, 100);
            randomGap = Random.Range(0, 100);
            if (randomNoise < percentage)// ノイズ
            {
                StartCoroutine(GeneratePulseNoise());
                noise_2.Play();
            }

            if (randomGap < percentage / 2)// ズレ
            {
                pos.localPosition = new Vector3(2, 2, 0);
                noise_1.Play();
            }
            else if (randomGap < percentage)
            {
                pos.localPosition = new Vector3(1, -2, 0);
                noise_1.Play();
            }
            else
            {
                pos.localPosition = new Vector3(0, 0, 0);
            }
        }
        else
        {
            noiseLong.Stop();
        }
    }
}