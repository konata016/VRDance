using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPos : MonoBehaviour
{

    public bool onFriezePosX;
    public bool onFriezePosY;
    public bool onFriezePosZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = JumpStart.groundPosition;
        Vector3 pos = transform.position;
        if (!onFriezePosX)
        {
            pos.x = JumpStart.groundPosition.x;
        }
        if (!onFriezePosY)
        {
            pos.y = JumpStart.groundPosition.y;
        }
        if (!onFriezePosZ)
        {
            pos.z = JumpStart.groundPosition.z;
        }
        transform.position = pos;
    }
}
