using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGroundPos : MonoBehaviour
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
            pos.x = GameDirector.GetGroundPos.x;
        }
        if (!onFriezePosY)
        {
            pos.y = GameDirector.GetGroundPos.y;
        }
        if (!onFriezePosZ)
        {
            pos.z = GameDirector.GetGroundPos.z;
        }
        transform.position = pos;

        //Debug.Log(StepDetermination.groundPosition);
    }
}