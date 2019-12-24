using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地面の位置を取得してその位置に合わせる
/// </summary>
public class GroundPos : MonoBehaviour
{

    public bool onFriezePosX;
    public bool onFriezePosY;
    public bool onFriezePosZ;
    public Vector3 fixPos;
    Vector3 tmpPos;

    // Start is called before the first frame update
    void Start()
    {
        tmpPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = JumpStart.groundPosition;
        Vector3 pos = transform.position;
        if (!onFriezePosX)
        {
            pos.x = GameDirector.GetGroundPos.x + tmpPos.x + fixPos.x;
        }
        if (!onFriezePosY)
        {
            pos.y = GameDirector.GetGroundPos.y + tmpPos.y + fixPos.y;
        }
        if (!onFriezePosZ)
        {
            pos.z = GameDirector.GetGroundPos.z + tmpPos.z + fixPos.z;
        }
        transform.position = pos;

        //Debug.Log(StepDetermination.groundPosition);
    }
}
