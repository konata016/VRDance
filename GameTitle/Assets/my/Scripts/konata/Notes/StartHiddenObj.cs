using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartHiddenObj : MonoBehaviour
{
    public bool onDebug;

    // Start is called before the first frame update
    void Start()
    {
        if (!onDebug) gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
