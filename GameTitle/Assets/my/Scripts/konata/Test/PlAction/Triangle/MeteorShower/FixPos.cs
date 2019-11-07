using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPos : MonoBehaviour
{
    public GameObject instantPos;
    bool onStart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onStart)
        {
            transform.position = instantPos.transform.position;
        }
        if (!onStart)
        {
            onStart = true;
        }
    }
}
