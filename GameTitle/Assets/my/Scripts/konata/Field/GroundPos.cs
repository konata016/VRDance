using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//地面の位置に合わせる

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
            pos.x += StepDetermination.groundPosition.x;
        }
        if (!onFriezePosY)
        {
            pos.y += StepDetermination.groundPosition.y;
        }
        if (!onFriezePosZ)
        {
            pos.z += StepDetermination.groundPosition.z;
        }
        transform.position = pos;

        //Debug.Log(StepDetermination.groundPosition);
    }
}
