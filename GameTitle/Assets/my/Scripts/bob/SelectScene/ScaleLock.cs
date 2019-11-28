using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleLock : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        Resize(gameObject, 0.0005f);
    }
    void Resize(GameObject obj, float val)
    {
        Vector3 vec = new Vector3(1, 1, 1);
        vec.x = val / transform.parent.lossyScale.x;
        vec.y = val / transform.parent.lossyScale.y;
        vec.z = val / transform.parent.lossyScale.z;
        obj.transform.localScale = vec;
    }
}