using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// デバッグ用
/// </summary>
public class FootManager : MonoBehaviour
{
    public GameObject footL;
    public GameObject footR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && footR.GetComponent<Foot>().Event== GroundManager.EVENT.Down)
        {
            footR.GetComponent<Foot>().Event = GroundManager.EVENT.End;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && footL.GetComponent<Foot>().Event == GroundManager.EVENT.Down)
        {
            footL.GetComponent<Foot>().Event = GroundManager.EVENT.End;
        }
    }
}
