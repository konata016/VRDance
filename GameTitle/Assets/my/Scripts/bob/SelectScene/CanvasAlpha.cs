using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAlpha : MonoBehaviour
{
    private float alphaValue;
    void Start()
    {
        alphaValue = 0.0f;
        gameObject.GetComponent<CanvasGroup>().alpha = alphaValue;
    }
    
    void Update()
    {

    }
    public void MusicInformation_Alpha(float a)
    {
        if(gameObject.GetComponent<CanvasGroup>().alpha >= 0 && gameObject.GetComponent<CanvasGroup>().alpha <= 1)
        {
            alphaValue = a;
            gameObject.GetComponent<CanvasGroup>().alpha += alphaValue;
            if (gameObject.GetComponent<CanvasGroup>().alpha < 0)
                gameObject.GetComponent<CanvasGroup>().alpha = 0;
            else if(gameObject.GetComponent<CanvasGroup>().alpha > 1)
                gameObject.GetComponent<CanvasGroup>().alpha = 1;
        }
    }
}
