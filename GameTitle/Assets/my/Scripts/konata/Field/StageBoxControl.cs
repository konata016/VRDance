using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StageBoxControl : MonoBehaviour
{
    public GameObject[] objArr = new GameObject[5];
    public Vector3 rotationAmount;
    public float rollTime = 1f;
    int count;
    int rollCount;
    public Vector3 roll;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Music.IsJustChangedBar())
        {
            count = 0;
            roll = rotationAmount * rollCount;

            AutoRotation();

            count++;
            if (rollCount != 3) rollCount++;
            else rollCount = 0;
        }
        else if (Music.IsJustChangedBeat())
        {
            AutoRotation();
            count++;
        }
    }

    void AutoRotation()
    {
        objArr[count].transform.DORotate(endValue: roll, duration: rollTime, mode: RotateMode.FastBeyond360);
    }
}
