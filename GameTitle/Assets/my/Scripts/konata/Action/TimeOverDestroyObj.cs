using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一定の時間が経過したらオブジェクトを消す
/// </summary>
public class TimeOverDestroyObj : MonoBehaviour
{
    public float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
