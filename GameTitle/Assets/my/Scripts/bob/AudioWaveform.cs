using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioWaveform : MonoBehaviour
{
    public GameObject prefab;
    public int numberOfObjects = 64;
    public int pikupikuCubes = 1;
    public float radius = 2.5f;
    public GameObject[] cubes;
    private AudioListener AL;
    private AudioSource AS;
    public GameObject[] pikupikuCube;
    public float firstAudioWaveformPos_x = 8.0f;
    public float firstAudioWaveformPos_y = 0.0f;
    public float firstAudioWaveformPos_z = 0.0f;
    public float AudioWaveformGaps = 0.5f;
    private int AudioWaveformCount = 2;

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
            // 直線状に配置
            Vector3 pos_R = new Vector3(firstAudioWaveformPos_x, firstAudioWaveformPos_y, i * AudioWaveformGaps + firstAudioWaveformPos_z);
            Vector3 pos_L = new Vector3(-firstAudioWaveformPos_x, firstAudioWaveformPos_y, i * AudioWaveformGaps + firstAudioWaveformPos_z);
            Instantiate(prefab, pos_R, Quaternion.identity);
            Instantiate(prefab, pos_L, Quaternion.identity);
        }

        cubes = GameObject.FindGameObjectsWithTag("AudioWaveformCube");
        pikupikuCube = GameObject.FindGameObjectsWithTag("PikupikuCube");

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
                Vector3 previousScale_R = cubes[i * 2].transform.localScale;
                Vector3 previousScale_L = cubes[i * 2 + 1].transform.localScale;
                previousScale_R.y = Mathf.Floor(Mathf.Lerp(previousScale_R.y, samples[i] * 40, Time.deltaTime * 30) * 10) / 10;
                previousScale_L.y = Mathf.Floor(Mathf.Lerp(previousScale_R.y, samples[i] * 40, Time.deltaTime * 30) * 10) / 10;
                if (previousScale_R.y == 0)
                    previousScale_R.y = 0.1f;
                if (previousScale_L.y == 0)
                    previousScale_L.y = 0.1f;
                cubes[i * 2].transform.localScale = previousScale_R;
                cubes[i * 2 + 1].transform.localScale = previousScale_L;
            }

            for (int i = 0; i < pikupikuCubes; i++)
            {
                //単体オブジェクトを音に合わせてぴくぴくさせる
                Vector3 preScale = pikupikuCube[i].transform.localScale;
                preScale.x = Mathf.Lerp(preScale.x, 1 + samples[0] * 5, Time.deltaTime * 30);
                preScale.y = Mathf.Lerp(preScale.y, 1 + samples[0] * 5, Time.deltaTime * 30);
                preScale.z = Mathf.Lerp(preScale.z, 1 + samples[0] * 5, Time.deltaTime * 30);
                pikupikuCube[i].transform.localScale = preScale;
                //Debug.Log(preScale);
            }

            timeCount = 0.0f;
        }
    }
}