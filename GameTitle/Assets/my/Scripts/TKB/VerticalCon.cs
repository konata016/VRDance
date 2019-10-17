using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalCon : MonoBehaviour
{
    Vector3 mVector = new Vector3();
    float nSpeed;

    // Start is called before the first frame update
    void Start()
    {
        mVector = this.transform.position.normalized;
        nSpeed = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position -= mVector * nSpeed;

    }
}
