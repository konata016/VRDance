using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPos : MonoBehaviour
{
    public GameObject headDisplay;
    public Vector3 fixPos;

    public bool onFreezeX;
    public bool onFreezeY;
    public bool onFreezeZ;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = transform.position;
        Vector3 headPos = headDisplay.transform.position;

        if (!onFreezeX) pos.x += headPos.x + fixPos.x;
        if (!onFreezeY) pos.y += headPos.y + fixPos.y;
        if (!onFreezeZ) pos.z += headPos.z + fixPos.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
