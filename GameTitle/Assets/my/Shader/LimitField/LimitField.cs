using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitField : MonoBehaviour
{
    public float fix = 1;
    public Material material;
    public GameObject limitFieldObj;
    float dis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dis = transform.position.z - limitFieldObj.transform.position.z;
        if (Mathf.Abs(dis) > fix)
        {
            material.SetFloat("_Move", (Mathf.Abs(dis) * 2) - fix);
        }
        else
        {
            material.SetFloat("_Move", 0);
        }
    }
}
