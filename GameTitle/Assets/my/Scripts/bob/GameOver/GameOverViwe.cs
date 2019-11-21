using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverViwe : MonoBehaviour
{
    private bool onOff = false;
    private int timeCount = 0;
    public int timeCountMax = 5;
    private NoiseController noiseController;
    public AudioSource MainSound;

    void Start()
    {
        GameObject childObject = transform.Find("GameOverUI").gameObject;
        noiseController = childObject.GetComponent<NoiseController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (onOff)
            {
                onOff = false;
                MainSound.volume = 0.272f;
            }
            else
            {
                onOff = true;
                MainSound.volume = 0.0f;
            }
            timeCount = 0;
            GameObject.Find("GameOverUI").GetComponent<UnityEngine.UI.Image>().enabled = onOff;
            noiseController.noiseOnOff = onOff;
        }

        if (onOff)
        {
            timeCount++;

            if (timeCount >= timeCountMax * 60)
            {
                SceneManager.LoadScene("SelectSecen");
            }
        }
    }
}