using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// リズムに合わせて、回転するやつ(ステージ上に浮かぶボックスを回転させている)
/// </summary>
public class StageBoxControl : MonoBehaviour
{
    
    public float rotationAmountZ;
    public float rollTime = 1f;

    List<GameObject> objList = new List<GameObject>();
    int rollCount;

    // Start is called before the first frame update
    void Start()
    {
        //子オブジェクトを  リストに格納
        for (int i = 0; i < transform.childCount; i++)
        {
            objList.Add(transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Music.IsJustChangedBeat())
        {
            AutoRotation(rollCount);

            //回転させるオブジェクトの順番を制御
            if (rollCount != objList.Count - 1) rollCount++;
            else rollCount = 0;
        }
    }

    //一定の時間をかけて一定の角度まで回転させる
    void AutoRotation(int count)
    {
        //objArr[count].transform.DORotate(endValue: roll, duration: rollTime, mode: RotateMode.FastBeyond360);
        DOTween
                .To(value => OnRotate(value), 0, 1, rollTime)
                .SetEase(Ease.OutCirc);

        void OnRotate(float value)
        {
            var rot = objList[count].transform.localEulerAngles;
            rot.z = Mathf.Lerp(rotationAmountZ* count, rotationAmountZ * (count + 1), value);
            objList[count].transform.localEulerAngles = rot;
        }
    }
}
