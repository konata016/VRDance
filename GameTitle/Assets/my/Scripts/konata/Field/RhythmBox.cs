using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RhythmBox : MonoBehaviour
{
    public float fixTime;
    Vector3 baseScale;
    int stepDataCount;
    bool onStart = false;
    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale;
    }

    //オブジェクトがアクティブになった時に実行される
    void OnEnable()
    {
        while (onStart)
        {
            if (StepData.GetStepData.Count != stepDataCount)
            {
                if (StepData.GetSoundPlayTime >= StepData.GetStepData[stepDataCount].musicScore - fixTime)
                {
                    stepDataCount++;
                }
                else break;
            }
        }
        onStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (StepData.GetStepData.Count != stepDataCount)
        {
            if (StepData.GetSoundPlayTime >= StepData.GetStepData[stepDataCount].musicScore - fixTime)
            {
                if (stepDataCount % 4 == 0)
                {
                    transform.DOScale(new Vector3(baseScale.x * 1.2f, transform.localScale.y, baseScale.z * 1.2f), 0.0f);
                    transform.DOScale(baseScale, 0.2f);
                }
                stepDataCount++;
            }
        }
    }
}
