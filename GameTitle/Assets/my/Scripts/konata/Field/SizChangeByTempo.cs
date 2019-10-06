using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SizChangeByTempo : MonoBehaviour
{
    Vector3 baseScale;

    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Music.IsPlaying && Music.IsJustChangedBeat())
        {
            transform.DOScale(new Vector3(baseScale.x * 1.2f, transform.localScale.y, baseScale.z * 1.2f), 0.0f);
            transform.DOScale(baseScale, 0.2f);
        }
    }
}
