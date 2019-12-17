using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPosSynchronize : MonoBehaviour
{
   public Vector3 fixPos;

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = GameObject.Find("EnemyPoint").transform.position + fixPos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GameObject.Find("EnemyPoint").transform.position + fixPos;
    }
}
