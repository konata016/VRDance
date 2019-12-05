using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechWaveformCircle : MonoBehaviour
{
    public GameObject prefab;
    public int numberOfObjects = 64;
    public float radius = 5f;
    public GameObject[] cubes;
    private AudioListener AL;
    private AudioSource AS;

    public float maxTime;
    public float currentTime;

    float[] samples = new float[1024];

    [Header("User Config")]
    public float volume = 1.0f;
    private Color color;

    private float timeCount;
    public float interval;

    void Start()
    {
        AS = GetComponent<AudioSource>();
        AL = GetComponent<AudioListener>();

        maxTime = AS.clip.length;

        volume = PlayerPrefs.GetFloat("volume");


        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            Instantiate(prefab, pos, Quaternion.identity);
        }

        cubes = GameObject.FindGameObjectsWithTag("AudioWaveformCube");

        AS.time = currentTime;

        timeCount = 0.0f;
    }

    void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount >= interval)
        {
            currentTime = AS.time;
            AudioListener.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);

            for (int i = 0; i < numberOfObjects; i++)
            {
                Vector3 previousScale = cubes[i].transform.localScale;
                previousScale.y = Mathf.Floor(Mathf.Lerp(previousScale.y, samples[i] * 40, Time.deltaTime * 30) * 10) / 10;
                if (previousScale.y == 0)
                    previousScale.y = 0.1f;
                cubes[i].transform.localScale = previousScale;
            }

            timeCount = 0.0f;
        }
    }
}