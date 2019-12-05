using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_RotationLock : MonoBehaviour
{
    private Vector3 def;

    // Start is called before the first frame update
    void Start()
    {
        def = transform.localRotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        float _parent_y = transform.parent.transform.localRotation.eulerAngles.y;

        // 修正箇所
        transform.localRotation = Quaternion.Euler(def.x, def.y - _parent_y, def.z);

        //// ログ用
        //Vector3 result = transform.localRotation.eulerAngles;
        //Debug.Log("def=" + def + "     _parent_y=" + _parent_y + "     result=" + result);
    }
}