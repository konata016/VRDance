using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//オブジェクトの方向をターゲットオブジェクトと同じ向きにする

public class FixRoll : MonoBehaviour
{
    public GameObject targetObj;
    bool onStart = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void LateUpdate()
    {
        if (onStart) a(); if (onStart) a();
    }

    void a()
    {
        transform.rotation = targetObj.transform.rotation*Quaternion.AngleAxis(180, Vector3.forward);
        onStart = false;
    }
}
